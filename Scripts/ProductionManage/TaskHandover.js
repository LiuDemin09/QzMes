var TaskHandover = function () {
    return {
        
        ID:null,
        init: function () {
            jQuery('#txtUserCode').focus();
            
            ID = TaskHandover.GetQueryString("ID");
       
        //交接任务
        $("#txtUserCode").keyup(function () {
            if (event.keyCode == 13) {
                //这里写你要执行的事件;                    
                //$("#txtPassword").focus();
                if ($("#txtUserCode").val() != null && $("#txtUserCode").val().trim().length > 5) {
                    if (ID == null)
                    {
                        ID = TaskHandover.GetQueryString("ID");
                    }
                    WsSystem.TaskHandover($("#txtUserCode").val(), ID,
                        function (result) {
                            if (result == "OK") {
                                JeffComm.succAlert("交接成功", 3000);
                               // $('#taskhandoverDialog').window('close');
                            }
                            else {
                                JeffComm.alertErr(result, 5000);
                                //$('#taskhandoverDialog').window('close');
                            }
                            $("#txtUserCode").val("");
                        },
                        function (err) {
                            JeffComm.alertErr(err.get_message(), 5000);
                            $("#txtUserCode").val("");
                            //$('#taskhandoverDialog').window('close');
                        });
                } else {
                    $("#txtUserCode").val("");
                    //$("#txtInputData").focus();
                }
               // $("#txtInputData").focus();
            }
        });
            jQuery('#btnSave').click(function () {
                WsSystem.TaskHandover($("#txtUserCode").val(), ID,
                           function (result) {
                               if (result == "OK") {
                                   JeffComm.alertSucc("交接成功", 3000);
                                   //$('#taskhandoverDialog').window('close');
                               }
                               else {
                                   JeffComm.alertErr(result, 5000);
                                   //$('#taskhandoverDialog').window('close');
                               }

                           },
                           function (err) {
                               JeffComm.alertErr(err.get_message(), 5000);
                              // $('#taskhandoverDialog').window('close');
                           });
            });
        },
       GetQueryString: function (name)
        {
            var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if(r!=null)return  unescape(r[2]); return null;
        }
    };
}();