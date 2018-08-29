<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WOQuery.aspx.cs" Inherits="Pages_OrderManage_WOQuery" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工单查询</title>
    <!-- begin  javascript files -->
    <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../CSS/SysManage.css" />
    <script type="text/javascript" src="../../Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap/bootstrap.js"></script>
    <!-- end  javascript files -->
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="../../ClientMedia/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/style-metro.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/style.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/style-responsive.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../ClientMedia/css/default.css" rel="stylesheet" type="text/css" id="style_color" />--%>
    <link href="../../ClientMedia/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/chosen.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/select2_metro.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/jquery.tagsinput.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/clockface.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/bootstrap-wysihtml5.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/bootstrap-toggle-buttons.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/multi-select-metro.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/profile.css" rel="stylesheet" type="text/css">
    <link rel="stylesheet" type="text/css" href="../../ClientMedia/css/datetimepicker.css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../../ClientMedia/css/jquery.gritter.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/fullcalendar.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/jqvmap.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../ClientMedia/css/jquery.easy-pie-chart.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../ClientMedia/css/DT_bootstrap.css" rel="stylesheet" type="text/css">
    <link href="../../ClientMedia/css/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/bootstrap-modal.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../CSS/style.css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN DHTMLX STYLES -->
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxtree.css" rel="stylesheet" type="text/css" media="screen" />
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxgrid_dhx_skyblue.css" rel="stylesheet" type="text/css" media="screen" />
    <!-- END DHTMLX STYLES -->
</head>
<body>
    <div class="container-fluid"><a href="OrderPublishMain.aspx" id="sun"  > 返回工作台</a>
        <div class="row-fluid" style="padding-top: 10px;">
           <%-- <a href="OrderPublishMain.aspx">
                <asp:Literal ID="Literal1" runat="server" Text="<<返回主菜单"></asp:Literal></a>--%>
            <div class="col-md-12 col-xs-12">
                <div class="portlet ">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h3 class="panel-title">查询条件</h3>
                        </div>
                        <div class="panel-body">

                            <form class="form-horizontal form-row-seperated form-inline" role="form">
                                <fieldset>
                                    <div class="form-group">
                                        <%--<div class="col-md-2">
                                            <div class="input-group">
                                                <div class="input-group-addon">工单单号:</div>--%>

                                                <input class="form-control" type="text" name="txtOrderNo" id="txtOrderNo" placeholder="工单单号"/>
                                           <%-- </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="input-group">
                                                <div class="input-group-addon">零件图号:</div>--%>
                                                <input class="form-control" type="text" name="txtPartsdrawing" id="txtPartsdrawing" placeholder="零件图号"/>
                                         <%--   </div>
                                        </div>
                                        <div class="col-md-2">--%>
                                            <div class="input-group">
                                                <div class="input-group-addon">开始时间:</div>
                                                <input class="form-control form_datetime" type="text" readonly name="txtStartTime" id="txtStartTime" />
                                            </div>
                                        <%--</div>
                                        <div class="col-md-2">--%>
                                            <div class="input-group">
                                                <div class="input-group-addon">结束时间:</div>
                                                <input class="form-control form_datetime" type="text" readonly name="txtEndTime" id="txtEndTime" />
                                            </div>
                                       <%-- </div>
                                        <div class="col-md-2">--%>
                                            <%--<div class="form-actions">
                                                <button id="btnSearch" class="btn form-control">
                                                    <asp:Literal ID="Literal25" runat="server" Text="查询"></asp:Literal>
                                                </button>--%>
                                                <a id="btnSearch" class="btn blue"><i class="icon-search"></i>
                                                     <asp:Literal ID="Literal1"   runat="server" Text="查询"></asp:Literal></a>
                                         <%--     </div>
                                      </div>--%>

                                    </div>

                                </fieldset>
                            </form>

                        </div>
                    </div>

                </div>
                <div id="divMsg" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg"></span>
                </div>
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">工单信息</h3>
                    </div>
                    <div class="panel-body" style="border: 1px solid #808080;">
                        <div id="recinfoArea"></div>
                        <div id="gdOrderList" style="width: 100%; height: 300px; overflow: auto; "></div>
                        <div id="pagingArea"></div>
                    </div>
                </div>



            </div>

        </div>
    </div>

    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../../ClientMedia/js/ui-tree.js" type="text/javascript"></script>
    <script src="../../ClientMedia/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../../ClientMedia/js/DT_bootstrap.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree_kn.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgridcell.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid_pgn.js" type="text/javascript"></script>
    <script src="../../Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../ClientMedia/js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/StringRes.js" type="text/javascript"></script>
    <script src="../../Scripts/Common.js" type="text/javascript"></script>
    <script src="../../ClientMedia/js/app.js" type="text/javascript"></script>
    <script src="../../Scripts/OrderManage/WOQuery.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    <script>
        jQuery(document).ready(function () {

            App.init(); // initlayout and core plugins
            WOQuery.init();
            WOQuery.initGrid();
            //SysMenu.initTable();

            //JeffComm.DownloadFile("../MDElse/DownloadPrintTpl.ashx", "E:\\test.txt");
        });

    </script>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="SMG" runat="server">
            <Services>
                <asp:ServiceReference Path="../../services/WsOrder.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
</body>
</html>
