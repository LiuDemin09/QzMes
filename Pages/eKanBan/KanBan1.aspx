<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KanBan1.aspx.cs" Inherits="Pages_eKanBan_KanBan1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  
   <%--<script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
     <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>--%>
  
 <link rel="stylesheet" type="text/css" href="../../CSS/KanBan1.css" />
    <script src="../../Scripts/eKanBan/jquery-1.8.3.min.js"></script>
<script src="../../Scripts/eKanBan/highcharts.js"></script>
    <script src="../../Scripts/eKanBan/data.js"></script>
    <script src="../../Scripts/eKanBan/drilldown.js"></script>
</head>
<body style="width: 1920px; height:1080px; overflow-x:hidden;overflow-y:hidden;background: url(&quot;../../images/kanbanbg.jpg&quot;) 0% 0% / 100%;transform: scale(0.702604);  transform-origin: left top 0px;">  
    <div id="Container">
        <div id="Header">
            <div class="logo">智能工厂车间实时状况</div> 
        </div>
        <div id="Content">
            <div id="Content-Top-Left"> 
                <div id="container" style="min-width: 500px; max-width: 700px; height: 300px; margin: 0 auto;padding-top:20px;"></div>         
            </div>
            <div id="Content-Top-Middle">
                
                <div id="containerOut" style="min-width: 500px; max-width: 700px; height: 300px; margin: 0 auto;padding-top:20px;"></div>
                 <%--<div id="total">
                   <span style="font-size:28px;">今日产出:</span>  <span id="a" class="digits"></span>
                </div>--%>
            </div>
            <div id="Content-Top-Right">
                <div id="containerYield" style="min-width: 500px; max-width: 700px; height: 300px; margin: 0 auto;padding-top:20px;"></div>
            </div>
            <div id="Content-Middle-Left">
                 <div id="containerMaterialDetail" style="min-width: 100%;height: 666px; margin: 0 auto;padding-top:20px;"></div>
            </div>
            <div id="Content-Middle-Right">
                <div id="containerOutDetail" style="min-width: 1200px;height: 300px; margin: 0 auto;padding-top:20px;"></div>
            </div>
           <%-- <div id="Content-Bottom-Left">发料详细：</div>--%>
            <div id="Content-Bottom-Right"> 
                <div id="containerYeildDetail" style="min-width: 1200px; height: 300px; margin: 0 auto;padding-top:20px;"></div>
            </div>
        </div>
        <div class="Clear"><!--如何你上面用到float,下面布局开始前最好清除一下。--></div>
        <div id="Footer">本看板由钦纵智能科技研发，联系方式:802
            
        </div>
    </div> 
     <script src="../../Scripts/eKanBan/KanBan1.js" type="text/javascript"></script> 
    <script>
            jQuery(document).ready(function () { 
                KanBan1.init();               
            });
	</script>
     <form id="Form1" runat="server">
         <asp:ScriptManager ID="SMG" runat="server">
             <Services>
                 <asp:ServiceReference Path="../../services/WsSystem.asmx" />
             </Services>
         </asp:ScriptManager>
     </form>  
</body>
</html>
