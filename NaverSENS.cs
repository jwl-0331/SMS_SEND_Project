using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Xml.Schema;
using System.Text.RegularExpressions;

namespace project_SMS
{
    public class NaverSENS
    {
        
        static string mTimeStamp = string.Empty;
        static string msg = null;
        private String from = null;
        private String type = null;
        private String subject = null;
        private String countryCode = null;
        private String content = null;
        private Socket socket;
        private IPEndPoint remoteAddr;

        public NaverSENS(string value, Socket socket)
        {
            this.socket = socket;
            remoteAddr = (IPEndPoint)socket.RemoteEndPoint;
            msg = value;
            this.from = "01048734882";
            this.type = "sms";
            this.countryCode = "82";
        }

        public void sendMessage()
        {
            string url = new StringBuilder()
                            .Append(project_SMS.DefineNaver.Host_API_URL).ToString();
            
            mTimeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string signature = makeSignature();
            // WebRequest 객체 생성
            // request.Method : GET , POST
            // request.ContentType : 요청 데이터 전달 형식 정의
            // request.Headers.Add : 헤더 정의
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("x-ncp-apigw-timestamp", mTimeStamp);
            request.Headers.Add("x-ncp-iam-access-key", project_SMS.DefineNaver.mSmsAccessKey);
            request.Headers.Add("x-ncp-apigw-signature-v2", signature);


            //json parse
            msg = "{" + msg + "}";
            JObject toJson = JObject.Parse(msg);


            // tel 유효성 검사 및  toJson["to"] 데이터 추가
            if (toJson["tel"] != null)
            {
                string tel = toJson["tel"].ToString();
                if (tel != "")
                {
                    if (telValidator(tel))
                    {
                        string[] numArray = tel.Split("-");
                        string to = null;
                        for (int i = 0; i < 3; i++)
                        {
                            to += numArray[i];
                        }
                        toJson.Add("to", to);
                        toJson.Remove("tel");
                    }
                    else
                    {
                        this.Send("Check tel format");
                        return;
                    }
                }
                else
                {
                    this.Send("Input tel");
                    return;
                }
            }
            else
            {
                //Client 메시지전송
                this.Send("Input tel");
                return;
            }

            // JSON body data
            JObject bodyJson = new JObject();
            JArray toArr = new JArray();
            toArr.Add(toJson);

            // content 유효성 검사 및 bodyJson["content"] 추가
            if (toJson["content"] != null) 
            {
                string content = toJson["content"].ToString();
                int byteCount = System.Text.Encoding.Default.GetByteCount(content);
                if(byteCount > 0)
                {
                    bodyJson.Add("content", toJson["content"]);
                    if(byteCount <= 80) //SMS
                    {
                        bodyJson.Add("type", "SMS");
                        bodyJson.Add("countryCode", countryCode);
                        bodyJson.Add("from", "01048734882");
                        bodyJson.Add("subject", subject);
                        bodyJson.Add("messages", toArr);
                    }
                    else if(byteCount > 80 && byteCount <= 2000) //LMS
                    {
                        bodyJson.Add("type", "LMS");
                        bodyJson.Add("countryCode", countryCode);
                        bodyJson.Add("from", "01048734882");
                        if (toJson["subject"] != null)
                        {
                            bodyJson.Add("subject", toJson["subject"]);
                            bodyJson.Add("messages", toArr);
                        }
                        else
                        {
                            this.Send("Input subject");
                            return;
                        }
                    }
                }
                else
                {
                    this.Send("Input content");
                    return;
                }
            }
            else
            {
                this.Send("Input content");
                return;
            }

            string postData = JsonConvert.SerializeObject(bodyJson, Formatting.Indented);

            var data = Encoding.UTF8.GetBytes(postData);
            try
            {
                using (var stream = request.GetRequestStream()) //전송
                {
                    stream.Write(data, 0, data.Length);
                }
                string responseText = string.Empty;
                using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse()) //응답
                {
                    HttpStatusCode status = resp.StatusCode;
                    Console.WriteLine(status);  // 정상 "Accept"

                    Stream respStream = resp.GetResponseStream();
                    using (StreamReader sr = new StreamReader(respStream))
                    {
                        responseText = sr.ReadToEnd();
                        this.Send("\nSMS Send Success\n" + postData);
                    }
                }
            }catch(Exception ex) { this.Send("\nSMS Send Fail\n" ); }
        }


        public static string makeSignature()
        {
            string space = " ";                                          // one space
            string newLine = "\n";                                       // new line
            string method = "POST";                                      // method
            string url = $"/sms/v2/services/{project_SMS.DefineNaver.mSmsServiceID}/messages";   // url (include query string)

            string message = new StringBuilder()
                .Append(method)
                .Append(space)
                .Append(url)
                .Append(newLine)
                .Append(mTimeStamp)
                .Append(newLine)
                .Append(project_SMS.DefineNaver.mSmsAccessKey)
                .ToString();

            string encodeBase64String = string.Empty;
            byte[] secretKey = Encoding.UTF8.GetBytes(project_SMS.DefineNaver.mSmsSecretKey);
            using (HMACSHA256 hmac = new HMACSHA256(secretKey))
            {
                hmac.Initialize();
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                byte[] rawHmac = hmac.ComputeHash(bytes);
                encodeBase64String = Convert.ToBase64String(rawHmac);
            }
            return encodeBase64String;
        }

        public void Send(String msg)
        {
            byte[] sendData = Encoding.UTF8.GetBytes(msg);
            //sendArgs.SetBuffer(sendData, 0, sendData.Length);
            //socket.SendAsync(sendArgs);
            // Client로 메시지 전송
            if (socket != null) socket.Send(sendData, sendData.Length, SocketFlags.None);
        }

        public Boolean telValidator(String number)
        {
            Regex regex = new Regex(@"01{1}[016789]{1}-[0-9]{3,4}-[0-9]{4}");

            Match m = regex.Match(number);

            if(m.Success)
            {
                return true;
            }
            else
            {
                return false; 
            }
        }
    }
}
