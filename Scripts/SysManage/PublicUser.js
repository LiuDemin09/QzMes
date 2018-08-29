var SysUser = function () {
    return {
        tree: null,
        groupTree: null,
        grid: null,
        userCode: "",
        //main function to initiate the module
        init: function () {

            $('.form-horizontal').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    txtUserCode: {
                        required: true
                    },
                    txtUserName: {
                        required: true
                    }
                },

                messages: {
                    txtUserCode: {
                        required: "User Name is required."
                    },
                    txtUserName: {
                        required: "User Code is required."
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    JeffComm.errorAlert("Please type in reqired items.", "divMsg");
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
                    //Check Login
                    var o = {
                        UserCode: $("#txtUserCode").val(),
                        UserName: $("#txtUserName").val(), 
                        DeptName: $("#txtDeptName").val(),
                        Mobile: $("#txtIphone").val()
                    };
                    WsSystem.SaveUser(o,
                        function (result) {
                            //window.location.href = "Index.aspx";
                            SysUser.userCode = "";
                            JeffComm.succAlert("Saved successfully.", "divMsg");
                            $('#txtUserCode').removeAttr("disabled");
                            $('#txtUserCode').val("");
                            $('#txtUserName').val("");
                            $('#txtDeptName').val("");                            
                            SysUser.bindGridData();
                        });

                }
            });
           
            jQuery('#btnSearch').click(function () {
                SysUser.bindGridData();
            });
          
           
        },

        //bindTreeData: function () {
        //    if (SysUser.tree) {
        //        SysUser.tree.destructor();
        //    }
        //    SysUser.tree = new dhtmlXTreeObject("divRoleTree", "100%", "100%", 0);
        //    SysUser.tree.setSkin('dhx_skyblue');
        //    SysUser.tree.setImagePath("../../ClientMedia/dhtmlx/imgs/csh_scbrblue/");
        //    SysUser.tree.enableCheckBoxes(1);
        //    SysUser.tree.enableThreeStateCheckboxes(true);
        //    SysUser.tree.enableSmartXMLParsing(true);

        //    WsSystem.TreeRoleUser(SysUser.userCode, function (result) {
        //        SysUser.tree.loadXMLString(JeffComm.parseXMLNode(result));
        //    });
        //},

        bindGridData: function () {
            WsSystem.FindPublicUsers($("#txtSUserCode").val(), $("#txtSUserName").val(),
                function (result) {                   
                    SysUser.grid.clearAll(false);
                    SysUser.grid.loadXMLString(JeffComm.parseXMLNode(result));
                });
        },

        initGrid: function () {
            SysUser.grid = new dhtmlXGridObject('gdUserList', '100%', '400');
            SysUser.grid.setImagePath("../../ClientMedia/dhtmlx/imgs/");
            SysUser.grid.setHeader(StringRes.PublicUserHeader);
            
            SysUser.grid.setInitWidths("80,100,100,*");
            SysUser.grid.setColAlign("left,left,left,left");
            SysUser.grid.setColTypes("ro,ro,ro,ro");
            SysUser.grid.setColSorting("str,str,str,date");
            SysUser.grid.init();
            SysUser.grid.setSkin("dhx_skyblue");
           
            //SysUser.grid.enablePaging(true, 2, 6, "pagingArea", true, "recinfoArea");
           
            SysUser.grid.attachEvent("onRowSelect", function (item) {
                SysUser.userCode = item;
                JeffComm.clearAlert();
                WsSystem.FindPublicUserByCode(item, function (result) {
                    $('#txtUserCode').val(result.UserCode);                   
                    $('#txtUserName').val(result.UserName);
                    $('#txtDeptName').val(result.DeptName);
                    $('#txtIphone').val(result.MOBILE);
                    $('#txtUserCode').attr("disabled", "disabled");
                    $('#txtUserName').attr("disabled", "disabled");
                    $('#txtDeptName').attr("disabled", "disabled");
                    $('#txtIphone').attr("disabled", "disabled");
                });

               // SysUser.bindTreeData();
            });

            SysUser.bindGridData();
        }

    };

}();