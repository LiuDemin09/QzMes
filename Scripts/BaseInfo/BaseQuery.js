var BaseQuery = function () {

    return {
        grid: null,
        baseName: null,
        startTime: null,
        endTime: null,
        init: function () {
            //初始化基本信息名称
            WsSystem.ListBaseName(function (result) {
                JeffComm.fillSelect($("#selBaseName"), result, true);
            });

        },
        bindGrid: function () {
            BaseQuery.baseName = $("#selBaseName").val().trim();
            BaseQuery.startTime = $("#txtStartTime").val().trim();
            BaseQuery.endTime = $("#txtEndTime").val().trim();
            var oDate1 = new Date(BaseQuery.startTime);
            var oDate2 = new Date(BaseQuery.endTime);
            if (oDate1.getTime() > oDate2.getTime()) {
                JeffComm.alertErr("开始时间不能大于结束时间");
                //alert("开始时间不能大于结束时间");
                return;
            }
            $('#grid').datagrid('load', { baseName: BaseQuery.baseName, startTime: BaseQuery.startTime, endTime: BaseQuery.endTime });

        },
       
        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid

                title: '基本查询',
                iconCls: 'icon-view',
                url: '../../services/WsBasic.asmx/FindBaseByCode',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                striped: true,//设置为true将交替显示行背景。
                autoRowHeight: false,
                singleSelect: true,
                pagination: true,
                rownumbers: true,
                  //pageSize: 5,
                  //pageNumber: 1,
                  //pageList: [5, 20, 30, 40, 50],
                  //beforePageText: '第',//页数文本框前显示的汉字   
                  //afterPageText: '页    共 {pages} 页',
                  //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                     { title: '信息编号', field: 'CODE', width: BaseQuery.fixWidth(0.15) },
                     { title: '信息名称', field: 'NAME', width: BaseQuery.fixWidth(0.15) },
                     { title: '子信息编号', field: 'SubCode', width: BaseQuery.fixWidth(0.15) },
                     { title: '子信息名称', field: 'SubName', width: BaseQuery.fixWidth(0.15) },
                     { title: '操作人', field: 'UpdatedBy', width: BaseQuery.fixWidth(0.15) },
                     {
                         title: '时间', field: 'CreatedDate',  formatter: function (value, row, index) {
                             var unixTimestamp = new Date(value);
                             return unixTimestamp.toLocaleString();
                         }, width: BaseQuery.fixWidth(0.15)
                     },
                ]],
                toolbar: [{
                    text: '信息名称<select tabindex="-1" name="selBaseName" id="selBaseName"><option value="0">请选择</option> </select>'
                }, '-', {
                     text: '开始时间 <input type="text" id="txtStartTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                },
                 '-', {
                     text: '结束时间<input type="text" id="txtEndTime" onfocus="WdatePicker({dateFmt:\'yyyy-MM-dd HH:mm:ss\'})" class="Wdate"/>'
                 },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        BaseQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
                        window.open('../../Pages/BaseInfo/export/BaseExport.aspx?startTime=' + $('#txtStartTime').val()
                    + '&endTime=' + $('#txtEndTime').val() + '&baseName=' + $('#selBaseName').val() 
                    + "", '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }],
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