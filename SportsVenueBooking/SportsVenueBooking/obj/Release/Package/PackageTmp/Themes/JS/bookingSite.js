function getIsAllIdle(value)
{
    if (value == "0")
    {
        return "是";
    }
    else
    {
        return "否";
    }
}

function getIsSearch(value)
{
    if (value != "0")
    {
        return "<a href='javascript:searchReser(\"" + value + "\")' id='reser_btn' class=\"btn72\">查看</a>";
    }
    else
    {
        return "<a disabled='false' class=\"btn72 disabled\">查看</a>";
    }
}

function getButton(value)
{
    if (value == "-1")
    {
        return "<a disabled='false' class=\"btn72 disabled\">预约</a>";
    }
    else
    {
        return "<a href='javascript:submitReser(\"" + value + "\")' id='reser_btn' class=\"btn72\">预约</a>";
    }

}

function ToDuble(num)
{
    if (num < 10)
    {
        return "0" + num;
    }

    return num;
}
var isSearched = false;
$("#query_ticket").click(function ()
{
    if ($("input[name=date]:checked").val() == "1")
    {
        if ($("#start_date").datebox("getText") != "" && $("#end_date").datebox("getText") != "")
        {
            var startDteArr = $("#start_date").datebox("getText").split('/');
            var endDateArr = $("#end_date").datebox("getText").split('/');
            var date = new Date();
            var today = parseInt("" + date.getFullYear() + ToDuble((date.getMonth() + 1)) + ToDuble(date.getDate()));
            var start = parseInt(startDteArr[2] + startDteArr[0] + startDteArr[1]);
            var end = parseInt(endDateArr[2] + endDateArr[0] + endDateArr[1]);
            if (start >= today)
            {
                if (start <= end)
                {
                    isSearched = true;
                    getData();
                }
                else
                {
                    $.messager.alert('错误', '结束时间必须大于等于开始时间！', 'error');
                }
            }
            else
            {
                $.messager.alert('错误', '开始时间必须大于等于当前时间！', 'error');
            }
        }
        else
        {
            $.messager.alert('错误', '开始时间和结束时间不能为空！', 'error');
        }
    }
    else
    {
        var start = parseInt($("#week_start").val().match(/[0-9]{1,2}/));
        var end = parseInt($("#week_end").val().match(/[0-9]{1,2}/));
        if (start <= end)
        {
            isSearched = true;
            getData();
        }
        else
        {
            $.messager.alert("提示", "结束时间晚于开始时间，无法进行查询！", "error");
        }
    }
});

$("#space").change(function ()
{
    if (isSearched)
    {
        getData();
    }

})

$("#duration").change(function ()
{
    if (isSearched)
    {
        getData();
    }
});

$("#weekOfDay").change(function ()
{
    if (isSearched)
    {
        getData();
    }
});

$("input[name=cc_type]").change(function ()
{
    if (isSearched)
    {
        getData();
    }
});

$("input[name=date]").change(function ()
{
    if ($("input[name=date]:checked").val() == "2")
    {
        $(".weekNumber").removeAttr("disabled");
        $(".datebox").css("color", "gray");
        $(".textbox-addon").css("display", "none");
        $('.textbox-text').attr('disabled', 'true');
        $(".week").css("color", "#000");
    }
    else
    {
        $(".weekNumber").attr("disabled", "true");
        $(".datebox").css("color", "#000");
        $(".textbox-addon").css("display", "block");
        $(".textbox-text").removeAttr("disabled");
        $(".week").css("color", "gray");
    }
})

function getData()
{
    if ($("input[name=date]:checked").val() == "1")
    {
        var startDteArr = $("#start_date").datebox("getText").split('/');
        var endDateArr = $("#end_date").datebox("getText").split('/');
        var start = startDteArr[2] + "/" + startDteArr[0] + "/" + startDteArr[1];
        var end = endDateArr[2] + "/" + endDateArr[0] + "/" + endDateArr[1];
        var isWeek = false;
    }
    else
    {
        var start = parseInt($("#week_start").val().match(/[0-9]{1,2}/));
        var end = parseInt($("#week_end").val().match(/[0-9]{1,2}/));
        var isWeek = true;
    }
    $('#dg').datagrid('load', {
        startDate: start,
        endDate: end,
        conditions: $("input[name=cc_type]:checked").val(),
        type: $("#space").val(),
        time: $("#duration").val(),
        dayOfWeek: $("#weekOfDay").val(),
        isWeek: isWeek
    });
}

function submitReser(info)
{
    $.messager.confirm("请确认...", "请再次确认你的预约！你确定预约吗？", function (r)
    {
        if (r)
        {
            $.messager.progress({ title: "预约中...", msg: "玩命预约中，请稍后...", text: "请稍后..." });
            var res_info = info.split('/');
            $("#dg").datagrid("selectRecord", res_info[2])
            var row = $("#dg").datagrid("getSelected");
            var start = row.start;
            var end = row.end;
            var dayOfWeek = row.dayOfWeek;
            $.ajax({
                type: "post",
                url: "/Teacher/ReservationIn",
                data: {
                    duration: res_info[0],
                    space: res_info[1],
                    start: start,
                    end: end,
                    dayOfWeek: dayOfWeek
                },
                success: function (data)
                {
                    $.messager.progress('close');
                    if (data.status)
                    {
                        $.messager.alert('消息', data.message, 'ok');
                    }
                    else
                    {
                        $.messager.alert('消息', data.message, 'error');
                    }
                }
            });
        }
    })
}

function searchReser(value)
{
    var res_info = value.split('/');
    $("#dg").datagrid("selectRecord", res_info[2])
    var row = $("#dg").datagrid("getSelected");
    var start = row.start;
    var end = row.end;
    var dayOfWeek = row.dayOfWeek;
    $('#win').window({
        title: "查看预约情况",
        width: 600,
        height: 400,
        modal: true,
        method: "post",
        href: "/Teacher/SearchReservation",
        queryParams: {
            duration: res_info[0],
            space: res_info[1],
            startDate: start,
            endDate: end,
            dayOfWeek: parseDayOfWeek(dayOfWeek)
        }
    });
}

function parseDayOfWeek(value)
{
    if (parseInt(value) < 7)
    {
        switch (parseInt(value))
        {
            case 0: return "周日";
            case 1: return "周一";
            case 2: return "周二";
            case 3: return "周三";
            case 4: return "周四";
            case 5: return "周五";
            case 6: return "周六";
        }
    }
    else
    {
        switch (value)
        {
            case "周日": return 0;
            case "周一": return 1;
            case "周二": return 2;
            case "周三": return 3;
            case "周四": return 4;
            case "周五": return 5;
            case "周六": return 6;
        }
    }
}