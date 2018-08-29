﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysRole.aspx.cs" Inherits="Pages_SysManage_SysRole" %>

<%--<!DOCTYPE html>--%>

<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>角色管理</title>   
     <!-- begin  javascript files -->
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
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
     <div id="restartDialog" class="easyui-dialog" title="角色维护" style="width: 30%; height: 40%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">
        <form id="resumeForm">
            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 98%; height: 80%">
                <tr>
                    <th class="panel-header">角色名称</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtRoleName" id="txtRoleName" style="height: 30px; width: 95%" />
                    </td>
                   
                </tr>
                <tr> 
                     <th class="panel-header">描&nbsp;&nbsp;述</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtDesc" id="txtDesc" style="height: 30px; width: 95%" />
                    </td>               
                </tr>                
                <tr>
                    <th class="panel-header">是否可用</th>
                    <td>
                         <input type="checkbox" id="chkActived" name="chkActived" value="1" style="height: 20px; width: 95%"   />                          
                    </td>                     
                </tr>             
            </table>

            <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:center" >
                <a  id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存</a>
                <a id="btnClear" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                <a id="btnClose" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'">关闭</a>
            </div>
        </form>
    </div>
    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 85%">
            <table id="grid">
            </table>
        </div>
    </div>
     <div id="selectPermissionWindow" class="easyui-dialog" title="选择权限" style="width: 60%; height: 75%"
        data-options="iconCls:'pag-list',modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,closed:true">
          <div style="background: #fafafa; padding: 5px; width: 98%; border: 1px solid #ccc;text-align:left;padding-right:30px" >               
               <a  id="btnUpdatePermission" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存</a>
              <a  id="btnClosePermission" class="easyui-linkbutton" data-options="iconCls:'icon-cancel'" >关闭</a>
            </div>
       <%-- <form id="selectPermissionForm">--%>
           <ul id="selectPermission" class="easyui-tree" ></ul>
        <%--</form>--%>
    </div>
    <script src="../../Scripts/SysManage/SysRole.js" type="text/javascript"></script>  
        <script>
            jQuery(document).ready(function () {
                SysRole.init();
                SysRole.initGrid();
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
