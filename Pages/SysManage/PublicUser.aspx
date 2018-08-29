<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PublicUser.aspx.cs" Inherits="Pages_SysManage_PublicUser" %>

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
    <div class=" row-fluid" style="padding:0px;">
        <div class="col-md-6 col-xs-12" >
             <div class="searchhead"  >
                <div class="alert alert-info"><span class="text-info"  style="font-size:15px;">查询</span> </div>
               <%--  <form class="form-horizontal form-inline" style="padding-left:20px;">                     
                  <div class="form-group">                  
                    <input type="text"  id="txtSUserCode" class="form-control" placeholder="工号"  >
                  </div>
                  <div class="form-group ">                   
                    <input type="text" class="form-control" id="txtSUserName" placeholder="姓名" >
                  </div>
                   <div class="form-group"  style="padding-left:50px;">
                  <button type="button" class="btn btn-default  btn-block"  id="btnSearch">查询</button>
                     </div>
                </form> --%>
                  <form action="#" class="form-horizontal form-row-seperated form-inline">                                 
                     <input type="text" id="txtSUserCode" class="form-control " placeholder="工号">
                     <input type="text" id="txtSUserName" class=" form-control " placeholder="姓名">
                     <a id="btnSearch" class="btn blue"><i class="icon-search"></i>
                         <asp:Literal ID="Literal2"   runat="server" Text="查询"></asp:Literal></a>                                                                                                               
                </form>                                 
             </div>
                    
            <div class="searchbody" style=" padding-top:10px;margin:0 auto;">                 
                 <div id="recinfoArea"></div>
                 <div id="gdUserList" style="width:100%; height:350px;overflow:auto;border:1px solid #808080;"></div>
                 <div id="pagingArea"></div> 
             </div>
        </div>
         <%--<div class="col-xs-1" ></div>--%>
        <div class="col-md-6 col-xs-12" >
            <div id="divMsg" class="alert alert-success ">
                            <button class="close" data-dismiss="alert"></button>
                            <span id="spMsg">message</span>
            </div>
            <div>
                 
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active" id="tab_user_modify" >
                        <div class="portlet box bg-info">

							<div class="portlet-title">

								<div class="caption"><i class="icon-comments"></i> <span class="text-info">选择结果</span></div>

							</div>
                            <div class="portlet-body ">                        
                            <form class="form-horizontal">
                                <div class="form-group">
                                        <div class="col-md-6 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">用户工号:</div>
                                                <input class="form-control" type="text" name="txtUserCode" id="txtUserCode" />
                                             </div>
                                        </div>
                                </div> 
                               <%-- <div class="form-group">
                                     <label class="col-xs-4 text-right"  for="txtUserCode">用户工号：</label>
                                     <div class="col-xs-8">
                                        <input type="text"  class="form-group " id="txtUserCode" name="usercode"/>
                                     </div>
                                </div>--%>
                                <div class="form-group">
                                        <div class="col-md-6 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">用户姓名:</div>
                                                <input class="form-control" type="text" name="txtUserName" id="txtUserName" />
                                             </div>
                                        </div>
                                </div> 
                               <%-- <div class="form-group">
                                     <label class="col-xs-4  text-right"  for="txtUserName">用户姓名：</label>
                                     <div class="col-xs-8">
                                        <input type="text"  class="form-group " id="txtUserName" name="username"/>
                                     </div>
                                </div>--%>
                                 <div class="form-group">
                                        <div class="col-md-6 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">部门名称:</div>
                                                <input class="form-control" type="text" name="txtDeptName" id="txtDeptName" />
                                             </div>
                                        </div>
                                </div>
                               <%-- <div class="form-group">
                                     <label class="col-xs-4  text-right"  for="txtDeptName">部门名称：</label>
                                     <div class="col-xs-8">
                                        <input type="text"  class="form-group " id="txtDeptName" name="deptname"/>
                                     </div>
                                </div>--%>
                                 <div class="form-group">
                                        <div class="col-md-6 col-md-offset-3">
                                            <div class="input-group">
                                             <div class="input-group-addon">联系电话:</div>
                                                <input class="form-control" type="text" name="txtIphone" id="txtIphone" />
                                             </div>
                                        </div>
                                </div>
                                <%--<div class="form-group">
                                     <label class="col-xs-4  text-right"  for="txtIphone">联系电话：</label>
                                     <div class="col-xs-8">
                                        <input type="text"  class="form-group " id="txtIphone" name="iphone"/>
                                     </div>
                                </div>--%>
                               
                                <div class="form-group">
                                 <div class="col-xs-4"></div>
                                 <div class="form-group  col-xs-offset-4 col-xs-4">                                       
                                        <button type="submit" id="btnSave" class="btn blue btn-block">完成</button>                                   
                                </div>
                               </div>
                            </form>
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
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxtree_kn.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgridcell.js" type="text/javascript"></script>
    <script src="../../ClientMedia/dhtmlx/js/dhtmlxgrid_pgn.js" type="text/javascript"></script>
     <script src="../../Scripts/bootstrap/jquery.validate.min.js" type="text/javascript"></script>
     <script src="../../Scripts/StringRes.js" type="text/javascript"></script>
     <script src="../../Scripts/Common.js" type="text/javascript"></script>
    <script src="../../Scripts/SysManage/PublicUser.js" type="text/javascript"></script> 
    <script src="../../ClientMedia/js/app.js" type="text/javascript"></script> 
    <!-- END PAGE LEVEL SCRIPTS -->
    
    
	<script>

	    jQuery(document).ready(function () {
	        App.init(); // initlayout and core plugins
	        SysUser.init();
	        SysUser.initGrid();
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
