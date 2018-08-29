<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FitterTask.aspx.cs" Inherits="Pages_ProductionManage_FitterTask" %>

<!DOCTYPE html>
<html lang="zh-cn">
<head >
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>钳工报工</title>    
    <!-- begin  javascript files -->
     <link rel="stylesheet" href="../../Scripts/bootstrap/css/bootstrap.min.css"  /> 
     <link rel="stylesheet" href="../../CSS/SysManage.css"  />   
    <script type="text/javascript" src="../../Scripts/bootstrap/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery-easyui-1.5.2/locale/easyui-lang-zh_CN.js"></script>
    <script type="text/javascript" src="../../Scripts/bootstrap/bootstrap.js"></script>           
	<link href="../../ClientMedia/css/bootstrap-responsive.min.css" rel="stylesheet" type="text/css" />
	<link href="../../ClientMedia/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
	<link href="../../ClientMedia/css/style-metro.css" rel="stylesheet" type="text/css" /> 
    <link href="../../ClientMedia/css/style.css" rel="stylesheet" type="text/css" /> 
	<link href="../../ClientMedia/css/style-responsive.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/default/easyui1.css" />
    <link rel="stylesheet" type="text/css" href="../../Scripts/easyui/themes/icon.css" />
    <script type="text/javascript" src="../../Scripts/easyui/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../Scripts/easyui/jquery.easyui1.min.js"></script>
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
        <div id="centerPanel" data-options="region:'center',split:true" style="height: 100%; width: 90%">
             <div class="easyui-layout" data-options="fit:true">
                <div data-options="region:'north',split:true" style="height:45%">
            <table id="grid">
            </table>
                    </div>
                 <div data-options="region:'center'" style="height:10%">
            <form action="#" class="form-horizontal " style="padding-top:10px;">                    
                <div class="form-group">                           
                    <%--<span class="col-md-7">报工顺序：1.扫来料条码；2：扫开工；3：扫成品条码；4：扫合格或不合格条码；5：扫收工。</span>--%>
                    <div class="btn-group" role="group" aria-label="...">
                        <button type="button" id="btnresult" class="btn btn-default">扫合格或不合格--></button>
                            <button type="button" id="btnfailcode" class="btn btn-default">扫不良代码--></button>
                            <button type="button" id="btnpsn" class="btn btn-default">扫成品条码</button>                                  
                    </div>
                        <%--<label for="txtInputData" class="col-md-2 text-right">报工作业:</label>
                    <div class="col-md-3">                                
                        <input type="text" class="form-control" name="txtInputData" id="txtInputData1" />
                    </div>--%>
                </div>
                <div id="divMsg" class="alert alert-success">
                    <button class="close" data-dismiss="alert"></button>
                    <span id="spMsg">message</span>
                </div>
            </form> 
                     </div>
             <div id="southPanel" data-options="region:'south',split:true" style="height: 45%">
            <table id="gridhistory">
            </table>
                  </div>
        </div>
             </div>
        <div data-options="region:'east',title:'参考码',split:true" style="width:200px; text-align: center; vertical-align: middle; padding-top: 5%">
            <table style=" text-align: center; vertical-align: middle;width:100%">
              <tr >                  
                    <td  style="height:40px">
                        <img src="../../images/product/snquery.png" class="img-responsive" alt="条码查询" />
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                        <img src="../../images/product/cancel.png" class="img-responsive" alt="取消" />
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                        <img src="../../images/product/pass.png" class="img-responsive" alt="合格" />
                   </td>         
              </tr> 
                <tr >                  
                    <td  style="height:40px">
                       <img src="../../images/product/fail.png" class="img-responsive" alt="不合格" />
                   </td>         
              </tr>              
           </table>
        </div>
    </div>
     <script src="../../Scripts/ProductionManage/FitterTask.js" type="text/javascript"></script> 
	<!-- END PAGE LEVEL SCRIPTS -->  
        <script>
            jQuery(document).ready(function () {
                FitterTask.init();
                FitterTask.initGrid();
                FitterTask.initGridHistory();
                $("#txtInputData1").focus();
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
