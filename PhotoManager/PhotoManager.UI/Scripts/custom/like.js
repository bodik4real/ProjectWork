$(document).ready(function () {
    $('.like').click(function (e) {
        e.preventDefault();
        //.hasClass('glyphicon glyphicon-thumbs-up') ? $(this).toggleClass("glyphicon glyphicon-thumbs-up").toggleClass("glyphicon glyphicon-thumbs-down") : $(this).toggleClass("glyphicon glyphicon-thumbs-down");
        var photo = $(e.target);
        if (!photo.hasClass('none-events')) {
            //$('#likeIfLiked').hide();
            //$('#unlikeIfLiked').removeAttr('hidden');
            //$('.like').next('.unlike');
            if (photo.hasClass("glyphicon glyphicon-thumbs-up"))
            {
                Like(photo);   
            }
            else if (photo.hasClass('glyphicon glyphicon-thumbs-down'))
            {
                UnLike(photo);   
            }
        }
    });

    //$('.unlike').click(function (e) {
    //    e.preventDefault();
    //    var photo = $(e.target);
    //    var p = photo.data("photoId");
    //    if (!photo.hasClass('none-events')) {
    //        $('.unlike').hide();
    //        //photo.toggleClass("glyphicon glyphicon-thumbs-down");
    //        //photo.toggleClass("glyphicon glyphicon-thumbs-up");
    //        $.ajax({
    //            type: "POST",
    //            url: '/api/Likes/UnLike',
    //            data: { photoId: p },
    //            success: function (response) {
    //                Refresh(photo);
    //            }
    //        });
    //    }
    //});

    function Like(photo) {
        photo.toggleClass("glyphicon glyphicon-thumbs-up");
        photo.toggleClass("glyphicon glyphicon-thumbs-down");
        var p = photo.data("photoId");
        //$.post('/api/Likes/Like', { photoId: p });
        $.ajax({
            type: "POST",
            url: '/api/Likes/Like',
            data: { photoId: p },
            success: function (response) {
                Refresh(photo);
            }

            //.done(function () {
            //    Refresh(photo);
            //});
        });
    }

    function UnLike(photo)
    {
        photo.toggleClass("glyphicon glyphicon-thumbs-down");
        photo.toggleClass("glyphicon glyphicon-thumbs-up");
        var p = photo.data("photoId");
        $.ajax({
            type: "POST",
            url: '/api/Likes/UnLike',
            data: { photoId: p },
        }).done(function () {
            Refresh(photo);
        });
    }

    function Refresh(photo) {
        var photoId = photo.data("photoId");
        var url = '/api/Likes?photoId=' + photoId;
        $.ajax({
            type: "GET",
            url: url,
            cache: false,
            success: function (count) {
                photo.text(count)
            }
        });
    }
});
