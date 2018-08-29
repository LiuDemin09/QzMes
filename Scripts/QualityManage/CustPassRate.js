var CustPassRate = function () {
    return {
        grid: null,

        init: function () {

            CustPassRate.piePassRate();

        },

        piePassRate: function () {
            var materialdata;
            var drilldownSerie;
            var brandsData = [], drilldownSeries = [];

            $.ajax({
                url: '../../services/WsSystem.asmx/CustRateProportion',
                type: 'POST',
                data: { month: CustPassRate.month },
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    materialdata = data[0].propertion;
                    for (var i = 0; i < materialdata[0].data.length; i++) {
                        brandsData.push({
                            name: materialdata[0].data[i].name,
                            y: materialdata[0].data[i].y,
                            drilldown: materialdata[0].data[i].drilldown
                        });
                    }
                    drilldownSerie = data[0].drilldown;
                    for (var i = 0; i < drilldownSerie.length; i++) {

                        drilldownSeries.push({
                            name: drilldownSerie[i].name,
                            id: drilldownSerie[i].id,
                            data: drilldownSerie[i].data
                        });
                    }

                    // 创建图例
                    $('#container').highcharts({
                        chart: {
                            type: 'column'
                        },

                        title: {
                            text: '二类品数量分布图',
                            style: {
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },

                        subtitle: {
                            text: '单击每个客户查看具体数据',
                            style: {
                                fontSize: '12pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        xAxis: {
                            type: 'category'
                        },
                        yAxis: {
                            title: {
                                text: '数量'
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                dataLabels: {
                                    enabled: true,
                                    //distance:-50,
                                    //format: '{point.name}: {point.y:.1f}',
                                    style: {
                                        color: '#ffffff',
                                        fontSize: '12pt',
                                        textShadow: '0px 0px 0px black'
                                    }
                                }

                                //borderWidth: 0,
                                //dataLabels: {
                                //    enabled: true,
                                //    format: '{point.y:.1f}%'
                                //}
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:16px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b> <br/>'
                        },
                        series: [{
                            name: '客户',
                            colorByPoint: true,
                            data: brandsData
                        }],
                        drilldown: {
                            series: drilldownSeries
                        }
                    });
                }
            });
        },


        bindGrid: function () {
            CustPassRate.month = $('#selMonth').val();
            $('#grid').datagrid('load', { month: CustPassRate.month });
            CustPassRate.piePassRate();

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '客户良率查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryCustYield',
                width: function () { return document.body.clientWidth * 0.6 },
                height: function () { return document.body.clientHeight * 0.9 },
                nowrap: true,
                fit: true,
                autoRowHeight: false,
                singleSelect: true,
                //pagination: true,
                rownumbers: true,
                //pageSize: 15,
                //pageNumber: 1,
                //pageList: [15, 20, 30, 40, 50],
                //beforePageText: '第',//页数文本框前显示的汉字   
                //afterPageText: '页    共 {pages} 页',
                //displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                columns: [[ //选择
                    { title: '委托单位', field: 'WO', width: CustPassRate.fixWidth(0.1) },
                   { title: '产品数量', field: 'QUANTITY', width: CustPassRate.fixWidth(0.07) },
                   { title: '一类品数量', field: 'PassCount', width: CustPassRate.fixWidth(0.07) },
                   { title: '二类品数量', field: 'FailCount', width: CustPassRate.fixWidth(0.07) },
                   { title: '返工数量', field: 'ReturnCount', width: CustPassRate.fixWidth(0.07) },
                   { title: '让步数量', field: 'SecondPass', width: CustPassRate.fixWidth(0.07) },
                   { title: '废品数量', field: 'DiscardCount', width: CustPassRate.fixWidth(0.07) },
                   { title: '一类品率', field: 'PassRate', width: CustPassRate.fixWidth(0.07) },
                   { title: '二类品率', field: 'FailRate', width: CustPassRate.fixWidth(0.07) },
                   { title: '返工率', field: 'ReturnRate', width: CustPassRate.fixWidth(0.07) },
                   { title: '让步率', field: 'SecPassRate', width: CustPassRate.fixWidth(0.07) },
                   { title: '废品率', field: 'DiscardRate', width: CustPassRate.fixWidth(0.07) },
                ]],
                toolbar: [{
                    text: '月份 <select tabindex="-1" name="selMonth" id="selMonth"><option value="0">请选择</option> </select>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        CustPassRate.bindGrid();
                    }
                }
                , '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {

                        window.open('../../Pages/QualityManage/export/CustPassRateExport.aspx?month=' + $('#selMonth').val(), '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
                    }
                }
                ],
                onLoadError: function (error) {
                    alert(error.responseText);
                }
            });

            var obj = document.getElementById('selMonth');
            obj.options.add(new Option("一月", "01"));
            obj.options.add(new Option("二月", "02"));
            obj.options.add(new Option("三月", "03"));
            obj.options.add(new Option("四月", "04"));
            obj.options.add(new Option("五月", "05"));
            obj.options.add(new Option("六月", "06"));
            obj.options.add(new Option("七月", "07"));
            obj.options.add(new Option("八月", "08"));
            obj.options.add(new Option("九月", "09"));
            obj.options.add(new Option("十月", "10"));
            obj.options.add(new Option("十一月", "11"));
            obj.options.add(new Option("十二月", "12"));

        }
    };
}();