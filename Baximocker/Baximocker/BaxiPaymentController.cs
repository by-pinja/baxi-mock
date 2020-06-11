
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Xml;
using Baximocker.Interfaces;
using Baximocker.Models;
using WebSocketSharp.Server;

namespace Baximocker
{
    public class BaxiPaymentController: ApiController
    {
        private readonly IBaxiMessageHandler _messageHandler;

        public BaxiPaymentController(IBaxiMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

     //   [HttpGet("success")]
        public void SetPaymentAsSuccessful(long terminalId)
        {
            _messageHandler.SetPaymentAsSuccessful(terminalId);
        }

     //   [HttpGet("error")]
        public void SetPaymentAsError(long terminalId)
        {

        }
    }
}
