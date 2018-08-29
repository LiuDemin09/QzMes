﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PassRate.aspx.cs" Inherits="Pages_QualityManage_PassRate" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>综合良率查询</title>
    <!-- begin  javascript files -->
    <%-- <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/easyui/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/easyui/jquery.easyui1.min.js"></script>--%>


    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Scripts/eKanBan/highcharts.js"></script>
    <script type="text/javascript" src="../../Scripts/eKanBan/data.js"></script>
    <script type="text/javascript" src="../../Scripts/eKanBan/drilldown.js"></script>
    <script type="text/javascript" src="../../Scripts/eKanBan/exporting.js"></script>
    <script type="text/javascript" src="../../Scripts/Highcharts-5.0.12/html2canvas.js"></script>
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


    <div id="container" style="min-width: 310px; max-width: 600px; height: 400px; margin: 0 auto"></div>


    <%--<div id="allPanel" style="width: 100%;" fit="true">
        <table id="grid">
        </table> 
    </div>--%>
     <div id="allPanel" class="easyui-layout" style="width: 100%; " fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="width: 100%;">          
            <table id="grid">
            </table>
        </div>
    </div>

    <script src="../../Scripts/QualityManage/PassRate.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {
            PassRate.init();
            PassRate.initGrid();
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