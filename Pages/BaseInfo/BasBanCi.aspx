<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasBanCi.aspx.cs" Inherits="Pages_BaseInfo_BasBanCi" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>班次维护</title>    
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
    <div id="restartDialog" class="easyui-dialog" title="新建班次" style="width: 60%; height: 60%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">

          <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:left;padding-right:30px" > 
               <a  id="btnNew" class="easyui-linkbutton" data-options="iconCls:'icon-add'" >新建</a>
            </div>
        <form id="resumeForm">

            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 100%">

                <tr>
                    <th class="panel-header">班次名称</th>
                    <td>
                        <%--<input type="text" data-options="required:true" name="txtBanCiName" id="txtBanCiName" style="height: 27px; width: 95%" />--%>
                        <select tabindex="-1" data-options="required:true" name="selBanCiName" id="selBanCiName"  style="height: 27px; width: 95%">
                            <option value="">请选择</option>
                            <option value="早班">早班</option>
                            <option value="中班">中班</option>
                            <option value="晚班">晚班</option>
                            <option value="正常班">正常班</option>                            
                        </select>
                    </td>
                    <th class="panel-header">开始时间</th>
                    <td>
                        <input id="txtStartTime" class="easyui-timespinner" style="width:80px;" required="required" data-options="showSeconds:false">
                        <%--<input type="text" data-options="required:true" name="txtStartTime" id="txtStartTime" style="height: 27px; width: 95%" />--%>
                    </td>
                </tr>
                <tr>
                    <th class="panel-header">结束时间</th>
                    <td>
                         <input id="txtEndTime" class="easyui-timespinner" style="width:80px;" required="required" data-options="showSeconds:false">
                        <%--<input type="text" data-options="required:true" name="txtEndTime" id="txtEndTime" style="height: 27px; width: 95%" />--%>
                    </td>
                    <th class="panel-header">休息开始时间</th>
                    <td>
                        <input id="txtRestStartTime" class="easyui-timespinner" style="width:80px;" required="required" data-options="showSeconds:false">
                        <%--<input type="text" data-options="required:true" name="txtStartTime" id="txtStartTime" style="height: 27px; width: 95%" />--%>
                    </td>
                     <th class="panel-header">休息结束时间</th>
                    <td>
                        <input id="txtRestEndTime" class="easyui-timespinner" style="width:80px;" required="required" data-options="showSeconds:false">
                        <%--<input type="text" data-options="required:true" name="txtStartTime" id="txtStartTime" style="height: 27px; width: 95%" />--%>
                    </td>
                </tr>
                              
            </table>

            <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:center" >
                <a  id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存</a>
                <a id="btnClear" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'">关闭</a>
            </div>
        </form>

    </div>

    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 85%">
            <table id="grid">
            </table>
        </div>
    </div>
     <script src="../../Scripts/BaseInfo/BasBanCi.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () { 
                BasBanCi.init();
                BasBanCi.initGrid();
                
                //JeffComm.DownloadFile("../MDElse/DownloadPrintTpl.ashx", "E:\\test.txt");
            });
	</script>
    <form id="Form1" runat="server">
     <asp:ScriptManager ID="SMG" runat="server">
         <Services>
              <asp:ServiceReference Path="../../services/WsBasic.asmx" />
              <asp:ServiceReference Path="../../services/WsSystem.asmx" />
         </Services>
     </asp:ScriptManager>
 </form>  
</body>
</html>
