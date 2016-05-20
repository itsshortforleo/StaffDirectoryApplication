using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace Source.SMS_Stuff
{
    public class SmsCommunication
    {


        public void SendSms()
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            string AccountSid = "AC742ad8b8154b3f102c98679d4e5aaa3c";
            string AuthToken = "c8d1f573322517eeb89535dc37a58259";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);

            var message = twilio.SendMessage("+14158141829", "+16518675309", "Hi this is a text message");


                //"+14158141829", "+16518675309",
                //"Hey Jenny! Good luck on the bar exam!",
                //new string[] {"http://farm2.static.flickr.com/1075/1404618563_3ed9a44a3a.jpg"},
                //null
                //);
            Console.WriteLine(message.Sid);
        }
    
}
}