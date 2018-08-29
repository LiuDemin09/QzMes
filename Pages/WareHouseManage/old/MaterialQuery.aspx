<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialQuery.aspx.cs" Inherits="Pages_WareHouseManage_MaterialQuery" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>物料查询</title>    
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
             
            <div class="tabbable tabbable-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab_inmaterial_query" data-toggle="tab"><asp:Literal ID="Literal8"   runat="server" Text="入库查询"></asp:Literal></a></li>
                                <li><a href="#tab_outmaterial_query" data-toggle="tab"><asp:Literal ID="Literal22"   runat="server" Text="出库查询"></asp:Literal></a></li>
                                 <li><a href="#tab_stock_query" data-toggle="tab"><asp:Literal ID="Literal1"   runat="server" Text="库存查询"></asp:Literal></a></li>
                            </ul>
                <div class="tab-content">
                 <div class="tab-pane active" id="tab_inmaterial_query">
					<div class="col-md-12"> 
                         <div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">入库查询</span></div>								 
							</div>
                        <div class="portlet-body fuelux" style="padding-left:50px; padding-bottom:0;">
                            <form action="#" class="form-horizontal  form-row-seperated form-inline">
                                 <div class="col-md-10 col-xs-7 col-md-offset-1">
                                    <input type="text" id="txtIMSN" class="form-control " placeholder="来料条码">
                                     <input type="text" id="txtIMaterialName" class="form-control " placeholder="物料名称">
                                     <input type="text" id="txtIBatchNumber" class="form-control " placeholder="批号">
                                     <input type="text" id="txtICustomer" class="form-control " placeholder="购货单位">
                                     <input class="form-control form_datetime" type="text" readonly name="txtIStartTime" id="txtIStartTime"  placeholder="开始时间"/>
                                      <input class="form-control form_datetime" type="text" readonly name="txtIEndTime" id="txtIEndTime" placeholder="结束时间"/>
                                     <a id="btnISNSearch" class="btn blue"><i class="icon-search"></i>
                                                 <asp:Literal ID="Literal2"   runat="server" Text="查询"></asp:Literal></a> 
                                    <a id="btnISNExport" class="btn blue"><i class=" icon-save"></i>
                                                 <asp:Literal ID="Literal9"   runat="server" Text="导出"></asp:Literal></a> 
                                    

                                </div>
                               <%-- <div class="col-md-1 col-xs-4">
                                     <img src="../../images/product/close.png" style="width:30px;height:30px;" class="img-responsive" alt="关闭本页面" />
                                </div>--%>                                                                               
                            </form>
                        </div>                            
                    </div>                      
                        <div class="portlet box"> 
                             <div class="portlet box  bg-info">							 
                            <div class="portlet-body fuelux">
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                 <div id="recinfoIMaterialShowArea"></div>
                                 <div id="gdIMaterialList" style="width:100%; height:350px;overflow:auto;"></div>
                                 <div id="pagingIMaterialArea"></div> 
                             </div>
                            </div>
                            </div>
                        </div>
					</div>					
				</div> 
                 <div class="tab-pane" id="tab_outmaterial_query">
                     <div class="col-md-12"> 
                         <div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">出库查询</span></div>								 
							</div>
                        <div class="portlet-body fuelux" style="padding-left:50px; padding-bottom:0;">
                            <form action="#" class="form-horizontal form-row-seperated form-inline">
                                 <div class="col-md-10 col-xs-7 col-md-offset-1">
                                    <input type="text" id="txtOMSN" class="form-control " placeholder="来料条码">
                                     <input type="text" id="txtOMaterialName" class="form-control " placeholder="物料名称">
                                     <input type="text" id="txtOBatchNumber" class="form-control " placeholder="批号">
                                     <input type="text" id="txtOCustomer" class="form-control " placeholder="购货单位">
                                     <input class="form-control form_datetime" type="text" readonly name="txtOStartTime" id="txtOStartTime"  placeholder="开始时间"/>
                                      <input class="form-control form_datetime" type="text" readonly name="txtOEndTime" id="txtOEndTime" placeholder="结束时间"/>
                                     <a id="btnOSNSearch" class="btn blue"><i class="icon-search"></i>
                                                 <asp:Literal ID="Literal3"   runat="server" Text="查询"></asp:Literal></a> 
                                    <a id="btnOSNExport" class="btn blue"><i class=" icon-save"></i>
                                                 <asp:Literal ID="Literal4"   runat="server" Text="导出"></asp:Literal></a> 
                                </div>                                                                             
                            </form>
                        </div>                            
                    </div>                      
                        <div class="portlet box"> 
                             <div class="portlet box  bg-info">							 
                            <div class="portlet-body fuelux">
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                 <div id="recinfoOMaterialShowArea"></div>
                                 <div id="gdOMaterialList" style="width:100%; height:350px;overflow:auto;"></div>
                                 <div id="pagingOMaterialArea"></div> 
                             </div>
                            </div>
                            </div>
                        </div>
					</div>	
                 </div>
                 <div class="tab-pane" id="tab_stock_query">
                     <div class="col-md-12"> 
                         <div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">库存查询</span></div>								 
							</div>
                        <div class="portlet-body fuelux" style="padding-left:50px; padding-bottom:0;">
                            <form action="#" class="form-horizontal form-row-seperated form-inline">
                                 <div class="col-md-10 col-xs-7 col-md-offset-1">
                                     <input type="text" id="txtIOMaterialName" class="form-control " placeholder="物料名称">
                                     <input type="text" id="txtIOBatchNumber" class="form-control " placeholder="批号">
                                     <input type="text" id="txtIOCustomer" class="form-control " placeholder="购货单位">
                                     <input class="form-control form_datetime" type="text" readonly name="txtIOStartTime" id="txtIOStartTime"  placeholder="开始时间"/>
                                      <input class="form-control form_datetime" type="text" readonly name="txtIOEndTime" id="txtIOEndTime" placeholder="结束时间"/>
                                     <a id="btnIOSNSearch" class="btn blue"><i class="icon-search"></i>
                                                 <asp:Literal ID="Literal5"   runat="server" Text="查询"></asp:Literal></a> 
                                    <a id="btnIOSNExport" class="btn blue"><i class=" icon-save"></i>
                                                 <asp:Literal ID="Literal6"   runat="server" Text="导出"></asp:Literal></a> 
                                </div>                                                                             
                            </form>
                        </div>                            
                    </div>                      
                        <div class="portlet box"> 
                             <div class="portlet box  bg-info">							 
                            <div class="portlet-body fuelux">
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                 <div id="recinfoIOMaterialShowArea"></div>
                                 <div id="gdIOMaterialList" style="width:100%; height:350px;overflow:auto;"></div>
                                 <div id="pagingIOMaterialArea"></div> 
                             </div>
                            </div>
                            </div>
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
     <script src="../../Scripts/WareHouseManage/MaterialQuery.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {

                App.init(); // initlayout and core plugins
                MaterialQuery.init();
                MaterialQuery.initGrid();
                MaterialQuery.initOGrid();
                MaterialQuery.initIOGrid();
                //JeffComm.DownloadFile("../MDElse/DownloadPrintTpl.ashx", "E:\\test.txt");
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
