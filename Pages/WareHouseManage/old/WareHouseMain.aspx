<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WareHouseMain.aspx.cs" Inherits="Pages_WareHouseManage_WareHouseMain" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>仓库工作台</title>    
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
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN DHTMLX STYLES -->
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxtree.css" rel="stylesheet" type="text/css" media="screen"/>
    <link href="../../ClientMedia/dhtmlx/css/dhtmlxgrid_dhx_skyblue.css" rel="stylesheet" type="text/css" media="screen"/>    
     <!-- END DHTMLX STYLES -->
</head>
<body>
    <div class="container-fluid">
        <div class="row-fluid"  style="padding:0px;">
					<div class="col-md-10"> 
                        <div class="col-md-12">
                         <%--<div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">我的备料任务</span></div>
								<div class="actions">	
								</div>                              
							</div>                                                
                        </div>  --%>                    
                        <div class="portlet box"> 
                             <div class="portlet box  bg-info">
                                 <div class="portlet-title">
                                    <div class="caption"><span class="text-info">我的备料任务</span></div>
                                 </div> 							 
                            <div class="portlet-body fuelux">                               
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                 <div id="recStandbyArea"></div>
                                 <div id="gdStandbyList" style="width:100%; height:150px;overflow:auto;"></div>
                                 <div id="pagingStandbyArea"></div> 
                             </div>
                            </div>
                            </div>
                        </div>
					</div>
                    <div class="col-md-12"> 
                         <%--<div class="portlet box  bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">我的出库任务</span></div>
								<div class="actions">	
								</div>                              
							</div>                                               
                        </div> --%>                      
                        <div class="portlet box"> 
                            <div class="portlet box  bg-info">
                                <div class="portlet-title">
								    <div class="caption"><span class="text-info">我的出库任务</span></div>								 
							    </div>
                                <div class="portlet-body fuelux" >
                                    <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #808080;"> 
                                       <%-- <div class="form-group form-group-lg bg-info">
                                            <h3> 信息列表 </h3> 
                                         </div>--%>
                                         <div id="recinfoArea"></div>
                                         <div id="gdWaitOutList" style="width:100%; height:200px;overflow:auto;"></div>
                                         <div id="pagingArea"></div> 
                                     </div>
                                </div>
                        </div>
                       </div>
					</div>
                </div>
					<div class="col-md-2" style="border:1px solid #808080;padding:0px;">
                       <div class="portlet-title  bg-info">
								<div class="caption"> <div class=" text-center"><span class="text-info">工具箱</span> </div></div>
								<div class="actions">	
								</div>
                                
							</div>
                        <div class=" text-center"><span class="text-info"></span> </div>
                       
                          <%-- <input type="button" class="btn btn-default" disabled  value="工具箱"/>--%>
                        <form action="#" class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="InWareHouse.aspx" id="btnReceived" class="btn blue form-control">                                             
                                          <asp:Literal ID="Literal1"   runat="server" Text="收料"></asp:Literal></a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="OutWareHouse.aspx" id="btnSend" class="btn blue form-control">                                             
                                            <asp:Literal ID="Literal2"   runat="server" Text="发料"></asp:Literal></a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="ProductInWH.aspx" id="btnInStock" class="btn blue form-control">                                             
                                           <asp:Literal ID="Literal3"   runat="server" Text="入库"></asp:Literal></a> 
                               </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="ProductOutWH.aspx" id="btnOutStock" class="btn blue form-control">                                             
                                          <asp:Literal ID="Literal5"   runat="server" Text="出库"></asp:Literal></a> 
                                </div>
                            </div> 
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="ReturnWareHouse.aspx" id="btnReturn" class="btn blue form-control">                                             
                                          <asp:Literal ID="Literal6"   runat="server" Text="退料"></asp:Literal></a>  
                               </div>
                             </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="ReturnWareHouse.aspx" id="btnReSend" class="btn blue form-control">                                             
                                          <asp:Literal ID="Literal7"   runat="server" Text="补料"></asp:Literal></a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                   <a href="MaterialQuery.aspx" id="btnMaterialQuery" class="btn blue form-control">                                             
                                           <asp:Literal ID="Literal9"   runat="server" Text="物料库存"></asp:Literal></a>   
                               </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                    <a href="ProductQuery.aspx" id="btnProductQuery" class="btn blue form-control">                                             
                                          <asp:Literal ID="Literal10"   runat="server" Text="成品库存"></asp:Literal></a>   
                                </div>
                            </div> 
                             <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                      <input type="button" class="btn default" style="display:none" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                      <input type="button" class="btn default" style="display:none" />
                                </div>
                            </div> 
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                      <input type="button" class="btn default" style="display:none" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 col-xs-10 col-md-offset-2 col-xs-offset-1">
                                      <input type="button" class="btn default" style="display:none" />
                                </div>
                            </div>
                        </form>
                       </div>
					</div>
				</div>
       <%-- <div class="row-fluid"  style="padding:0px;">--%>
                
           <%-- </div>--%>
   <%-- </div>--%>

     <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../../ClientMedia/js/ui-tree.js" type="text/javascript"></script>     
	<script src="../../ClientMedia/js/jquery.dataTables.js" type="text/javascript" ></script>
	<script src="../../ClientMedia/js/DT_bootstrap.js" type="text/javascript" ></script>    
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
     <script src="../../Scripts/WareHouseManage/WareHouseMain.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {

                App.init(); // initlayout and core plugins
                WareHouseMain.init();
                WareHouseMain.initGrid();
                WareHouseMain.initGridWaitOut();
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
