var CreatePartsdrawingNo = function () {

    return {       
        grid: null,
        partsdrawingNoID: "",
        //userTable: null,
        //main function to initiate the module
        init: function () {

            $('.form-horizontal').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    txtPartsdrawingNo: {
                        required: true
                    }
                },

                messages: {
                    txtPartsdrawingNo: {
                        required: StringRes.E0003//"必須輸入項目."
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    JeffComm.errorAlert(StringRes.E0002,"divMsg");  //"Please type in reqired items.",                  
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
	                    .closest('.control-group').addClass('error'); // set error class to the control group
                },

                success: function (label) {       
                    label.closest('.control-group').removeClass('error');
                    label.remove();
                },

                errorPlacement: function (error, element) {
                    error.addClass('help-small no-left-padding').appendTo(element.closest('.controls'));
                },

                submitHandler: function (form) {
                    var partsdrawinginfo = {
                        PartsCode: $("#txtPartsdrawingNo").val(),
                        CustName: $("#selCustomerName").find("option:selected").text(),
                        CustCode: $("#txtCustomerCode").val(),
                        ProductName: $("#selProductName").find("option:selected").text(),
                        ProductCode: $("#selProductName").val(),
                        PlanQuantity: $("#txtNumber").val(),//投产数量
                        QualityCode: $("#txtQuality").val(),//质量编号
                        AskQuantity: $("#txtAskQuantity").val(),//交付数量
                        BatchNumber: $("#txtBatchNo").val(),//炉批号
                        AskDate: $("#txtPlanTime").val(),
                    };
                    WsSystem.SavePartsDrawing(partsdrawinginfo,
                        function (result) {
                            JeffComm.succAlert("Saved successfully.", "divMsg");
                            $('#txtPartsdrawingNo').val("");
                            $('#txtCustomerCode').val("");
                            $('#txtNumber').val("");
                            $('#txtQuality').val("");
                            $('#txtAskQuantity').val("");
                            $('#txtBatchNo').val("");
                            CreatePartsdrawingNo.bindGridData();//更新左边列表
                        }, function (err) {
                            JeffComm.errorAlert(err.get_message(), "divMsg");
                        });
                }
            });
            $('#txtCustomerCode').attr("disabled", "disabled");
            $(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd hh:ii', autoclose: true });
            //$(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd hh:ii' });
            //初始化客户名称
            WsSystem.ListBindCustName(function (result) {
                 JeffComm.fillSelect($("#selCustomerName"), result, true);
            });
            //初始化产品名称
            WsSystem.ListBindProductName(function (result) {
                JeffComm.fillSelect($("#selProductName"), result, true);
            });
            //Set status
            //$('.basic-toggle-button').toggleButtons();

            //WsSystem.set_timeout(3 * 60 * 1000);
            //WsSystem.set_defaultFailedCallback(
            //        function (error) {
            //            JeffComm.errorAlert(error.get_message(), "divMsg");                        
            //        });

            //Event binding
            $("#selCustomerName").change(function () {
                $('#txtCustomerCode').val($("#selCustomerName").val());
                //WsSystem.QueryBackupInfo($('#selWorkOrder').val(),
                // function (result) {
                //     OutWarehouse.grid.clearAll(false);
                //     OutWarehouse.grid.loadXMLString(JeffComm.parseXMLNode(result));
                // });
            });

            jQuery('#btnNew').click(function () {
                //SysRole.roleId = null;
                CreatePartsdrawingNo.partsdrawingNoID = null;
                $('#txtPartsdrawingNo').val("");
                $('#txtCustomerCode').val("");
                $('#txtNumber').val("");
                $('#txtQuality').val("");
                $('#txtAskQuantity').val("");
                $('#txtBatchNo').val("");
            });
           
            jQuery('#btnDel').click(function () {
                if (CreatePartsdrawingNo.partsdrawingNoID == "" || CreatePartsdrawingNo.partsdrawingNoID == null) {
                    JeffComm.errorAlert(StringRes.E0001, "divMsg");//"Pls at lease select one item to delete on the left grid.",
                    return;
                }

                WsSystem.RemovePartsdrawingNo(CreatePartsdrawingNo.partsdrawingNoID, function (result) {
                    JeffComm.succAlert(StringRes.M0003,  "divMsg");//"Deleted successfully.",
                    CreatePartsdrawingNo.partsdrawingNoID = null;
                    $('#txtPartsdrawingNo').val("");
                    $('#txtCustomerCode').val("");
                    $('#txtNumber').val("");
                    $('#txtQuality').val("");
                    $('#txtAskQuantity').val("");
                    $('#txtBatchNo').val("");
                    CreatePartsdrawingNo.bindGridData();//更新左边列表
                }, function (err) {
                    JeffComm.errorAlert(err.get_message(), "divMsg");
                });
            });            

            jQuery('#btnDownload').click(function () {                
                window.open('../../Pages/WorkOrderManage/PartsDrawingDownload.aspx', '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
            });
            jQuery('#btnImport').click(function () {
                window.open('../../Pages/WorkOrderManage/PartsDrawingUpload.aspx', '', 'height=100,width=330, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=700 , top=" + button.Style["top"] + "');
            });
            jQuery('#btnSearch').click(function () {
                WsSystem.QueryPartsDrawingInfo($('#txtPartsDrawing').val(), 
                function (result) {
                    CreatePartsdrawingNo.grid.clearAll(false);
                    CreatePartsdrawingNo.grid.loadXMLString(JeffComm.parseXMLNode(result));
                });   
           });
            //防止回車提交
            $('.horizontal-form input').keypress(function (e) {
                if (e.which == 13) {
                    //if ($('.horizontal-form').validate().form()) {
                    //    // window.location.href = "/MDSystem/index.aspx";
                    //}
                    return false;
                }
            });
        },
        bindGridData:function(){
            WsSystem.QueryPartsDrawingInfo("", 
                function (result) {
                    CreatePartsdrawingNo.grid.clearAll(false);
                    CreatePartsdrawingNo.grid.loadXMLString(JeffComm.parseXMLNode(result));
                });            
        },
       
        initGrid: function () {
            CreatePartsdrawingNo.grid = new dhtmlXGridObject('gdCreatePartsdrawingNoList', '100%', '500');
            CreatePartsdrawingNo.grid.setImagePath("../../ClientMedia/dhtmlx/imgs/");
            CreatePartsdrawingNo.grid.setHeader(StringRes.CreatePartsDrawingHeader);
            CreatePartsdrawingNo.grid.setInitWidths("120,80,70,80,80,80,80,80,80,80,*");
            CreatePartsdrawingNo.grid.setColAlign("left,left,left,left,left,left,left,left,left,left,left");
            CreatePartsdrawingNo.grid.setColTypes("ro,ro,ro,ro,ro,ro,ro,ro,ro,ro,ro");
            CreatePartsdrawingNo.grid.setColSorting("str,str,str,str,str,str,str,str,str,str,date");
            CreatePartsdrawingNo.grid.init();
            CreatePartsdrawingNo.grid.setSkin("dhx_skyblue");
            CreatePartsdrawingNo.grid.setColumnHidden(9, true);
            CreatePartsdrawingNo.grid.setColumnHidden(10, true);
            //var combobox = SysRole.grid.getCombo(2);
            //combobox.put("1", "Actived");
            //combobox.put("0", "Disabled");
            CreatePartsdrawingNo.grid.attachEvent("onRowSelect", function (item) {
                CreatePartsdrawingNo.partsdrawingNoID = item;
                JeffComm.clearAlert();
                $('#txtPartsdrawingNo').val(CreatePartsdrawingNo.grid.cells(item, 0).getValue());
                $('#selCustomerName').val(CreatePartsdrawingNo.grid.cells(item, 1).getValue());
                $('#txtCustomerCode').val(CreatePartsdrawingNo.grid.cells(item, 2).getValue());
                $('#selProductName').val(CreatePartsdrawingNo.grid.cells(item, 3).getValue());
                $('#txtNumber').val(CreatePartsdrawingNo.grid.cells(item,4).getValue());
                $('#txtQuality').val(CreatePartsdrawingNo.grid.cells(item, 5).getValue());
                $('#txtAskQuantity').val(CreatePartsdrawingNo.grid.cells(item, 6).getValue());
                $('#txtBatchNo').val(CreatePartsdrawingNo.grid.cells(item,7).getValue());
                $('#txtPlanTime').val(CreatePartsdrawingNo.grid.cells(item,8).getValue());               
            });            
            CreatePartsdrawingNo.bindGridData();
        }
    };
}();