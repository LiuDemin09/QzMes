<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QualityMain.aspx.cs" Inherits="Pages_QualityManage_QualityMain" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>质量主管工作台</title>    
   <!-- begin  javascript files -->     
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/easyui/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/easyui/jquery.easyui1.min.js"></script>
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

        #myResume input {
            width: 180px;
        }

        .resumeTable {
            width: 98%;
        }

         /*-- 消除grid屏闪问题 --//*/
        .datagrid-mask{
          opacity:0;
          filter:alpha(opacity=0);
        }
        .datagrid-mask-msg{
          opacity:0;
          filter:alpha(opacity=0);
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
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 85%">
             <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:true" style="height:50%">
                    <table id="grid">
                    </table>
                </div>
                <div data-options="region:'center'">
                    <table id="gridyield">
                    </table>
                </div>
            </div>
        </div>
        <div data-options="region:'east',title:'工具箱',split:true" style="width: 15%; text-align: center; vertical-align: middle; padding-top: 5%">
            <table style=" text-align: center; vertical-align: middle;width:100%">
              <tr >                  
                    <td  style="height:40px">
                         <a href="../WorkOrderManage/WorkOrderQuery3.aspx" id="btnReceived" data-options="iconCls:'icon-search'"  class="easyui-linkbutton">&nbsp;工单查询&nbsp;</a>
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                        <a href="QualityQuery.aspx" id="btnSend" data-options="iconCls:'icon-search'"  class="easyui-linkbutton">&nbsp;质量查询&nbsp;</a>
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                        <a href="YieldQuery.aspx" id="btnInStock" data-options="iconCls:'icon-search'"  class="easyui-linkbutton">&nbsp;良率查询&nbsp;</a> 
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                       <a href="UnsurenessQuery.aspx" id="btnOutStock" data-options="iconCls:'icon-search'"  class="easyui-linkbutton">&nbsp;待处理品&nbsp;</a>
                   </td>         
              </tr> 
                 <tr >                  
                    <td  style="height:40px">
                       <a href="FailItemsQuery2.aspx" id="btnReturn" data-options="iconCls:'icon-search'"  class="easyui-linkbutton"> &nbsp;不良项目&nbsp;</a>
                   </td>         
              </tr>             
           </table>
        </div>
    </div>
     <script src="../../Scripts/QualityManage/QualityMain.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                QualityMain.init();
                QualityMain.initGrid();
                QualityMain.initGridYield();
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
