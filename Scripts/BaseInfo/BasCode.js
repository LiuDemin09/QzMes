var BasCode = function () {

    return {       
        grid: null,
        bascodeid: null,
        codename:null,
        //main function to initiate the module
        init: function () {
            //初始化序列号
            WsSystem.ListBindSeqNo(function (result) {
                JeffComm.fillSelect($("#selBindSeqNo"), result, true);
            });
            jQuery('#btnClose').click(function () {
                BasCode.bascodeid = null;
                $('#txtBasCodeName').val("");
                $('#txtBasCodeType').val("");
                $('#txtPrefix').val("");
                $("#txtBasCodeLen").val("");
                $('#restartDialog').window('close');
            });
            
            jQuery('#btnNew').click(function () {
                BasCode.bascodeid = null;
                $('#txtBasCodeName').val("");
                $('#txtBasCodeType').val("");
                $('#txtPrefix').val("");
                $("#txtBasCodeLen").val("");
            });
            jQuery('#btnSave').click(function () {
                var bascode = {
                    ID:BasCode.bascodeid,
                    NAME: $("#txtBasCodeName").val(),
                    TYPE: $("#txtBasCodeType").val(),
                    PREFIX: $("#txtPrefix").val(),
                    DateFormat: $("#selDateFormat").val(),
                    BindSequence: $("#selBindSeqNo").val(),
                    CodeLen: $("#txtBasCodeLen").val(),
                };
                WsSystem.SaveBasCode(bascode,
                        function (result) {
                            if (result == null) {
                                JeffComm.alertSucc("保存成功", 500);
                               // alert("保存成功", "提示");
                                $('#txtBasCodeName').val("");
                                $('#txtBasCodeType').val("");
                                $('#txtPrefix').val("");
                                $("#txtBasCodeLen").val("");
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
                $('#txtBasCodeName').val("");
                $('#txtBasCodeType').val("");
                $('#txtPrefix').val("");
                $("#txtBasCodeLen").val("");
            });
        },    
           
        bindGrid: function () {
            BasCode.codename = $("#txtCodeName").val();          
            $('#grid').datagrid('load', { codeName: BasCode.codename });
        },
        delCustInfo: function () {
            if (BasCode.bascodeid == null || BasCode.bascodeid == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
                //alert("请选择一条数据进行删除","提示");
            } else {
                WsSystem.RemoveBasCode(BasCode.bascodeid, 
                    function (result) {
                        if (result == null) {
                            BasCode.bascodeid = null;
                            JeffComm.alertSucc("删除成功", 500);
                           // alert("删除成功", "提示");
                            BasCode.codename = $("#txtCodeName").val();                            
                            $('#grid').datagrid('load', { codeName: BasCode.codename });
                        } else {
                            alert(result, "提示");
                            BasCode.codename = $("#txtCodeName").val();                           
                            $('#grid').datagrid('load', { codeName: BasCode.codename });
                        }
                    }, function (err) {
                        // JeffComm.errorAlert(err.get_message(), "divMsg");
                        alert("删除失败:" + err.get_message(), "提示");

                    });
            }
            BasCode.codename = $("#txtCodeName").val();            
            $('#grid').datagrid('load', { codeName: BasCode.codename });
        },
        ShowEditOrViewDialog:function(){
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            BasCode.bascodeid = rows[0].ID;
            $('#txtBasCodeName').val(rows[0].NAME);
            $('#txtBasCodeType').val(rows[0].TYPE);
            $('#txtPrefix').val(rows[0].PREFIX);
            $("#txtBasCodeLen").val(rows[0].CodeLen);
            $("#selDateFormat").val(rows[0].DateFormat);
            $("#selBindSeqNo").val(rows[0].BindSequence);
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },    
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '编码维护',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/LisBasCode',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
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
                     { title: 'ID', field: 'ID', width: BasCode.fixWidth(0.05),hidden:'true' },
                     { title: '编码名称', field: 'NAME', width: BasCode.fixWidth(0.09) },
                     { title: '编码类型', field: 'TYPE', width: BasCode.fixWidth(0.09) },
                     { title: '前缀字符', field: 'PREFIX', width: BasCode.fixWidth(0.09) },
                     { title: '日期格式', field: 'DateFormat', width: BasCode.fixWidth(0.09) },
                     { title: '绑定序列', field: 'BindSequence', width: BasCode.fixWidth(0.09) },
                     { title: '编码长度', field: 'CodeLen', width: BasCode.fixWidth(0.09) },
                     { title: '操作人', field: 'UpdatedBy', width: BasCode.fixWidth(0.09) },
                     {
                         title: '时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: BasCode.fixWidth(0.12)
                     }
                ]],
                toolbar: [{
                    text: '编码名称 <input type="text" id="txtCodeName" />'
                }, '-', {
                      id: 'btnSearch',
                      text: '查询',
                      iconCls: 'icon-search',
                      handler: function () {
                          BasCode.bindGrid();
                      }
                  }, '-', {
                      id: 'btnAdd',
                      text: '新建编码',
                      iconCls: 'icon-add',
                      handler: function () {
                          $('#restartDialog').panel({ title: "新建编码" });
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
                            $('#restartDialog').panel({ title: "修改编码" });
                            $('#btnNew').hide();
                            BasCode.ShowEditOrViewDialog();//实现修改记录的方法
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
                        BasCode.delCustInfo();//实现直接删除数据的方法
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
                    BasCode.bascodeid = rowData["ID"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
        }
    };

}();