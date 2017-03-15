
function createPageBar(pagelistdiv, pageindex, rowcount, pagecount) { 
    var divHtml = "共" + rowcount + "条<a style='padding-left:1em;'></a>分" + pagecount + "页<a style='padding-left:1em;'></a>" +
        "第" + pageindex + "页<a style='padding-left:1em;'></a>跳转到<input type='Text' id='index' name='index' style='width:20px;height:18px;padding:0;vertical-align:middle;'/>" +
        "<input type='Button' id='go' value='GO' style='width:22px;height:20px;font-size:8pt;padding:0;vertical-align:middle;' onclick='Go(" + pagecount + ")'/><a style='padding-left:1em;'></a>" +
        "每页5条<a style='padding-left:2em;'></a>" +
        "<a href='javascript:loadPageList(1)' style='color:#000000'>首页</a><a style='padding-left:1em;'>" +
        "<a href='javascript:loadPageList(" + prevPage(pageindex) + ")' style='color:#000000'>上一页<a style='padding-left:1em;'>" +
    "<a href='javascript:loadPageList(" + lastPage(pageindex, pagecount) + ")' style='color:#000000'>下一页<a style='padding-left:1em;'></a>" +
    "<a href='javascript:loadPageList(" + pagecount + ")' style='color:#000000'>尾页</a>";
    pagelistdiv.html(divHtml);
}

function Go(pageCount) {
    var reg = /^[0-9]*[1-9][0-9]*$/;
    if (reg.test($("#index").val()) && parseInt($("#index").val()) <= pageCount) {
        loadPageList(parseInt($("#index").val()));

    } else {
        alert("请输入正确的页码值");
        return;
    }
}
function prevPage(pi) {
    if (pi > 1) return pi - 1;
    else return 1;
}
function lastPage(pi, pageCount) {
    if (pi < pageCount) return pi + 1;
    else return pageCount;
}