<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KanBan21.aspx.cs" Inherits="Pages_eKanBan_KanBan21" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  
   <%--<script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
     <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>--%>
  
 <link rel="stylesheet" type="text/css" href="../../CSS/KanBan21.css" />
    <script src="../../Scripts/eKanBan/jquery-1.8.3.min.js"></script>
<script src="../../Scripts/eKanBan/highcharts.js"></script>
    <script src="../../Scripts/eKanBan/data.js"></script>
    <script src="../../Scripts/eKanBan/drilldown.js"></script>
</head>
<body style="width: 1080px; height:1920px; overflow-x:hidden;overflow-y:hidden;background: url(&quot;../../images/machine/bk.jpg&quot;) 0% 0% / 100%;  transform-origin: left top 0px;">  
    <div id="Container">
        <div id="Header">
            <div class="logo">智能车间机床实时状况</div> 
        </div>
        <div id="Content">
            <div id="Content-Top-1" class="machineInfo"> 
                <div id="machine1-1" class="machinestyle">                    
                    <img src="../../images/machine/快亚.jpg" class="machineimage"/>
                    <span class="machinename">快亚</span>
                </div> 
                <div id="curstatus1-1" class="curstatusstyle">
                    <span id="curstatus1-1-out" class="textstyle"></span>
                    <span id="curstatus1-1-wo"  class="textstyle"></span>
                    <span id="curstatus1-1-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator1-1" class="operatorstyle">
                    <img id="operator1-1-img" src="./" class="operatorimage"/> <%--src="../../images/machine/1.ico"--%>
                    <span id="operator1-1-name"  class="machinename"></span>
                </div> 
                <div id="machineout1-1" class="machineoutstyle"></div>         
            </div>
            <div id="Content-Top-2" class="machineInfo">
                <div id="machine1-2" class="machinestyle">
                    <img src="../../images/machine/友嘉.jpg" class="machineimage"/>
                    <span class="machinename">友嘉</span>
                </div>
                <div id="curstatus1-2" class="curstatusstyle">
                    <span id="curstatus1-2-out" class="textstyle"></span>
                    <span id="curstatus1-2-wo"  class="textstyle"></span>
                    <span id="curstatus1-2-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator1-2" class="operatorstyle">
                    <img id="operator1-2-img" src="./" class="operatorimage"/><%--src="../../images/machine/2.ico"--%>
                    <span id="operator1-2-name"  class="machinename"></span>
                </div> 
                <div id="machineout1-2" class="machineoutstyle"></div>
            </div>
            <div id="Content-Top-3" class="machineInfo">
                <div id="machine1-3" class="machinestyle">
                    <img src="../../images/machine/欧马850.jpg" class="machineimage"/>
                    <span class="machinename">欧马850</span>
                </div>
                <div id="curstatus1-3" class="curstatusstyle">
                    <span id="curstatus1-3-out" class="textstyle"></span>
                    <span id="curstatus1-3-wo"  class="textstyle"></span>
                    <span id="curstatus1-3-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator1-3" class="operatorstyle">
                    <img id="operator1-3-img" src="./" class="operatorimage"/><%--src="../../images/machine/3.ico"--%>
                    <span id="operator1-3-name"  class="machinename"></span>
                </div> 
                <div id="machineout1-3" class="machineoutstyle"></div>
            </div>
             <div id="Content-Top-4" class="machineInfo">
                <div id="machine1-4" class="machinestyle">
                    <img src="../../images/machine/1160A.jpg" class="machineimage"/>
                    <span class="machinename">1160A</span>
                </div>
                 <div id="curstatus1-4" class="curstatusstyle">
                     <span id="curstatus1-4-out" class="textstyle"></span>
                    <span id="curstatus1-4-wo"  class="textstyle"></span>
                    <span id="curstatus1-4-pdn"  class="textstyle"></span>
                 </div> 
                <div id="operator1-4" class="operatorstyle">
                    <img id="operator1-4-img" src="./" class="operatorimage"/><%--src="../../images/machine/4.ico"--%>
                    <span id="operator1-4-name"  class="machinename"></span>
                </div> 
                <div id="machineout1-4" class="machineoutstyle"></div>
            </div>
            <div id="Content-Middle-1" class="machineInfo">
                  <div id="machine2-1" class="machinestyle">
                      <img src="../../images/machine/1160A-1.jpg" class="machineimage"/>
                      <span class="machinename">1160A-1</span>
                  </div> 
                <div id="curstatus2-1" class="curstatusstyle">
                    <span id="curstatus2-1-out" class="textstyle"></span>
                    <span id="curstatus2-1-wo"  class="textstyle"></span>
                    <span id="curstatus2-1-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator2-1" class="operatorstyle">
                    <img  id="operator2-1-img" src="./"  class="operatorimage"/><%--src="../../images/machine/5.ico"--%>
                    <span id="operator2-1-name"  class="machinename"></span>
                </div> 
                <div id="machineout2-1" class="machineoutstyle"></div>  
            </div>
            <div id="Content-Middle-2" class="machineInfo">
                <div id="machine2-2" class="machinestyle">
                    <img src="../../images/machine/1160A-2.jpg" class="machineimage" />
                    <span class="machinename">1160A-2</span>
                </div>
                <div id="curstatus2-2" class="curstatusstyle">
                    <span id="curstatus2-2-out" class="textstyle"></span>
                    <span id="curstatus2-2-wo"  class="textstyle"></span>
                    <span id="curstatus2-2-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator2-2" class="operatorstyle">
                    <img id="operator2-2-img" src="./" class="operatorimage"/><%-- src="../../images/machine/6.ico" --%>
                    <span id="operator2-2-name"  class="machinename"></span>
                </div> 
                <div id="machineout2-2" class="machineoutstyle"></div>
            </div>
             <div id="Content-Middle-3" class="machineInfo">
                <div id="machine2-3" class="machinestyle">
                    <img src="../../images/machine/1160A-3.jpg" class="machineimage"/>
                    <span class="machinename">1160A-3</span>
                </div>
                <div id="curstatus2-3" class="curstatusstyle">
                    <span id="curstatus2-3-out" class="textstyle"></span>
                    <span id="curstatus2-3-wo"  class="textstyle"></span>
                    <span id="curstatus2-3-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator2-3" class="operatorstyle">
                    <img id="operator2-3-img" src="./"  class="operatorimage"/><%-- src="../../images/machine/7.ico"--%>
                    <span id="operator2-3-name"  class="machinename"></span>
                </div> 
                <div id="machineout2-3" class="machineoutstyle"></div>
            </div>
             <div id="Content-Middle-4" class="machineInfo">
                 <div id="machine2-4" class="machinestyle">                      
                     <img src="../../images/machine/车铣.jpg" class="machineimage"/>
                     <span class="machinename">车铣</span>
                 </div>
                 <div id="curstatus2-4" class="curstatusstyle">
                     <span id="curstatus2-4-out" class="textstyle"></span>
                    <span id="curstatus2-4-wo"  class="textstyle"></span>
                    <span id="curstatus2-4-pdn"  class="textstyle"></span>
                 </div> 
                <div id="operator2-4" class="operatorstyle">
                    <img  id="operator2-4-img" src="./" class="operatorimage"/><%--src="../../images/machine/8.ico"--%>
                    <span id="operator2-4-name"  class="machinename"></span>
                </div> 
                <div id="machineout2-4" class="machineoutstyle"></div>
            </div>
            <div id="Content-Bottom-1" class="machineInfo"> 
                 <div id="machine3-1" class="machinestyle">
                     <img src="../../images/machine/五轴.jpg" class="machineimage" />
                     <span class="machinename">五轴</span>
                 </div> 
                <div id="curstatus3-1" class="curstatusstyle">
                     <span id="curstatus3-1-out" class="textstyle"></span>
                    <span id="curstatus3-1-wo"  class="textstyle"></span>
                    <span id="curstatus3-1-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator3-1" class="operatorstyle">
                    <img id="operator3-1-img" src="./" class="operatorimage"/><%--src="../../images/machine/9.ico"--%>
                    <span id="operator3-1-name"  class="machinename"></span>
                </div> 
                <div id="machineout3-1" class="machineoutstyle"></div> 
            </div>
            <div id="Content-Bottom-2" class="machineInfo"> 
                <div id="machine3-2" class="machinestyle">
                    <img src="../../images/machine/大友嘉.jpg" class="machineimage"/>
                    <span class="machinename">大友嘉</span>
                </div>
                <div id="curstatus3-2" class="curstatusstyle">
                     <span id="curstatus3-2-out" class="textstyle"></span>
                    <span id="curstatus3-2-wo"  class="textstyle"></span>
                    <span id="curstatus3-2-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator3-2" class="operatorstyle">
                    <img  id="operator3-2-img" src="./" class="operatorimage"/><%-- src="../../images/machine/10.ico" --%>
                    <span id="operator3-2-name"  class="machinename"></span>
                </div> 
                <div id="machineout3-2" class="machineoutstyle"></div>
            </div>
             <div id="Content-Bottom-3" class="machineInfo"> 
                  <div id="machine3-3" class="machinestyle">
                      <img src="../../images/machine/SAJO.jpg" class="machineimage"/>
                      <span class="machinename">SAJO</span>
                  </div>
                <div id="curstatus3-3" class="curstatusstyle">
                     <span id="curstatus3-3-out" class="textstyle"></span>
                    <span id="curstatus3-3-wo"  class="textstyle"></span>
                    <span id="curstatus3-3-pdn"  class="textstyle"></span>
                </div> 
                <div id="operator3-3" class="operatorstyle">
                    <img id="operator3-3-img" src="./" class="operatorimage"/><%--src="../../images/machine/11.ico"--%>
                    <span id="operator3-3-name"  class="machinename"></span>
                </div> 
                <div id="machineout3-3" class="machineoutstyle"></div>
            </div>
             <div id="Content-Bottom-4" class="machineInfo"> 
                 <div id="machine3-4" class="machinestyle">
                     <%--<img src="./" class="machineimage"/>--%>
                     <span class="machinename"></span>
                 </div>
                 <div id="curstatus3-4" class="curstatusstyle">
                      <span id="curstatus3-4-out" class="textstyle"></span>
                    <span id="curstatus3-4-wo"  class="textstyle"></span>
                    <span id="curstatus3-4-pdn"  class="textstyle"></span>
                 </div> 
                <div id="operator3-4" class="operatorstyle">
                    <%--<img  id="operator3-4-img" src="./"  class="operatorimage"/>src="../../images/machine/12.ico"--%>
                    <span id="operator3-4-name"  class="machinename"></span>
                </div> 
                <div id="machineout3-4" class="machineoutstyle"></div>
            </div>
        </div>
        <div class="Clear"><!--如何你上面用到float,下面布局开始前最好清除一下。--></div>
        <div id="Footer">本看板由钦纵智能科技研发，联系方式:802
            
        </div>
    </div> 
     <script src="../../Scripts/eKanBan/KanBan21.js" type="text/javascript"></script> 
    <script>
            jQuery(document).ready(function () { 
                KanBan2.init();               
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
