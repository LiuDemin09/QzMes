<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnWareHouse.aspx.cs" Inherits="Pages_WareHouseManage_OutWareHouse" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>退料补料</title>    
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
					<div class="col-md-9" style="border:1px solid #e6e0e0;padding:0px;">                                               
                        <div class="portlet box"> 
                             <div class="portlet box bg-info">
							<div class="portlet-title">
								<div class="caption"><span class="text-info">备料信息</span></div>								 
							</div>
                            <div class="portlet-body fuelux">
                            <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #e6e0e0;"> 
                                 <div id="recinfobackupArea"></div>
                                 <div id="gdbackupList" style="width:100%; height:200px;overflow:auto;"></div>
                                 <div id="pagingbackupArea"></div> 
                             </div>
                            </div>
                            </div>
                        </div>
					</div>
					<div class="col-md-3" style="border:1px solid #e6e0e0;padding:0px;">                        
                            <div class="portlet box  bg-info">
							    <div class="portlet-title">
								    <div class="caption"><span class="text-info">补料</span></div>								    
							    </div>
                            <div class="portlet-body fuelux" style="padding-left:0px">
                                <form action="#" class="form-horizontal ">
                                    <div class="col-md-10 col-md-offset-1">
                                     <div class="form-group">
                                                     <%--<label class="col-xs-4 text-right"  for="selWorkOrder">工单号：</label>
                                                 <div class="col-xs-8">
                                                     <div class="controls">
                                                            <select tabindex="-1" name="selWorkOrder" id="selWorkOrder" class="form-group ">
                                                               <option value="0">请选择</option>
                                                            </select>
                                                        </div>
                                                </div>--%>
                                          <div class="input-group">
                                                 <div class="input-group-addon">工单号:</div>
                                                    <select tabindex="-1" name="selWorkOrder" id="selWorkOrder" class="form-control ">
                                                       <option value="0">请选择</option>
                                                    </select>
                                            </div>
                                      </div>
                                     <div class="form-group">                                                      
                                                     <%--<label class="col-xs-4 text-right"  for="txtGetBy">领料员：</label>
                                                     <div class="col-xs-8">
                                                        <div class="controls">
                                                             <input type="text" id="txtGetBy" class=" form-group " placeholder="领料员">
                                                        </div>
                                                    </div>--%>
                                         <div class="input-group">
                                                <div class="input-group-addon">领料员:</div>
                                                <input class="form-control" type="text" name="txtGetBy" id="txtGetBy" />
                                         </div>
                                     </div>
                                    <div class="form-group">
                                                    <%--<label class="col-xs-4 text-right"  for="txtResendMSN">条码：</label>
                                                 <div class="col-xs-8">
                                                     <div class="controls">
                                                        <input class="form-group" name="txtResendMSN" id="txtResendMSN" type="text" placeholder="来料条码">
                                                    </div>
                                                     </div>--%>
                                            <div class="input-group">
                                                <div class="input-group-addon">条&nbsp;码:</div>
                                                <input class="form-control" type="text" name="txtResendMSN" id="txtResendMSN" />
                                            </div>
                                      </div>
                                     </div>                                                                                                            
                                </form>
                            </div>                            
                        </div> 	
					</div>
				</div>
        <div class="row-fluid"  style="padding:0px;">
                <div class="col-md-9" style="border:1px solid #e6e0e0;padding:0px;">                                            
                        <div class="portlet box"> 
                            <div class="portlet box  bg-info">
                                <div class="portlet-title">
								    <div class="caption"><span class="text-info">补退料记录</span></div>								 
							    </div>
                                <div class="portlet-body fuelux" >
                                    <div class="searchbody" style=" padding-top:0;margin:0 auto;border:1px solid #e6e0e0;"> 
                                         <div id="recinfoArea"></div>
                                         <div id="gdReSendReturnHistoryList" style="width:100%; height:400px;overflow:auto;"></div>
                                         <div id="pagingArea"></div> 
                                     </div>
                                </div>
                        </div>
                       </div>
				</div>
                <div class="col-md-3" style="border:1px solid #e6e0e0;padding:0px;">  
                     <div class="portlet box">                       
                            <div class="portlet box  bg-info">
							    <div class="portlet-title">
								    <div class="caption"><span class="text-info">退料</span></div>								    
							    </div>
                            <div class="portlet-body fuelux" style="padding-left:0px">  
                                <div class="col-md-10 col-md-offset-1">                                                         
                                    <div class="form-group">                                                    
                                                 <%--<div class="col-xs-12">
                                                     <div class="controls">
                                                       <b>条码：</b> <input name="txtReturnMSN"  id="txtReturnMSN" type="text" placeholder="来料条码">
                                                    </div>
                                                 </div>--%>
                                            <div class="input-group">
                                                <div class="input-group-addon">条&nbsp;码:</div>
                                                <input class="form-control" type="text" name="txtReturnMSN" id="txtReturnMSN" />
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
     <script src="../../Scripts/WareHouseManage/ReturnWarehouse.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                App.init(); // initlayout and core plugins
                ReturnWarehouse.init();
                ReturnWarehouse.initGrid();
                ReturnWarehouse.initGridHistory();
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
