var PassRateQuery = function () {
    return {
        grid: null,
        month: null,
        chart: null,
        title: null,
        titleD: null,
        subtitle: null,
        xAxis: null,
        yAxis: null,
        yAxisD: null,
        tooltip: null,
        tooltipD: null,
        plotOptions: null,
        credits: null,
        series: null,
        seriesD: null,
        json: null,
        jsonD: null,
        xData: null,
        passNum: null,
        failNum: null,
        returnNum: null,
        secNum: null,
        disNum:null,
        init: function () {

            var url = "../../services/WsSystem.asmx/FindMachineName";
            $.ajax({
                url: url,
                type: "post",
                cache: false,
                data: { month: PassRateQuery.month },
                dataType: "json",
                ifModified: false,
                success: function (result) {
                    var xdata = [];
                    var passnum = [];
                    var failnum = [];
                    var returnnum = [];
                    var secnum = [];
                    var disnum = [];
                    jQuery.each(result, function (m, obj) {
                        xdata.push(obj.WO); //动态取值
                        passnum.push(obj.PassCount);
                        failnum.push(obj.FailCount);
                        returnnum.push(obj.ReturnCount);
                        secnum.push(obj.SecondPass);
                        disnum.push(obj.DiscardCount);
                    });
                    PassRateQuery.xData = xdata;
                    PassRateQuery.passNum = passnum;
                    PassRateQuery.failNum = failnum;
                    PassRateQuery.returnNum = returnnum;
                    PassRateQuery.secNum = secnum;
                    PassRateQuery.disNum = disnum;
                    PassRateQuery.xAxis = {
                        crosshair: true,
                        categories: PassRateQuery.xData

                    };
                    PassRateQuery.tooltip = {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormatter: function () {
                            var x = this.x;
                            var y = this.y;
                            var tip= '<tr><td style="color:' + this.series.color + ';padding:0">' + this.series.name + ': </td>' +
                               '<td style="color:' + this.series.color + ';padding:0"><b>' + y + ' %</b></td></tr>' +
                               '<tr><td style="color:' + this.series.color + ';padding:0">数量: </td>'
                            if (this.series.name == "合格率") {
                                tip=tip+ '<td style="color:' + this.series.color + ';padding:0"><b>' + PassRateQuery.passNum[x] + '</b></td></tr>';
                            } else {
                                tip=tip+'<td style="color:' + this.series.color + ';padding:0"><b>' + PassRateQuery.failNum[x] + '</b></td></tr>';
                            }
                            return tip;
                      
                        },
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    };

                    PassRateQuery.tooltipD = {
                        headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                        pointFormatter: function () {
                            var x = this.x;
                            var y = this.y;
                            var tip = '<tr><td style="color:' + this.series.color + ';padding:0">' + this.series.name + ': </td>' +
                               '<td style="color:' + this.series.color + ';padding:0"><b>' + y + ' %</b></td></tr>' +
                               '<tr><td style="color:' + this.series.color + ';padding:0">数量: </td>';
                            if (this.series.name == "返工率") {
                                tip = tip + '<td style="color:' + this.series.color + ';padding:0"><b>' +PassRateQuery. returnNum[x] + '</b></td></tr>';
                            } else if (this.series.name == "让步率") {
                                tip = tip + '<td style="color:' + this.series.color + ';padding:0"><b>' + PassRateQuery.secNum[x] + '</b></td></tr>';
                            } else {
                                tip = tip + '<td style="color:' + this.series.color + ';padding:0"><b>' + PassRateQuery.disNum[x] + '</b></td></tr>';
                            }
                            return tip;
                        },
                        footerFormat: '</table>',
                        shared: true,
                        useHTML: true
                    };
                    PassRateQuery.series = PassRateQuery.create();
                    PassRateQuery.seriesD = PassRateQuery.createD();
                    PassRateQuery.json = {};
                    PassRateQuery.json.chart = PassRateQuery.chart;
                    PassRateQuery.json.title = PassRateQuery.title;
                    PassRateQuery.json.subtitle = PassRateQuery.subtitle;
                    PassRateQuery.json.tooltip = PassRateQuery.tooltip;
                    PassRateQuery.json.xAxis = PassRateQuery.xAxis;
                    PassRateQuery.json.yAxis = PassRateQuery.yAxis;
                    PassRateQuery.json.series = PassRateQuery.series;
                    PassRateQuery.json.plotOptions = PassRateQuery.plotOptions;
                    PassRateQuery.json.credits = PassRateQuery.credits;


                    PassRateQuery.jsonD = {};
                    PassRateQuery.jsonD.chart = PassRateQuery.chart;
                    PassRateQuery.jsonD.title = PassRateQuery.titleD;
                    PassRateQuery.jsonD.subtitle = PassRateQuery.subtitle;
                    PassRateQuery.jsonD.tooltip = PassRateQuery.tooltipD;
                    PassRateQuery.jsonD.xAxis = PassRateQuery.xAxis;
                    PassRateQuery.jsonD.yAxis = PassRateQuery.yAxisD;
                    PassRateQuery.jsonD.series = PassRateQuery.seriesD;
                    PassRateQuery.jsonD.plotOptions = PassRateQuery.plotOptions;
                    PassRateQuery.jsonD.credits = PassRateQuery.credits;
                    $('#container').highcharts(PassRateQuery.json);
                    $('#container1').highcharts(PassRateQuery.jsonD);
                    
                }
            });



            PassRateQuery.chart = {
                type: 'column'
            };
            PassRateQuery.title = {
                text: '产品一类品率分布图'
            };
            PassRateQuery.titleD = {
                text: '产品二类品率分布图'
            };
            PassRateQuery.subtitle = {
                text: '（以机床为单位）'
            };
            
 
            

           

            PassRateQuery.yAxis = {
                min: 0,
                max: 100,
                title: {
                    text: '百分比 (%)'
                }
            };
            PassRateQuery.yAxisD = {
                min: 0,
                title: {
                    text: '百分比 (%)'
                }
            };

            //var a =point.x;
           

            PassRateQuery.plotOptions = {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0,
                    dataLabels: {
                        enabled: true, // dataLabels设为true
                        style: {
                            color: '#D7DEE9'
                        },
                        formatter: function () {
                            return this.y + "%";  //返回百分比和个数
                        }
                    }
                }
            };


            PassRateQuery.credits = {
                enabled: false
            };

          



        },
       
        create: function () {
            var series = new Array();

            $.ajax({
                type: "POST",
                url: '../../services/WsSystem.asmx/MachineYieldChart',
                data: { month: PassRateQuery.month },
                async: false, //表示同步，如果要得到ajax处理完后台数据后的返回值，最好这样设置
                success: function (result) {
                    var obj = JSON.parse(result);

                    for (var i in obj) {
                        if (i == "HC") {
                            for (var j in obj[i]) {
                                var name = obj[i][j].name;
                                var data = obj[i][j].data;
                                series.push({ "name": name, "data": data });

                            }

                        }
                    }
                }
            }, false);  //false表示“遮罩”，前台不显示“请稍后”进度提示
            // alert(series);
            return series;

        },


        createD: function () {
            var series = new Array();

            $.ajax({
                type: "POST",
                url: '../../services/WsSystem.asmx/MachineYieldChart',
                data: { month: PassRateQuery.month },
                async: false, //表示同步，如果要得到ajax处理完后台数据后的返回值，最好这样设置
                success: function (result) {
                    var obj = JSON.parse(result);

                    for (var i in obj) {
                        if (i == "DETAIL") {
                            for (var j in obj[i]) {
                                var name = obj[i][j].name;
                                var data = obj[i][j].data;
                                series.push({ "name": name, "data": data });

                            }

                        }
                    }
                }
            }, false);  //false表示“遮罩”，前台不显示“请稍后”进度提示
            // alert(series);
            return series;

        },
        bindGrid: function () {
            PassRateQuery.month = $('#selMonth').val();
            $('#grid').datagrid('load', { month: PassRateQuery.month });
            PassRateQuery.init();

        },

        fixWidth: function (percent) {
            return document.body.clientWidth * percent;
        },


        initGrid: function () {
            $('#grid').datagrid({   //定位到Table标签，Table标签的ID是grid
                title: '机床良率查询',
                iconCls: 'icon-view',
                url: '../../services/WsSystem.asmx/QueryMachineYield',
               // data: { month: PassRateQuery.month },
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
                    { title: '机床名称', field: 'WO', width: PassRateQuery.fixWidth(0.1) },
                   { title: '产品数量', field: 'QUANTITY', width: PassRateQuery.fixWidth(0.07) },
                   { title: '一类品数量', field: 'PassCount', width: PassRateQuery.fixWidth(0.07) },
                   { title: '二类品数量', field: 'FailCount', width: PassRateQuery.fixWidth(0.07) },
                   { title: '返工数量', field: 'ReturnCount', width: PassRateQuery.fixWidth(0.07) },
                   { title: '让步数量', field: 'SecondPass', width: PassRateQuery.fixWidth(0.07) },
                   { title: '废品数量', field: 'DiscardCount', width: PassRateQuery.fixWidth(0.07) },
                   { title: '一类品率', field: 'PassRate', width: PassRateQuery.fixWidth(0.07) },
                   { title: '二类品率', field: 'FailRate', width: PassRateQuery.fixWidth(0.07) },
                   { title: '返工率', field: 'ReturnRate', width: PassRateQuery.fixWidth(0.07) },
                   { title: '让步率', field: 'SecPassRate', width: PassRateQuery.fixWidth(0.07) },
                   { title: '废品率', field: 'DiscardRate', width: PassRateQuery.fixWidth(0.07) },
                ]],
                toolbar: [{
                    text: '月份 <select tabindex="-1" name="selMonth" id="selMonth"><option value="0">请选择</option> </select>'
                },
                '-', {
                    id: 'btnSearch',
                    text: '查询',
                    iconCls: 'icon-search',
                    handler: function () {
                        PassRateQuery.bindGrid();
                    }
                }, '-', {
                    id: 'btnExpoet',
                    text: '导出',
                    iconCls: 'icon-save',
                    handler: function () {
               
                        window.open('../../Pages/QualityManage/export/PassRateQueryExport.aspx?month=' + $('#selMonth').val(), '', 'height=400,width=430, resizable=1,scrollbars=0,status=1,menubar=no,toolbar=no,location=no,menu=no,left=" + button.Style["left"] + " , top=" + button.Style["top"] + "');
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