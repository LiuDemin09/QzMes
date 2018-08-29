<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutWH.aspx.cs" Inherits="Pages_WareHouseManage_OutWH" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成品出库</title>

    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
     <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css"  /> 
     <link rel="stylesheet" href="../../CSS/SysManage.css"  />   

    <script type="text/javascript" src="../../Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap/bootstrap.js"></script>           
    
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

    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">


        <div id="northPanel" data-options="region:'north',split:true" style="height: 33%">

             <div id="divMsg" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg">message</span>
                </div>

            <table id="grid">
            </table>

        </div>
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 33%">


            <table id="grid1">
            </table>

        </div>
        <div id="southPanel" data-options="region:'south',split:true" style="height: 33%">


            <table id="grid2">
            </table>

        </div>



    </div>

    <script src="../../Scripts/WareHouseManage/OutWH.js" type="text/javascript"></script>

    <script>
        jQuery(document).ready(function () {


            OutWH.init();
            OutWH.initGrid();
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
