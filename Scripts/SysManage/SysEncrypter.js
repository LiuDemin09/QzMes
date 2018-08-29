var SysEncrypter = function () {

    return {
        //main function to initiate the module
        init: function () {
            //設置腳本超時
            WsSystem.set_timeout(3 * 60 * 1000);
            WsSystem.set_timeout(3 * 60 * 1000);
            //設置默認的Ajax失敗回調函數
            WsSystem.set_defaultFailedCallback(
                    function (error) {
                        JeffComm.errorAlert(error.get_message(), "divMsg");
                    });
            WsSystem.set_defaultFailedCallback(
                    function (error) {
                        JeffComm.errorAlert(error.get_message(), "divMsg");
                    });            
            //$("#btnSearch").click(function () {
            //    if ($("#txtTimeFrom").val() == "" || $("#txtTimeTo").val() == "") {
            //        JeffComm.errorAlert("必須選擇Time Range.", "divMsg");
            //        return false;
            //    }                
            //});
            //加密
            $("#txtEncrypt").click(function () {                
                WsSystem.Encrypt($("#txtOriginal").val(), function (r) {
                    JeffComm.succAlert(StringRes.txtOperation, "divMsg");
                    $("#txtDist").val(r);                    
                });
            });
            //解密
            $("#txtDecrypt").click(function () {
                WsSystem.Decrypt($("#txtDist").val(), function (r) {
                    JeffComm.succAlert(StringRes.txtOperation, "divMsg");
                    $("#txtOriginal").val(r);
                });
            });

            $('.form-horizontal').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    txtOriginal: {
                        required: false
                    },
                    txtDist: {
                        required: false
                    }
                },

                messages: {
                    txtOriginal: {
                        required: StringRes.E0003
                    },
                    txtDist: {
                        required: StringRes.E0003
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    $("#spErrMsg").html(StringRes.E0002);
                    $('.alert-error', $('.login-form')).show();
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
                    error.addClass('help-small no-left-padding').insertAfter(element.closest('.input-icon'));
                },

                submitHandler: function (form) {
                    //Check Login
                    return false;                   
                }
            });
        }
    };

}();