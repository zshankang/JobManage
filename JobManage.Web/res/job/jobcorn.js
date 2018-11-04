/**
 * 复杂任务生成CronExpression表达式
 * */
function CreateCronExpression() {
    var seconds = $("#c_seconds").val();
    var minutes = $("#c_minutes").val();
    var hours = $("#c_hours").val();
    var dayOfMonth = $("#c_dayofmonth").val();
    var month = $("#c_month").val();
    var dayOfWeek = $("#c_dayweek").val();

    var _seconds = seconds + ",";
    var _minutes = minutes + ",";
    var _hours = hours + ",";
    var _dayOfMonth = dayOfMonth + ",";
    var _month = month + ",";
    var _dayOfWeek = dayOfWeek + ",";

    //校验表达是否合法

    var patt1 = /^[1-5]{0,1}[0-9]{1}\/[1-5]{0,1}[0-9]{1},/; //秒，分 格式：[0-59]/[1-59]
    var patt2 = /^[0-5]{0,1}[0-9]{1},/; //秒，分 格式：[0-59]
    var patt3 = /^\*,/; //秒，分,小时,dayOfMonth,month,dayOfWeek 格式：*
    var patt4 = /^\?,/; //dayOfMonth ,dayOfWeek 格式：?
    var patt5 = /(^[0-9]{1}|^[1]{0,1}[0-9]{0,1}|^[2]{1}[0-3]{1}),$/; //小时 格式：[0-23]
    var patt6 = /^[0-2]{0,1}[0-9]{0,1}[3]{0,1}[0-1]{0,1},/;  //dayOfMonth 格式：[0-31]
    var patt7 = /^[0]{0,1}[0-9]{0,1}[]{0,1}[0-1]{0,1},/; //Month 格式：[0-11]
    var patt8 = /\b[JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC]{3},/; //Month 格式：[JAN|FEB|MAR|APR|MAY|JUN|JUL|AUG|SEP|OCT|NOV|DEC]
    var patt9 = /^[1-7],/; //dayOfWeek 格式1-7
    var patt10 = /\b[SUN|MON|TUE|WED|THU|FRI|SAT]{3},/; //dayOfWeek 格式[SUN|MON|TUE|WED|THU|FRI|SAT]
    var patt11 = /(^[0-9]{1}|^[1]{0,1}[0-9]{0,1}|^[2]{1}[0-3]{1})\/([0-9]{1}|[1]{0,1}[0-9]{0,1}|[2]{1}[0-3]{1}),$/;  //小时 格式：[0-23]/[1-23]


    var error = "";
    //秒
    if (!(patt1.test(_seconds) || patt2.test(_seconds))) {
        error += "秒：设置数值错误,格式:[0-59]/[1-59]或[0-59]";
    }
    //分
    if (!(patt1.test(_minutes) || patt2.test(_minutes) || patt3.test(_minutes))) {
        error += "分：设置数值错误,格式:[0-59]/[1-59]或[0-59]或*";
    }
    //小时
    if (!(patt3.test(_hours) || patt5.test(_hours) || patt11.test(_hours))) {
        error += "小时：设置数值错误,格式:[0-23]或*或[0-23]/[0-23]";
    }
    //天/月
    if (!(patt5.test(_dayOfMonth) || patt3.test(_dayOfMonth) || patt6.test(_dayOfMonth))) {
        error += "天/月：设置数值错误,格式:[0-23]或*或?";
    }

    //月份
    if (!(patt7.test(_month) || patt8.test(_month) || patt3.test(_month))) {
        error += "月份：设置数值错误,格式:[0-11]或JAN,FEB,MAR,APR,MAY,JUN,JUL,AUG,SEP,OCT,NOV,DEC";
    }
    //日/周
    if (!(patt3.test(_dayOfWeek) || patt4.test(_dayOfWeek) || patt9.test(_dayOfWeek) || patt10.test(_dayOfWeek))) {
        error += "日/周：设置数值错误,格式:[0-23]或*或?或SUN,MON,TUE,WED,THU,FRI,SAT";
    }

    if (error.length > 1) {
        job.modal.alert(error, 1);
        return false;
    }
    job.modal.msg("表达式正确！", 0);
    ////////////////////////////////////////////////////////////////////
    var cmdtext = seconds + " " + minutes + " " + hours + " " + dayOfMonth + " " + month + " " + dayOfWeek;

    $("#job_corn").val(cmdtext);
    return true;
}
/**
 * 初始任务表达式
 * */
function InitCrom() {
    var cronstr = $("#job_corn").val();
    var cronArr = cronstr.split(' ');
    $("#c_seconds").val(cronArr[0]);
    $("#c_minutes").val(cronArr[1]);
    $("#c_hours").val(cronArr[2]);
    $("#c_dayofmonth").val(cronArr[3]);
    $("#c_month").val(cronArr[4]);
    $("#c_dayweek").val(cronArr[5]);
}