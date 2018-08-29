<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateWorkOrder.aspx.cs" Inherits="Pages_WorkOrderManage_CreateWorkOrder" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>创建工单</title>    
    <!-- begin  javascript files -->
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/locale/easyui-lang-zh_CN.js"></script>
  <%-- <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/easyui/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/easyui/jquery.easyui1.min.js"></script>--%>
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
   <div id="restartDialog1" class="easyui-dialog" title="任务列表" style="width: 60%; height: 40%"
       data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">
       <table id="grid1">
            </table>
       </div>

   <div id="restartDialog" class="easyui-dialog" title="新建工单" style="width: 80%; height: 80%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">

          <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:left;padding-right:30px" >
              
                <a id="btnDownload" class="easyui-linkbutton" >模板下载</a>
                <a  id="btnImport" class="easyui-linkbutton" >导入</a>
               <a  id="btnNew" class="easyui-linkbutton" data-options="iconCls:'icon-add'" >新建</a> 
            </div>
        <form id="resumeForm">

            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 100%">

                <tr>
                    <th class="panel-header">工单单号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtWorkOrderNo" id="txtWorkOrderNo" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">零件图号</th>
                    <td>
                        <select tabindex="-1" data-options="required:true" name="selPartsDrawing" id="selPartsDrawing"  style="height: 27px; width: 75%">                            
                        </select>
                         <a  id="btnQuery" class="easyui-linkbutton" >查看列表</a>
                    </td>
                    <th class="panel-header">订单单号</th>
                    <td>
                         <select tabindex="-1" data-options="required:true" name="selOrderNo" id="selOrderNo"  style="height: 27px; width: 95%">                            
                        </select>
                    </td> 
                    <th class="panel-header">产品名称</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtProductName" id="txtProductName" style="height: 27px; width: 95%" />
                    </td>                  
                </tr>
                <tr>     
                    <th class="panel-header">工序</th>
                    <td>
                        <input class="easyui-combobox" name="selStationName" id="selStationName" style="width:90%;">
                    </td>
                     <th class="panel-header">工序号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtStationId" id="txtStationId" style="height: 27px; width: 95%" />
                    </td>               
                    <th class="panel-header">机床类型</th>
                    <td>
                        <input class="easyui-combobox" name="selEquitmentType" id="selEquitmentType" style="width:90%;" data-options="
					        url:'../../services/WsSystem.asmx/ListBindMachineType',
					        method:'get',
					        valueField:'id',
					        textField:'text',
					        multiple:true,
					        panelHeight:'auto'
					        ">
                    </td>
                    <th class="panel-header">机床名称</th>
                    <td>
                        <input class="easyui-combobox" name="selEquipment" id="selEquipment" style="width:90%;" data-options="
					        multiple:true,
					        panelHeight:'auto'
					        ">
                        <%--<input class="easyui-combobox" name="selEquipment" id="selEquipment" style="width:90%;" data-options="
					        url:'../../services/WsSystem.asmx/ListBindMachineInfo',
					        method:'get',
					        valueField:'id',
					        textField:'text',
					        multiple:true,
					        panelHeight:'auto'
					        ">--%>
                    </td>
                 </tr>
                <tr>
                     <th class="panel-header">计划开始时间</th>
                    <td>
                        <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">生产班次</th>
                    <td>
                        <input class="easyui-combobox" name="selBanci" id="selBanci" style="width:90%;" data-options="
					        url:'../../services/WsSystem.asmx/ListBindBanCi',
					        method:'get',
					        valueField:'id',
					        textField:'text',
					        multiple:true,
					        panelHeight:'auto'
					        ">
                    </td>
               
                    <th class="panel-header">生产工时</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtUnitTime" id="txtUnitTime" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">订单数量</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtOrderQty" id="txtOrderQty" style="height: 27px; width: 95%" />
                    </td>
                 </tr>    
                 <tr>
                    
                    <th class="panel-header">计划生产数量</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtPlanQuantity" id="txtPlanQuantity" style="height: 27px; width: 95%" />
                    </td> 
                   
                    <th class="panel-header">计划结束时间</th>
                    <td>
                        <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate" style="height: 27px; width: 95%" />
                    </td>                    
               
                    <th class="panel-header">计划检验时间</th>
                    <td>
                        <input type="text" id="txtPlanCheckTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate" style="height: 27px; width: 95%" />
                    </td> 
                    <th class="panel-header">计划入库时间</th>
                    <td>
                        <input type="text" id="txtPlanInTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate" style="height: 27px; width: 95%" />
                    </td>
                 </tr>
                <tr>    
                      <th class="panel-header">炉/批号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtBatchNumber" id="txtBatchNumber" style="height: 27px; width: 95%" />
                    </td>   
                     <th class="panel-header">基本数</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtBaseQty" id="txtBaseQty" style="height: 27px; width: 95%" />
                    </td>                
                
                     <th class="panel-header">交付时间</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtAskTime" id="txtAskTime" style="height: 27px; width: 95%" />
                    </td>
                
                   <th class="panel-header">质量编号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtQualityCode" id="txtQualityCode" style="height: 27px; width: 95%" />
                    </td>
                 </tr>   
                 <tr>
                    <th class="panel-header">物料库存</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtMaterialQty" id="txtMaterialQty" style="height: 27px; width: 95%;"/>
                    </td>
                      <th class="panel-header">工艺状态</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtTechnologyFile" id="txtTechnologyFile" style="height: 27px; width: 95%" />
                    </td>
                 
                      <th class="panel-header">查看工艺</th>
                    <td>
                        <a  href="#" id="btnTechnologyQuery"class="easyui-linkbutton" data-options="iconCls:'icon-search'"  style="width:90px">工艺查询</a>
                    </td>
                    <td>
                        <input type="text" data-options="required:true" name="txtCustName" id="txtCustName" style="height: 27px; width: 95%;display:none" />
                    </td>
                 </tr>   
                 <tr>
                      <td>
                        <input type="text" data-options="required:true" name="txtProductCode" id="txtProductCode" style="height: 27px; width: 95%;display:none" />
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
     <div id="allPanel" class="easyui-layout" style="width: 600px; height:500px;"fit="true">
          <div id="tb" data-options="region:'north',split:true,hideCollapsedContent:false" style="padding:1px 1px;height: 15%;">
             <table>
                 <tr>
                     <th class="panel-header">订单单号</th>
                     <td>
                         <input type="text" data-options="required:true" name="txtFOrderNo" id="txtFOrderNo" style="height: 25px; " /> 
                     </td>
                     <th class="panel-header">工单单号</th>
                     <td>
                         <input type="text" data-options="required:true" name="txtWorkOrder" id="txtWorkOrder" style="height: 25px; " /> 
                     </td>
                     <th class="panel-header">零件图号</th>
                     <td>
                         <input type="text" data-options="required:true" name="txtPartsDrawingNo2" id="txtPartsDrawingNo2" style="height: 25px;  " /> 
                     </td>
                     
                     <th class="panel-header">工单状态</th>
                     <td>
                          <select tabindex="-1" name="selWOStatus2" style="height: 25px;  "  id="selWOStatus2"><option value="4">工单状态</option><option value="0">创建</option> <option value="1">运行</option><option value="2">暂停</option><option value="3">关闭</option></select>
                     </td>
                     </tr>
                 <tr>
                     <th class="panel-header">客户名称</th>
                     <td>
                         <select tabindex="-1" name="selCustName2" id="selCustName2"><option value="0">请选择</option> </select>
                     </td>
                     <th class="panel-header">负责人 </th>
                     <td>
                          <input type="text" data-options="required:true" name="txtWorker2" id="txtWorker2" style="height: 25px;  " />
                     </td>
                 </tr>
             </table> 
	    </div>
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 85%; ">          
            <table id="grid">
            </table>
        </div>
    </div>
     <script src="../../Scripts/WorkOrderManage/CreateWorkOrder.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () { 
                CreateWorkOrder.init();
                CreateWorkOrder.initGrid(); 
            });

	</script>
    <form id="Form1" runat="server">
     <asp:ScriptManager ID="SMG" runat="server">
         <Services>
              <asp:ServiceReference Path="../../services/WsSystem.asmx" />
             <asp:ServiceReference Path="../../services/WsWareHouse.asmx" />
         </Services>
     </asp:ScriptManager>
 </form>  
</body>
</html>
