var FailReasonRate = function () {
    return {
        grid: null,
        month: null,
        detailChart: null,
        init: function () {

            FailReasonRate.ReasonRate();
        },

        ReasonRate: function () {
            var materialdata;
            var drilldownSerie;
            var brandsData = [], drilldownSeries = [];

            $.ajax({
                url: '../../services/WsSystem.asmx/FailTypeProportion',
                type: 'POST',
                data: { month: FailReasonRate.month },
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                   // alert(data);
                    

                    // 创建图例
                    $('#container').highcharts({
                        chart: {
                            zoomType: 'x'
                        },
                        title: {
                            text: '不合格原因分布图'
                        },
                        subtitle: {
                            text: document.ontouchstart === undefined ?
                            '鼠标拖动可以选择时间段' : '手势操作进行缩放'
                        },
                        xAxis: {
                            type: 'datetime',
                            dateTimeLabelFormats: {
                                millisecond: '%H:%M:%S.%L',
                                second: '%H:%M:%S',
                                minute: '%H:%M',
                                hour: '%H:%M',
                                day: '%m-%d',
                                week: '%m-%d',
                                month: '%Y-%m',
                                year: '%Y'
                            }
                        },
                        tooltip: {
                            dateTimeLabelFormats: {
                                millisecond: '%H:%M:%S.%L',
                                second: '%H:%M:%S',
                                minute: '%H:%M',
                                hour: '%H:%M',
                                day: '%Y-%m-%d',
                                week: '%m-%d',
                                month: '%Y-%m',
                                year: '%Y'
                            }
                        },
                        yAxis: {
                            title: {
                                text: '数量'
                            }
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            verticalAlign: 'middle',
                            borderWidth: 0
                        },
                        plotOptions: {
                            area: {
                                fillColor: {
                                    linearGradient: {
                                        x1: 0,
                                        y1: 0,
                                        x2: 0,
                                        y2: 1
                                    },
                                    stops: [
                                        [0, Highcharts.getOptions().colors[0]],
                                        [1, Highcharts.Color(Highcharts.getOptions().colors[0]).setOpacity(0).get('rgba')]
                                    ]
                                },
                                marker: {
                                    radius: 2
                                },
                                lineWidth: 1,
                                states: {
                                    hover: {
                                        lineWidth: 1
                                    }
                                },
                                threshold: null
                            }
                        },
                        series: data
             

                    });
                }
            });
        }
     

    };
}();