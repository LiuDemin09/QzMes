<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<html>
<head>
<meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <title>Mes智能制造执行系统</title>
     <%-- <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>--%>

   <link href="CSS/default.css" rel="stylesheet" type="text/css" />
   <link rel="stylesheet" type="text/css" href="Scripts/easyui/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="Scripts/easyui/themes/icon.css" /> 
  
     <script type="text/javascript" src="Scripts/easyui/jquery-1.4.4.min.js"></script> 
     <script type="text/javascript" src="Scripts/easyui/jquery.easyui.min.js"></script>
   <script type="text/javascript" src="Scripts/JqueryMedia/jquery.media.js"></script>
    <script type="text/javascript">
	     
    </script>
</head>
<body class="easyui-layout" style="overflow-y: hidden"  scroll="no">
<noscript>
<div style=" position:absolute; z-index:100000; height:2046px;top:0px;left:0px; width:100%; background:white; text-align:center;">
    <img src="images/noscript.gif" alt='抱歉，请开启脚本支持！' />
</div></noscript>
    <div region="north" split="false" border="false" style="overflow: hidden; height: 50px;
        /*background: url(images/layout-browser-hd-bg.gif) #7f99be repeat-x center 50%;*/
        background: url(images/head_bg.gif) #7f99be repeat-x center 50%;
        /*.header{ background:url(head_bg.gif) repeat-x left bottom; color:#E8E8E8; font-family:Verdana, Geneva, sans-serif; }*/
        line-height: 50px;color: #fff; font-family: Verdana, 微软雅黑,黑体">
        <span style="float:right; padding-right:20px;" class="head">欢迎您：<span class="username"><%=this.userInfo.UserName %>  </span> <a href="#" id="editpass" style="text-decoration:none;">修改密码</a> <a class="media" style="text-decoration:none;" href="../Download/MES用户手册.pdf">用户手册</a><%--<a href="#" id="userhelp"  style="text-decoration:none;">用户手册</a>--%> <a href="#" id="loginOut"  style="text-decoration:none;">安全退出</a></span>
        <span style="float:left;padding-left:10px; font-size: 16px; "><img src="images/qztb.gif" width="20" height="20" align="absmiddle" /> 钦纵MES智能制造执行系统</span>
    </div>
    <div region="south" split="true" style="height: 30px; background: #D2E0F2; ">
        <div class="footer">By 研发部 Email:pmw@qzmc.com</div>
    </div>
    <div region="west" split="true" title="导航菜单" style="width:150px;" id="west">
        <div id="nav" class="easyui-accordion" fit="true" border="false">
		<!--  导航内容 -->
		</div>
    </div>
   <%-- <div data-options="region:'west',split:true,hideCollapsedContent:false" title="导航菜单" style="width:150px;" id="west">
         <div id="nav" class="easyui-accordion" fit="true" border="false">
		<!--  导航内容 -->
		</div>
    </div>--%>
    <div id="mainPanle" region="center" style="background: #eee; overflow-y:hidden;">
        <div id="tabs" class="easyui-tabs"  fit="true" border="false" style="overflow:hidden;">
			<div title="欢迎使用" style="padding:20px;overflow:hidden;" id="home">
                <h1>欢迎来到钦纵Mes智能制造执行系统！</h1>
				<img src="images/login/welcometoMes.jpg" style="width:80%;height:70%"/>	
			</div>
		</div>
    </div>
    
    
    <!--修改密码窗口-->
    <div id="w" class="easyui-window" title="修改密码"  style="width: 300px; height: 200px; padding: 5px;background: #fafafa;"
        data-options="modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">
        <div class="easyui-layout" fit="true">
            <div region="center" border="false" style="padding: 10px; background: #fff; border: 1px solid #ccc;">
                <table cellpadding=3>
                    <tr>
                        <td>原密码：</td>
                        <td><input id="txtOldPass" type="Password" class="txt01" /></td>
                    </tr>
                    <tr>
                        <td>新密码：</td>
                        <td><input id="txtNewPass" type="Password" class="txt01" /></td>
                    </tr>
                    <tr>
                        <td>确认密码：</td>
                        <td><input id="txtRePass" type="Password" class="txt01" /></td>
                    </tr>
                </table>
            </div>
            <div region="south" border="false" style="text-align: right; height: 30px; line-height: 30px;">
                <a id="btnConfirm" class="easyui-linkbutton" icon="icon-ok" href="javascript:void(0)" > 确定</a> 
                <a id="btnCancel" class="easyui-linkbutton" icon="icon-cancel" href="javascript:void(0)">取消</a>
            </div>
        </div>
    </div>

	<div id="mm" class="easyui-menu" style="width:150px;">  
        <div id="mm-tabupdate">刷新</div>     
		<div id="mm-tabclose">关闭</div>
		<div id="mm-tabcloseall">全部关闭</div>
		<div id="mm-tabcloseother">除此之外全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-tabcloseright">当前页右侧全部关闭</div>
		<div id="mm-tabcloseleft">当前页左侧全部关闭</div>
		<div class="menu-sep"></div>
		<div id="mm-exit">退出</div>
	</div>
     <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="ClientMedia/js/ui-tree.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxtree.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxtree_kn.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxgrid.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxgridcell.js" type="text/javascript"></script>
    <script src="ClientMedia/dhtmlx/js/dhtmlxgrid_pgn.js" type="text/javascript"></script>
     <script src="Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
     <script src="Scripts/StringRes.js" type="text/javascript"></script>
     <script src="Scripts/Common.js" type="text/javascript"></script> 
     <script type="text/javascript" src='Scripts/main.js'> </script>
    <script src="ClientMedia/js/app.js" type="text/javascript"></script> 
    <!-- END PAGE LEVEL SCRIPTS -->
 <script>

	    jQuery(document).ready(function () {
	        Main.init(); // initlayout and core plugins
	        Main.InitLeftMenu();
	        Main.tabClose();
	        Main.tabCloseEven();
	    });

	</script>
      
 <form  runat="server">
     <asp:ScriptManager ID="SMG" runat="server">
         <Services>
             <asp:ServiceReference Path="services/WsSystem.asmx" />
         </Services>
     </asp:ScriptManager>
 </form> 
</body>
</html>
