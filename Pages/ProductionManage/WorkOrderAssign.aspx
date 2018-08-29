<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkOrderAssign.aspx.cs" Inherits="Pages_ProductionManage_WorkOrderAssign" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>工单指派</title>    
    <!-- begin  javascript files -->
     <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css"  /> 
     <link rel="stylesheet" href="../../CSS/SysManage.css"  />   
    <script type="text/javascript" src="../../Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap/bootstrap.js"></script>           
	<link href="../../ClientMedia/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
	<link href="../../ClientMedia/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<link href="../../ClientMedia/css/style-metro.css" rel="stylesheet" type="text/css" /> 
    <link href="../../ClientMedia/css/style.css" rel="stylesheet" type="text/css" /> 
	<link href="../../ClientMedia/css/style-responsive.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
     <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/datagrid-detailview.js"></script> 
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
    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 80%">
            <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:true" style="height:50%">
            <table id="grid">
            </table>
             </div>
                <div data-options="region:'center'">
            <table id="gridhistory">
            </table>
                     </div>
            </div>
        </div>
        <div data-options="region:'east',title:'作业区',split:true" style="width: 20%; text-align: center; vertical-align: middle; padding-top: 15%">
             <div class="easyui-panel" title="工单指派"  >
                 <div style="padding: 3% 10px ">
            <table style=" text-align: center;width:100%">
              <tr >                  
                    <th class="panel-header">工单号</th>
                    <td>                        
                        <%--<select tabindex="-1" data-options="required:true" name="selWorkOrder" id="selWorkOrder"  style="height: 27px; width: 95%">                            
                        </select>--%>
                        <input type="text" data-options="required:true" name="txtWorkOrder" id="txtWorkOrder" readonly="readonly" style="height: 27px; width: 95%" /></td>

                    </td>      
              </tr> 
               <tr >                  
                    <th class="panel-header">工序</th>
                    <td>                        
                        <select tabindex="-1" data-options="required:true" name="selStation" id="selStation"  style="height: 27px; width: 95%">                            
                        </select> 
                    </td>      
              </tr> 
                <tr >                  
                    <th class="panel-header">操作人</th>
                    <td>  
                       <%-- <select class="easyui-combobox" name="selOperater" data-options="required:true,multiple:true,multiline:true" id="selOperater"  label="Select States:" labelPosition="top" style="width:95%;height:67px;">
                                             
                        </select>--%>
                       <input class="easyui-combobox" name="language[]" id="selOperater" style="width:95%;" data-options="
					        url:'../../services/WsSystem.asmx/ListBindUserByOperators',
					        method:'get',
					        valueField:'id',
					        textField:'text',
					        multiple:true,
					        panelHeight:'auto'
					        ">
                    </td>       
              </tr>          
           </table>
                      </div>
                 </div>
            <div style="background: #fafafa; padding: 5px; width: 95%; border: 1px solid #ccc;text-align:center" >
                 <a id="btnDownload" class="easyui-linkbutton" >模板下载</a>
                <a  id="btnImport" class="easyui-linkbutton" >导入</a>
                <a  id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存</a>                
            </div>
             <div id="divMsg" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg">message</span>
                </div>
        </div>
    </div>
     <script src="../../Scripts/ProductionManage/WorkOrderAssign.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () { 
                WorkOrderAssign.init();
                WorkOrderAssign.initGrid();
                WorkOrderAssign.initGridHistory();
                WorkOrderAssign.bindGrid();
                WorkOrderAssign.bindGridHistory();
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
