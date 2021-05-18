using System;
using NativeMessaging;
using Newtonsoft.Json.Linq;

namespace DailyPmsClient.Services
{
    public class ChromeMessagingHost : Host
    {
        public ChromeMessagingHost() : base (true)
        {
        }

        public override string Hostname
        {
            get => "com.Mi8.DailyPMS";
        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            //SendMessage(data);
            Console.WriteLine("Client has received message from chrome ext. Good !");
        }
    }
}
