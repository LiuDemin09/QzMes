using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
namespace MES
{
    /// <summary>
    /// QQ机器人操作类
    /// </summary>
    public static class QQRoHelp
    {
        public static string AdminQQ = XmlCOM.ReadConfig("~/_data/config/qqro", "AdminQQ");
        public static string QunQQ = XmlCOM.ReadConfig("~/_data/config/qqro", "QunQQ");
        public static string ApiServer = XmlCOM.ReadConfig("~/_data/config/qqro", "ApiServer");//机器人地址
        public static string ApiPort = XmlCOM.ReadConfig("~/_data/config/qqro", "ApiPort");//API端口
        public static string Copyright = XmlCOM.ReadConfig("~/_data/config/qqro", "Copyright");//密钥
        private static string GetSendType(string type)
        {
            switch (type)
            {
                case "发送普通消息":
                    return "SendMessage";
                case "发送群消息":
                    return "SendClusterMessage";
                case "发送窗口抖动":
                    return "SendVibration";
                case "退出机器人":
                    return "ExitRo ";
                case "返回当前登录状态":
                    return "LoginStatus";
                case "获取验证码":
                    return "GetVerify";
                case "发送验证码":
                    return "SendVerify";
                case "登陆机器人":
                    return "LoginRo ";
                case "重启机器人":
                    return "ResetRo ";
                case "加入黑名单":
                    return "AddBlackList";
                case "删除黑名单":
                    return "DelBlackList";
                case "更改签名":
                    return "ModifySignature";
                case "及时更新配置文件，无需重启。":
                    return "UpdateConfig";
                case "更新机器人，群组，好友，以及其他信息":
                    return "UpdateInfo";
                case "开启/关闭群回复":
                    return "Cluster";
                case "开启/关闭好友回复":
                    return "Friend";
                case "更新QQ状态":
                    return "ChangeStatus";
                case "获取所有群信息":
                    return "GetClusterList";
                case "清理内存":
                    return "ClearMemory";
                case "加入群":
                    return "AddCluster";
                case "获取加群验证码":
                    return "GetClusterVerify";
                case "发送加群验证码":
                    return "SendClusterVerify";
                case "更改昵称":
                    return "ChangeNick";
                case "退出群":
                    return "ExitCluster";
                case "邀请对方加入群":
                    return "InviteMember";
                case "将对方从群中移除":
                    return "KickOutMember";
                case "发送讨论组消息":
                    return "SendTempClusterMessage";
                case "发送临时会话消息":
                    return "SendTempSession";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 发送普通消息
        /// </summary>
        /// <param name="ID">QQ号</param>
        /// <param name="Message">消息</param>
        /// <returns></returns>
        public static String SendMessage(string ID, string Message)
        {
            return SendApi("SendMessage", ID, Message);
        }
        public static String SendMessage(string Message)
        {
            return SendApi("SendMessage", AdminQQ, Message);
        }
        /// <summary>
        /// 发送群消息
        /// </summary>
        /// <param name="ID">群号</param>
        /// <param name="Message">消息</param>
        /// <returns></returns>
        public static String SendClusterMessage(string ID, string Message)
        {
            return SendApi("SendClusterMessage", ID, Message);
        }

        public static String SendClusterMessage(string Message)
        {
            return SendApi("SendClusterMessage", QunQQ, Message);
        }
        /// <summary>
        /// APi操作类
        /// </summary>
        /// <param name="SendType">发送类型</param>
        /// <param name="ID">QQ号码或群内部ID</param>
        /// <param name="Message">消息内容</param>
        public static String SendApi(string SendType, string ID, string Message)
        {
            return "";
            Message = System.Web.HttpUtility.UrlEncode(Message);
            string Api = "http://" + ApiServer + ":" + ApiPort + "/Api?Key=" + Copyright + "&SendType=" + SendType + "&utf=1" + "&ID=" + ID + "&Message=" + Message;
            String _result = GetData(Api, "Utf-8");
            return _result;
        }
        public static String GetData(string url, string bianma)
        {
            try
            {
                string getcode;
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                using (Stream resultStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(resultStream, Encoding.GetEncoding(bianma)))
                    {
                        getcode = reader.ReadToEnd();
                    }
                }
                return getcode;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
    }
}
