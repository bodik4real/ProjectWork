function Preview() {
    var preview = document.querySelector('img');
    var fileForPreview = document.querySelector('input[type=file]').files[0];
    var reader = new FileReader();

    reader.onloadend = function () {
        preview.src = reader.result;
    }

    if (fileForPreview) {
        reader.readAsDataURL(fileForPreview);
    }
    else {
        preview.src = "";
    }
    var image = new Image();
    image.src = reader.result;

    image.onload = function () {
        alert(image.width);
    };
};