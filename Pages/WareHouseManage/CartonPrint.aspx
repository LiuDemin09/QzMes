<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CartonPrint.aspx.cs" Inherits="Pages_WareHouseManage_CartonPrint" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>箱号打印</title>    
     <!-- begin  javascript files -->
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/locale/easyui-lang-zh_CN.js"></script>
    <script src="../../Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/My97DatePicker/WdatePicker.js"></script>
    <script src="../../Scripts/StringRes.js" type="text/javascript"></script>
     <script src="../../Scripts/Common.js" type="text/javascript"></script> 

    <style type="text/css">
        #myResume {
            border-collapse: collapse; /* 让边框合并 */
            border: 1px solid #95B8E7; /*边框1个像素  */
        }
        #myResume1 {
            border-collapse: collapse; /* 让边框合并 */
            border: 1px solid #95B8E7; /*边框1个像素  */
        }
        /**改变th的字体*/
        th {
            font-weight: normal;
            color: #0E2D5F;
            height: 20px;
            line-height: 20px;
            width: 80px;
            text-align: center;
        }

        td {
            padding: 2px;
        }

        .resumeTable {
            width: 98%;
        }
        /**消息提示部分 start*/
        #tip_message {
            z-index: 9999;
            position: fixed;
            left: 0;
            top: 40%;
            text-align: center;
            width: 100%;
        }

        #tip_message span {
            background-color: #03C440;
            opacity: .8;
            padding: 20px 50px;
            border-radius: 5px;
            text-align: center;
            color: #fff;
            font-size: 20px;
        }

        #tip_message span.error {
            background-color: #EAA000;
        }
        /*end*/
    </style>
    <!-- END javascript files -->
</head>
<body>
    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">
        <div id="centerPanel" data-options="region:'north',split:true" title="箱号打印" style="height: 45%; width: 100%">
            <div class="easyui-layout" data-options="fit:true">
             <div class="easyui-panel"  data-options="region:'center',split:true" style="height: 100%; width: 55%">
                <form id="ff" method="post" style="float:left;padding: 3% ">
                     <div style="padding: 3% ">
                        <table id="myResume"  >
                             <tr>
                                <td>产品图号:</td>
                                <td>
                                        <input type="text" id="txtPartsdrawingNo" style="height: 27px; width: 180px"/></td>
                            </tr>
                            <tr>
                                <td>质量编号:</td>
                                <td>
                                        <input type="text" id="txtQualityCode" style="height: 27px; width: 180px"/></td>
                            </tr>
                            <tr>
                                <td>件&nbsp;&nbsp;数:</td>
                                <td>
                                    <input type="text" id="txtQty" style="height: 27px; width:180px"/></td>
                           </tr>
                            <tr>
                                <td> <a  id="btnSave"class="easyui-linkbutton" data-options="iconCls:'icon-print'"  style="width:90px">打&nbsp;&nbsp;印</a></td>
                                <td><a  id="btnReset"class="easyui-linkbutton" data-options="iconCls:'icon-clear'"  style="width:90px">重&nbsp;&nbsp;置</a></td>
                           </tr>
                    </table>
                </div>                   
                <div id="divMsg" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg">message</span>
                </div>
            </form>
                 <form id="ff1" method="post" style="float:left;padding: 3% ">
                     <div style="padding: 3% ">
                        <table id="myResume1"  >
                             <tr>
                                <td>产品图号:</td>
                                <td>
                                        <input type="text" id="txtPartsdrawingNo1" style="height: 27px; width: 180px"/></td>
                            </tr>
                            <tr>
                                <td>产品名称:</td>
                                <td>
                                        <input type="text" id="txtProductName1" style="height: 27px; width: 180px"/></td>
                            </tr>
                            <tr>
                                <tr>
                                <td>生产批号:</td>
                                <td>
                                        <input type="text" id="txtBatchNum1" style="height: 27px; width: 180px"/></td>
                            </tr>
                            <tr>
                                <td>件&nbsp;&nbsp;数:</td>
                                <td>
                                    <input type="text" id="txtQty1" style="height: 27px; width: 180px"/></td>
                           </tr>
                            <tr>
                                <td> <a  id="btnSave1"class="easyui-linkbutton" data-options="iconCls:'icon-print'"  style="width:90px">打&nbsp;&nbsp;印</a></td>
                                <td><a  id="btnReset1"class="easyui-linkbutton" data-options="iconCls:'icon-clear'"  style="width:90px">重&nbsp;&nbsp;置</a></td>
                           </tr>
                    </table>
                </div>                   
                <div id="divMsg1" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg1">message</span>
                </div>
            </form>
               
        </div>

        <div class="easyui-panel" data-options="region:'east',split:true" style="height: 100%; width:45%;">
                <table id="grid">
                </table>
        </div>
      </div> 
    </div> 
    <div data-options="region:'center',split:true" style="width: 100%;height: 55%; text-align: center; vertical-align: middle; padding-top: 1%">
        <table id="gridcartonhistory">
            </table>
    </div>
</div>
     <script src="../../Scripts/WareHouseManage/CartonPrint.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () { 
                CartonPrint.init();
                CartonPrint.initGrid();
               // CartonPrint.initGridHistory();
                //JeffComm.DownloadFile("../MDElse/DownloadPrintTpl.ashx", "E:\\test.txt");
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
