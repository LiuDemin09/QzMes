<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DevlopTaskSubmit.aspx.cs" Inherits="Pages_TechnologyManage_DevlopTaskSubmit" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>编程任务提交</title>       
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

    <%--上传图片 --%>
     <%--<script src="/jquery-1.4.1.js"  type="text/javascript"></script>  --%>
     <script src="../../Scripts/uploadify/jquery.uploadify.js"  type="text/ecmascript"></script>
     <script src="../../Scripts/uploadify/jquery.uploadify.min.js"  type="text/ecmascript"></script>
     <link href="../../Scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />

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
    <div id="restartDialog" class="easyui-dialog" title="任务提交" style="width: 70%; height: 80%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">

        <form id="resumeForm">

            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 100%">

                <tr>
                    <th class="panel-header">零件图号</th>
                    <td>
                         <select id="selPartDrawingNo" class="easyui-combobox" name="selPartDrawingNo" style="width:95%;">                           
                        </select>                        
                    </td>
                    <th class="panel-header">客户名称</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtCustName" id="txtCustName" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">产品名称</th>
                    <td>                        
                       <input type="text" data-options="required:true" name="txtProductName" id="txtProductName" style="height: 27px; width: 95%" />
                    </td>
                </tr>
                <tr>
                     <th class="panel-header">工序</th>
                    <td> 
                          <select id="selStations" class="easyui-combobox" name="selStations" style="width:95%;">                           
                        </select>
                    </td> 
                    <th class="panel-header">程序文件上传</th>
                    <td>
                          <input type="file" name="uploadify" id="uploadify" />
                    </td> 
                     <th class="panel-header">程序文件路径</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtuploadpath" id="txtuploadpath" style="height: 27px; width: 95%" />
                    </td>
                   
                </tr>
                <tr>
                     <th class="panel-header">工时</th>
                    <td> 
                         <input type="text" data-options="required:true" name="txtUnitTime" id="txtUnitTime" style="height: 27px; width: 95%" />
                    </td> 
                    <th class="panel-header">刀具文件上传</th>
                    <td>
                          <input type="file" name="knifeuploadify" id="knifeuploadify" />
                    </td> 
                     <th class="panel-header">刀具文件路径</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtknifeuploadpath" id="txtknifeuploadpath" style="height: 27px; width: 95%" />
                    </td>
                   
                </tr>
                <tr><th class="panel-header"><a  id="btnSaveStation" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存工序</a></th></tr>
               <%-- <tr>
                    <th class="panel-header">工序号</th>
                    <td>
                         <input type="text" data-options="required:true" name="txtStationNo" id="txtStationNo" style="height: 27px; width: 95%" />
                    </td>
                    <th class="panel-header">工序名称</th>
                    <td>
                         <input type="text" data-options="required:true" name="txtStation" id="txtStation" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">机床类型</th>
                    <td>                         
                         <input class="easyui-combobox" name="selMachinetype" id="selMachinetype" style="width:90%;" data-options="
					        url:'../../services/WsSystem.asmx/ListBindMachineType',
					        method:'get',
					        valueField:'id',
					        textField:'text',
					        multiple:true,
					        panelHeight:'auto'
					        "> 
                         <a  id="btnAdd" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >添加</a>
                    </td>                    
                </tr> --%>
               <%-- <tr>
                    <td>
                        <a  id="btnRemove" class="easyui-linkbutton" data-options="iconCls:'icon-remove'" >移除</a>
                    </td>
                     <td>
                        <a  id="btnUp" class="easyui-linkbutton"  >上移</a>
                    </td>
                     <td>
                        <a  id="btnDown" class="easyui-linkbutton" >下移</a>
                    </td>
                </tr> --%>
                             
            </table>
           
            <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:center" >
                <a  id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >提交任务</a>
                <a id="btnClear" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'">关闭</a>
            </div> 
             <table id="gridRoute">
            </table>
        </form>
    </div>
     <div id="allPanel" class="easyui-layout" style="width: 600px; height:500px;"fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; ">          
            <table id="grid">
            </table>
        </div>
    </div>
     <script src="../../Scripts/TechnologyManage/DevlopTaskSubmit.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                DevlopTaskSubmit.init();
                DevlopTaskSubmit.initGrid();
                DevlopTaskSubmit.initGridRoute();
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
