var Login = function () {

    return {
        loginMode: true,
        //main function to initiate the module
        init: function () {
            //隐藏提示框
            $('.alert-danger').hide(); 
            $("#txtUserName").focus();
            $('.login-form').validate({
                errorElement: 'label', //default input error message container
                errorClass: 'help-inline', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                rules: {
                    username: {
                        required: true
                    },
                    password: {
                        required: true
                    }
                },

                messages: {
                    username: {
                        required: StringRes.E0003
                    },
                    password: {
                        required: StringRes.E0003
                    } 
                },

                invalidHandler: function (event, validator) { //display error alert on form submit   
                    $("#spErrMsg").html(StringRes.E0002);
                    //$('.alert-danger', $('.login-form')).show();
                    $('.alert-danger').show();
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

                //submitHandler: function (form) { 
                //    var un = $("[name='username']", $('.login-form')).val();
                //    var pwd = $("[name='password']", $('.login-form')).val();
                //    var isRemember = $('#chkRemember').is(':checked');
                //    WsLogin.Login(un, pwd, isRemember,
                //        function (result) {
                //            window.location.href = result;
                //        },
                //        function (err) {
                //            $("#spErrMsg").html(err.get_message());
                //            $('.alert-danger').show();
                //        });
                //}
                //
               } );
            jQuery('#submitBtn').click(function () {
                var un = $("[name='username']", $('.login-form')).val();
                var pwd = $("[name='password']", $('.login-form')).val();
                var isRemember = $('#chkRemember').is(':checked');
                WsLogin.Login(un, pwd, isRemember,
                    function (result) {
                        window.location.href = result;
                    },
                    function (err) {
                        $("#spErrMsg").html(err.get_message());
                        $('.alert-danger').show();
                    });

            });
            $("#txtUserName").keyup(function () {
                if (event.keyCode == 13) {
                    //这里写你要执行的事件;                    
                    //$("#txtPassword").focus();
                    if ($("#txtUserName").val() != null && $("#txtUserName").val().trim().length > 5) {
                        var un = $("[name='username']", $('.login-form')).val();
                        var pwd = $("[name='password']", $('.login-form')).val();
                        var isRemember = $('#chkRemember').is(':checked');
                        WsLogin.Login(un, pwd, isRemember,
                            function (result) {
                                window.location.href = result;
                            },
                            function (err) {
                                $("#spErrMsg").html(err.get_message());
                                $('.alert-danger').show();

                            });
                    } else {
                        $("#txtPassword").focus();
                    }
                }
            });
            $("#txtPassword").keyup(function () {
                if (event.keyCode == 13) {
                    if ($('.login-form').validate().form()) {
                        // window.location.href = "/MDSystem/index.aspx";

                        var un = $("[name='username']", $('.login-form')).val();
                        var pwd = $("[name='password']", $('.login-form')).val();
                        var isRemember = $('#chkRemember').is(':checked');
                        WsLogin.Login(un, pwd, isRemember,
                            function (result) {
                                window.location.href = result;
                            },
                            function (err) {
                                $("#spErrMsg").html(err.get_message());
                                $('.alert-danger').show();

                            });
                    }

                    return false;
                }
            });
            //初始化登录名
            //$("[name='username']", $('.login-form')).val("135");
            //$("[name='password']", $('.login-form')).val("password");
           
            //$('.login-form input').keypress(function (e) {
            //    if (e.which == 13) {
            //        if ($('.login-form').validate().form()) {
            //            // window.location.href = "/MDSystem/index.aspx";

            //            var un = $("[name='username']", $('.login-form')).val();
            //            var pwd = $("[name='password']", $('.login-form')).val();
            //            var isRemember = $('#chkRemember').is(':checked');
            //            WsLogin.Login(un, pwd,isRemember,
            //                function (result) {
            //                    window.location.href = result;
            //                },
            //                function (err) {
            //                    $("#spErrMsg").html(err.get_message());
            //                    $('.alert-danger').show();
                          
            //                });
            //        }
                    
            //        return false;
            //    }
            //});

            jQuery('#spClose').click(function () {               
                $('.alert-danger').hide();
            
            });

            //检查cookie有没有值
            WsLogin.getCookieValue(function (result) {
                if(result!="")
                {
                    var res = result.split(',');
                    $('#txtUserName').val(res[0]);
                    $('#txtPassword').val(res[1]);
                   // $('#chkRemember').checked = true;
                    $("[name = chkRemember]:checkbox").attr("checked", true);
                }
                else
                {
                    $('#chkRemember').checked = false;
                }
              });
            //設置腳本超時            
            //WsLogin.set_timeout(10 * 1000); 
        }

    };

}();