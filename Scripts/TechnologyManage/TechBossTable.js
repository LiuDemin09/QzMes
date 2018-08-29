var TechBossTable = function () {
    return {
        init: function () {
            setInterval("TechBossTable.bindGrid()", 30000);//每隔30秒刷新一次
        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        bindGrid: function () {
            $('#grid').datagrid('load', { 
            });
            $('#grid1').datagrid('load', {});
        },
        bindGridTask: function () {
            $('#grid').datagrid('load', {
                drawingno: $('#txtQPartsNo').val()
            }); 
        },
        bindGridTasking: function () {
            $('#grid1').datagrid('load', {
                drawingno: $('#txtQingPartsNo').val()
            });
        },
        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '我的任务',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/FindDrawingCodeToTechnology',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: TechBossTable.fixWidth(0.1) },
                     { title: '客户名称', field: 'CustName', width: TechBossTable.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: TechBossTable.fixWidth(0.1) },
                     { title: '创建人', field: 'UpdatedBy', width: TechBossTable.fixWidth(0.1) },                     
                        {
                            title: '创建时间', field: 'CreatedDate', formatter: function (value, row, index) {
                        if (value != null & value != "") {
                            var unixTimestamp = new Date(value);
                            return unixTimestamp.toLocaleString();
                        }
                    }, width: TechBossTable.fixWidth(0.1)
                }
                ]],
                toolbar: [{
                        text: '零件图号<input type="text" id="txtQPartsNo"/>'
                    },

                    '-', 
                {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        TechBossTable.bindGridTask();
                    }
                },
                '-',{
                    id: 'btnReload',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid").datagrid("reload");
                    }
                }],
            });

            $('#grid1').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '进行中的任务进度',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/FindNoFinishTechnology',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 10,
                pageNumber: 1,
                pageList: [10, 20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择                     
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: TechBossTable.fixWidth(0.1) },
                     { title: '状态', field: 'StatusMemo', width: TechBossTable.fixWidth(0.1) },
                     { title: '产品名称', field: 'ProductName', width: TechBossTable.fixWidth(0.1) },
                     { title: '客户名称', field: 'CustName', width: TechBossTable.fixWidth(0.1) },
                     { title: '工艺工程师', field: 'ProcessName', width: TechBossTable.fixWidth(0.07) },
                     { title: '编程工程师', field: 'ProgramName', width: TechBossTable.fixWidth(0.07) },
                     { title: '工艺文件', field: 'ProcessFname', width: TechBossTable.fixWidth(0.07) },
                      {
                          title: '计划时间', field: 'PlanDate', formatter: function (value, row, index) {
                              if (value != null & value != "") {
                                  var unixTimestamp = new Date(value);
                                  return unixTimestamp.toLocaleString();
                              }
                          }, width: TechBossTable.fixWidth(0.1)
                      },
                      {
                          title: '提交时间', field: 'RealDate', formatter: function (value, row, index) {
                              if (value != null & value != "") {
                                  var unixTimestamp = new Date(value);
                                  return unixTimestamp.toLocaleString();
                              }
                          }, width: TechBossTable.fixWidth(0.1)
                      },
                     
                ]],
                toolbar: [{
                    text: '零件图号<input type="text" id="txtQingPartsNo"/>'
                },

                    '-',
                {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        TechBossTable.bindGridTasking();
                    }
                },
                '-', {
                    id: 'btnReload1',
                    text: '刷新',
                    iconCls: 'icon-reload',
                    handler: function () {
                        //实现刷新栏目中的数据
                        $("#grid1").datagrid("reload");
                    }
                }]//,
                //onDblClickRow: function (rowIndex, rowData) {
                //    location.href = "OutWH.aspx?" + "orderNo=" + rowData.OrderNo + "&partNo=" + rowData.PartsdrawingCode;
                //}

            });

            //回车事件
            $('#txtQPartsNo').keydown(function (e) {
                if (e.keyCode == 13) {
                    TechBossTable.bindGridTask();
                }
            });
            //回车事件
            $('#txtQingPartsNo').keydown(function (e) {
                if (e.keyCode == 13) {
                    TechBossTable.bindGridTasking();
                }
            });
        }
    };

}();