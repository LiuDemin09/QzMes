using System;
using System.Text;

namespace MES
{
    /// <summary>
    /// BasicPage 的摘要说明
    /// </summary>
    public class BasicPage : System.Web.UI.Page
    {
        /// <summary>
        /// 当前登录的用户ID
        /// </summary>
        protected int UserId = 0;
        /// <summary>
        /// 当前登录的用户名
        /// </summary>
        protected string UserName = "";
        /// <summary>
        /// 当前登录者的职务
        /// </summary>
        protected string UserPosition = "";
        /// <summary>
        /// 权限 1=管理员,2=主管,3=部门主管,4=员工
        /// </summary>
        protected int UserPowerId = 0;
        /// <summary>
        /// 部门编号
        /// </summary>
        protected int UserDepartmentId = 0;
        /// <summary>
        /// 用户权限
        /// </summary>
        protected string UserSetting = "";
        protected bool UserIsLogin = false;
        override protected void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        /// <summary>
        /// 验证登陆
        /// </summary>
        private void chkLogin(string go2Url, int BackStep)
        {
            //if (MES.Cookie.GetValue("oa_user") != null)
            //{
            //    UserId = Validator.StrToInt(MES.Cookie.GetValue("oa_user", "id"), 0);
            //    UserName = MES.Cookie.GetValue("oa_user", "name");
            //    if (UserId != 0 && UserName.Length != 0)
            //    {
            //        MES.Entity.UserEntity model = new MES.Entity.UserEntity();
            //        model = new BLL.UserBLL().GetEntity(UserId);
            //        if (model.Uipaddress.Length > 0)
            //        {
            //            if (Request.UserHostAddress != model.Uipaddress)
            //            {
            //                showErrMsg("非法IP地址登录", go2Url, BackStep);
            //                return;
            //            }
            //        }
            //        this.UserIsLogin = true;
            //        UserPosition = model.Position;
            //        UserPowerId = model.Pid;
            //        UserDepartmentId = model.Did;
            //        UserSetting = model.Setting;
            //    }
            //}
        }
        /// <summary>
        /// 初始化用户信息，包括IP的判断等
        /// </summary>
        protected void User_Load(string powerStr, string go2Url, int BackStep)
        {
            chkPower(powerStr, go2Url, BackStep);
        }
        /// <summary>
        /// 初始化用户信息，包括IP的判断等
        /// </summary>
        protected void User_Load(string powerStr)
        {
            chkPower(powerStr, "", 1);
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s"></param>
        protected void chkPower(string s, string go2Url, int BackStep)
        {
            if (!IsPower(s, go2Url, BackStep))
            {
                showErrMsg("权限不足或未登录", go2Url, BackStep);
            }
        }
        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="s">空时只要求登录</param>
        protected bool IsPower(string s, string go2Url, int BackStep)
        {
            if (s == "ok") return true;
            if (!this.UserIsLogin)//验证一次本地信息
                chkLogin(go2Url, BackStep);
            if (s == "") return (this.UserIsLogin);
            return (this.UserSetting.Contains(",all,") || this.UserSetting.Contains("," + s + ","));
        }
        /// <summary>
        /// 输出错误信息
        /// </summary>
        /// <param name="msg"></param>
        protected void showErrMsg(string msg, string go2Url, int BackStep)
        {
            FinalMessage(msg, go2Url, BackStep);
        }
        public bool UserLogout()
        {
            MES.Cookie.Del("oa_user");
            return true;
        }
        /// <summary>
        /// 管理菜单--管理员
        /// </summary>
        /// <returns></returns>
        protected string[,] powerMenu1()
        {
            string[,] menu = new string[3, 6];
            menu[0, 0] = "人员管理";
            menu[0, 1] = "查看|user-show";
            menu[0, 2] = "添加|user-add";
            menu[0, 3] = "修改|user-edit";
            menu[0, 4] = "删除|user-del";

            menu[1, 0] = "日志管理";
            menu[1, 1] = "查看管理员日志|adminlog-show";
            menu[1, 2] = "查看其他日志|operatelog-show";

            menu[2, 0] = "其他管理";
            menu[2, 1] = "修改密码|password-edit";
            menu[2, 2] = "登录系统|login|1";

            return menu;
        }
        /// <summary>
        /// 管理菜单--组织管理者
        /// </summary>
        /// <returns></returns>
        protected string[,] powerMenu2()
        {
            string[,] menu = new string[9, 10];
            menu[0, 0] = "学习资料管理";
            menu[0, 1] = "查看|learning-show";
            menu[0, 2] = "添加|learning-add";
            menu[0, 3] = "修改|learning-edit";
            menu[0, 4] = "删除|learning-del";

            menu[1, 0] = "通知公告管理";
            menu[1, 1] = "查看|placard-show";
            menu[1, 2] = "添加|placard-add";
            menu[1, 3] = "修改|placard-edit";
            menu[1, 4] = "删除|placard-del";

            menu[2, 0] = "工作总结管理";
            menu[2, 1] = "查看|summarize-show";
            menu[2, 2] = "删除|summarize-del|1";

            menu[3, 0] = "员工日志管理";
            menu[3, 1] = "查看办公日志|worklog-show";
            menu[3, 2] = "查看来电处理|callinfo-show";

            menu[4, 0] = "员工考勤管理";
            menu[4, 1] = "查看|time-show";

            menu[5, 0] = "工作分配管理";
            menu[5, 1] = "查看|task-show";
            menu[5, 2] = "添加|task-add";
            menu[5, 3] = "修改|task-edit";
            menu[5, 4] = "删除|task-del";

            menu[6, 0] = "员工计划管理";
            menu[6, 1] = "查看|plan-show";

            menu[7, 0] = "站内短信管理";
            menu[7, 1] = "查看|message-show";
            menu[7, 2] = "发送|message-add";
            menu[7, 3] = "删除|message-del";

            menu[8, 0] = "其他管理";
            menu[8, 1] = "修改密码|password-edit|1";
            menu[8, 2] = "登录系统|login|1";
            return menu;
        }
        /// <summary>
        /// 管理菜单--
        /// </summary>
        /// <returns></returns>
        protected string[,] powerMenu3()
        {
            string[,] menu = new string[9, 10];
            menu[0, 0] = "学习资料管理";
            menu[0, 1] = "查看|learning-show";
            menu[0, 2] = "添加|learning-add";
            menu[0, 3] = "修改|learning-edit";
            menu[0, 4] = "删除|learning-del";

            menu[1, 0] = "通知公告管理";
            menu[1, 1] = "查看|placard-show";
            menu[1, 2] = "添加|placard-add";
            menu[1, 3] = "修改|placard-edit";
            menu[1, 4] = "删除|placard-del";

            menu[2, 0] = "工作总结管理";
            menu[2, 1] = "查看|summarize-show";
            menu[2, 2] = "删除|summarize-del";

            menu[3, 0] = "员工日志管理";
            menu[3, 1] = "查看办公日志|worklog-show";
            menu[3, 2] = "查看来电处理|callinfo-show";

            menu[4, 0] = "员工考勤管理";
            menu[4, 1] = "查看|time-show";

            menu[5, 0] = "工作分配管理";
            menu[5, 1] = "查看|task-show";
            menu[5, 2] = "添加|task-add";
            menu[5, 3] = "修改|task-edit";
            menu[5, 4] = "删除|task-del";

            menu[6, 0] = "员工计划管理";
            menu[6, 1] = "查看|plan-show";

            menu[7, 0] = "站内短信管理";
            menu[7, 1] = "查看|message-show";
            menu[7, 2] = "发送|message-add";
            menu[7, 3] = "删除|message-del";

            menu[8, 0] = "其他管理";
            menu[8, 1] = "修改密码|password-edit|1";
            menu[8, 2] = "登录系统|login|1";
            return menu;
        }
        /// <summary>
        /// 管理菜单--
        /// </summary>
        /// <returns></returns>
        protected string[,] powerMenu4()
        {
            string[,] menu = new string[2, 10];
            menu[0, 0] = "基本权限";
            menu[0, 1] = "修改密码|password-edit|1";
            menu[0, 2] = "登录系统|login|1";
            menu[0, 3] = "查看学习资料|my_learning-show";
            menu[0, 4] = "查看公告通知|my_placard-show";

            menu[1, 0] = "站内短信管理";
            menu[1, 1] = "查看|message-show";
            menu[1, 2] = "发送|message-add";
            menu[1, 3] = "删除|message-del";
            return menu;
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep)
        {
            FinalMessage(pageMsg, go2Url, BackStep, 2);
        }
        /// <summary>
        /// 处理过程完成
        /// </summary>
        /// <param name="pageMsg">页面提示信息</param>
        /// <param name="go2Url">如果倒退步数为0，就转到该地址</param>
        /// <param name="BackStep">倒退步数</param>
        /// <param name="BackStep">自动转向的秒数</param>
        protected void FinalMessage(string pageMsg, string go2Url, int BackStep, int Seconds)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\r\n");
            sb.Append("<html xmlns='http://www.w3.org/1999/xhtml'>\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />\r\n");
            sb.Append("<head>\r\n");
            sb.Append("<title>系统提示</title>\r\n");
            sb.Append("<style>\r\n");
            sb.Append("body {padding:0; margin:0; }\r\n");
            sb.Append("#infoBox{padding:0; margin:0; position:absolute; top:40%; width:100%; text-align:center;}\r\n");
            sb.Append("#info{padding:0; margin:0;position:relative; top:-40%; right:0; border:0px #B4E0F7 solid; text-align:center;}\r\n");
            sb.Append("</style>\r\n");
            sb.Append("<script language=\"javascript\">\r\n");
            sb.Append("var seconds=" + Seconds + ";\r\n");
            sb.Append("for(i=1;i<=seconds;i++)\r\n");
            sb.Append("{window.setTimeout(\"update(\" + i + \")\", i * 1000);}\r\n");
            sb.Append("function update(num)\r\n");
            sb.Append("{\r\n");
            sb.Append("if(num == seconds)\r\n");
            if (BackStep > 0)
                sb.Append("{ history.go(" + (0 - BackStep) + "); }\r\n");
            else
            {
                if (go2Url != "")
                    sb.Append("{ self.location.href='" + go2Url + "'; }\r\n");
                else
                    sb.Append("{window.close();}\r\n");
            }
            sb.Append("else\r\n");
            sb.Append("{ }\r\n");
            sb.Append("}\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<base target='_self' />\r\n");
            sb.Append("</head>\r\n");
            sb.Append("<body>\r\n");
            sb.Append("<div id='infoBox'>\r\n");
            sb.Append("<div id='info'>\r\n");
            sb.Append("<div style='text-align:center;margin:0 auto;width:320px;padding-top:4px;line-height:26px;height:26px;font-weight:bold;color:#2259A6;font-size:14px;border:1px #B4E0F7 solid;background:#CAEAFF;'>提示信息</div>\r\n");
            sb.Append("<div style='text-align:center;padding:20px 0 20px 0;margin:0 auto;width:320px;font-size:12px;background:#F5FBFF;border-right:1px #B4E0F7 solid;border- tom:1px #B4E0F7 solid;border-left:1px #B4E0F7 solid;'>\r\n");
            sb.Append(pageMsg + "<br /><br />\r\n");
            if (BackStep > 0)
                sb.Append("        <a href=\"javascript:history.go(" + (0 - BackStep) + ")\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            else
                sb.Append("        <a href=\"" + go2Url + "\">如果您的浏览器没有自动跳转，请点击这里</a>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("</body>\r\n");
            sb.Append("</html>\r\n");
            Response.Write(sb.ToString());
            Response.End();
        }
        /// <summary>
        /// 输出json结果
        /// </summary>
        /// <param name="success">是否操作成功,0表示失败;1表示成功</param>
        /// <param name="str">输出字符串</param>
        /// <returns></returns>
        protected string JsonResult(int success, string str)
        {
            return "{result :\"" + success.ToString() + "\",returnval :\"" + str + "\"}";

        }
        /// <summary>
        /// 获取querystring
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string q(string s)
        {
            if (Request.QueryString[s] != null && Request.QueryString[s] != "")
            {
                return Request.QueryString[s].ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取post得到的参数
        /// </summary>
        /// <param name="s">参数名</param>
        /// <returns>返回值</returns>
        public string f(string s)
        {
            if (Request.Form[s] != null && Request.Form[s] != "")
            {
                return Request.Form[s].ToString();
            }
            return string.Empty;
        }
        /// <summary>
        /// 返回非负整数，默认为t
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s, int t)
        {
            return MES.Validator.StrToInt(s, t);
        }

        /// <summary>
        /// 返回非负整数，默认为0
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public int Str2Int(string s)
        {
            return Str2Int(s, 0);
        }

        /// <summary>
        /// 返回非空字符串，默认为"0"
        /// </summary>
        /// <param name="s">参数值</param>
        /// <returns>返回值</returns>
        public string Str2Str(string s)
        {
            return MES.Validator.StrToInt(s, 0).ToString();
        }
        public void DownloadFile(string _filePath)
        {
            Response.Clear();
            bool success = true;
            if (_filePath.StartsWith("http://") || _filePath.StartsWith("https://") || _filePath.StartsWith("ftp://"))
                Response.Redirect(_filePath);
            else if (!MES.DirFile.FileExists(_filePath))
            {
                Response.Write("指定的文件不存在,请通知管理员");
            }
            else
            {
                success = MES.DirFile.DownloadFile(Request, Response, Server.MapPath(_filePath), 1024000);
                if (!success)
                {
                    Response.Redirect(_filePath);
                }
            }
            Response.End();
        }
        public string GetTimeRemark(string TimeType, string TimeInfo, string Retime, string Nowtime)
        {
            string _remark = TimeType;
            switch (TimeType)
            {
                case "上班登记":
                    if (Str2Int(Retime) < Str2Int(Nowtime))
                        _remark += "(<span style='color:red;'>疑似迟到</span>)";
                    break;
                case "下班登记":
                    if (Str2Int(Retime) > Str2Int(Nowtime))
                        _remark += "(<span style='color:red;'>疑似早退</span>)";
                    break;
                case "外出登记":
                    _remark += "(<span style='color:blue;'>" + TimeInfo + "</span>)";
                    break;
                case "出差登记":
                    _remark += "(<span style='color:blue;'>" + TimeInfo + "</span>)";
                    break;
                case "事病假登记":
                    _remark += "(<span style='color:red;'>" + TimeInfo + "</span>)";
                    break;
                case "销假登记":
                    _remark += "(<span style='color:red;'>" + TimeInfo + "</span>)";
                    break;
                default:
                    _remark = TimeType;
                    break;
            }
            return _remark;
        }
        public string GetUserNameById(string uid)
        {
            //MES.Entity.UserEntity user = new MES.BLL.UserBLL().GetEntity(Str2Int(uid));
            //return user.Uname;
            return string.Empty;
        }
        /// <summary>
        /// 获取当前登录信息
        /// 1编号，2用户名称,3ip,4职务编号pid
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string getvalue(int i)
        {
            string powerid ="";
            if (MES.Cookie.GetValue("oa_user") != null)
            {
                if(i==1)
                    powerid = MES.Cookie.GetValue("oa_user", "id").ToString();
                 else if(i==2)
                    powerid = MES.Cookie.GetValue("oa_user", "name").ToString();
                   else if(i==3)
                    powerid = MES.Cookie.GetValue("oa_user", "ip").ToString();
                 else if(i==4)
                    powerid = MES.Cookie.GetValue("oa_user", "Powerid").ToString();
            }
            return powerid;
        }
    }
}
