var Main = function () {

    return {
        loginMode: true,
        //main function to initiate the module
        init: function () {
            //隐藏提示框
            $('.alert-danger').hide();
            //隐藏左边菜单栏
            $(".layout-button-left").live("click", function () {
            });
            // $(".layout-button-left").on("click" , function () {
            //});
            $(".layout-button-left").click();
            //设置登录窗口
            //function openPwd() {
            //    $('#w').window({
            //        title: '修改密码',
            //        width: 300,
            //        modal: true,
            //        shadow: true,
            //        closed: true,
            //        height: 160,
            //        resizable: false
            //    });
            //};
            //关闭登录窗口
            //function close() {
            //    $('#w').window('close');
            //};

            $('#w').window('close');//关闭修改密码窗口
            $('#editpass').click(function () {
                $('#w').window('open');
            });
            $('#btnConfirm').click(function () {
                Main.modifyPassword();
            });
            $('#btnCancel').click(function () {
                $('#w').window('close');//关闭修改密码窗口
            })
            $('#userhelp').click(function () {
                try {
                    var elemIF = document.createElement("iframe");
                    elemIF.src = "../Download/MES用户手册.pptx";
                    elemIF.style.display = "none";
                    document.body.appendChild(elemIF);
                } catch (e) {

                }
            });
            $('#loginOut').click(function () {
                $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function (r) {

                    if (r) {
                        location.href = '../login.aspx';
                    }
                });

            });

            jQuery('#spClose').click(function () {
                $('.alert-danger').hide();

            });


            //設置腳本超時            
            //WsLogin.set_timeout(10 * 1000); 
        },
        //弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
        msgShow: function (title, msgString, msgType) {
            $.messager.alert(title, msgString, msgType);
        },
        //修改密码
        modifyPassword: function () {
            var $oldPass = $('#txtOldPass');
            var $newpass = $('#txtNewPass');
            var $rePass = $('#txtRePass');
            if ($oldPass.val() == '') {
                Main.msgShow('系统提示', '请输入原密码！', 'warning');
                return false;
            }
            if ($newpass.val() == '') {
                Main.msgShow('系统提示', '请输入密码！', 'warning');
                return false;
            }
            if ($rePass.val() == '') {
                Main.msgShow('系统提示', '请再一次输入密码！', 'warning');
                return false;
            }

            if ($newpass.val() != $rePass.val()) {
                Main.msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
                return false;
            }
            WsSystem.UpdatePassword($('#txtOldPass').val(), $("#txtNewPass").val(),
                       function (result) {
                           $('#txtNewPass').val("");
                           $('#txtRePass').val("");
                           Main.msgShow('系统提示', '恭喜，密码修改成功', 'warning');
                           $('#w').window('close');//关闭修改密码窗口
                       }, function (err) {
                           Main.msgShow('更新失败', err.get_message(), 'warning');
                       });
            //$.post('../ajax/editpassword.ashx?newpass=' + $newpass.val(), function (msg) {
            //    Main.msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + msg, 'info');
            //    $newpass.val('');
            //    $rePass.val('');
            //    close();
            //})

        },
        //初始化左侧
        InitLeftMenu: function () {
            // $(".easyui-accordion").empty();
            $("#nav").accordion({ animate: false });//为id为nav的div增加手风琴效果，并去除动态滑动效果

            WsSystem.GetUserMenu(function (result) {
                //var _menus = JSON.parse(result);
                var _menus = result;
                //$.each(_menus.menus, function (i, n) {
                var defaultname = "";
                var defaulturl = "";
                $.each(_menus, function (i, n) {
                    var menulist = "";
                    menulist += '<div title="' + n.menuname + '" icon="' + n.icon + ' " style="overflow:auto;text-align:left">';
                    menulist += '<ul style="text-align:left">';
                    $.each(n.menus, function (j, o) {
                        if (defaultname == "") {
                            defaultname = o.menuname;
                            defaulturl = o.url;
                        }
                        // menulist += '<li><div><a target="mainFrame" href="' + o.url + '" ><span class="icon ' + o.icon + '" ></span>' + o.menuname + '</a></div></li> ';
                        menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="' + o.url + '" ><span class="nav" style="text-decoration:none;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + o.menuname + '</span></a></div></li> ';
                    })
                    menulist += '</ul></div>';

                    $('#nav').accordion('add', {
                        title: n.menuname,
                        content: menulist,
                        iconCls: 'icon ' + n.icon
                    });

                });
                // $(".easyui-accordion").append(menulist);
                $('.easyui-accordion li a').click(function () {
                    // var tabTitle = $(this).text();
                    var tabTitle = $(this).children('.nav').text();//获取超链里span中的内容作为新打开tab的标题
                    // var url = $(this).attr("href");
                    var url = $(this).attr("rel");
                    var menuid = $(this).attr("ref");//获取超链接属性中ref中的内容

                    addTab(tabTitle, url);
                    $('.easyui-accordion li div').removeClass("selected");
                    $(this).parent().addClass("selected");
                }).hover(function () {
                    $(this).parent().addClass("hover");
                }, function () {
                    $(this).parent().removeClass("hover");
                });
                $(".easyui-accordion").accordion();
                //defaulturl = "Pages/PrintManage/Reprint.aspx";
                setDefaultPage(defaultname, defaulturl);//设置默认页
            },
                           function (err) {
                               $("#spErrMsg").html(err.get_message());
                               $('.alert-danger').show();
                           });

            function addTab(subtitle, url) {
                if (!$('#tabs').tabs('exists', subtitle)) {
                    $('#tabs').tabs('add', {
                        title: subtitle,
                        content: createFrame(url),
                        closable: true
                        //width: $('#mainPanle').width() - 10,
                        //height: $('#mainPanle').height() - 26
                    });
                } else {
                    $('#tabs').tabs('select', subtitle);//选中tab并显示 
                    $('#mm-tabupdate').click();
                    //var currTab = $('#tabs').tabs('getSelected');
                    //var url = $(currTab.panel('options').content).attr('src');
                    //if (url != undefined && currTab.panel('options').title != 'Home') {
                    //    $('#tabs').tabs('update', {  //刷新这个tab
                    //        tab: currTab,
                    //        options: {
                    //            content: createFrame(url)
                    //        }
                    //    })
                    //}
                }
                Main.tabClose();
            };

            function createFrame(url) {
                var s = '<iframe name="mainFrame" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
                return s;
            };
            function setDefaultPage(tabTitle, url) {
                addTab(tabTitle, url);
                $('.easyui-accordion li div').removeClass("selected");
                $(this).parent().addClass("selected");
            };
        },
        createFrame:function(url) {
            var s = '<iframe name="mainFrame" scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
            return s;
        },
        tabClose: function () {
            /*双击关闭TAB选项卡*/
            $(".tabs-inner").dblclick(function () {
                var subtitle = $(this).children("span").text();
                $('#tabs').tabs('close', subtitle);
            })

            $(".tabs-inner").bind('contextmenu', function (e) {
                $('#mm').menu('show', {
                    left: e.pageX,
                    top: e.pageY,
                });

                var subtitle = $(this).children("span").text();
                $('#mm').data("currtab", subtitle);

                return false;
            });
        },
        //绑定右键菜单事件
        tabCloseEven: function () {
            //刷新
            $('#mm-tabupdate').click(function () {
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != 'Home') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: main.createFrame(url)
                        }
                    })
                }
            })
            //关闭当前
            $('#mm-tabclose').click(function () {
                var currtab_title = $('#mm').data("currtab");
                $('#tabs').tabs('close', currtab_title);
            })
            //全部关闭
            $('#mm-tabcloseall').click(function () {
                $('.tabs-inner span').each(function (i, n) {
                    var t = $(n).text();
                    $('#tabs').tabs('close', t);
                });
            });
            //关闭除当前之外的TAB
            $('#mm-tabcloseother').click(function () {
                var currtab_title = $('#mm').data("currtab");
                $('.tabs-inner span').each(function (i, n) {
                    var t = $(n).text();
                    if (t != currtab_title)
                        $('#tabs').tabs('close', t);
                });
            });
            //关闭当前右侧的TAB
            $('#mm-tabcloseright').click(function () {
                var nextall = $('.tabs-selected').nextAll();
                if (nextall.length == 0) {
                    //msgShow('系统提示','后边没有啦~~','error');
                    alert('后边没有啦~~');
                    return false;
                }
                nextall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });
            //关闭当前左侧的TAB
            $('#mm-tabcloseleft').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                if (prevall.length == 0) {
                    alert('到头了，前边没有啦~~');
                    return false;
                }
                prevall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });

            //退出
            $("#mm-exit").click(function () {
                $('#mm').menu('hide');
            })
        }
    };

}();