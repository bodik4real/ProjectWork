$(document).ready(function () {
    $('.like').click(function (e) {
        e.preventDefault();
        var photo = $(e.target);//.hasClass('glyphicon glyphicon-thumbs-up') ? $(this).toggleClass("glyphicon glyphicon-thumbs-up").toggleClass("glyphicon glyphicon-thumbs-down") : $(this).toggleClass("glyphicon glyphicon-thumbs-down");
        var p = photo.data("photoId");
        if (photo.hasClass('glyphicon glyphicon-thumbs-up')) {
            photo.toggleClass("glyphicon glyphicon-thumbs-up");
            photo.toggleClass("glyphicon glyphicon-thumbs-down");
            $.post('/api/Likes/UnLike', { photoId: p });
            //success: {
            //    $(".glyphicon glyphicon-thumbs-down").html(data);
            //}
        }
        else if (photo.hasClass('glyphicon glyphicon-thumbs-down')) {
            photo.toggleClass("glyphicon glyphicon-thumbs-down");
            photo.toggleClass("glyphicon glyphicon-thumbs-up");
            $.post('/api/Likes/Like', { photoId: p });
        }
       

        //if (this.value == 'Like') {
        //    this.value = 'Unlike';
        //    photo.toggleClass("glyphicon glyphicon-thumbs-up").toggleClass("glyphicon glyphicon-thumbs-down");
        //   // photo.addClass("glyphicon glyphicon-thumbs-down");

        //}
        //else {
        //    this.value = 'Like';
        //    photo.removeClass("glyphicon glyphicon-thumbs-down");
        //    photo.addClass("glyphicon glyphicon-thumbs-up");
        //}
    });

    //unLike()
});
