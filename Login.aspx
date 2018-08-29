<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<!DOCTYPE html>

<html lang="zh-cn">
<head>
<title>登录</title>
    <%--<link href="images/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="CSS/login.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="Scripts/bootstrap/css/bootstrap.min.css"  />
    <script type="text/javascript" src="Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/bootstrap/bootstrap.js"></script>
</head>
<body class="loginbody">
<div class="login_div"> 
    <div id="login">
		<table id="center" cellpadding="0" cellspacing="0">
			<tr>
		 	  <td id="center_left">
		 	  	<!-- <div class="logo"><img src="${basepath}/resource/framework/images/login1/logo.png" /></div> -->
		 	  	<div class="welcome"></div>
		 	  </td>
		 	  <td id="center_horn">
		 	  	<div id="horn"></div>
		 	  </td>
			  <td id="center_middle">
			  	   <div id="homeImage">
			       		<img alt="MES智能制造执行系统" src="images/login/title_mes.png">
			       </div>
			       <div id="login_m">
					   <div id="formDiv">
					   	   <div id="formDiv_title">登录系统</div>
					   	   <form id="form1" autocomplete="off" class="login active login-form">
					   	   <div id="formDiv_content">
							   <div id="user">
								 <input type="text" id="txtUserName" name="username" placeholder="英文输入法下的用户名"/>
							   </div>
							   <div id="pword">
								 <input type="password" id="txtPassword" name="password" placeholder="英文输入法下的密码"/>
							   </div>
                                <div class="checkbox">
                                      <label>
                                        <input name="chkRemember" type="checkbox" id="chkRemember" value=""/>记住用户名         
                                      </label>
                                </div>
						   </div>
						   <div id="btn"><a href="#" id="submitBtn" type="submit" name="submitBtn">登录</a></div>
                               <%-- <div id="btn">             
                                        <button type="submit" class="btn btn-primary btn-lg btn-block"  >登录</button>            
                                </div>--%>
                                <div class="alert alert-danger alert-dismissible" role="alert">
                                    <span  id="spErrMsg"> </span>
                                  <button type="button" class="close" id="spClose"><span aria-hidden="true">&times;</span></button>
                                </div>
						   </form>
						   <div id="logoIcon"></div>
					   </div>
					   <div id="copyright">Copyright © 2017 QIN ZONG Inc. All Rights Reserved 钦纵机电 版权所有</div>
				   </div>
				</td>
			</tr>
		  </table> 
	  </div>
	</div>
    <script src="Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
     <script src="Scripts/StringRes.js" type="text/javascript"></script>
    <script src="Scripts/Login.js" type="text/javascript"></script> 
    
	<script>

	    jQuery(document).ready(function () {

	        //var bro = $.browser;
	        //var binfo = "";
	        //if (bro.msie) {
	        //    if ("6.0-7.0-8.0".indexOf(bro.version) != -1) {
	        //        alert("系統檢測到您的IE瀏覽器" + bro.version + "版本過低，這將嚴重影響到系統的使用體驗，甚至造成部分功能無法正常使用\t\n建議立即聯繫IT升級您的系統；本系統完美支持IE9.0+或Chrome20+或者Firefox8+");
	        //    }
	        //}
	        Login.init();	       
	    });

	</script>
      
 <form runat="server">
     <asp:ScriptManager ID="SMG" runat="server">
         <Services>
             <asp:ServiceReference Path="/services/WsLogin.asmx" />
             <asp:ServiceReference Path="/services/WsSystem.asmx" />
         </Services>
     </asp:ScriptManager>
 </form> 
</body>
</html>