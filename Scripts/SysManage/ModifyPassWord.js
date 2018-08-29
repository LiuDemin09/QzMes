var ModifyPassWord = function () {
    return {
        tree: null,
        grid: null,
        userTable: null,
        //main function to initiate the module
        init: function () {

            $('.form-horizontal').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    oldPassword: {
                        required: true
                    },
                    newPassword: {
                        required: true,
                    },
                    reNewPassword: {
                        required: true,
                        equalTo: "#newPassword"
                    }
                },

                messages: {
                    oldPassword: {
                        required: StringRes.E0003
                    },
                    newPassword: {
                        required: StringRes.E0003
                    },
                    reNewPassword: {
                        required: StringRes.E0003,
                        equalTo: StringRes.CheckPassWord
                    }
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    JeffComm.errorAlert(StringRes.E0002, "divMsg");
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
                    WsSystem.UpdatePassword($("#oldPassword").val(), $("#newPassword").val(),
                        function (result) {
                           
                            JeffComm.succAlert(StringRes.M0002, "divMsg");
                            $('#oldPassword').val("");
                            $('#newPassword').val("");
                            $('#reNewPassword').val("");
                           
                            
                        }, function (err) {
                            JeffComm.errorAlert(err.get_message(), "divMsg");

                        });
                }
            });
            //Set status
            $('.basic-toggle-button').toggleButtons();

            WsSystem.set_timeout(3 * 60 * 1000);
            WsSystem.set_defaultFailedCallback(
                    function (error) {
                        JeffComm.errorAlert(error.get_message(), "divMsg");
                    });                  
        }

    };

}();