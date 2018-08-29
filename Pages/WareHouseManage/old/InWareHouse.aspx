<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InWareHouse.aspx.cs" Inherits="Pages_WareHouseManage_InWareHouse" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>入库作业</title>    
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
					<div class="col-md-7 col-xs-12"> 
                        <div class="portlet ">
                            <div class="alert alert-info">
                                 
                                <span class="text-info text-left"  style="font-size:15px;">查询</span>
                                
                            </div>
                            <div class="portlet-body fuelux" >
                               <%-- <form class="navbar-form navbar-left" role="search">
                                    <div class="form-group"><h4><p class="navbar-text"></p></h4></div>
                                  <div class="form-group">
                                    <input type="text" id="txtMaterial" class="form-control" placeholder="物料料号">
                                  </div>
                                     <a id="btnSearch" class="btn blue"><i class="icon-search"></i>
                                                 <asp:Literal ID="Literal19"   runat="server" Text="查询"></asp:Literal></a>
                                </form>--%>
                               <%-- <form class="form-horizontal">
                                  <div class="input-group col-md-offset-3 col-md-4">
                                    <input type="text" id="txtMaterial" class="form-control" placeholder="物料料号">
                                    <span class="input-group-btn ">
                                      <button class="btn btn-primary form-control" id="btnSearch" runat="server" type="submit">查&nbsp;&nbsp;&nbsp;&nbsp;询</button>
                                    </span>
                                  </div>
                                </form>--%>
                                <div class="col-md-10 col-md-offset-1">
                                   <form action="#" class="form-horizontal form-row-seperated form-inline">
                                     <div class="form-group">
                                        <input type="text" id="txtMaterial" class="form-control " placeholder="物料料号">                                      
                                         <a id="btnSearch" class="btn blue"><i class="icon-search"></i>
                                                     <asp:Literal ID="Literal4"   runat="server" Text="查询"></asp:Literal></a>                                       
                                     </div>                                                                                
                                </form>
                               </div>
                            </div>
                           </div>
                        <div class="portlet box"> 
                            <div class="searchbody" style=" padding-top:10px;margin:0 auto;border:1px solid #808080;"> 
                                <%--<div class="form-group form-group-lg bg-info">
                                    <h3> 信息列表 </h3> 
                                 </div>--%>
                                 <div id="recinfoArea"></div>
                                 <div id="gdInWareHouseList" style="width:100%; height:350px;overflow:auto;"></div>
                                 <div id="pagingArea"></div> 
                             </div>
                        </div>
					</div>
                    
					<div class="col-md-5 col-xs-12">
                        <div id="divMsg" class="alert alert-success">
                            <button class="close" data-dismiss="alert"></button>
                            <span id="spMsg">message</span>
                            
                        </div>
                         <!--BEGIN TABS-->                         
                        <div class="tabbable tabbable-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#tab_inwarehouse_modify" data-toggle="tab"><asp:Literal ID="Literal8"   runat="server" Text="修改"></asp:Literal></a></li>
                                <li><a href="#tab_inwarehouse_help" data-toggle="tab"><asp:Literal ID="Literal22"   runat="server" Text="帮助"></asp:Literal></a></li>
                            </ul>
                             
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_inwarehouse_modify">
                                    <div class="portlet box bg-info">
                                        <div class="portlet-title">
                                            <div class="caption"><i class="icon-comments"></i><span class="text-info">  编辑</span></div>
                                            <div class="actions">
                                                   <a href="javascript:;" id="btnNew" class="btn blue">
                                                    <i class="icon-plus"></i>
                                                   <asp:Literal ID="Literal3"   runat="server" Text="新建"></asp:Literal></a>
                                                  <a href="javascript:;" id="btnDel" class="btn red">
                                                    <i class=" icon-remove"></i>
                                                    <asp:Literal ID="Literal24"   runat="server" Text="删除"></asp:Literal></a>
                                            </div>
                                        </div>

                                        <div class="portlet-body ">
                                            <form action="#" class="form-horizontal">
                                                <div class="form-group">
                                                     <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">购货单位:</div>
                                                             <select tabindex="-1" name="selCustomeCompany" id="selCustomeCompany" class="form-control ">
                                                                    <option value="0">请选择</option>
                                                                </select>
                                                        </div>
                                                    </div>                                               
                                                    <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">收料仓库:</div>                                                         
                                                                <select tabindex="-1" name="selReceivedHouse" id="selReceivedHouse" class="form-control ">
                                                                    <option value="0">请选择</option>
                                                                </select>
                                                        </div>
                                                     </div>
                                                </div>
                                                <div class="form-group">
                                                     <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">单据编号:</div>
                                                            <input class="form-control" type="text" name="txtInlistNo" id="txtInlistNo" />
                                                        </div>
                                                     </div>
                                                    <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">物料代码:</div>
                                                            <input class="form-control" type="text" name="txtMaterialCode" id="txtMaterialCode" />
                                                        </div>
                                                   </div>
                                                </div>
                                                <div class="form-group">
                                                     <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">物料名称:</div>
                                                            <input class="form-control" type="text" name="txtMaterialName" id="txtMaterialName" />
                                                        </div>
                                                     </div>
                                                    <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">批&nbsp;&nbsp;号:</div>
                                                            <input class="form-control" type="text" name="txtBatchNo" id="txtBatchNo" />
                                                        </div>
                                                   </div>
                                                </div>
                                               <div class="form-group">
                                                     <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">单&nbsp;&nbsp;位:</div>
                                                            <select tabindex="-1" name="selUnit" id="selUnit" class="form-control ">
                                                               <option value="0">请选择</option>
                                                            </select>
                                                        </div>
                                                     </div>
                                                     <div class="col-md-6">
                                                        <div class="input-group">
                                                            <div class="input-group-addon">数&nbsp;&nbsp;量:</div>
                                                            <input class="form-control" type="text" name="txtQuantity" id="txtQuantity" />
                                                        </div>
                                                   </div>
                                                 </div>
                                                <%-- <div class="form-group">
                                                     <div class="col-md-6"> 
                                                      <div class="input-group">
                                                            <div class="input-group-addon">备&nbsp;&nbsp;注:</div>
                                                            <input class="form-control" type="text" name="txtMemo" id="txtMemo" />
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="form-actions"> 
                                                    <button id="btnSave" type="submit" class="btn blue col-xs-offset-5">
                                                        <i class="icon-save"></i>
                                                       <asp:Literal ID="Literal25"   runat="server" Text="保存并打印"></asp:Literal>
                                                    </button>
                                                </div>
                                            </form>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab_inwarehouse_help">

                                </div>
                            </div>
                        </div>
                        <!--END TABS-->
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
     <script src="../../Scripts/WareHouseManage/InWareHouse.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                if (!JeffComm.isIE) {
                    var objHtml = "<object id='objControls' type='application/x-itst-activex' style='border:0px;width:0px;height:0px;'";
                    objHtml = objHtml + "clsid='{CEF30987-4B6D-4698-9E81-297800093A10}' progid='NewfuseControls.CommonControl'></object>";
                    $(".page-content").after(objHtml);
                }
                App.init(); // initlayout and core plugins
                InWareHouse.init();
                InWareHouse.initGrid();
                //SysMenu.initTable();
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
