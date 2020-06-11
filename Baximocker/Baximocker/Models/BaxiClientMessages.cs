using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baximocker.Models
{
    public class BaxiClientMessages
    {
        public const string XmlVersion = "1.0";
        public const string XmlEncoding = "UTF-8";
        public const string XmlStandAlone = "yes";
        public const string RsStr = "*RS*";
        public const string UsStr = "*US*";
        public const string FsStr = "*FS*";
        public const string NpStr = "*NP*";
        public const string SoStr = "*SO*";
        public const string LessStr = "*LESS*";
        public const string MoreStr = "*MORE*";
        public const int US = 0x1f; //Unit separator
        public const int RS = 0x1e; //Record separator
        public const int FS = 0x1c; //File separator
        public const int NP = 0x0c; //Formfeed
        public const int SO = 0x0e; //Shift Out (End of PrintText)
        public const int LESS = 0x3c; //< Causes problems in the xml handling
        public const int MORE = 0x3e; //>


        public const int Purchase = 48;
        public const int ReturnOfGoods = 49;

        /// <summary>
        /// XML template for Open request
        /// </summary>
        public const string Open =
        @"<?xml version=""1.0"" encoding=""UTF-8""?> 
        <NetsRequest>
            <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1"" /> 
            <Open/>
        </NetsRequest>";

        /// <summary>
        /// XML template for TransferAmount request for Purchase
        /// </summary>
        public const string TransferAmount =
        @"<?xml version=""1.0"" encoding=""UTF-8""?> 
        <NetsRequest>
          <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1"" /> 
          <Dfs13TransferAmount>
            <TransactionType></TransactionType> 
            <OperId>4321</OperId> 
            <Amount1 /> 
            <Amount2>0</Amount2> 
            <Amount3>0</Amount3> 
            <Type2>48</Type2> 
            <Type3>48</Type3> 
            <HostData /> 
            <ArticleNumber /> 
            <PCC /> 
            <AuthorisationCode /> 
            <OptionalData />
          </Dfs13TransferAmount>
        </NetsRequest>";

        /// <summary>
        /// XML template for Administration command
        /// </summary>
        public const string Administration =
        @"<?xml version=""1.0"" encoding=""UTF-8"" ?> 
        <NetsRequest>
          <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1"" /> 
          <Dfs13Administration>
            <OperId>0000</OperId> 
            <AdmCode /> 
            <OptionalData /> 
          </Dfs13Administration>
        </NetsRequest>";

        public const string SuccessfulPayment = @"<?xml version=""1.0"" encoding=""UTF-8"" ?> 
        <NetsResponse>
           <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1"" /> 
           <Dfs13LocalMode>
            <Result> </Result>
            <TerminalID></TerminalID>
            <OperId>0000</OperId> 
            <StanAuth> </StanAuth>
          </Dfs13LocalMode>
        </NetsResponse>";

        public const string ErrorPayment = @"<?xml version=""1.0"" encoding=""UTF-8""?>
        <NetsResponse>
         <MessageHeader ECRID=""ECR1"" TerminalID=""Terminal1"" VersionNumber=""1"" />
         <Dfs13Error>
            <ErrorCode>7012</ErrorCode>
            <ErrorString>ERR_UNEXPECTED_TERMINAL_FRAME</ErrorString>
         </Dfs13Error>
        </NetsResponse>";
      

        /// <summary>
        /// XML template for TransferCardData command
        /// </summary>
        public const string TransferCardData =
        @"<?xml version=""1.0"" encoding=""UTF-8""?> 
        <NetsRequest>
          <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""""/> 
          <Dfs13TransferCardData>
            <Track2/>
            <Track3/>
            <Origin/>
          </Dfs13TransferCardData>
        </NetsRequest>";

        /// <summary>
        /// XML template for Get Baxi properties
        /// </summary>
        public const string BaxiProperties =
        @"<?xml version=""1.0"" encoding=""UTF-8""?> 
        <NetsRequest>
          <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1""/> 
          <BaxiProperties />
        </NetsRequest>";


        public const string XlsSetBaxiProperties = "NetsRequest/SetBaxiProperties";

        /// <summary>
        /// XML template for Set Baxi properties
        /// </summary>
        public const string SetBaxiProperties =
        @"<?xml version=""1.0"" encoding=""UTF-8""?> 
        <NetsRequest>
          <MessageHeader ECRID="""" TerminalID="""" VersionNumber=""1""/> 
          <SetBaxiProperties />
        </NetsRequest>";


    }
}
