

$(document).ready(function () {

    $("#print").click(function () {
        window.print();
    });
    $("#submitnew_img").click(function () {
        $("#imagesfile").click();
    });
    $("#imagesfile").change(function (e) {
        var files = e.target.files;
        var formData = new FormData();
        $.each(files, function (i, file) {
            var reader = new FileReader();
            reader.readAsDataURL(file);
            formData.append("FileUpload", file);
        });
        $.ajax({
            type: "POST",
            url: '../../Admin/UpdateImageProduct',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (data) {
                $.each(data, function (i, d) {
                    var path = "../ImgProduct/" + d;
                    var template = '<tr>                                                    <td>                                                        <a href="" class="fancybox-button" data-rel="fancybox-button">                                                            <input type="hidden" name="imgSP" value="' + path + '"/>                                                            <img type="image"  class="img-responsive" src="' + path + '"  alt="">                                                        </a>                                                    </td>                                                    <td>                                                        <label>                                                            <input type="radio" name="imgKey" value="' + path + '">                                                            Ảnh bìa                                                        </label>                                                    </td>                                                                                                                                                         <td>                                                        <a href="javascript:;" class="btn default btn-sm btnXoa">                                                            <i class="fa fa-times"></i> Xóa                                                        </a>                                                    </td>                                                </tr>';
                    $("#tableImage tbody").prepend(template);
                });
            },
            error: function (error) {
            }
        });
    });
});

$(document).on('click', ".btnXoa", function () {
    $(this).parent('td').parent('tr').fadeOut('slow', function (c) {
        $(this).empty();
        $(this).remove();
    });
});
