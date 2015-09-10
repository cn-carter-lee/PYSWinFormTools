using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace PYSWindowsService
{
    public partial class PYSService : ServiceBase
    {
        public PYSService()
        {
            InitializeComponent();
            if (!System.Diagnostics.EventLog.SourceExists("PYSSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("PYSSource", "PYSNewLog");
            }
            eventLog1.Source = "PYSSource";
            eventLog1.Log = "PYSNewLog";

            Timer timer = new Timer(10000);
            timer.Enabled = true;
            timer.Start();

        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }
    }
}
