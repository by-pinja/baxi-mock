using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Xml;
using Baximocker.Enums;
using Baximocker.Interfaces;
using Microsoft.AspNetCore.Builder;
using Baximocker.Models;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace Baximocker
{
    /// <summary>
    /// The basic flow of Baxi payments is as follows:
    /// 1) Open connetion with Open message -> Baxi server does not respond to this
    /// 2) Create payment to Baxi -> Baxi starts to send current situation to client -> this can be skipped, since this is just a noice
    /// 3) Payment is finished after it is paid, encounters an error or is canceled. Payment is pending until that
    /// </summary>
    public class BaxiMessagehandler: WebSocketBehavior
    {
        public List<CardPayment> ActivePayments = new List<CardPayment>();
        private int PaymentId;
        protected override void OnMessage(MessageEventArgs e)
        {
            var message = e.Data;
            if (message.Contains("Open"))
            {
                Send(message);
            }
            else
            {
                var xmlMessage = GetMessageXMLFromData(e.RawData);
                HandleMessage(xmlMessage);
            }
        }

        private void HandleMessage(XmlDocument xmlMessage)
        {
            BaxiTransferType transactionType = BaxiTransferType.Purchase;
            var transactionTypeNode = xmlMessage.SelectSingleNode("NetsRequest/Dfs13TransferAmount/TransactionType");
            if (transactionTypeNode != null)
            {
              var  transactionTypeString = transactionTypeNode.InnerText;
              transactionType = (BaxiTransferType)int.Parse(transactionTypeString);
            }

            if (transactionType == BaxiTransferType.Purchase)
            {
                var terminalId = long.Parse(xmlMessage.SelectSingleNode("NetsRequest/MessageHeader/@TerminalID").InnerText);
                AddActivePayment(terminalId);
              
                Console.WriteLine("Will this payment (S)ucceed or (F)ail?");
                var response = Console.ReadKey();
                if (response.Key == ConsoleKey.S)
                {
                    SetPaymentAsSuccessful(terminalId);
                }
                else
                {
                    SetPaymentAsFailed(terminalId);
                }
            }
            
        }

        private void AddActivePayment(long terminalId)
        {
            var payment = new CardPayment
            {
                TerminalId = terminalId,
                PaymentId = PaymentId
            };
            ActivePayments.Add(payment);
            PaymentId++;
        }


        private XmlDocument GetMessageXMLFromData(byte[] rawData)
        {
            var xml = new XmlDocument();
            var document = Encoding.UTF8.GetString(rawData);
            try
            {
                xml.LoadXml(document);
                return xml;
            }
            catch (XmlException e)
            {
                var message = "Unable to parse XML document: " + document;
                throw;
            }
        }


        private void SetPaymentAsFailed( long terminalId)
        {
            RemovePayment(terminalId);

            var errorMessage = new XmlDocument();
            errorMessage.LoadXml(BaxiClientMessages.ErrorPayment);

            SetNodeText(errorMessage, "NetsResponse/MessageHeader/@TerminalID", terminalId.ToString());
            SetNodeText(errorMessage, "NetsResponse/Dfs13Error/ErrorString",terminalId.ToString());

            //receiving end expects raw byte array
            Send(Encoding.Default.GetBytes(errorMessage.OuterXml));
        }
        private void SetPaymentAsSuccessful(long terminalId)
        {
            RemovePayment(terminalId);

            var successfulMessage = new XmlDocument();
            successfulMessage.LoadXml(BaxiClientMessages.SuccessfulPayment);

            SetNodeText(successfulMessage, "NetsResponse/Dfs13LocalMode/Result", BaxiTransActionResult.TransactionOK.ToString());
            SetNodeText(successfulMessage, "NetsResponse/Dfs13LocalMode/TerminalID",terminalId.ToString());

            //receiving end expects raw byte array
            Send(Encoding.Default.GetBytes(successfulMessage.OuterXml));
        }

        private void SetNodeText( XmlDocument document, string nodePath, string nodeValue)
        {
            var node = document.SelectSingleNode(nodePath);
            node.InnerText = nodeValue;
        }

        private void RemovePayment(long terminalId)
        {
            var payment = ActivePayments.First(ap => ap.TerminalId == terminalId);
            ActivePayments.Remove(payment);
        }
    }

  
}
