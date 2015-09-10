using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace PYS.Entity
{
    // Declare a delegate with a string parameter
    public delegate void MyDelegate(string temp);

    public class ClientTcp
    {
        private NetworkStream ns;

        public event MyDelegate MyEvent;

        public ClientTcp(NetworkStream ns)
        {
            this.ns = ns;
        }

        public void TcpThead()
        {
            // Get corresponding encapsulated stream
            StreamReader sr = new StreamReader(ns);
            string temp = sr.ReadLine();

            // trigger event and send back after received client information
            MyEvent(temp);
            StreamWriter sw = new StreamWriter(ns);

            // Convert to capital characters and send back to client end
            sw.WriteLine(temp.ToUpper());
            sw.Flush();
            sw.Close();
            sr.Close();
        }
    }
}
