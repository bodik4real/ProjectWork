$(document).ready(function () {
    $('#btnSend').click(function (e) {
        e.preventDefault();
        var photo = $(e.target);
        var p = photo.data("photoId");
        var text = $("#comment").val();
        if (text) {
            $.ajax({
                type: "POST",
                url: '/api/Comment/AddComment',
                data: { photoId: p, text: text },
            }).done(function () {
                Refresh(p);
            });
        }
    });

    function Refresh(photoId) {
        var url = '/api/Comment?photoId=' + photoId;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            success: function (data) {
                $("#createArea").empty();

                var newNode = document.createElement('div');
                newNode.id = "divId";

                $('#createArea').append(newNode);

                var html = '';

                for (var i in data) {
                    html += '<div class = "textwrapper border border-dark">'
                    html += "<p>" + formatDate(data[i].Date) + "<b>" + data[i].UserName + "</b></p>" + data[i].Text;
                    html += '</div>';
                }
                $('#divId').append(html);
            }
        });
    }

    function formatDate(date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear(),
            hour = '' + d.getHours(),
            minute = '' + d.getMinutes(),
            second = '' + d.getSeconds();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;
        if (hour.length < 2) hour = '0' + hour;
        if (minute.length < 2) minute = '0' + minute;

        return [year, month, day].join('.') + ' ' + [hour, minute].join(':');
    }
});