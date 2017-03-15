/// <reference path="../jquery-1.8.2.min.js" /> 
$(function () {
    //下拉框自动补全功能
          $.ajax({
              cache: false,
              type: "get",
              url: "/Frame/AutocompleteDisease",
              dataType: "text",
              success: function (data) {
                  jsonArr = eval("(" + data + ")");
                  $("#DiseaseList").autocomplete(jsonArr, {
                      autoFill: false,
                      matchContains: true,
                      //width: '50%',
                      max: 10,
                      scrollHeight: 300,
                      matchContains: true,
                      matchSubset: true,
                      matchCase: true,
                      scroll: true,
                      formatItem: function (row, i, max) {
                          return "<span style='font-weight:bold;font-size:12pt;float:left'>" + i + "/" + max + "：" + row.VDiseaseList + "</span><span style='font-weight:bold;font-size:12pt;float:right;color:green'>[" + row.VDiseaseType + "]" + "</span>";
                      },
                      formatMatch: function (row, i, max) {
                          return row.VDiseaseList;
                      },
                      formatResult: function (row) {
                          return row.VDiseaseList;
                      }
                  }).result(function (event, row, formatted) {
                      $("#DiseaseCode").val(row.VDiseaseCode);
                  });

              },
              error: function () {
                  alert("系统繁忙，请联系管理员");
              }
          });
          //禁用鼠标右键，文档选择，复制
          $("body").bind("contextmenu", function (event) {
              return false;
          });
          $("body").bind("selectstart", function () { return false; }); 
});
//根据疾病id或名称进行搜索
function Search(type) { 
    var diseaseCode = $("#DiseaseCode").val();
    var diseaseName = $("#DiseaseList").val();
    $("#ModulesContents div").remove();
    $("#ConditionsTable tr").remove();
    $("#CheckAll2").removeAttr("onclick");
    if (diseaseCode != "") {
        $.ajax({
            cache: false,
            type: "get",
            url: "/Frame/SearchByCode",
            dataType: "text",
            data: { DiseaseCode: diseaseCode },
            success: function (data) {
                var jsonArr = eval("(" + data + ")");
                var jsonRes = jsonArr.data;
                var strHTML = "", tdHtml = "", trHtml = "", k = 0,nameList="";
                var arrDiseaseName = new Array(), arrYWMC = new Array(), arrYWSX = new Array();
                arrDiseaseName = jsonRes[0].VDiseaseName.split('}');
                arrYWMC = jsonRes[0].VYWMC.split('}');
                arrYWSX = jsonRes[0].VYWSX.split('}');
                if (arrDiseaseName[0] != "") { 
                    nameList += "疾病中文名称：";
                    for (var i = 0; i < arrDiseaseName.length-1; i++) {
                        nameList += arrDiseaseName[i]+"、";
                    }
                    nameList = nameList.substr(0,nameList.length-1)+" ";
                }
                if (arrYWMC[0] != "") {
                    nameList += "疾病英文全称：";
                    for (var i = 0; i < arrYWMC.length-1; i++) {
                        nameList += arrYWMC[i] + "、";
                    }
                    nameList = nameList.substr(0, nameList.length - 1) + " ";
                }
                if (arrYWSX[0] != "") {
                    nameList += "疾病英文缩写：";
                    for (var i = 0; i < arrYWSX.length-1; i++) {
                        nameList += arrYWSX[i] + "、";
                    }
                    nameList = nameList.substr(0, nameList.length - 1) + " ";
                }
                strHTML += '<div style="margin:10px 0px"><span style="font-weight:bold;">'+nameList+'</div>';
                for (i = 0; i < jsonRes.length; i++) {
                    count = jsonRes.length;
                    strHTML += '<div id="div' + jsonRes[i].VModulesId + '">' +
                               '<div style="padding-top:20px"><span style="font-size:22pt;font-weight:bold;">' + jsonRes[i].VModulesName + '</span></div>' +
                               '<div>' + jsonRes[i].VModulesContent + '</div></div>';
                    k++;
                    tdHtml += '<td><label><input type="checkbox" name="modules" id="CB' + jsonRes[i].VModulesId + '" onclick="Animate(' + jsonRes[i].VModulesId + ')" value="' + jsonRes[i].VModulesId + '"/>' + jsonRes[i].VModulesName + '</label></td>';
                    if (k % 4 == 0) {
                        trHtml += '<tr>'+tdHtml+'</tr>';
                        tdHtml = "";
                    } 
                }
                if (trHtml == "" && tdHtml!="") {
                    trHtml += '<tr>' + tdHtml + '</tr>';
                }
                if (trHtml!="" && tdHtml != "") {
                    trHtml += '<tr>' + tdHtml + '</tr>';
                }
                $("#ModulesContents").css("background-color", "#ffffff");
                $("#ModulesContents").append(strHTML);
                if (type == 'gj') {
                    $("#ConditionsTable").append(trHtml);
                    $("#ModulesContents div:eq(0)").show().siblings().hide();
                } else {
                    $("#ModulesContents").show();
                }
                $("#PrintDIV").show();
                //$("#CheckAll2").attr("onclick", "SelectAll()");
                //$("#CheckAll2").attr("disabled", false);

                //if ($("input[name=modules]").is(":checked")) {
                //    $("input[name=modules]").each(function (i) {
                //        LoadDiv($(this).val());
                //    });
                //}
                //else {
                //    $("input[name=modules]").attr("checked", true);
                //    $("#CheckAll2").val("全不选").width("50px");
                //}

            },
            error: function () {
                alert("系统繁忙，请联系管理员");
            }
        });
        $("#DiseaseCode").val("");
    } else if (diseaseCode == "" && diseaseName != "") {
        $.ajax({
            cache: false,
            type: "get",
            url: "/Frame/SearchByName",
            dataType: "text",
            data: { DiseaseName: diseaseName },
            success: function (data) {
                var jsonArr = eval("(" + data + ")");
                if (jsonArr.status == "error") {
                    alert(jsonArr.data);
                } else {
                    var strHTML = "", tdHtml = "", trHtml = "", k = 0, nameList = "";
                    var jsonRes = jsonArr.data;
                    var arrDiseaseName = new Array(), arrYWMC = new Array(), arrYWSX = new Array();
                    arrDiseaseName = jsonRes[0].VDiseaseName.split('}');
                    arrYWMC = jsonRes[0].VYWMC.split('}');
                    arrYWSX = jsonRes[0].VYWSX.split('}');
                    if (arrDiseaseName[0] != "") {
                        nameList += "疾病中文名称：";
                        for (var i = 0; i < arrDiseaseName.length-1; i++) {
                            nameList += arrDiseaseName[i] + "、";
                        }
                        nameList = nameList.substr(0, nameList.length - 1) + " ";
                    }
                    if (arrYWMC[0] != "") {
                        nameList += "疾病英文全称：";
                        for (var i = 0; i < arrYWMC.length-1; i++) {
                            nameList += arrYWMC[i] + "、";
                        }
                        nameList = nameList.substr(0, nameList.length - 1) + " ";
                    }
                    if (arrYWSX[0] != "") {
                        nameList += "疾病英文缩写：";
                        for (var i = 0; i < arrYWSX.length-1; i++) {
                            nameList += arrYWSX[i] + "、";
                        }
                        nameList = nameList.substr(0, nameList.length - 1) + " ";
                    }
                    strHTML += '<div style="margin:10px 0px"><span style="font-weight:bold;">' + nameList + '</div>';

                    for (i = 0; i < jsonRes.length; i++) {
                        strHTML += '<div id="div' + jsonRes[i].VModulesId + '">' +
                                '<div style="padding-top:20px"><span style="font-size:22pt;font-weight:bold;">' + jsonRes[i].VModulesName + '</span></div>' +
                                '<div>' + jsonRes[i].VModulesContent + '</div></div>';
                        k++;
                        tdHtml += '<td><label><input type="checkbox" name="modules" id="CB' + jsonRes[i].VModulesId + '" onclick="Animate(' + jsonRes[i].VModulesId + ')" value="' + jsonRes[i].VModulesId + '"/>' + jsonRes[i].VModulesName + '</label></td>';
                        if (k % 4 == 0) {
                            trHtml += '<tr>' + tdHtml + '</tr>';
                            tdHtml = "";
                        }
                    }
                    if (trHtml == "" && tdHtml != "") {
                        trHtml += '<tr>' + tdHtml + '</tr>';
                    }
                    if (trHtml != "" && tdHtml != "") {
                        trHtml += '<tr>' + tdHtml + '</tr>';
                    }
                    $("#ModulesContents").css("background-color", "#ffffff");
                    $("#ModulesContents").append(strHTML);
                    if (type == 'gj') {
                        $("#ConditionsTable").append(trHtml);
                        $("#ModulesContents div:eq(0)").show().siblings().hide();
                    } else {
                        $("#ModulesContents").show();
                    }
                    $("#PrintDIV").show();
                    //$("#CheckAll2").attr("onclick", "SelectAll()");
                    //$("#CheckAll2").attr("disabled", false);

                    //if ($("input[name=modules]").is(":checked")) {
                    //    $("input[name=modules]").each(function (i) {
                    //        LoadDiv($(this).val());
                    //    });
                    // }
                    //else {
                    //    $("input[name=modules]").attr("checked", true);
                    //    $("#CheckAll2").val("全不选").width("50px"); 
                    //}
                }

            },
            error: function () {
                alert("系统繁忙，请联系管理员");
            }
        });
    } else {
        alert("请输入疾病名称");
    }
}

function Animate(code) {
    //$("#ModulesContents").show();
    //if ($("input[name=modules]").is(":checked")) {
    //    $("input[name=modules]").each(function (i) {
    //        LoadDiv($(this).val());
    //    });
    //}
    var cb = "#CB" + code;
    var div = "#div" + code;
    //当控件的id含有.  时  需转义才能操作
    cb = cb.replace(".", "\\.");
    div = div.replace(".", "\\.");
    if ($(cb).is(":checked")) {
        $(div).slideDown("slow");
    } else {
        $(div).slideUp("slow");
    }
}
//function LoadDiv(code) {
//    var cb = "#CB" + code;
//    var div = "#div" + code;
//    //当控件的id含有.  时  需转义才能操作
//    cb = cb.replace(".", "\\.");
//    div = div.replace(".", "\\.");
//    if ($(cb).is(":checked")) {
//        $(div).show();
//    } else {
//        $(div).hide();
//    }
//}

function SelectAll() { 
    if ($("#CheckAll2").val() == "全选") {
        $("#CheckAll2").val("全不选").width("50px");
        $("input[name=modules]").attr("checked", true);
        $("input[name=modules]").each(function (i) {
            LoadDiv($(this).val());
        });
    } else {
        $("#CheckAll2").val("全选").width("40px");
        $("input[name=modules]").attr("checked", false);
        $("input[name=modules]").each(function (i) {
            LoadDiv($(this).val());
        });
    }
}
//打印
function divPrint() {
    $("#ModulesContents").jqprint();
}
//退出
function Quit() {
    top.window.opener = top;
    top.window.open("/Home/Index", '_self', '');
}
 


 