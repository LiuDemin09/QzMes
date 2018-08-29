var SequenceNo = function () {
    return {
        grid: null,
        seqid: null,
        seqName: null,
        //main function to initiate the module
        init: function () {
            
            jQuery('#btnClose').click(function () {
                SequenceNo.seqid = null;
                $('#txtSequenceNoName').val("");
                $('#txtProductSequence').val("");
                $('#txtSequenceLen').val("");                
                $('#restartDialog').window('close');
            });

            jQuery('#btnNew').click(function () {
                SequenceNo.seqid = null;
                $('#txtSequenceNoName').val("");
                $('#txtProductSequence').val("");
                $('#txtSequenceLen').val("");
            });
            jQuery('#btnSave').click(function () {
                if ($("#txtSequenceNoName").val().trim() == null || $("#txtSequenceNoName").val().trim() == "")
                {
                    JeffComm.alertErr("序列号名不能为空");
                    //alert("序列号名不能为空");
                    return;
                }
                if ($("#txtProductSequence").val().trim() == null || $("#txtProductSequence").val().trim() == "") {
                    JeffComm.alertErr("产品系列不能为空");
                    //alert("产品系列不能为空");
                    return;
                }
                if ($("#txtSequenceLen").val().trim() == null || $("#txtSequenceLen").val().trim() == "") {
                    JeffComm.alertErr("数字长度不能为空");
                    //alert("数字长度不能为空");
                    return;
                }
                var sequenceno = {
                    ID: SequenceNo.seqid,
                    SeqName: $("#txtSequenceNoName").val().trim(),
                    FAMILY: $("#txtProductSequence").val(),
                    DigitalLen: $("#txtSequenceLen").val(),
                    DigitalType: $("#selDigitalType").val(),
                    IncreaseMode: $("#selIncreaseMode").val(),
                    DigitalTypeMemo: $("#selDigitalType").find("option:selected").text(),
                    IncreaseModeMemo: $("#selIncreaseMode").find("option:selected").text(),
                };
                WsSystem.SaveSequenceNo(sequenceno,
                        function (result) {
                            if (result == null) {
                                JeffComm.alertSucc("保存成功", 500);
                               // alert("保存成功", "提示");
                                $('#txtSequenceNoName').val("");
                                $('#txtProductSequence').val("");
                                $('#txtSequenceLen').val("");
                                $("#grid").datagrid("reload");
                            } else {
                                alert(result, "提示");
                            }
                        }, function (err) {
                            // JeffComm.errorAlert(err.get_message(), "divMsg");
                            alert("保存失败" + err.get_message(), "提示");

                        });
            });
            jQuery('#btnClear').click(function () {
                $('#txtSequenceNoName').val("");
                $('#txtProductSequence').val("");
                $('#txtSequenceLen').val("");
            });
        },

        bindGrid: function () {
            SequenceNo.seqName = $("#txtSeqName").val().trim();
            $('#grid').datagrid('load', { seqName: SequenceNo.seqName });
        },
        delCustInfo: function () {
            if (SequenceNo.seqid == null || SequenceNo.seqid == "") {
                JeffComm.alertErr("请选择一条数据进行删除");
               // alert("请选择一条数据进行删除", "提示");
            } else {
                WsSystem.RemoveSequence(SequenceNo.seqid,
                    function (result) {
                        if (result == null) {
                            SequenceNo.seqid = null;
                            JeffComm.alertSucc("删除成功", 500);
                           // alert("删除成功", "提示");
                            SequenceNo.seqName = $("#txtSeqName").val();
                            $('#grid').datagrid('load', { seqName: SequenceNo.seqName });
                        } else {
                            alert(result, "提示");
                            SequenceNo.seqName = $("#txtSeqName").val();
                            $('#grid').datagrid('load', { seqName: SequenceNo.seqName });
                        }
                    }, function (err) {
                        // JeffComm.errorAlert(err.get_message(), "divMsg");
                        alert("删除失败:" + err.get_message(), "提示");

                    });
            }
            SequenceNo.seqName = $("#txtSeqName").val().trim();
            $('#grid').datagrid('load', { seqName: SequenceNo.seqName });
        },
        ShowEditOrViewDialog: function () {
            var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
            SequenceNo.seqid = rows[0].ID;
            $('#txtSequenceNoName').val(rows[0].SeqName);
            $('#txtProductSequence').val(rows[0].FAMILY);
            $('#txtSequenceLen').val(rows[0].DigitalLen);
            $("#selDigitalType").val(rows[0].DigitalType);
            $("#selIncreaseMode").val(rows[0].IncreaseMode);       
            $("#restartDialog").dialog('open');
        },
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '序列号维护',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/ListBasSequence',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit:true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                //pageSize: 15,
                //pageNumber: 1,
                //pageList: [15, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',

                columns: [[ //选择
                     { title: 'ID', field: 'ID', width: SequenceNo.fixWidth(0.05), hidden: 'true' },
                     { title: '序列号名称', field: 'SeqName', width: SequenceNo.fixWidth(0.09) },
                     { title: '产品系列', field: 'FAMILY', width: SequenceNo.fixWidth(0.09) },
                     { title: '数字长度', field: 'DigitalLen', width: SequenceNo.fixWidth(0.09) },
                     { title: '数字类型值', field: 'DigitalType', width: SequenceNo.fixWidth(0.09), hidden: 'true' },
                     { title: '增加模式值', field: 'IncreaseMode', width: SequenceNo.fixWidth(0.09), hidden: 'true' },
                     { title: '数字类型', field: 'DigitalTypeMemo', width: SequenceNo.fixWidth(0.09) },
                      { title: '增长模式', field: 'IncreaseModeMemo', width: SequenceNo.fixWidth(0.09) },
                     { title: '操作人', field: 'UpdatedBy', width: SequenceNo.fixWidth(0.09) },
                     {
                         title: '时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: SequenceNo.fixWidth(0.12)
                     }
                ]],
                toolbar: [{
                    text: '序列号名称 <input type="text" id="txtSeqName" />'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        SequenceNo.bindGrid();
                    }
                }, '-', {
                    id: 'btnAdd',
                    text: '新建',
                    iconCls: 'icon-add',
                    handler: function () {
                        $('#restartDialog').panel({ title: "新建序列号" });
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
                            $('#restartDialog').panel({ title: "修改序列号" });
                            $('#btnNew').hide();
                            SequenceNo.ShowEditOrViewDialog();//实现修改记录的方法
                        } else {
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
                        SequenceNo.delCustInfo();//实现直接删除数据的方法
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
                    SequenceNo.seqid = rowData["ID"];
                    // ShowEditOrViewDialog();
                },
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 15,
                pageNumber: 1,
                pageList: [15, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        }
    };
}();