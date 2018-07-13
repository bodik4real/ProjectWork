function checkFile() {

    var _URL = window.URL || window.webkitURL;
    var file, img

    var fileElement = document.getElementById("inputfile");

    //check file extenssion and image size
    var fileExtension = "";
    if (fileElement.value.lastIndexOf(".") > 0) {
        fileExtension = fileElement.value.substring(fileElement.value.lastIndexOf(".") + 1, fileElement.value.length);


        if (fileExtension.toLowerCase() == "jpg" || fileExtension.toLowerCase() == "jpeg") {
            var callback = function (img) {
                if (img.height >= 1000 || img.width >= 1000) {
                    //alert("Your image has enough quality" + img.height + "x" + img.height);
                    Preview();
                    return true;
                }
                else {
                    document.getElementById("mytext").value = "Your image hasn’t enough quality!!!";
                    //alert("Your image hasn’t enough quality");
                    return false;
                }
            }
        }
        else
        {
            //alert("You must select a JPG file for upload");
            document.getElementById("mytext").value = "You must select a JPG file for upload";
            return false;
        }
    }
    file = document.querySelector('input[type=file]').files[0];
    img = new Image();

    //async fun
    img.onload = function () {
        //alert("width : " + this.width + " and height : " + this.height);
        callback(img);
    }
    img.src = _URL.createObjectURL(file);
};