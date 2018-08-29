var DevelopmentCheck = function () {
    return {
        grid: null,        
        Partsdrawing: null,
        status:5,
        myArray:new Array(),
        init: function () {
        },
        bindGrid: function () {
            DevelopmentCheck.Partsdrawing = $("#txtPartCode").val().trim();            
            $('#grid').datagrid('load', { PartsDrawingNo: DevelopmentCheck.Partsdrawing, Status: DevelopmentCheck.status });
        },

        agreeDevelop: function () {

            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item){

               // names.push(item.ORDER_NO + "," + item.PARTSDRAWING_CODE);
                names.push(item.PARTSDRAWINGNO);
                });

            //alert(names.join(";"));
            WsSystem.AgreeDevelop(names.join(";"),
                   function (result) {
                       if (result == "OK") {
                           JeffComm.alertSucc("保存成功", 500);
                           //alert("保存成功", "提示");
                           $("#grid").datagrid("reload");
                       } else {
                           alert(result, "提示");
                       }
                   }, function (err) {
                       // JeffComm.errorAlert(err.get_message(), "divMsg");
                       alert("保存失败:" + err.get_message(), "提示");

                   });
        },
        RejectDevelop: function () {

            var checkedItems = $('#grid').datagrid('getChecked');
            var names = [];
            $.each(checkedItems, function (index, item) {

                // names.push(item.ORDER_NO + "," + item.PARTSDRAWING_CODE);
                names.push(item.PARTSDRAWINGNO);
            });

            //alert(names.join(";"));
            WsSystem.RejectDevelop(names.join(";"),
                   function (result) {
                       if (result == "OK") {
                           JeffComm.alertSucc("保存成功", 500);
                          // alert("保存成功", "提示");
                           $("#grid").datagrid("reload");
                       } else {
                           alert(result, "提示");
                       }
                   }, function (err) {
                       // JeffComm.errorAlert(err.get_message(), "divMsg");
                       alert("保存失败:" + err.get_message(), "提示");

                   });
        },
    
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },
        //ShowEditOrViewDialog: function () {
        //    var rows = $('#grid').datagrid('getSelections');  //得到所选择的行
        //    DevelopmentCheck.Partsdrawing = rows[0].PARTSDRAWINGNO;
            
        //    //TechTaskSubmit.bindGridRoute();//绑定路由信息 
        //    $('#griddetail').datagrid('load', { PartsDrawingNo: rows[0].PARTSDRAWINGNO });
             
        //},

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '我的看板',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnologyInfo',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                queryParams: { Status: DevelopmentCheck.status },
               // singleSelect: true,
                   pagination: true,
                rownumbers: true,
                  pageSize: 20,
                  pageNumber: 1,
                  pageList: [ 20, 30, 40, 50],
                  beforePageText: '第',//页数文本框前显示的汉字   
                  afterPageText: '页    共 {pages} 页',
                  displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                  columns: [[ //选择
                      { field: 'ck', checkbox: true },
                      //{ title: 'ID', field: 'ID', width: DevelopmentCheck.fixWidth(0.09), hidden: 'true' },
                    
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: DevelopmentCheck.fixWidth(0.08) },
                     { title: '客户名称', field: 'CustName', width: DevelopmentCheck.fixWidth(0.06) },
                     { title: '客户代码', field: 'CustCode', width: DevelopmentCheck.fixWidth(0.06) },
                     { title: '产品名称', field: 'ProductName', width: DevelopmentCheck.fixWidth(0.05) },
                     { title: '状态', field: 'StatusMemo', width: DevelopmentCheck.fixWidth(0.05) },
                     { title: '工艺工程师', field: 'ProcessName', width: DevelopmentCheck.fixWidth(0.06) },
                      {
                          title: '工艺文件', field: 'ProcessFname', width: DevelopmentCheck.fixWidth(0.06),
                          formatter: function (value, row, index) {
                              return '<a style="color:blue" href="' + row.ProcessPath + '", target="_blank">' + row.ProcessFname + '</a>';
                             // alert(row.ProcessPath);
                          }
                      },
                      { title: '工艺文件路径', field: 'ProcessPath', width: DevelopmentCheck.fixWidth(0.06), hidden: 'true' },
                      { title: '编程工程师', field: 'ProgramName', width: DevelopmentCheck.fixWidth(0.06) },
                     {
                         title: '计划完成时间', field: 'DevplanDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: DevelopmentCheck.fixWidth(0.09)
                     },
                     {
                         title: '提交时间', field: 'DevrealDate', formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         },  width: DevelopmentCheck.fixWidth(0.09) },
                     { title: '创建人', field: 'UpdatedBy', width: DevelopmentCheck.fixWidth(0.05) },
                     {
                         title: '时间', field: 'CreatedDate',formatter: function (value, row, index) {
                             if (value != null & value != "") {
                                 var unixTimestamp = new Date(value);
                                 return unixTimestamp.toLocaleString();
                             }
                         }, width: DevelopmentCheck.fixWidth(0.09)
                     }
                ]],
                toolbar: [{
                    text: '零件图号 <input type="text" id="txtPartCode"/>'
                }, '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        DevelopmentCheck.bindGrid();
                    }
                },
                '-', {
                    id: 'btnPublish',
                    text: '审核',
                    iconCls: 'icon-ok',
                    handler: function () {
                        DevelopmentCheck.agreeDevelop();
                    }
                },
               '-', {
                   id: 'btnReject',
                   text: '驳回',
                   iconCls: 'icon-remove',
                   handler: function () {
                       DevelopmentCheck.RejectDevelop();
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
                }, '-', {
                    id: 'btnBack',
                    text: '返回',
                    iconCls: 'icon-back',
                    handler: function () {
                        window.location.href = "TechBossTable.aspx";
                    }
                }],
                onClickRow: function (rowIndex, rowData) {
                    // $('#txtMSN').val(rowData["MSN"]);
                    DevelopmentCheck.partsdrawingno = rowData["PARTSDRAWINGNO"];
                    DevelopmentCheck.bindGridDetail();
                },
               

            });
        },
        bindGridDetail: function () {
            $('#griddetail').datagrid('load', { PartsDrawingNo: DevelopmentCheck.partsdrawingno });
        },
        initGridDetail: function () { 
            $("#griddetail").datagrid({
                //height: 300,
                //width: 450,
                title: '工序信息',
                collapsible: true,
                singleSelect: true,
                url: '../../services/WsSystem.asmx/QueryDevelopmentDetails',
                fit: true,

                autoRowHeight: false,
                // singleSelect: true,
                pagination: true,
                rownumbers: true,
                pageSize: 20,
                pageNumber: 1,
                pageList: [20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[
                    { title: 'ID', field: 'ID', width: DevelopmentCheck.fixWidth(0.1), hidden: 'true' },

                     { title: '工序号', field: 'ProcessNo', width: DevelopmentCheck.fixWidth(0.1) },
                     { title: '工序名称', field: 'ProcessName', width: DevelopmentCheck.fixWidth(0.1) },
                     { title: '工时', field: 'UnitTime', width: DevelopmentCheck.fixWidth(0.05) },
                     {
                         title: '程序文件', field: 'ProgramFname', width: DevelopmentCheck.fixWidth(0.08),
                        formatter: function (value, row, index) {
                            return '<a style="color:blue" href="' + row.ProgramPath + '", target="_blank">' + row.ProgramFname + '</a>';
                            // alert(row.ProcessPath);
                        }
                    },
                    { title: '程序文件路径', field: 'ProgramPath', width: DevelopmentCheck.fixWidth(0.06), hidden: 'true' },
                     {
                         title: '刀具文件', field: 'ToolName', width: DevelopmentCheck.fixWidth(0.08),
                         formatter: function (value, row, index) {
                             return '<a style="color:blue" href="' + row.ToolPath + '", target="_blank">' + row.ToolName + '</a>';
                             // alert(row.ProcessPath);
                         }
                     },
                    { title: '刀具文件路径', field: 'ToolPath', width: DevelopmentCheck.fixWidth(0.06), hidden: 'true' }

                ]]
            });
        }

    };

}();