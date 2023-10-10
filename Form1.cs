using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_SMS
{

    public partial class Form1 : Form
    {

        static string mTimeStamp = string.Empty;
        public List<Socket> clientSocketList = new List<Socket>();//클라이언트 소켓을 관리하는 리스트, 소켓과 접속 아이디를 관리
        ServerProgram multiServer;
        int serverPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_LISTEN_Click(object sender, EventArgs e)
        {
            button_LISTEN.Enabled = false;
            try
            {
                serverPort = Int32.Parse(PortNum.Text.ToString()); // 소켓 번호 설정
            }
            catch (FormatException ex)
            {
                //textbox 에 숫자 외의 문자인 경우
                serverPort = 9999;
            }

            multiServer = new ServerProgram(serverPort);
            multiServer.OnConnect += clientConnected;
            multiServer.OnDisconnect += clientDisconncted;
            multiServer.OnReceive += clientReceive;
        }

        private void clientReceive(Socket sock, String msg)
        {
            int index = msg.IndexOf("$");
            String curDate = DateTime.Now.ToString("HH:mm:ss"); // 현재 날짜 받기
            String stCmd = "";
            String stData = "";
            String sendMsg = "";
            if (index > 0)
            {
                stCmd = msg.Substring(0, index); //$를 기준으로 앞 부분을 cmd로 
                stData = msg.Substring(index + 1);

            }
            else
            {
                sendMsg = "[ " + curDate + " ] " + stCmd + " : " + stData;
            }
            if (multiServer != null) multiServer.SendMessage(sendMsg);

        }
        private void clientDisconncted(Socket sock)
        {
            if (clientSocketList.Contains(sock))
            {
                clientSocketList.Remove(sock);
            }
        }
 
        private void clientConnected(Socket sock)
        {
            clientSocketList.Add(sock);
        }
    }
}