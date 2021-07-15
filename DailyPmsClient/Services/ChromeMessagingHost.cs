//using System;
//using NativeMessaging;
//using Newtonsoft.Json.Linq;

//namespace DailyPmsClient.Services
//{
//    public class ChromeMessagingHost : Host
//    {
//        const bool SendConfirmationReceipt = true;

//        public ChromeMessagingHost() : base (SendConfirmationReceipt)
//        {
//        }

//        public override string Hostname
//        {
//            get => "com.minception.dailypms";
//        }

//        protected override void ProcessReceivedMessage(JObject data)
//        {
//            //SendMessage(data);
//            Console.WriteLine("Client has received message from chrome ext. Good !");
//        }
//    }
//}
