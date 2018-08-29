var JeffComm = function () {
    return {
        //﹚竡北ン   
        isIE: (!!window.ActiveXObject || "ActiveXObject" in window) ? true : false,
        localPath: "",
        token: "",
        newfuseContorl: null,
        downloadUrl: "",
        getControl: function () {
            if (JeffComm.newfuseContorl == null) {
                JeffComm.IsPlugIn('dlControl');
                if (!JeffComm.isContorl) {
                    return false;
                }
                if (JeffComm.isIE) {//IE Broswer
                    try {
                        JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                    } catch (e) {
                        alert(e.message);
                        return false;
                    }
                } else {
                    JeffComm.newfuseContorl = document.getElementById("objControls");
                }
            }

            return JeffComm.newfuseContorl;
        },
        //填充List
        //jqElm:要填充的list Jquery對象
        //data:後臺獲取的TextValue對象數組
        //withBlank:是否需要添加一個空白項目
        fillSelect: function (jqElm, data, withBlank) {
            jqElm.find("option").remove();
            if (withBlank == true) {
                jqElm.append("<option selected value=''>" + StringRes.SelectBlank + "</option>");
            }
            for (var i = 0; i < data.length; i++) {
                if(data[i]!=null)
                {
                jqElm.append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
                }
            }
        },
        //轉換後臺返回的XML對象成字符串
        parseXMLNode: function (node) {
            if (node.xml) {
                return node.xml;
            }
            var oSerializer = new XMLSerializer();
            return oSerializer.serializeToString(node);
        },
        //頁面上顯示錯誤信息
        //msg:要顯示的錯誤消息
        //elmId:顯示的DIV ID
        errorAlert: function (msg, elmId) {
            JeffComm.clearAlert();
            $('#' + elmId).removeClass("alert-success");
            $('#' + elmId).addClass("alert-error");
            $('#' + elmId).addClass("alert-warning");
            $('span', $('#' + elmId)).html(msg);
            $('#' + elmId).show();
        },
        //頁面上顯示成功信息
        //msg:要顯示的錯誤消息
        //elmId:顯示的DIV ID
        succAlert: function (msg, elmId) {
            JeffComm.clearAlert();
            $('#' + elmId).removeClass("alert-warning");
            $('#' + elmId).removeClass("alert-error");
            $('#' + elmId).addClass("alert-success");            
            $('span', $('#' + elmId)).html(msg);
            $('#' + elmId).show();
        },
        //清除頁面上的消息
        clearAlert: function () {
            $('.alert-error').hide();
            $('.alert-success').hide();
        },
        //耞琌杆础ン
        IsPlugIn: function (divId) {
            isContorl = true;
            if (JeffComm.newfuseContorl == null) {
                if (JeffComm.isIE) {//IE Broswer
                    try {
                        JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                    } catch (e) {
                        JeffComm.newfuseContorl = null;
                        isContorl = false;
                        alert(e.message);

                    }
                } else {
                    JeffComm.newfuseContorl = document.getElementById("objControls");
                    try {
                        JeffComm.newfuseContorl.ListPrinters();
                    } catch (e) {
                        JeffComm.newfuseContorl = null;
                        isContorl = false;
                    }
                }
            }
            if (!isContorl) {
                $('#' + divId).modal({ backdrop: "static" });
                if (JeffComm.isIE) {
                    $("#" + divId + " ul").html('<li><a href="../Download/SetupNewFuseControls.msi" target="_blank"><i class="icon-download-alt"></i> Download ActiveX</a></li>');
                } else {
                    $("#" + divId + " ul").html('<li><a href="../Download/SetupNewFuseControls.msi" target="_blank"><i class="icon-download-alt"></i> Download ActiveX</a></li> <li><a href="../Download/ffactivex-setup-r39.exe" target="_blank"><i class="icon-download-alt"></i> Download Ffactivex</a></li>');
                }
                return;
            }
        },

        //莉籓把计ゴ兵絏
        CommonLabelPrint: function (tplPath, data, labelId, copyNum) {
            var ret = AjaxInterface.GetLabelParameters(data, labelId).value;           
            if (ret.Result != null && ret.Result == "ERR") {
                JeffComm.errorAlert(ret.Message, "divMsg");
                return false;
            }
            var tems = ret.Message.split("^");
            var paraNames = new Array();
            var paraValues = new Array();
            for (var n = 0; n < tems.length; n++) {
                var tem = tems[n].split("=");
                paraNames.push(tem[0]);
                paraValues.push(tem[1]);
            }
            //var ret = true;
            //$.ajax({
            //    async: false,
            //    cache: false,
            //    type: "post",
            //    timeout: 1000 * 60 * 3,
            //    contentType: "application/json",
            //    url: "../Services/WsCommon.asmx/GetLabelParameters",
            //    data: "{data:'" + data + "', labelId:'" + labelId + "'}",
            //    dataType: "json",
            //    error: function (xml) {
            //        JeffComm.errorAlert('Fail to get label parameters.', "divMsg");
            //        ret = false;
            //    },
            //    success: function (result) {
            //        //钵盽薄猵
            //        if (result.d.Result != "OK") {
            //            JeffComm.errorAlert(result.d.Message, "divMsg");
            //            ret = false;
            //            return false;
            //        }
            //        var tems = result.d.Message.split("^");
            //        var paraNames = new Array();
            //        var paraValues = new Array();
            //        for (var n = 0; n < tems.length; n++) {
            //            var tem = tems[n].split("=");
            //            paraNames.push(tem[0]);
            //            paraValues.push(tem[1]);
            //        }

            //        //莉Label ID
            //        var labelId1 = tplPath.substring(tplPath.lastIndexOf("\\") + 1, tplPath.lastIndexOf("."));

            //        var labelEx = JeffComm.GetLabelExtendName(labelId1);
            //        if (labelEx == "CS") {
            //            ret = JeffComm.NewfuseCSPrint(tplPath, "", paraNames, paraValues, copyNum);
            //        }
            //        else if (labelEx == "BTW") {
            //            tplPath = tplPath.replace('lab', "btw");
            //            ret = JeffComm.NewfuseBTWPrint(tplPath, "", paraNames, paraValues, copyNum);
            //        }
            //        else if (labelEx == "ZPL") {
            //            tplPath = tplPath.replace('lab', "txt");
            //            ret = JeffComm.NewfuseZPLPrint(tplPath, "", paraNames, paraValues, copyNum);
            //        }
            //        else {
                        ret = JeffComm.NewfuseCSPrint(tplPath, "", paraNames, paraValues, copyNum);
                   // }
               // }
           // });

            return ret;
        },

        NewfuseCSPrint: function (tplPath, printerName, paraNames, paraValues, num) {
            if (JeffComm.newfuseContorl == null) {
                if (JeffComm.isIE) {//IE Broswer
                    try {
                        JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                    } catch (e) {
                        alert(e.message);
                        return false;
                    }
                } else {
                    JeffComm.newfuseContorl = document.getElementById("objControls");
                }
            }
            //锣種才腹矪瞶 by jeffrey start
            //paraValues = paraValues.replace(',', '~');
            var tempValues = new Array();
            for (var i = 0; i < paraValues.length; i++) {
                if (!isNaN(paraValues[i])) {
                    paraValues[i] = paraValues[i];
                } else {
                    if (paraValues[i] != null && paraValues[i] != "") {
                        paraValues[i] = paraValues[i].replace(/,/g, "~");
                    }
                }
                tempValues.push(paraValues[i]);
            }
            //锣種才腹矪瞶 by jeffrey end
            return JeffComm.newfuseContorl.PrintCSLabel(tplPath, printerName, paraNames.join(","), tempValues.join(","), num);

        },
        //硄筁CodeSoft舱ンゴ兵絏
        //tplPath:家ゅン隔畖
        //params:把计
        PrintCodeSoft: function (tplPath, params) {

            try {
                try {
                    objCodeSoftApp = new ActiveXObject("lppx.Application");
                } catch (e) {
                    alert(e.message + "创建CodeSoft对象失败,请确认争取安装了CodeSoft程序,并设置了ActiveX相应的权限");
                    return false;
                }

                objCodeSoftDoc = objCodeSoftApp.ActiveDocument;
                objCodeSoftDoc.Open(tplPath);
                if (objCodeSoftDoc.Variables.Count == 0) {
                    alert("CodeSoft文件打开失败或者文件沒有定义变量");
                    return false;
                }

                for (i = 1; i <= objCodeSoftDoc.Variables.Count; i++) {
                    var vItem = objCodeSoftDoc.Variables.item(i);
                    if (params.has(vItem.Name)) {
                        vItem.Value = params.get(vItem.Name);
                    } else {
                        vItem.Value = "0";
                    }
                }

                objCodeSoftDoc.Printlabel(1);
            } catch (e) {
                alert(e.message);
                return false;
            } finally {
                if (objCodeSoftDoc) {
                    objCodeSoftDoc.FormFeed;
                    objCodeSoftDoc.Close;
                    objCodeSoftDoc = null;
                }
                if (objCodeSoftApp) {
                    objCodeSoftApp.Quit();
                    objCodeSoftApp = null;
                }
            }

            return true;
        },

        //更家
        NewfuseDownload: function (url, localPath) {
            if (JeffComm.newfuseContorl == null) {
                if (JeffComm.isIE) {//IE Broswer
                    try {
                        JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                    } catch (e) {
                        alert(e.message);
                        return false;
                    }
                } else {
                    JeffComm.newfuseContorl = document.getElementById("objControls");
                }
            }

            //莉Label ID
            arrayObj = new Array();
            arrayObj = url.split('=');
            var labelId = arrayObj[1];

            //ノめ獺
            url = url + "&t=" + JeffComm.GetToken() + "&m=BASE64";

            var labelEx = JeffComm.GetLabelExtendName(labelId);
            var ExtendName = "lab";
            if (labelEx == "CS") {
                ExtendName = "lab";
            } else if (labelEx == "BTW") {
                ExtendName = "btw";
            } else if (labelEx == "ZPL") {
                ExtendName = "txt";
            }
            localPath = localPath.replace('lab', ExtendName);

            return JeffComm.newfuseContorl.DownloadTpl2(url, localPath);
        },
        //莉Label耎甶嘿
        GetLabelExtendName: function (labelId) {
            //莉把计
            var ret = "";
            $.ajax({
                async: false,
                cache: false,
                type: "post",
                timeout: 1000 * 60 * 10,
                contentType: "application/json",
                url: "../Services/WsCommon.asmx/GetLabelExtendName",
                data: "{labelId:'" + labelId + "'}",
                dataType: "json",
                error: function (xml) {
                    JeffComm.errorAlert('Fail to get label parameters.', "divMsg");
                    ret = false;
                },
                success: function (result) {
                    if (result.d == null) {
                        JeffComm.errorAlert(result.d.Message, "divMsg");
                        return false;
                    }
                    else { ret = result.d; }
                }
            });
            return ret;
        },
        GetToken: function () {
            if (JeffComm.token == "") {
                JeffComm.token = AjaxInterface.GetToken().value;
                //JeffComm.CheckLocalPath(JeffComm.localPath);
            }

            return JeffComm.token;
        },
        DownloadFile2: function (url, callback) {
            var xGet = false;
            if (window.ActiveXObject) {
                var versions = ['Microsoft.XMLHTTP', 'MSXML.XMLHTTP', 'Microsoft.XMLHTTP', 'Msxml2.XMLHTTP.7.0', 'Msxml2.XMLHTTP.6.0', 'Msxml2.XMLHTTP.5.0', 'Msxml2.XMLHTTP.4.0', 'MSXML2.XMLHTTP.3.0', 'MSXML2.XMLHTTP'];
                for (var i = 0; i < versions.length; i++) {
                    try {
                        xGet = new ActiveXObject(versions[i]);
                    } catch (e) { alert(e); }

                }
            } else if (window.XMLHttpRequest) {
                xGet = new XMLHttpRequest();
            }

            //var xGet = false;
            //try {
            //    xGet = new XMLHttpRequest();
            //}
            //catch (trymicrosoft) {
            //    try {
            //        xGet = new ActiveXObject("Msxml2.XMLHTTP");
            //    }
            //    catch (othermicrosoft) {
            //        try {
            //            xGet = new ActiveXObject("Microsoft.XMLHTTP");
            //        }
            //        catch (failed) {
            //            alert(failed);
            //            xGet = false;
            //            return false;
            //        }
            //    }
            //}
            try {
                xGet.open("GET", url.toLowerCase(), 0);
                xGet.onreadystatechange = function () { callback(xGet); };
                xGet.send();
                //return JeffComm.SaveLocalFile(xGet, localPath);
            } catch (failed) {
                alert(failed);
                return false;
            }

            return true;
            //JeffComm.DownloadFile_Callback(xGet, localPath);
        },
        PlayFailSound: function () {
            var audio = document.getElementById("mysound");
            audio.src = "../Download/FAIL.mp3";
            audio.play();

            //var wmp = new ActiveXObject("WMPlayer.OCX");
            //wmp.url = "D:\\wwwroot\\newfuse\\FAIL.mp3";
            //wmp.Controls.Play();

        },
        PlayPassSound: function () {
            var audio = document.getElementById("mysound");
            audio.src = "../Download/OK.mp3";
            audio.play();

        },

        DownloadFile: function (url, localPath) {
            //var objArgs = WScript.Arguments;

            var xGet = false;
            try {
                xGet = new XMLHttpRequest();
            }
            catch (trymicrosoft) {
                try {
                    xGet = new ActiveXObject("Msxml2.XMLHTTP");
                }
                catch (othermicrosoft) {
                    try {
                        xGet = new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    catch (failed) {
                        xGet = false;
                        return false;
                    }
                }
            }
            try {
                xGet.open("GET", url.toLowerCase(), false);
                //xGet.Open("GET", url.toLowerCase(), 0);
                //xGet.onreadystatechange = function () { JeffComm.DownloadFile_Callback(xGet,localPath);};
                xGet.send();
                return JeffComm.SaveLocalFile(xGet, localPath);
            } catch (failed) {
                alert(failed);
                return false;
            }

            return true;
            //JeffComm.DownloadFile_Callback(xGet, localPath);
        },
        SaveLocalFile: function (req, localPath) {
            if (req.responseText) {
                if (req.responseText.indexOf("FALSE^") != -1) {
                    var rets = req.responseText.split("^");
                    alert(rets[1]);
                    return false;
                }
            }
            var sGet = new ActiveXObject("ADODB.Stream");
            sGet.Mode = 3;
            sGet.Type = 1;
            sGet.Open();
            sGet.Write(req.responseBody);
            sGet.SaveToFile(localPath.toLowerCase(), 2);
            return true;
        },
        GetDownloadUrl: function () {
            if (JeffComm.downloadUrl == "") {
                JeffComm.downloadUrl = AjaxInterface.GetTplDownloadUrl().value;
                //JeffComm.CheckLocalPath(JeffComm.localPath);
            }
            return JeffComm.downloadUrl;
        },
        DownloadFile_Callback: function (req, localPath) {
            if (req.readyState == 4) {
                if (req.status == 200) {
                    JeffComm.DownloadFile(req, localPath);
                }
            }
        },

        //础癘魁菌20150418
        CSCUpdateHisTable: function (elmID, result1, result) {
            var rowCount = $("#" + elmID + " tr").length;
            if (rowCount == 10) {
                $("#" + elmID + " tr:not(:first)").remove();
            }
            var htmtxt = "<tr><td >#</td>";
            htmtxt += "<td>" + result[0].IMEI + "</td>";
            htmtxt += "<td>" + result[0].MaterialNumber + "</td>";
            htmtxt += "<td>" + result[0].MaterialName + "</td>";
            htmtxt += "<td>" + result[0].SN + "</td>";
            htmtxt += "<td>" + RepairIn.AppointEngineer + "</td>";
            htmtxt += "<td>" + RepairIn.Group + "</td>";

            var htmlRes = "";
            if (result1 == "OK") {
                htmlRes = '<span class="label label-success">PASS</span>';
            } else {
                htmlRes = '<span class="label label-warning">FAIL</span>';
            }
            htmtxt += "<td>" + htmlRes + "</td>";

            htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
            $("#" + elmID + " tr:first").after(htmtxt);
        },

        //础癘魁菌
        UpdateHisTable: function (elmID, PID, result, msg) {
            var rowCount = $("#" + elmID + " tr").length;
            if (rowCount == 21) {
                $("#" + elmID + " tr:not(:first)").remove();
            }

            var htmtxt = "<tr><td >#</td>";
            htmtxt += "<td>" + PID + "</td>";
            var htmlRes = "";
            if (result == "OK") {
                htmlRes = '<span class="label label-success">PASS</span>';
            } else {
                htmlRes = '<span class="label label-warning">FAIL</span>';
            }
            htmtxt += "<td>" + htmlRes + "</td>";

            htmtxt += "<td>" + msg + "</td>";
            htmtxt += "<td>" + (new Date()).format("yyyy-MM-dd hh:mm:ss") + "</td></tr>";
            $("#" + elmID + " tr:first").after(htmtxt);
        },
        CheckLocalPath: function (path) {
            try {
                var fso = new ActiveXObject("Scripting.FileSystemObject");
                if (!fso.FolderExists(path)) {
                    fso.CreateFolder(path);
                }

            } catch (e) {
                alert(e);
                return false;
            } finally {
                fso = null;
            }

            return true;
        },
        //秙巨舦
        GetMenuPermission: function (menuCode, userCode, permNames, divs) {
            for (var i = 0; i < permNames.length; i++) {
                var falg = AjaxInterface.ChkPermPrivilege(menuCode, userCode, permNames[i]).value;
                if (!falg) {
                    $("#" + divs[i]).addClass('hidden');
                }
            }
        },
        GetLocalPath: function () {
            if (JeffComm.localPath == "") {
                JeffComm.localPath = AjaxInterface.GetSystemConfig("CLIENT_TPL_PATH").value;
            }
            if (JeffComm.isIE) {
                return JeffComm.localPath + "\\";
            } else {
                return JeffComm.localPath + "\\\\";
            }

        },
        ConvertSN: function (input) {
            var ret = AjaxInterface.ConvertSN(input);
            if (ret.error != null && ret.error.Message != "") {
                return "";
            }

            if (ret.value) {
                return ret.value;
            }

            return "";
        },
        //Accは琩SN
        ConvertSNAcc: function (input) {
            var ret = AjaxInterface.ConvertSNAcc(input);
            if (ret.error != null && ret.error.Message != "") {
                return "";
            }

            if (ret.value) {
                return ret.value;
            }

            return "";
        },
        ConvertHPSN: function (input) {
            var ret = AjaxInterface.ConvertHPSN(input);
            if (ret.error != null && ret.error.Message != "") {
                return "";
            }

            if (ret.value) {
                return ret.value;
            }

            return "";
        },

        alertCenter: function (msg) {            
            $.messager.show({
                title: '提示',
                msg: msg,
                showType: 'fade',
                timeout: 1000,
                style: {
                    right: '',
                    bottom: ''
                }
            });

        },
        alertCenterEx: function (msg, timeout) {
            if (timeout == null || timeout == "")
            {
                timeout = 1000;
            }
            $.messager.show({
                title: '提示',
                msg: msg,
                showType: 'fade',
                timeout: timeout,
                style: {
                    right: '',
                    bottom: ''
                }
            });
        },
        alertSucc: function (message, timeout) {            
            if (timeout == null || timeout == "") {
                timeout = 1000;
            }
            if ($("#tip_message").text().length > 0) {
                var msg = "<span>" + message + "</span>";
                $("#tip_message").empty().append(msg);
            } else {
                var msg = "<div id='tip_message'><span>" + message + "</span></div>";
                $("body").append(msg);
            }         
            $("#tip_message").fadeIn(timeout);
            $("#tip_message").fadeOut(timeout);
 
        },
        alertErr: function (message, timeout) {
            JeffComm.alertSucc(message, timeout);
            $("#tip_message span").addClass("error");
        },
        //计锣传だ
        Commafy: function (num) {
            //1.埃,?琌㎝獶? 
            num = num + "";
            num = num.replace(/[ ]/g, ""); //埃
            if (num == "") {
                return;
            }
            if (isNaN(num)) {
                return;
            }
            //2.??琌Τ??だ薄??瞶 
            var index = num.indexOf(".");
            if (index == -1) {//??? 
                var reg = /(-?\d+)(\d{3})/;
                while (reg.test(num)) {
                    num = num.replace(reg, "$1,$2");
                }
            } else {
                var intPart = num.substring(0, index);
                var pointPart = num.substring(index + 1, num.length);
                var reg = /(-?\d+)(\d{3})/;
                while (reg.test(intPart)) {
                    intPart = intPart.replace(reg, "$1,$2");
                }
                num = intPart + "." + pointPart;
            }
            return num;
        }

    };
   
}();

$.ajaxSetup({
    error: function (XMLHttpRequest, textStatus, errorThrown) {
        if (XMLHttpRequest.status == 403) {
            alert('您没有权限访问此资源或进行此操作');
            return false;
        }
    },
    complete: function (XMLHttpRequest, textStatus) {
        var sessionstatus = XMLHttpRequest.getResponseHeader("sessionstatus"); //通过XMLHttpRequest取得响应头,sessionstatus， 
        if (sessionstatus == 'timeout') {
            //如果超时就处理 ，指定要跳转的页面  
            var top = getTopWinow(); //获取当前页面的顶层窗口对象
            alert('登录超时, 请重新登录.');
            top.location.href = path + "../Login.aspx"; //跳转到登陆页面
        }
    }
});
/** 
  * 在页面中任何嵌套层次的窗口中获取顶层窗口 
  * @return 当前页面的顶层窗口对象 
  */
function getTopWinow() {
    var p = window;
    while (p != p.parent) {
        p = p.parent;
    }
    return p;
}


