using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using XO.Registry.Entities;
using XO.Registry.SDK;

namespace PYS.Stock
{
    public partial class frmMain : Form
    {
        public String UserId = "9643080308083945";
        public String FirstName1 = "Brittany";
        public String LastName1 = "Peitsmeyer";
        public String Email1 = "claudia@forever-events.com";
        public String FirstName2 = "Joe";
        public String LastName2 = "Smith";
        public String Email2 = "";
        public String City = "BAY HARBOR ISLANDS";
        public String State = "FL";
        public String Zip = "33154";
        public DateTime EventDate = new DateTime(2014, 03, 22);
        public String UserName = "Claudia Forever";
        public String OperationType = "A";
        public String EventType = "wedding";
        public String Country = "US";
        public Boolean IsTBVerified = false;
        public Boolean IsTKVerified = false;
        public Boolean IsWCVerified = false;
        public Boolean IsGR360Verified = false;
        public Boolean IsMobileOrigin = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtResult.Text = "";
            var MemberShip = new User();

            long UserIdInt = Convert.ToInt64(UserId);
            if (EventType.ToLower() == "wedding")
            {
                if (EventDate.ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    MemberShip.WeddingDate = EventDate;
                }
                MemberShip.EventTypeId = 1;
            }
            else if (EventType.ToLower() == "baby")
            {
                if (EventDate.ToString("yyyy-MM-dd") != "1900-01-01")
                {
                    MemberShip.DueDate = EventDate;
                }
                MemberShip.EventTypeId = 2;
            }
            Console.WriteLine("start");
            MemberShip.Id = UserIdInt;
            MemberShip.Email1 = Email1;
            if (Email2 != String.Empty)
            {
                MemberShip.Email2 = Email2;
            }

            MemberShip.FirstName1 = FirstName1;
            if (FirstName2 != String.Empty)
            {
                MemberShip.FirstName2 = FirstName2;
            }

            MemberShip.LastName1 = LastName1;
            if (LastName2 != String.Empty)
            {
                MemberShip.LastName2 = LastName2;
            }
            if (State != String.Empty)
            {
                MemberShip.State = State;
            }
            if (Zip != String.Empty)
            {
                MemberShip.Zip = Zip;
            }
            if (City != String.Empty)
            {
                MemberShip.City = City;
            }
            if (UserName != String.Empty)
            {
                MemberShip.UserName = UserName;
            }
            if (Country != String.Empty)
            {
                MemberShip.Country = Country;
            }
            MemberShip.IsGR360Verified = IsGR360Verified;
            MemberShip.IsTBVerified = IsTBVerified;
            MemberShip.IsTKVerified = IsTKVerified;
            MemberShip.IsMobileOrigin = IsMobileOrigin;
            MemberShip.IsWCVerified = IsWCVerified;
            //System.Console.WriteLine(MemberShip.ToString());
            //componentEvents.FireError(0, String.Format("Failed to push the UWFeed information to Cloud {0} [{1}]"), "1111", null, 0);
            if (OperationType == "D")
            {
                RegistryProviderFactory.CurrentProvider.DeleteRawUser(UserIdInt);
            }
            else
            {
                RegistryProviderFactory.CurrentProvider.PutRawUser(MemberShip);
            }

            txtResult.Text = "Success.";

        }
    }
}
