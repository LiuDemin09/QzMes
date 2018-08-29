<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysMenu.aspx.cs" Inherits="Pages_SysManage_SysMenu" %>

<!DOCTYPE html>

<
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>菜单管理</title>    
    <!-- begin  javascript files -->
     <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/jquery-easyui-1.5.2/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/jquery.easyui.min.js"></script>
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
    <div id="allPanel" class="easyui-layout" style="width: 600px; height: 400px;" fit="true">
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 20%">
            <ul id="Menu" class="easyui-tree" ></ul>
        </div>
        <div data-options="region:'east',title:'菜单维护',split:true" style="width: 80%; text-align: center; vertical-align: middle; padding-top: 0%">
            <table id="myResume" bordercolordark="#FFFFFF" bordercolorlight="#45b97c" border="1px" cellpadding="0" cellspacing="0" style="width: 50%; height: 60%">
                <tr>
                    <th class="panel-header">上级目录</th>
                    <td>
                         <select tabindex="-1" data-options="required:true" name="selParent" id="selParent"  style="height: 20px; width: 80%">
                             </select>
                    </td>
                </tr>
                <tr>
                    <th class="panel-header">标&nbsp;&nbsp;识</th>
                    <td>
                        <input type="text" data-options="required:true" name="txtCode" id="txtCode" style="height: 20px; width: 80%" />
                    </td>
                </tr>                
                <tr>
                    <th class="panel-header">名&nbsp;&nbsp;称</th>
                    <td>
                         <input type="text" data-options="required:true" name="txtCN" id="txtCN" style="height: 20px; width: 80%" />                          
                    </td>                     
                </tr> 
                <tr>
                    <th class="panel-header">页&nbsp;&nbsp;面</th>
                    <td>
                         <input type="text" data-options="required:true" name="txtPageURL" id="txtPageURL" style="height: 20px; width: 80%" />                          
                    </td>                     
                </tr>            
            </table>
             <div style="background: #fafafa; padding: 5px; width:50%; border: 1px solid #ccc;text-align:center" >
                <a  id="btnSave" class="easyui-linkbutton" data-options="iconCls:'icon-save'" >保存</a>
                <a id="btnClear" class="easyui-linkbutton" data-options="iconCls:'icon-remove'">清空</a>
                  <a id="btndel" class="easyui-linkbutton" data-options="iconCls:'icon-no'">删除</a>
            </div>
        </div>
    </div>
     <script src="../../Scripts/SysManage/SysMenu.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                SysMenu.init();
                SysMenu.initMenu();
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
