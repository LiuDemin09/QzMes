var BasBanCi = function () {

    return {       
        grid: null,
        basbanciid: null,
        //main function to initiate the module
        init: function () {
            $('#txtStartTime').timespinner('setValue', '00:00');
            $('#txtEndTime').timespinner('setValue', '00:00');
            $('#txtRestStartTime').timespinner('setValue', '00:00');
            $('#txtRestEndTime').timespinner('setValue', '00:00');

            jQuery('#btnClose').click(function () {
                BasBanCi.basbanciid = null;
                $('#selBanCiName').val("");
                $('#txtStartTime').timespinner('setValue', '00:00');
                $('#txtEndTime').timespinner('setValue', '00:00');
                $('#txtRestStartTime').timespinner('setValue', '00:00');
                $('#txtRestEndTime').timespinner('setValue', '00:00');
                $('#restartDialog').window('close');
            });
            
            jQuery('#btnNew').click(function () {
                BasBanCi.basbanciid = null;
                $('#selBanCiName').val("");
                $('#txtStartTime').timespinner('setValue', '00:00');
                $('#txtEndTime').timespinner('setValue', '00:00');
                $('#txtRestStartTime').timespinner('setValue', '00:00');
                $('#txtRestEndTime').timespinner('setValue', '00:00');
            });
            jQuery('#btnSave').click(function () {

                var basbanci = {
                    ID:BasBanCi.basbanciid,
                    NAME: $("#selBanCiName").val(),
                    StartTime: $("#txtStartTime").val(),
                    EndTime: $("#txtEndTime").val(),
                    StartResttime: $("#txtRestStartTime").val(),
                    EndResttime: $("#txtRestEndTime").val(),
                };
                WsSystem.SaveBanCi(basbanci,
                        function (result) {
                            if (result == null) {
                                JeffComm.alertSucc("保存成功", 500);
                               // alert("保存成功", "提示");
                                $('#selBanCiName').val("");                               
                                $('#txtStartTime').timespinner('setValue', '00:00');
                                $('#txtEndTime').timespinner('setValue', '00:00');
                                $('#txtRestStartTime').timespinner('setValue', '00:00');
                                $('#txtRestEndTime').timespinner('setValue', '00:00');
                                $("#grid").datagrid("reload");
                            } else {
                                alert(result, "提示");
                            }
                        }, function (err) {
                            // JeffComm.errorAlert(err.get_message(), "divMsg");
                            alert("保存失败:" + err.get_message(), "提示");

                        });
            });
            jQuery('#btnClear').click(function () {
                $('#selBanCiName').val("");
                $('#txtStartTime').timespinner('setValue', '00:00');
                $('#txtEndTime').timespinner('setValue', '00:00');
                $('#txtRestStartTime').timespinner('setValue', '00:00');
                $('#txtRestEndTime').timespinner('setValue', '00:00');
            });
        },    
           
        bindGrid: function () {
           // BasBanCi.codename = $("#txtCodeName").val();          
            $('#grid').datagrid('load', { });
        },
        delCustInfo: function () {
            if (BasBanCi.basbanciid == null || BasBanCi.basbanciid == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除","提示");
            } else {
                WsSystem.RemoveBanCi(BasBanCi.basbanciid,
                    function (result) {
                        if (result == null) {
                            BasBanCi.basbanciid = null;
                            JeffComm.alertSucc("删除成功", 500);
                                                   
                            $('#grid').datagrid('load', {  });
                        } else {
                            alert(result, "提示");
                                                     
                            $('#grid').datagrid('load', {  });
                        }
                    }, function (err) {
                        // JeffComm.errorAlert(err.get_message(), "divMsg");
                        alert("删除失败:" + err.get_message(), "提示");

                    });
            }
                
            $('#grid').datagrid('load', { });
        },
        ShowEditOrViewDialog:function(){
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            BasBanCi.basbanciid = rows[0].ID;
            $('#selBanCiName').val(rows[0].NAME);
            $('#txtStartTime').timespinner('setValue',rows[0].StartTime);
            $('#txtEndTime').timespinner('setValue', rows[0].EndTime);
            $("#txtRestStartTime").timespinner('setValue', rows[0].StartResttime);
            $("#txtRestEndTime").timespinner('setValue', rows[0].EndResttime);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },    
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '班次维护',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/LisBasBanCi',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',

                columns: [[ //选择
                     { title: 'ID', field: 'ID', width: BasBanCi.fixWidth(0.05),hidden:'true' },
                     { title: '班次名称', field: 'NAME', width: BasBanCi.fixWidth(0.09) },
                     { title: '开始时间', field: 'StartTime', width: BasBanCi.fixWidth(0.09) },
                     { title: '结束时间', field: 'EndTime', width: BasBanCi.fixWidth(0.09) },
                     { title: '时长', field: 'DURATION', width: BasBanCi.fixWidth(0.09) },
                      { title: '休息开始时间', field: 'StartResttime', width: BasBanCi.fixWidth(0.09) },
                     { title: '休息结束时间', field: 'EndResttime', width: BasBanCi.fixWidth(0.09) },
                     { title: '休息时长', field: 'DurationRest', width: BasBanCi.fixWidth(0.09) },
                     { title: '操作人', field: 'UpdatedBy', width: BasBanCi.fixWidth(0.09) },
                     {
                         title: '创建时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: BasBanCi.fixWidth(0.12)
                     }
                ]],
                toolbar: [ {
                      id: 'btnSearch',
                      text: '查询',
                      iconCls: 'icon-search',
                      handler: function () {
                          BasBanCi.bindGrid();
                      }
                  }, '-', {
                      id: 'btnAdd',
                      text: '新建班次',
                      iconCls: 'icon-add',
                      handler: function () {
                          $('#restartDialog').panel({ title: "新建班次" });
                          $('#btnNew').show();
                          $("#restartDialog").dialog('open');  //实现添加记录的页面
                      }
                  },
                '-', {
                    id: 'btnEdit',
                    text: '修改',
                    iconCls: 'icon-edit',
                    handler: function () {
                        var rows = $('#grid').datagrid('getSelections');  //得到所选择的行 
                        if (rows.length == 1) {
                            $('#restartDialog').panel({ title: "修改班次" });
                            $('#btnNew').hide();
                            BasBanCi.ShowEditOrViewDialog();//实现修改记录的方法
                        }
                        else {
                            JeffComm.alertErr("请选择一条数据进行修改");
                           // alert("请选择一条数据进行修改", "提示");
                        }
                    }
                },
                '-', {
                    id: 'btnDelete',
                    text: '删除',
                    iconCls: 'icon-remove',
                    handler: function () {
                        BasBanCi.delCustInfo();//实现直接删除数据的方法
                    }
                },
                '-', {
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    BasBanCi.basbanciid = rowData["ID"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
        }
    };

}();