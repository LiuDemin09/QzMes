<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyPassWord.aspx.cs" Inherits="Pages_SysManage_ModifyPassWord" %>

<!DOCTYPE html>

<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户管理</title>
    <!-- begin  javascript files --> 
     <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css"  />   
    <script type="text/javascript" src="../../Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap/bootstrap.js"></script>
     <!-- end  javascript files --> 
     <!-- BEGIN GLOBAL MANDATORY STYLES -->  
    <link rel="stylesheet" href="../../CSS/SysManage.css"  />    
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
   <!-- BEGIN PAGE CONTENT-->

				<div class="row-fluid">

					
					<div class="span12">
                        <div id="divMsg" class="alert alert-success hide">

                            <button class="close" data-dismiss="alert"></button>

                            <span id="spMsg">message</span>

                        </div>
                        <!--BEGIN TABS-->                         
                        <div class="tabbable tabbable-custom">
                            <div class="tab-content">

                                <div class="tab-pane active" id="tab_1_1">

                                <%--   <div class="portlet box grey">--%>
							<div class="portlet-body ">

                                <form action="#" class="form-horizontal">
                                     <div class="form-group">
                                        <div class="col-md-4 col-xs-12  col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">原&nbsp;密&nbsp;码:</div>
                                                <input class="form-control" type="password" name="oldPassword" id="oldPassword" />
                                             </div>
                                        </div>
                                    </div>  
                                     <div class="form-group">
                                        <div class="col-md-4 col-xs-12 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">新&nbsp;密&nbsp;码:</div>
                                                <input class="form-control" type="password" name="newPassword" id="newPassword" />
                                             </div>
                                        </div>
                                    </div>                                                                    
                                   <div class="form-group">
                                        <div class="col-md-4 col-xs-12 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">确认新密码:</div>
                                                <input class="form-control" type="password" name="reNewPassword" id="reNewPassword" />
                                             </div>
                                        </div>
                                    </div>

                                                                   
                                    <div class="form-actions">
                                        <div class="col-md-4 col-xs-12 col-md-offset-4">
                                            <button id="btnSave" type="submit" class="btn blue">
                                                <i class="icon-save"></i>
                                                <asp:Literal ID="Literal3" runat="server" Text="保存"></asp:Literal>
                                            </button>
                                            <button id="Button1" type="reset" class="btn blue">
                                                <i class="icon-save"></i>
                                                <asp:Literal ID="Literal4" runat="server" Text="重置"></asp:Literal>
                                            </button>
                                        </div>
                                    </div>
                                </form>

                            </div>

						</div>
                                   

                            <%--    </div>--%>
                                                     
                            </div>

                        </div>

                        <!--END TABS-->
					</div>
				</div>

				<!-- END PAGE CONTENT-->   
</div>
     <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <script src="../../ClientMedia/js/ui-tree.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree_kn.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgridcell.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid_pgn.js" type="text/javascript"></script>
     <script src="../../Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
     <script src="../../Scripts/StringRes.js" type="text/javascript"></script>
     <script src="../../Scripts/Common.js" type="text/javascript"></script> 
    <script src="../../ClientMedia/js/app.js" type="text/javascript"></script> 
     <script src="../../Scripts/SysManage/ModifyPassWord.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL SCRIPTS -->
    
    
	<script>

	    jQuery(document).ready(function () {
	        App.init(); // initlayout and core plugins
	        ModifyPassWord.init();
	    });

	</script>
      
 <form  runat="server">
     <asp:ScriptManager ID="SMG" runat="server">
         <Services>
             <asp:ServiceReference Path="../../services/WsSystem.asmx" />
         </Services>
     </asp:ScriptManager>
 </form> 
</body>
</html>
