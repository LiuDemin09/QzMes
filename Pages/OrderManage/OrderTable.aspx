<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderTable.aspx.cs" Inherits="Pages_OrderManage_OrderTable" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>订单工作台</title>

    <!-- begin  javascript files -->
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/locale/easyui-lang-zh_CN.js"></script>
    <%--<link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
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
    <div id="restartDialog" class="easyui-dialog" title="新建订单" style="width: 60%; height: 60%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">

        <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc; text-align: left; padding-right: 30px">

            <a id="btnDownload" class="easyui-linkbutton">模板下载</a>
            <a id="btnImport" class="easyui-linkbutton">导入</a>
            <a id="btnNew" class="easyui-linkbutton" data-options="iconCls:'icon-add'">新建</a>
            <a id="btnNewNoNumber" class="easyui-linkbutton" data-options="iconCls:'icon-add'">新建（不带单号）</a>
        </div>
        <form id="resumeForm">

            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 100%">

                <tr>
                    <th class="panel-header">订单号码</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtOrderNoAdd" id="txtOrderNoAdd" style="height: 27px; width: 95%" /></td>
                    <th class="panel-header">合同号码</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtContractNo" id="txtContractNo" style="height: 27px; width: 95%" /></td>

                

                    <th class="panel-header">零件图号</th>
                    <td>
                        <select id="txtPartsDrawingAdd" class="easyui-combobox" name="dept" style="width:200px;">
                            <%--<option value="aa">aitem1</option>
                            <option>bitem2</option>
                            <option>bitem3</option>
                            <option>ditem4</option>
                            <option>eitem5</option>--%>
                        </select>
                        <%--<select tabindex="-1" name="txtPartsDrawingAdd" id="txtPartsDrawingAdd"  style="height: 27px; width: 95%">
                            <option value="0">请选择</option>
                        </select>--%>
                    </tr>
                <tr>    
                    <th class="panel-header">客户名称</th>
                    <td>

                        <select tabindex="-1" name="selCustName" id="selCustName"  style="height: 27px; width: 95%">
                            <option value="0">请选择</option>
                        </select>
                    </td>
                
                    <th class="panel-header">产品名称</th>
                    <td>
                        <%--<select tabindex="-1" name="selProductName" id="selProductName"  style="height: 27px; width: 95%">
                            <option value="0">请选择</option>
                        </select>--%>
                        <select id="selProductName" class="easyui-combobox" name="dept" style="width:200px;">                             
                        </select>
                    </td>

                    <th class="panel-header">批次号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtBatchNo" id="txtBatchNo" style="height: 27px; width: 95%" /></td>

                </tr>
                <tr>
                    <th class="panel-header">数量</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtNumber" id="txtNumber" style="height: 27px; width: 95%" /></td>
                    <th class="panel-header">交付时间</th>
                    <td>
                        <input type="text" id="txtPlanTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})" class="Wdate" style="height: 27px; width: 95%" />
                    </td>
                     <th class="panel-header">备注</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtMemo" id="txtMemo" style="height: 27px; width: 95%" /></td>
                </tr>
            </table>

            <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc; text-align: center">
                <a id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'">保存</a>
                <a id="btnClear" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'">关闭</a>
            </div>
        </form>

    </div>


    <div id="restartDialog2" class="easyui-dialog" title="新建图号" style="width: 60%; height: 60%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">

        <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc; text-align: left; padding-right: 30px">

            <a id="btnDownload2" class="easyui-linkbutton">模板下载</a>
            <a id="btnImport2" class="easyui-linkbutton">导入</a>
            <a id="btnNew2" class="easyui-linkbutton" data-options="iconCls:'icon-add'">新建</a>
        </div>
        <form id="resumeForm2">

            <table id="myResume2" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 100%">

                <tr>
                    <th class="panel-header">零件图号</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtPartCode2" id="txtPartCode2" style="height: 27px; width: 95%" /></td>
                    <th class="panel-header">客户名称</th>
                    <td>
                        <select tabindex="-1" name="selCustName2" id="selCustName2" style="height: 27px; width: 95%">
                            <option value="0">请选择</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <th class="panel-header">产品名称</th>
                    <td>
                        <select id="selProductName2" class="easyui-combobox" name="dept" style="width:200px;">                             
                        </select>
                       <%-- <select tabindex="-1" name="selProductName2" id="selProductName2" style="height: 27px; width: 95%">
                            <option value="0">请选择</option>
                        </select>--%>
                    </td>
                </tr>

            </table>

            <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc; text-align: center">
                <a id="btnSave2" class="easyui-linkbutton" data-options="iconCls:'icon-save'">保存</a>
                <a id="btnClear2" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                <a id="btnClose2" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'">关闭</a>
            </div>
        </form>

    </div>

    <div id="allPanel" class="easyui-layout" style="width: 600px; height:400px;overflow:hidden;"fit="true" >


        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 85%">


            <table id="grid">
            </table>

        </div>
        <div data-options="region:'east',title:'工具箱',split:true" style="width: 15%; text-align: center; vertical-align: middle; padding-top: 20%">
            <table style="text-align: center; vertical-align: middle; width: 100%">
                <tr>

                    <td style="height: 40px">
                        <a id="btnAddOrder" class="easyui-linkbutton" data-options="iconCls:'icon-add'">新建订单</a>
                </tr>
                <tr>

                    <td style="height: 40px">
                        <a id="btnAddParts" class="easyui-linkbutton" data-options="iconCls:'icon-add'">新建图号</a>
                </tr>
                <tr>

                    <td style="height: 40px">
                        <a href="DrawingQuery.aspx" id="btnSearchParts" class="easyui-linkbutton" data-options="iconCls:'icon-search'">查询图号</a>
                </tr>
            </table>


        </div>


    </div>

    <script src="../../Scripts/OrderManage/OrderTable.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function () {


            OrderTable.init();
            OrderTable.initGrid();
        });

    </script>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="SMG" runat="server">
            <Services>
                <asp:ServiceReference Path="../../services/WsOrder.asmx" />
                <asp:ServiceReference Path="../../services/WsSystem.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
</body>
</html>
