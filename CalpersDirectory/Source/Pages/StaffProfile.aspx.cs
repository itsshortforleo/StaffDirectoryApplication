
using System;
using System.Linq;
using System.Web.UI.WebControls;
using Source.Classes.DataRetrieval;
using Source.Classes.Entities;
using Source.SMS_Stuff;
using Twilio;

namespace Source.Pages
{
    public partial class StaffProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int entityKey = Convert.ToInt32(Request.QueryString["EntityKey"]);
                MapEntityDataToLabels(entityKey);
            }
        }

        private void MapEntityDataToLabels(int entityKey)
        {
            EntityDataAction eda = new EntityDataAction();
            Entity entityDetails = eda.GetEntityDetailsByKey(entityKey);

            String name = string.Empty;
            name = entityDetails.Names.FirstOrDefault().FirstName + " " + entityDetails.Names.FirstOrDefault().LastName;
            lblNameValue.Text = name;
            lblJobTitleValue.Text = entityDetails.JobTitle.DescShort;

            var deskPhone = from dp in entityDetails.Phones
                                        where dp.PhoneType.Code.Equals("DESKPHN")
                                        select dp;
            var defaultDeskPhone = deskPhone.FirstOrDefault();

            if (defaultDeskPhone != null)
            {
                var deskAreaCode = defaultDeskPhone.AreaCode;
                var deskPhoneNumber = defaultDeskPhone.PhoneNumber;
                lblDeskPhoneValue.Text = "(" + deskAreaCode + ")" + " " + deskPhoneNumber.Substring(0, 3) + "-" +
                                         deskPhoneNumber.Substring(3, 4);
            }


            var mobilePhone = from mp in entityDetails.Phones
                            where mp.PhoneType.Code.Equals("MOBILEPHN")
                            select mp;

            var defaultMobilePhone = mobilePhone.FirstOrDefault();

            if (defaultMobilePhone != null)
            {
                var mobileAreaCode = defaultMobilePhone.AreaCode;
                var mobilePhoneNumber = defaultMobilePhone.PhoneNumber;
                lblMobilePhoneValue.Text = "(" + mobileAreaCode + ")" + " " + mobilePhoneNumber.Substring(0, 3) + "-" +
                                           mobilePhoneNumber.Substring(3, 4);
            }


            lblBuildingValue.Text = entityDetails.GridLocation.Floor.Building.DescShort;
            lblFloorValue.Text = entityDetails.GridLocation.Floor.DescShort;
            lblUnitValue.Text = entityDetails.Unit.DescShort;

            var email = from e in entityDetails.Emails
                              where e.EmailType.Code.Equals("email")
                              select e;
            var defaultEmail = email.FirstOrDefault();

            if (defaultEmail?.EmailAddress != null)
            {
                lblEmailValue.Text = defaultEmail.EmailAddress;

            }

            //lblManagerValue.Text = entityDetails.en
        }

        protected void BtnSendTextMessage(object sender, EventArgs e)
        {
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            string AccountSid = "AC742ad8b8154b3f102c98679d4e5aaa3c";
            string AuthToken = "c8d1f573322517eeb89535dc37a58259";
            var twilio = new TwilioRestClient(AccountSid, AuthToken);
            String messageToSend = keyboard.Text;


            var message = twilio.SendMessage("+18604847864", "+19165409398", messageToSend);


            //"+14158141829", "+16518675309",
            //"Hey Jenny! Good luck on the bar exam!",
            //new string[] {"http://farm2.static.flickr.com/1075/1404618563_3ed9a44a3a.jpg"},
            //null
            //);
            Console.WriteLine(message.Sid);
        }
    }
}