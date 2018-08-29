var KanBan1 = function () {

    return {
        grid: null,
        squenceName: null,
        init: function () {
            //var myDate = new Date();
            //$('todaytime').text(myDate.toLocaleDateString());
            //$('todaytime').val(myDate.toLocaleDateString());
           // KanBan1.getdata();//今日产出
           // setInterval('KanBan1.getdata()', 10000);//每隔10秒执行一次 
            KanBan1.pieMaterial();//上左
            KanBan1.pieOut();//上中
            KanBan1.pieYeild();//上右
            KanBan1.barOut();//中右
            KanBan1.barYeild();//下右
            KanBan1.barMaterial();//下左
            setInterval('KanBan1.pieMaterial()', 30000);//每隔30秒执行一次
            setInterval('KanBan1.pieOut()', 90000);//每隔90秒执行一次
            setInterval('KanBan1.pieYeild()', 60000);//每隔60秒执行一次
            setInterval('KanBan1.barOut()', 90000);//每隔90秒执行一次
            setInterval('KanBan1.barYeild()', 60000);//每隔60秒执行一次
           
        },
       
        getdata:function () {
            $.ajax({ 
                url: '../../services/WsSystem.asmx/QueryCurOutQty',
                type: 'POST', 
            dataType: "json", 
            cache: false, 
            timeout: 10000, 
            error: function(){}, 
            success: function(data){ 
                KanBan1.setNumber(data[0].count);
            } 
        }); 
        },
        setNumber:function(number){
            var n = String(number),len = n.length;
            var dom = $("#a");
            //如果新的数字短于当前的，要移除多余的i
            if(dom.find("i").length > len){
                dom.find("i:gt(" + (len - 1) + ")").remove();
            }
         
            //移除千分位分隔符
            dom.find("b").remove();
 
            //开始填充每一位
            for(var i=0;i<len;++i){
                //位数不足要补
                if(dom.find("i").length < len){
                    dom.append("<i></i>");
                }
                var obj = dom.find("i").eq(i);
                var y = -40 * parseInt(n.charAt(i), 10);
 
                //加分隔符
                if(i < len - 1 && (len - i - 1) % 3 == 0)
                    $("<b></b>").insertAfter(obj);
 
                //利用动画变换数字
                obj.animate({ backgroundPositionY:y+"px" }, 350);
            }
        },
        //发料状态占比
        pieMaterial: function () {
            var materialdata;
            var drilldownSerie;
            var brandsData = [], drilldownSeries=[];
            //var vvv;
            //$.post("../../services/WsWareHouse.asmx/QueryMaterialsProportion", function (result) {
            //      vvv = result;
            //});
            $.ajax({
                url: '../../services/WsWareHouse.asmx/QueryMaterialsProportion',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    materialdata = data[0].propertion;
                    for (var i = 0; i < materialdata[0].data.length; i++)
                    {
                        brandsData.push({
                            name: materialdata[0].data[i].name,
                            y: materialdata[0].data[i].y,
                            drilldown: materialdata[0].data[i].drilldown
                        });
                    }
                    drilldownSerie = data[0].drilldown;
                    for (var i = 0; i < drilldownSerie.length; i++) {
                        /*var value = [];
                        for (var j = 0; j < drilldownSerie[i].data.length; j++)
                        {
                            value.push([drilldownSerie[i].data[j][0], drilldownSerie[i].data[j][1]]);
                        }*/
                        drilldownSeries.push({
                            name: drilldownSerie[i].name,
                            id: drilldownSerie[i].id,
                            data: drilldownSerie[i].data
                        });
                    }

                    // 创建图例
                    $('#container').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            type: 'pie'
                        },
                        colors: [
                       'blue',//第一个颜色                       
                       'yellow',//第二个颜色                       
                        'red', //。。。。
                          '#492970',
                          '#f28f43',
                          '#77a1e5',
                          '#c42525',
                          '#a6c96a'
                        ],
                        title: {
                            text: '发料和未发料占比',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        subtitle: {
                            text: '单击每个占比查看详细信息',
                            style: {
                                color: '#ffffff',
                                fontSize: '12pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        plotOptions: {
                            series: {
                                dataLabels: {
                                    enabled: true,
                                    //distance:-50,
                                    format: '{point.name}: {point.y:.1f}%',
                                    style: {
                                        color: '#ffffff',
                                        fontSize: '8pt',
                                        textShadow:'0px 0px 0px black'
                                    }
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:16px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> <br/>'
                        },
                        series: [{
                            name: '物料',
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
     
        //产出状态占比
        pieOut: function () {
            var materialdata;
            var drilldownSerie;
            var brandsData = [], drilldownSeries = [];            
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryOutQtyProportion',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 20000,
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
                    $('#containerOut').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            type: 'pie',
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow:false
                        },
                        colors: [
                      'blue',//第一个颜色                       
                      'yellow',//第二个颜色
                       'red', //。。。。
                         '#492970',
                         '#f28f43',
                         '#77a1e5',
                         '#c42525',
                         '#a6c96a'
                        ],
                        title: {
                            text: '产出和待产出占比',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        subtitle: {
                            text: '单击每个占比查看详细信息',
                            style: {
                                color: '#ffffff',
                                fontSize: '12pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        plotOptions: {
                            series: {
                                dataLabels: {
                                    enabled: true,
                                    //distance:-50,
                                    format: '{point.name}: {point.y:.1f}%',
                                    style: {
                                        color: '#ffffff',
                                        fontSize: '8pt',
                                        textShadow: '0px 0px 0px black'
                                    }
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:16px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> <br/>'
                        },
                        series: [{
                            name: '产品',
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
        //良率
        pieYeild: function () {
            var materialdata;
            var drilldownSerie;
            var brandsData = [], drilldownSeries = [];
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryOutYeildProportion',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 20000,
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
                    $('#containerYield').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            type: 'pie',
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false
                        },
                        colors: [
                      'blue',//第一个颜色                       
                      'yellow',//第二个颜色
                       'red', //。。。。
                         '#492970',
                         '#f28f43',
                         '#77a1e5',
                         '#c42525',
                         '#a6c96a'
                        ],
                        title: {
                            text: '各类品质占比',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        subtitle: {
                            text: '单击每个占比查看详细信息',
                            style: {
                                color: '#ffffff',
                                fontSize: '12pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                       
                        plotOptions: {
                            series: {
                                dataLabels: {
                                    enabled: true,
                                    //distance:-50,
                                    format: '{point.name}: {point.y:.1f}%',
                                    style: {
                                        color: '#ffffff',
                                        fontSize: '8pt',
                                        textShadow: '0px 0px 0px black'
                                    }
                                }
                            }
                        },
                        tooltip: {
                            headerFormat: '<span style="font-size:16px">{series.name}</span><br>',
                            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}%</b> <br/>'
                        },
                        series: [{
                            name: '品质',
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
        //柱状图折线图产出
        barOut: function () {
            var categoriesdata =[],qtyRateData=[]; 
            var planData = [], qtyData = [];
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryPlanAndOutQtyProportion',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 20000,
                error: function () { },
                success: function (data) {
                    categoriesdata = data[0].categories[0].data;
                    planData = data[0].planqty[0].data;
                    qtyData = data[0].qty[0].data;
                    qtyRateData = data[0].qtyrate[0].data;

                    // 创建图例
                    Highcharts.setOptions({
                        colors: ['yellow', '#50B432', '#ED561B', '#DDDF00', '#24CBE5', '#64E572', '#FF9655', '#FFF263', '#ff0000', '#6AF9C4']
                    });
                    $('#containerOutDetail').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            //type: 'column'
                            zoomType:'xy'
                        },
                        title: {
                            text: '计划和产出实时统计图',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        //subtitle: {
                        //    text: '数据来源: WorldClimate.com'
                        //},
                        xAxis: [{
                            categories:categoriesdata,                           
                            crosshair: true,
                            labels: {
                                style: {
                                    color: Highcharts.getOptions().colors[6]
                                }
                            }
                        }],
                        yAxis: [{
                            labels:{
                                format:'{value}%',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            },
                            title:{
                                text:'百分比',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            }
                            },{
                            title:{
                                text:'产出',
                                style:{
                                    color:Highcharts.getOptions().colors[0]
                                }
                            },
                            labels:{
                                format:'{value} 件',
                                style:{
                                    color:Highcharts.getOptions().colors[0]
                                }
                            },
                            opposite:true
                            }],                            
                        tooltip: {
                            //headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                            //pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            //'<td style="padding:0"><b>{point.y:.0f} 件</b></td></tr>',
                            //footerFormat: '</table>',
                            shared: true//,
                            //useHTML: true
                        },
                        plotOptions: {
                            column: {
                                pointPadding: 0.2,
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,//柱状图末端显示数字
                                    allowOverlap: true,
                                    style: {
                                        color: Highcharts.getOptions().colors[7]
                                    }
                                }
                            },
                            spline: {
                                dataLabels: {
                                    enabled: true,
                                    formatter: function () {
                                        return this.point.y + '%'; 
                                    },
                                    style: {
                                        color: Highcharts.getOptions().colors[9]
                                    }
                                }

                            }
                            
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'left',
                            x: 70,
                            verticalAlign: 'top',
                            y: 0,
                            floating: true,
                            itemStyle : {
                                'color':Highcharts.getOptions().colors[2],
                                'fontSize' : '10px'
                            },
                            
                            backgroundColor: 'rgba(255, 255, 255, 0)'//(Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                        },
                        series: [{
                            name: '计划',
                            type: 'column',
                            yAxis:1,
                            data: planData,//[49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
                            tooltip:{valueSuffix:' 件'}
                        }, {
                            name: '产出',
                            type: 'column',
                            yAxis: 1,
                            data: qtyData,//[83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
                            tooltip: { valueSuffix: ' 件' }
                        }, {
                            name: '百分比',
                            type: 'spline',
                            data: qtyRateData,//[7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
                            tooltip: {
                                valueSuffix: '%'
                            }
                        }]
                    });
                }
            });
        }
        ,
        //柱状图折线图良率
        barYeild: function () {
            var categoriesdata =[],qtyRateData=[]; 
            var planData = [], qtyData = [];
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryYeildQtyProportion',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 20000,
                error: function () { },
                success: function (data) {
                    categoriesdata = data[0].categories[0].data;
                    planData = data[0].planqty[0].data;
                    qtyData = data[0].qty[0].data;
                    qtyRateData = data[0].qtyrate[0].data;

                    // 创建图例
                    $('#containerYeildDetail').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            //type: 'column'
                            zoomType:'xy'
                        },
                        title: {
                            text: '生产品质实时统计图',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        //subtitle: {
                        //    text: '数据来源: WorldClimate.com'
                        //},
                        xAxis: [{
                            categories:categoriesdata,                           
                            crosshair: true,
                            labels: {
                                style: {
                                    color: Highcharts.getOptions().colors[6]
                                }
                            }
                        }],
                        yAxis: [{
                            labels:{
                                format:'{value}%',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            },
                            title:{
                                text:'百分比',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            }
                        },{
                            title:{
                                text:'产出',
                                style:{
                                    color:Highcharts.getOptions().colors[0]
                                }
                            },
                            labels:{
                                format:'{value} 件',
                                style:{
                                    color:Highcharts.getOptions().colors[0]
                                }
                            },
                            opposite:true
                        }],                            
                        tooltip: {
                            //headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                            //pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                            //'<td style="padding:0"><b>{point.y:.0f} 件</b></td></tr>',
                            //footerFormat: '</table>',
                            shared: true//,
                            //useHTML: true
                        },
                        plotOptions: {
                            column: {
                                pointPadding: 0.2,
                                borderWidth: 0,
                                dataLabels: {
                                    enabled: true,//柱状图末端显示数字
                                    allowOverlap: true,
                                    style: {
                                        color: Highcharts.getOptions().colors[7]
                                    }
                                }
                            },
                            spline: {
                                dataLabels: {
                                    enabled: true,
                                    formatter: function () {
                                        return this.point.y + '%';
                                    },
                                    style: {
                                        color: Highcharts.getOptions().colors[9]
                                    }
                                }

                            }
                            
                        },
                        legend: {
                            layout: 'vertical',
                            align: 'left',
                            x: 70,
                            verticalAlign: 'top',
                            y: 0,
                            floating: true,
                            itemStyle: {
                                'color': Highcharts.getOptions().colors[2],
                                'fontSize': '10px'
                            },
                            backgroundColor: 'rgba(255, 255, 255, 0)'//(Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                        },
                        series: [{
                            name: '产出',
                            type: 'column',
                            yAxis:1,
                            data: planData,//[49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
                            tooltip:{valueSuffix:' 件'}
                        }, {
                            name: '一类数',
                            type: 'column',
                            yAxis: 1,
                            data: qtyData,//[83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
                            tooltip: { valueSuffix: ' 件' }
                        }, {
                            name: '百分比',
                            type: 'spline',
                            data: qtyRateData,//[7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
                            tooltip: {
                                valueSuffix: '%'
                            }
                        }]
                    });
                }
            });
        },
         //柱状图备料
        barMaterial: function () {
            var categoriesdata = [], qtyRateData = [];
            var planData = [], qtyData = [];
            $.ajax({
                url: '../../services/WsWareHouse.asmx/QueryBackUpMaterialDetail',
                type: 'POST',
                dataType: "json",
                cache: false,
                timeout: 20000,
                error: function () { },
                success: function (data) {
                    categoriesdata = data[0].categories[0].data;//图号
                    planData = data[0].planqty[0].data;//需求数量
                    qtyData = data[0].qty[0].data;//已发数量
                    qtyRateData = data[0].qtyrate[0].data;//库存数量

                    // 创建图例
                    $('#containerMaterialDetail').highcharts({
                        chart: {
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            type: 'column' 
                        },
                        title: {
                            text: '发料实时统计图',
                            style: {
                                color: '#ffffff',
                                fontSize: '18pt',
                                padding: 5,
                                zIndex: 9999
                            }
                        },
                        xAxis: [{
                            categories: categoriesdata,
                            crosshair: true,
                            labels: {
                                style: {
                                    color: Highcharts.getOptions().colors[6]
                                }
                            }
                        }],
                        yAxis: {                             
                            min: 0,
                            title: {
                                text: '发料数量(件)',
                                align:'high',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            },
                            labels: {
                                overflow:'justify',
                                style:{
                                    color:Highcharts.getOptions().colors[2]
                                }
                            }
                        },
                        tooltip: {
                            valueSuffix:'件'
                        },
                        plotOptions: {
                            column: {
                                dataLabels: {
                                    enabled: true,//柱状图末端显示数字
                                    allowOverlap: true,
                                    style: {
                                        color: Highcharts.getOptions().colors[7]
                                    }
                                }
                            }

                        },
                        legend: {
                            layout: 'vertical',
                            align: 'right',
                            x: -450,
                            verticalAlign: 'top',
                            y: 0,
                            floating: true,
                            itemStyle: {
                                'color': Highcharts.getOptions().colors[2],
                                'fontSize': '10px'
                            },
                            //borderWidth:1,
                            backgroundColor: 'rgba(255, 255, 255, 0)'//(Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
                        },
                        credits:{
                            enabled:false
                        },
                        series: [{
                            name: '需求数量',                            
                            data: planData,//[49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4]
                             
                        }, {
                            name: '已发数量',                            
                            data: qtyData,//[83.6, 78.8, 98.5, 93.4, 106.0, 84.5, 105.0, 104.3, 91.2, 83.5, 106.6, 92.3]
                            
                        }, {
                            name: '库存数量',                            
                            data: qtyRateData,//[7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6],
                            
                        }]
                    });
                }
            });
        }
    };

}();