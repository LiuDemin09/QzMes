var TechnologyQuery = function () {
    return {       
        grid: null,
        PartsDrawingNo:null,
        //main function to initiate the module
        init: function () {          
             
            //初始化工程师 
            WsSystem.ListUserByRole(function (result) {
                JeffComm.fillSelect($("#selEngineer"), result, true);
            });
            
        },
        bindGrid: function () {
            TechnologyQuery.partsdrawingno = $("#txtPartsdrawingno").val();
            $('#grid').datagrid('load', { PartsDrawingNo: TechnologyQuery.partsdrawingno, Status: $("#selStatus").val(), Engineer: $("#selEngineer").val(), StartTime: $("#txtStartTime").val(), EndTime: $("#txtEndTime").val() });
        },      
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },

        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '工艺查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryTechnology',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                remoteSort: false,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,               
                columns: [[ //选择
                     { title: 'ID', field: 'ID', width: TechnologyQuery.fixWidth(0.1),hidden: 'true' },
                     { title: '零件图号', field: 'PARTSDRAWINGNO', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                     { title: '客户名称', field: 'CustName', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                     { title: '产品名称', field: 'ProductName', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                     { title: '状态', field: 'StatusMemo', width: TechnologyQuery.fixWidth(0.1),sortable:true },
                     { title: '工艺工程师', field: 'ProcessName', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                     { title: '编程工程师', field: 'ProgramName', width: TechnologyQuery.fixWidth(0.1), sortable: true },
                      {
                          title: '工艺文件', field: 'ProcessFname', width: TechnologyQuery.fixWidth(0.06),
                          formatter: function (value, row, index) {
                              var temp = row.ProcessFname;
                              if (temp == null)
                              {
                                  temp = "";
                              }
                              return '<a style="color:blue" href="' + row.ProcessPath + '", target="_blank">' + temp + '</a>';
                              // alert(row.ProcessPath);
                          }
                      },
                      { title: '工艺文件路径', field: 'ProcessPath', width: TechnologyQuery.fixWidth(0.06), hidden: 'true' },

                     {
                         title: '工艺计划完成时间', field: 'PlanDate', formatter: function (value, row, index) {
                             if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechnologyQuery.fixWidth(0.1)
                     },
                      {
                          title: '工艺实际完成时间', field: 'RealDate', formatter: function (value, row, index) {
                              if (value == null) return null;
                              var unixTimestamp = new Date(value);
                              return unixTimestamp.toLocaleString();
                          }, width: TechnologyQuery.fixWidth(0.1)
                      },
                     {
                         title: '编程实际完成时间', field: 'DevrealDate', formatter: function (value, row, index) {
                              if(value==null)return null;
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechnologyQuery.fixWidth(0.1)
                     },
                     
                     { title: '操作人', field: 'UpdatedBy', width: TechnologyQuery.fixWidth(0.1) },
                     {
                         title: '时间', field: 'CreatedDate', formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: TechnologyQuery.fixWidth(0.1),sortable:true
                     },
                ]],
                toolbar: [{
                    text: '零件图号<input type="text" id="txtPartsdrawingno"/>'
                }, '-', {
                    text: '状态 <select tabindex="-1" name="selStatus" id="selStatus"><option value="">请选择</option><option value="0">新建任务</option> <option value="1">分派工艺</option><option value="2">提交工艺</option><option value="3">审核工艺</option>'
                        + '<option value="4">分派编程</option><option value="5">提交编程</option><option value="6">审核编程</option><option value="7">维护工时</option><option value="8">工艺完成</option><option value="9">工艺驳回</option><option value="10">编程驳回</option></select>'
                },  '-', {
                    text: '工程师 <select tabindex="-1" name="selEngineer" id="selEngineer"><option value="">请选择</option> </select>'
                }, '-', {
                    text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间 <input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        TechnologyQuery.bindGrid();
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
                onLoadError: function (error) {
                    alert(error.responseText);
                },
                onClickRow: function (rowIndex, rowData) {
                    // $('#txtMSN').val(rowData["MSN"]);
                    TechnologyQuery.partsdrawingno = rowData["PARTSDRAWINGNO"];
                    TechnologyQuery.bindGridDetail();
                },
            });
            $('#grid').datagrid('getPager').pagination({
                pageSize: 20,
                pageNumber: 1,
                pageList: [20, 30, 40, 50],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                // layout:['refresh']
            });
        },
        bindGridDetail: function () {
            $('#griddetail').datagrid('load', { PartsDrawingNo: TechnologyQuery.partsdrawingno });
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
                pageSize: 5,
                pageNumber: 1,
                pageList: [5, 10, 20],
                beforePageText: '第',//页数文本框前显示的汉字   
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[
                    { title: 'ID', field: 'ID', width: TechnologyQuery.fixWidth(0.1), hidden: 'true' },

                     { title: '工序号', field: 'ProcessNo', width: TechnologyQuery.fixWidth(0.1) },
                     { title: '工序名称', field: 'ProcessName', width: TechnologyQuery.fixWidth(0.1) },
                     { title: '工时', field: 'UnitTime', width: TechnologyQuery.fixWidth(0.05) },
                     {
                         title: '程序文件', field: 'ProgramFname', width: TechnologyQuery.fixWidth(0.08),
                         formatter: function (value, row, index) {
                             return '<a style="color:blue" href="' + row.ProgramPath + '", target="_blank">' + row.ProgramFname + '</a>';
                             // alert(row.ProcessPath);
                         }
                     },
                    { title: '程序文件路径', field: 'ProgramPath', width: TechnologyQuery.fixWidth(0.06), hidden: 'true' },
                     {
                         title: '刀具文件', field: 'ToolName', width: TechnologyQuery.fixWidth(0.08),
                         formatter: function (value, row, index) {
                             return '<a style="color:blue" href="' + row.ToolPath + '", target="_blank">' + row.ToolName + '</a>';
                             // alert(row.ProcessPath);
                         }
                     },
                    { title: '刀具文件路径', field: 'ToolPath', width: TechnologyQuery.fixWidth(0.06), hidden: 'true' }

                ]]
            });
        }
    };
}();