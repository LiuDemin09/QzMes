var KanBan2 = function () {

    return {
        grid: null,
        squenceName: null,
        init: function () {
            KanBan2.getkuaiyadata(); 
            KanBan2.getyoujiadata();
            KanBan2.getoma850data();
            KanBan2.get1160data();
            KanBan2.get1160_1data();
            KanBan2.get1160_2data();
            KanBan2.get1160_3data();
            KanBan2.getchexidata();
            KanBan2.getwuzhoudata();
            KanBan2.getdayoujiadata();
            KanBan2.getsajodata(); 
           // setInterval('KanBan2.getdata()', 10000);//每隔10秒执行一次 
            //KanBan2.pieMaterial();//上左
            //KanBan2.pieOut();//上中
            //KanBan2.pieYeild();//上右
            //KanBan2.barOut();//中右
            //KanBan2.barYeild();//下右
            //KanBan2.barMaterial();//下左
            //setInterval('KanBan2.pieMaterial()', 30000);//每隔30秒执行一次
            //setInterval('KanBan2.pieOut()', 90000);//每隔90秒执行一次
            //setInterval('KanBan2.pieYeild()', 60000);//每隔60秒执行一次
            //setInterval('KanBan2.barOut()', 90000);//每隔90秒执行一次
            //setInterval('KanBan2.barYeild()', 60000);//每隔60秒执行一次
           
        },
       
        getkuaiyadata: function () {
            var params = {
                machinename: '快亚' 
            };
            $.ajax({ 
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data:params,
            dataType: "json", 
            cache: false, 
            timeout: 10000, 
            error: function(){}, 
            success: function (data) {
                var vv = data[0].wo;
                $("#curstatus1-1-out").text("今日数量:"+data[0].curqty);
                $("#curstatus1-1-wo").text("当前工单:"+data[0].wo);
                $("#curstatus1-1-pdn").text("当前图号:" + data[0].partsdrawingno);
                $("#operator1-1-name").text(data[0].workername);
                var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername+".jpg";
                $("#operator1-1-img").attr("src", imgsrc);
                //画圆环图
                $('#machineout1-1').highcharts({
                    chart: {
                        plotBackgroundColor: null,
                        plotBorderWidth: null,
                        plotShadow: false,
                        backgroundColor: 'rgba(255, 255, 255, 0)',
                        spacing: [50, -10, 40, 0]
                    },
                    colors: [
                      'yellow',//第一个颜色                       
                      'blue',//第二个颜色                       
                       'red', //。。。。
                         '#492970',
                         '#f28f43',
                         '#77a1e5',
                         '#c42525',
                         '#a6c96a'
                    ],
                    title: {
                        floating: true,
                        text: data[0].qty + '/' + data[0].planqty,
                        style: {
                            color: 'white'
                        }
                    },
                    tooltip: {
                        pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                    },
                    plotOptions: {
                        pie: {
                            size: 150,
                            innerSize:'10',
                            allowPointSelect: true,
                            cursor: 'pointer',
                            dataLabels: {
                                enabled: true,
                                distance:-10,
                                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                style: {
                                    color: 'white',
                                    fontSize:'12pt'
                                }
                            },
                            point: {
                                events: {
                                    mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                        chart.setTitle({
                                            text: e.target.name + '\t' + e.target.y + ' %'
                                        });
                                    }
                                    
                                }
                            },
                        }
                    },
                    series: [{
                        type: 'pie',
                        innerSize: '80%',
                        name: '产出比',
                        data: [
                            ['待产', parseInt(data[0].planqty)-parseInt(data[0].qty)],
                                                     
                            ['产出', parseInt(data[0].qty)]
                        ]
                    }]
                }, function (c) {
                    // 环形图圆心
                    var centerY = c.series[0].center[1],
                        titleHeight = parseInt(c.title.styles.fontSize);
                    c.setTitle({
                        y: centerY + titleHeight / 2
                    });
                    chart = c;
                });
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
        getyoujiadata: function () {
            var params = {
                machinename: '友嘉'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus1-2-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus1-2-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus1-2-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator1-2-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator1-2-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout1-2').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        getoma850data: function () {
            var params = {
                machinename: '小欧马'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus1-3-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus1-3-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus1-3-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator1-3-name").text(data[0].workername); 
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator1-3-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout1-3').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        get1160data: function () {
            var params = {
                machinename: '欧马A-1'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus1-4-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus1-4-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus1-4-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator1-4-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator1-4-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout1-4').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        get1160_1data: function () {
            var params = {
                machinename: '友嘉'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus2-1-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus2-1-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus2-1-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator2-1-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator2-1-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout2-1').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        get1160_2data: function () {
            var params = {
                machinename: '欧马A-1'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus2-2-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus2-2-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus2-2-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator2-2-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator2-2-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout2-2').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        get1160_3data: function () {
            var params = {
                machinename: '友嘉'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus2-3-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus2-3-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus2-3-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator2-3-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator2-3-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout2-3').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        getchexidata: function () {
            var params = {
                machinename: '欧马A-1'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus2-4-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus2-4-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus2-4-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator2-4-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator2-4-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout2-4').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        getwuzhoudata: function () {
            var params = {
                machinename: '友嘉'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus3-1-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus3-1-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus3-1-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator3-1-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator3-1-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout3-1').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        getdayoujiadata: function () {
            var params = {
                machinename: '欧马A-1'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus3-2-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus3-2-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus3-2-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator3-2-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator3-2-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout3-2').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        },
        getsajodata: function () {
            var params = {
                machinename: '友嘉'
            };
            $.ajax({
                url: '../../services/WsSystem.asmx/QueryCurMachineInfo',
                type: 'POST',
                data: params,
                dataType: "json",
                cache: false,
                timeout: 10000,
                error: function () { },
                success: function (data) {
                    var vv = data[0].wo;
                    $("#curstatus3-3-out").text("今日数量:" + data[0].curqty);
                    $("#curstatus3-3-wo").text("当前工单:" + data[0].wo);
                    $("#curstatus3-3-pdn").text("当前图号:" + data[0].partsdrawingno);
                    $("#operator3-3-name").text(data[0].workername);
                    var imgsrc = "../../images/emp/" + parseInt(data[0].worker) + data[0].workername + ".jpg";
                    $("#operator3-3-img").attr("src", imgsrc);
                    //画圆环图
                    $('#machineout3-3').highcharts({
                        chart: {
                            plotBackgroundColor: null,
                            plotBorderWidth: null,
                            plotShadow: false,
                            backgroundColor: 'rgba(255, 255, 255, 0)',
                            spacing: [50, -10, 40, 0]
                        },
                        colors: [
                          'yellow',//第一个颜色                       
                          'blue',//第二个颜色                       
                           'red', //。。。。
                             '#492970',
                             '#f28f43',
                             '#77a1e5',
                             '#c42525',
                             '#a6c96a'
                        ],
                        title: {
                            floating: true,
                            text: data[0].qty + '/' + data[0].planqty,
                            style: {
                                color: 'white'
                            }
                        },
                        tooltip: {
                            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                        },
                        plotOptions: {
                            pie: {
                                size: 150,
                                innerSize: '10',
                                allowPointSelect: true,
                                cursor: 'pointer',
                                dataLabels: {
                                    enabled: true,
                                    distance: -10,
                                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                                    style: {
                                        color: 'white',
                                        fontSize: '12pt'
                                    }
                                },
                                point: {
                                    events: {
                                        mouseOver: function (e) {  // 鼠标滑过时动态更新标题
                                            chart.setTitle({
                                                text: e.target.name + '\t' + e.target.y + ' %'
                                            });
                                        }

                                    }
                                },
                            }
                        },
                        series: [{
                            type: 'pie',
                            innerSize: '80%',
                            name: '产出比',
                            data: [
                                ['待产', parseInt(data[0].planqty) - parseInt(data[0].qty)],

                                ['产出', parseInt(data[0].qty)]
                            ]
                        }]
                    }, function (c) {
                        // 环形图圆心
                        var centerY = c.series[0].center[1],
                            titleHeight = parseInt(c.title.styles.fontSize);
                        c.setTitle({
                            y: centerY + titleHeight / 2
                        });
                        chart = c;
                    });
                }
            });
        }
    };

}();