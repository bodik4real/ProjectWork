$(document).ready(function () {
    $('#btnSend').click(function (e) {
        e.preventDefault();
        var photo = $(e.target);//.hasClass('glyphicon glyphicon-thumbs-up') ? $(this).toggleClass("glyphicon glyphicon-thumbs-up").toggleClass("glyphicon glyphicon-thumbs-down") : $(this).toggleClass("glyphicon glyphicon-thumbs-down");
        var p = photo.data("photoId");
        var text = $("#comment").val();
        if (text) {
            //$.post('/api/Comment/AddComment', { photoId: p, text: text }).done(Refresh(p));
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

                for (var i in data)
                {
                    html += '<div class = "textwrapper border border-dark">'
                    html += "<p>" + data[i].Date + "<b>" + data[i].UserName + "</b></p>" + data[i].Text;
                    html += '</div>';
                }
                $('#divId').append(html);
            }
        });
    }
});