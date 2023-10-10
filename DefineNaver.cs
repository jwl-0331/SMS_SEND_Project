using System;
using System.Collections.Generic;
using System.Text;

namespace project_SMS
{
    class DefineNaver
    {
        public const string mSmsAccessKey = "VS6yu4bhI43eNgf1CYUp"; //Access Key
        public const string mSmsSecretKey = "OhzgNDhGwr7h8AVE1jctv9rHgjBZuYg2TBv4swjs"; // SecretKey
        public const string mSmsServiceID = "ncp:sms:kr:316360727527:sms_test"; // 서비스 ID
        public const String hostNameUrl = "https://sens.apigw.ntruss.com"; //host url
        public const String requestUrl = "/sms/v2/services/"; //요청 url
        public const String requestUrlType = "/messages";


        public const String Host_API_URL = hostNameUrl + requestUrl + mSmsServiceID + requestUrlType;

    }
}
