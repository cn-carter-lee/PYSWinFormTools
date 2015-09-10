using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using PYS.Entity;

namespace PYSServer
{
    public partial class frmMain : Form
    {
        // Declare object
        private TcpListener tl;
        private NetworkStream ns;
        public frmMain()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Open 8888 port to listen
            tl = new TcpListener(8888);
            tl.Start();
            
            // Start a thead to process request(s)
            Thread th = new Thread(new ThreadStart(Listen));
            th.IsBackground = true;
            th.Start();
            
            btnStart.Enabled = false;
        }

        private void Listen()
        {
            while (true)
            {
                // Accept a request and initialize network
                Socket socket = tl.AcceptSocket();
                ns = new NetworkStream(socket);

                // ClientTcp is an newly add class with specifications below
                ClientTcp clientTcp = new ClientTcp(ns);
                clientTcp.MyEvent += new MyDelegate(clientTcp_MyEvent);

                // Open thread
                Thread th = new Thread(new ThreadStart(clientTcp.TcpThead));
                th.IsBackground = true;
                th.Start();
            }
        }

        void clientTcp_MyEvent(string temp)
        {
            // Set value of Server's TextBox
            txtMessage.Text = temp;
        }
    }
}

/*
这里说下为什么需要ClientTcp这么个类，说这个之前，先说一下为什么服务器端需要开启一个新的线程来监控端口，这个原因比较简单，
Socket sock = tl.AcceptSocket();  这个方法会造成阻塞，也就是说如果没有得到客户端的响应，
TcpListenr将一直监听下去，这就会造成程序的假死，因此我们需要单独开一个线程来监听我们的8888端口，
我们观察服务器端（Form2）可以看出，NetworkStream是一个全局变量（实际上局部与全局都是一样），如果CPU忙的过来，
直接把ClientTcp里的方法拿到Form2里写没问题，但是一旦客户端过多造成数据拥挤，那很可能当运算还未结束，
NetworkStream就已经换人了，因此当我们取得某客户端对应的NetworkStream后，应该考虑立刻将它封装到一个类中，
再在该类中再对该NetworkStream做相应的操作，ClientTcp这个类就是为这个设计的，而当封装了NetworkStream后，
我们发现从客户端传过来的值是我们需要的，因此就用到了事件的回调，这个我前面有篇文章里讲过了，
基于TCP协议的网络编程基础的东西就这些，写法很固定，但是需要很多的技巧，前几天试着写一个聊天室程序，差点没吐血，果然不是一般的麻烦。
*/