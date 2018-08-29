<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductQuery.aspx.cs" Inherits="Pages_WareHouseManage_ProductQuery" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>成品查询</title>    
    <!-- begin  javascript files --> 
     <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css"  /> 
     <link rel="stylesheet" href="../../CSS/SysManage.css"  />   
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
    <link  href="../../ClientMedia/css/select2_metro.css" rel="stylesheet" type="text/css"/>
    <link  href="../../ClientMedia/css/jquery.tagsinput.css" rel="stylesheet" type="text/css"/>
    <link  href="../../ClientMedia/css/clockface.css" rel="stylesheet" type="text/css"/>
    <link  href="../../ClientMedia/css/bootstrap-wysihtml5.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/bootstrap-toggle-buttons.css" rel="stylesheet" type="text/css" />
    <link href="../../ClientMedia/css/multi-select-metro.css" rel="stylesheet" type="text/css" />    
     <link href="../../ClientMedia/css/profile.css" rel="stylesheet" type="text/css">  
    <link rel="stylesheet" type="text/css" href="../../ClientMedia/css/datetimepicker.css"/>   
	<!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES --> 
	 <link href="../../ClientMedia/css/jquery.gritter.css" rel="stylesheet" type="text/css"/>
    <link href="../../ClientMedia/css/jquery.dataTables.css" rel="stylesheet" type="text/css"/> 
	 <link href="../../ClientMedia/css/fullcalendar.css" rel="stylesheet" type="text/css"/>
	<link href="../../ClientMedia/css/jqvmap.css" rel="stylesheet" type="text/css" media="screen"/>
	<link href="../../ClientMedia/css/jquery.easy-pie-chart.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="../../ClientMedia/css/DT_bootstrap.css" rel="stylesheet" type="text/css">
 	<link href="../../ClientMedia/css/jquery-ui-1.10.1.custom.min.css" rel="stylesheet" type="text/css" />
	<link href="../../ClientMedia/css/bootstrap-modal.css" rel="stylesheet" type="text/css"/> 
    <link rel="stylesheet" href="../../CSS/style.css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN DHTMLX STYLES -->
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxtree.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxgrid_dhx_skyblue.css" rel="stylesheet" type="text/css" media="screen"/>    
     <!-- END DHTMLX STYLES -->
</head>
<body>
    <div class="container-fluid"><a href="WareHouseMain.aspx" id="sun"  > 返回工作台</a>
        <div class="row-fluid"  style="padding:0px;">
					<div class="col-md-12"> 
                         <div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">成品查询</span></div>								 
							</div>
                        <div class="portlet-body fuelux" style="padding-left:0px; padding-bottom:0;">
                            <form action="#" class="form-horizontal form-row-seperated form-inline">
                                 <div class="col-md-12 col-xs-12 ">
                                     <input type="text" id="txtWorkOrder" class="form-control " placeholder="工单号码">
                                      <input type="text" id="txtProductName" class="form-control " placeholder="产品名称">
                                     <input type="text" id="txtBatchNumber" class="form-control " placeholder="批号">                                     
                                    <%-- <div class="input-group-addon">成品状态:</div>--%>
                                                    <select tabindex="-1" name="selProductStatus" id="selProductStatus" class="form-control ">
                                                       <option value="3">成品状态</option>
                                                         <option value="0">入库</option>
                                                         <option value="1">出库</option>
                                                         <option value="2">库存</option>
                                                    </select>
                                      <input class="form-control form_datetime" type="text" readonly name="txtStartTime" id="txtStartTime"  placeholder="开始时间"/>
                                      <input class="form-control form_datetime" type="text" readonly name="txtEndTime" id="txtEndTime" placeholder="结束时间"/>
                                     <a id="btnSearch" class="btn blue"><i class="icon-search"></i>
                                                 <asp:Literal ID="Literal2"   runat="server" Text="查询"></asp:Literal></a> 
                                <a id="btnExport" class="btn blue"><%--<i class=" icon-save"></i>--%>
                                                 <asp:Literal ID="Literal9"   runat="server" Text="导出"></asp:Literal></a> 
                                </div>
                                <%--<div class="col-md-1 col-xs-4">
                                     <img src="../../images/product/close.png" style="width:30px;height:30px;" class="img-responsive" alt="关闭本页面" />
                                </div>   --%>                                                                            
                            </form>
                        </div>                            
                    </div>                      
                        <div class="portlet box"> 
                             <div class="portlet box  bg-info">							 
                            <div class="portlet-body fuelux">
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                 <div id="recinfoProductQueryArea"></div>
                                 <div id="gdProductQueryList" style="width:100%; height:380px;overflow:auto;"></div>
                                 <div id="pagingProductQueryArea"></div> 
                             </div>
                            </div>
                            </div>
                        </div>
					</div>					
				</div>       
    </div>

     <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../../ClientMedia/js/ui-tree.js" type="text/javascript"></script>     
	<script src="../../ClientMedia/js/jquery.dataTables.js" type="text/javascript" ></script>
	<script src="../../ClientMedia/js/DT_bootstrap.js" type="text/javascript" ></script> 
     <script src="../../ClientMedia/js/bootstrap-datetimepicker.js" type="text/javascript" ></script>    
    <script  src="../../ClientMedia/dhtmlx/js/dhtmlxcommon.js" type="text/javascript"></script>
    <script  src="../../ClientMedia/dhtmlx/js/dhtmlxtree.js" type="text/javascript"></script>    
    <script  src="../../ClientMedia/dhtmlx/js/dhtmlxtree_kn.js" type="text/javascript"></script>  
    <script  src="../../ClientMedia/dhtmlx/js/dhtmlxgrid.js" type="text/javascript"></script>  
    <script  src="../../ClientMedia/dhtmlx/js/dhtmlxgridcell.js" type="text/javascript"></script>  
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid_pgn.js" type="text/javascript"></script>
     <script src="../../Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
     <script src="../../Scripts/StringRes.js" type="text/javascript"></script>
     <script src="../../Scripts/Common.js" type="text/javascript"></script> 
   <script src="../../ClientMedia/js/app.js" type="text/javascript"></script>
     <script src="../../Scripts/WareHouseManage/ProductQuery.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                App.init(); // initlayout and core plugins
                ProductQuery.init();
                ProductQuery.initGrid();
                //JeffComm.DownloadFile("../MDElse/DownloadPrintTpl.ashx", "E:\\test.txt");
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
