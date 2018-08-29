var JeffComm = function () {
    return {
        //自定義控件        
        localPath: "",
        token: "",
        newfuseContorl: null,
        downloadUrl:"",
        getControl: function () {
            if (JeffComm.newfuseContorl == null) {
                try {
                    JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                } catch (e) {
                    alert(e);
                    return null;
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
                jqElm.append("<option value=''>" + StringRes.SelectBlank + "</option>");
            }
            for (var i=0; i < data.length;i++) {
                jqElm.append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
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
            $('#'+elmId).addClass("alert-error");
            $('span', $('#' + elmId)).html(msg);
            $('#' + elmId).show();
        },
        //頁面上顯示成功信息
        //msg:要顯示的錯誤消息
        //elmId:顯示的DIV ID
        succAlert: function (msg, elmId) {
            JeffComm.clearAlert();
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
        //獲取後臺參數打印條碼
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
            //        //異常情況
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

                    ret = JeffComm.NewfuseCSPrint(tplPath, "", paraNames, paraValues, copyNum);
               // }
           // });

            return ret;
        },
        NewfuseCSPrint:function(tplPath,printerName,paraNames,paraValues,num){
            if (JeffComm.newfuseContorl == null) {
                try{
                    JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                } catch (e) {
                    alert(e);
                    return false;
                }
            }
            return JeffComm.newfuseContorl.PrintCSLabel(tplPath, printerName, paraNames.join(","), paraValues.join(","),num);

        },
        //通過CodeSoft組件打印條碼
        //tplPath:模版文件路徑
        //params:參數列表
        PrintCodeSoft : function (tplPath, params) {            
          
            try {
                try {
                    objCodeSoftApp = new ActiveXObject("lppx.Application");
                } catch (e) {
                    alert(e.message + " 創建CodeSoft對象失敗,請確認正確安裝了CodeSoft程序,并設置了ActiveX相應的權限");
                    return false;
                }
              
                objCodeSoftDoc = objCodeSoftApp.ActiveDocument;
                objCodeSoftDoc.Open(tplPath);
                if (objCodeSoftDoc.Variables.Count == 0) {
                    alert("CodeSoft文件打開失敗或者文件沒有定義變量");
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
        //通過COM口打印ZPL條碼
        //zplInfo:打印指令
        //port:打印端口
        //參數列表
        PrintZPL : function (zplInfo,port, params) {           
            try {
               
                try {
                    objFSOApp = new ActiveXObject("Scripting.FileSystemObject");
                } catch (e) {
                    alert(e.message + " 創建Scripting.FileSystemObject對象失敗,請設置ActiveX相應的權限");
                    return false;
                }
              
                try {
                    if (port) {
                        objFSODoc = objFSOApp.CreateTextFile(port, true);
                        //objFSODoc = objFSOApp.CreateTextFile("c://txt.txt", true);
                    } else {
                        alert("ZPL打印端口沒有設置");
                        return false;
                    }
                } catch (e) {
                    alert(e.message + " 創建FSO.document對象失敗");
                    return false;
                }

                var strZPLIICommand = zplInfo;
                
                //params.each(function (v, n) {
                //    var regTemp = "\\$" + n + "\\$";
                //    strZPLIICommand = strZPLIICommand.replace(eval("/" + regTemp + "/g"), v);
                //});

                objFSODoc.write(strZPLIICommand);
            } catch (e1) {
                if (objFSODoc) {
                    objFSODoc.Close();
                    objFSODoc = null;
                }
                if (objFSOApp) {
                    objFSOApp.Close;
                    objFSOApp = null;
                }
                alert(e1.message);
            }
            finally {
                if (objFSODoc) {
                    objFSODoc.Close();
                    objFSODoc = null;
                }
                if (objFSOApp) {
                    objFSOApp.Close;
                    objFSOApp = null;
                }
            }

            return true;
        },
        NewfuseDownload: function (url, localPath) {
            if (JeffComm.newfuseContorl == null) {
                try {
                    JeffComm.newfuseContorl = new ActiveXObject("NewfuseControls.CommonControl");
                } catch (e) {
                  
                    return false;
                }
            }

            //加上用戶信息
            url = url + "&t=" + JeffComm.GetToken();
            return JeffComm.newfuseContorl.DownloadTpl(url, localPath);
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
                    } catch (e) { alert(e);}

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


        DownloadFile: function (url,localPath) {
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
            try{
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

        //插入記錄的歷史表格20150418
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

        //插入記錄的歷史表格
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
        GetLocalPath: function () {
            if (JeffComm.localPath == "") {
                JeffComm.localPath = AjaxInterface.GetSystemConfig("CLIENT_TPL_PATH").value;               
            }
            return JeffComm.localPath + "\\";
            
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
        }

    };
}();

Date.prototype.format = function (formatStr) {
    var date = this;
    /*  
	函數擴展：格式化日期  
	参数：formatStr  格式
	返回：格式化後字符串  
	*/
    var zeroize = function (value, length) {
        if (!length) {
            length = 2;
        }
        value = new String(value);
        for (var i = 0, zeros = ''; i < (length - value.length) ; i++) {
            zeros += '0';
        }
        return zeros + value;
    };
    return formatStr.replace(/"[^"]*"|'[^']*'|\b(?:d{1,4}|M{1,4}|yy(?:yy)?|([hHmstT])\1?|[lLZ])\b/g, function ($0) {
        switch ($0) {
            case 'd': return date.getDate();
            case 'dd': return zeroize(date.getDate());
            case 'ddd': return ['Sun', 'Mon', 'Tue', 'Wed', 'Thr', 'Fri', 'Sat'][date.getDay()];
            case 'dddd': return ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'][date.getDay()];
            case 'M': return date.getMonth() + 1;
            case 'MM': return zeroize(date.getMonth() + 1);
            case 'MMM': return ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'][date.getMonth()];
            case 'MMMM': return ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'][date.getMonth()];
            case 'yy': return new String(date.getFullYear()).substr(2);
            case 'yyyy': return date.getFullYear();
            case 'h': return date.getHours() % 12 || 12;
            case 'hh': return zeroize(date.getHours() % 12 || 12);
            case 'H': return date.getHours();
            case 'HH': return zeroize(date.getHours());
            case 'm': return date.getMinutes();
            case 'mm': return zeroize(date.getMinutes());
            case 's': return date.getSeconds();
            case 'ss': return zeroize(date.getSeconds());
            case 'l': return date.getMilliseconds();
            case 'll': return zeroize(date.getMilliseconds());
            case 'tt': return date.getHours() < 12 ? 'am' : 'pm';
            case 'TT': return date.getHours() < 12 ? 'AM' : 'PM';
        }
    });
}
