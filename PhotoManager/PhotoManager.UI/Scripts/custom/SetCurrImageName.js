$("#CheckboxForImageName").change(function () {
    var fullPath = $("#inputfile").val();
    var filename = fullPath.replace(/^.*[\\\/]/, '');
    var name = filename.split('.');
    filename = name[0];


    if ($(this).prop('checked')) {
        $('#Name').val(filename);
    }
    else {
        $('#Name').val('');
    }
});