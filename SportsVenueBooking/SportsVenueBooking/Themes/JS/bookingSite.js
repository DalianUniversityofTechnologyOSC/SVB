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
    if (value != "-1")
    {
        return "<a href='javascript:submitReser(\"" + value + "\")' id='reser_btn' class=\"btn72\">预约</a>";
    }
    else
    {
        return "<a disabled='false' class=\"btn72 disabled\">预约</a>";
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
    if ($("input[name=date]").val() == "2")
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
        var start = parseInt($(".week_start").val());
        var end = parseInt($(".week_end").val());
        if (start <= end)
        {

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
    if ($(this).val() == "2")
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
    var startDteArr = $("#start_date").datebox("getText").split('/');
    var endDateArr = $("#end_date").datebox("getText").split('/');
    var start = startDteArr[2] + "/" + startDteArr[0] + "/" + startDteArr[1];
    var end = endDateArr[2] + "/" + endDateArr[0] + "/" + endDateArr[1];
    $('#dg').datagrid('load', {
        startDate: start,
        endDate: end,
        conditions: $("input[name=cc_type]:checked").val(),
        type: $("#space").val(),
        time: $("#duration").val(),
        weekOfDay: $("#weekOfDay").val()
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
            var startDteArr = $("#start_date").datebox("getText").split('/');
            var endDateArr = $("#end_date").datebox("getText").split('/');
            var start = startDteArr[2] + "/" + startDteArr[0] + "/" + startDteArr[1];
            var end = endDateArr[2] + "/" + endDateArr[0] + "/" + endDateArr[1];
            $.ajax({
                type: "post",
                url: "/Teacher/ReservationIn",
                data: {
                    duration: res_info[0],
                    space: res_info[1],
                    start: start,
                    end: end,
                    dayOfWeek: $("#weekOfDay").val()
                },
                success: function (data)
                {
                    $.messager.progress('close');
                    data = JSON.parse(data);
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
    var startDteArr = $("#start_date").datebox("getText").split('/');
    var endDateArr = $("#end_date").datebox("getText").split('/');
    var start = startDteArr[2] + "/" + startDteArr[0] + "/" + startDteArr[1];
    var end = endDateArr[2] + "/" + endDateArr[0] + "/" + endDateArr[1];
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
            dayOfWeek: $("#weekOfDay").val()
        }
    });
}