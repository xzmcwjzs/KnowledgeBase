function createPageBar(pagelistdiv, pageindex, recordcount, pagecount) {
    //var pageListDiv = $("#PageList");
    var divHtml = "共" + recordcount + "条<a style='padding-left:1em;'></a>分" + pagecount + "页<a style='padding-left:1em;'></a>" +
       "第" + pageindex + "页<a style='padding-left:1em;'></a>跳转到<input type='Text' id='index' name='index' style='width:20px;height:18px;padding:0;vertical-align:middle;'/>" +
       "<input type='Button' id='go' value='GO' style='width:22px;height:20px;font-size:8pt;padding:0;vertical-align:middle;'  onclick='Go(" + pagecount + ")'/><a style='padding-left:1em;'></a>" +
       "每页15条<a style='padding-left:2em;'></a>" +
       "<a href='javascript:loadPageList(1)'><img src='/images/first.gif' style='border:#fff'/></a><a style='padding-left:1em;'>" +
       "<a href='javascript:loadPageList(" + prevPage(pageindex) + ")'><img src='/images/prev.gif' style='border:#fff'/></a><a style='padding-left:1em;'>" +
   "<a href='javascript:loadPageList(" + lastPage(pageindex, pagecount) + ")'><img src='/images/next.gif' style='border:#fff' /></a><a style='padding-left:1em;'></a>" +
   "<a href='javascript:loadPageList(" + pagecount + ")'><img src='/images/last.gif' style='border:#fff'/></a>";
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