<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnMar.aspx.cs" Inherits="Pages_OrderManage_ReturnMar" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>补料&退料</title>

    <!-- begin  javascript files -->
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/easyui/jquery-1.4.2.min.js"></script>
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



    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">


        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 80%">
            <div id="northPanel" data-options="region:'north',split:true" style="height: 50%">


                <table id="grid">
                </table>

            </div>
            <div id="southPanel" data-options="region:'south',split:true" style="height: 50%">


                <table id="grid1">
                </table>

            </div>

            <%--            <table id="grid">
            </table>--%>
        </div>
        <div data-options="region:'east',split:true" style="width: 20%; text-align: center; vertical-align: middle;">
                
                <div class="easyui-panel" title="补料" style="height: 50%;width:95%">
                    <div style="padding: 30% 10px 20px 10px">
                        <form id="ff" method="post">
                            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 50%">
                                <tr>
                                    <td>工单号:</td>
                                    <td>
                                       <select tabindex="-1" name="selWorkOrder" id="selWorkOrder" style="height: 27px; width: 95%"><option value="0">请选择</option> </select></td>
                                </tr>
                                <tr>
                                    <td>领料员:</td>
                                    <td>
                                         <input type="text" id="txtEmp" style="height: 27px; width: 95%"/></td>
                                </tr>
                                <tr>
                                    <td>条码:</td>
                                    <td>
                                       <input type="text" id="txtMSN" style="height: 27px; width: 95%"/></td>
                                </tr>
                              
                            </table>
                        </form>
                        <div style="text-align: center; padding: 5px">
                             <a  id="btnReSend"class="easyui-linkbutton" data-options="iconCls:'icon-add'"  style="width:90px">补&nbsp;&nbsp;料</a>
                        </div>
                    </div>
                </div>

         
        
                 <div class="easyui-panel" title="退料" style="height: 50%;width:95%" >
                    <div style="padding: 30% 10px 20px 10px">
                        <form id="ff1" method="post">
                            <table id="myResume1" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%;height:50%">
                               
                                <tr>
                                    <td>条码:</td>
                                    <td>
                                       <input type="text" id="txtReMSN" style="height: 27px; width: 95%"/></td>
                                </tr>
                              
                            </table>
                        </form>
                        <div style="text-align: center; padding: 5px">
                             <a  id="btnReturn"class="easyui-linkbutton" data-options="iconCls:'icon-redo'"  style="width:90px">退&nbsp;&nbsp;料</a>
                        </div>
                    </div>
                </div>
           
        </div>

    </div>

    <script src="../../Scripts/WareHouseManage/ReturnMar.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function () {


            ReturnMar.init();
            ReturnMar.initGrid();
        });

    </script>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="SMG" runat="server">
            <Services>
                <asp:ServiceReference Path="../../services/WsWareHouse.asmx" />
                <asp:ServiceReference Path="../../services/WsSystem.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
</body>
</html>
