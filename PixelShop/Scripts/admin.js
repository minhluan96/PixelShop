$(document).ready(function () {
    $("#submitnew_img").click(function () {
        $("#imagesfile").click();
    });
    $("#imagesfile").change(function (e) {
        var files = e.target.files;
        var formData = new FormData();
        formData.append("masp", $("#masp").val());
        $.each(files, function (i, file) {
            var reader = new FileReader();
            reader.readAsDataURL(file);
            formData.append("FileUpload", file);
            reader.onload = function (e) {
                var path = e.target.result;
                var template = ' <tr><td><a href="" class="fancybox-button" data-rel="fancybox-button"><input type="hidden" name="statusimgSP" value="new" /><input type="hidden" name="imgSP" value="' + path + '"/><img type="image"  class="img-responsive" src="' + path + '"  alt=""></a></td><td><label><input type="radio" name="imgKey" value="' + path + '"' + path + ' ? "checked" : "")>Ảnh bìa</label>                                                    </td>          <td>                                                        <a href="javascript:;" class="btn default btn-sm">                                                            <i class="fa fa-times"></i> Xóa                                                        </a>                                                        <a id="tab_images_uploader_pickfiles" href="javascript:;" class="btn yellow">                                                            <i class="fa fa-plus"></i> Chọn lại ảnh                                                        </a>                                                    </td>                                                </tr>';
                $("#tableImage tbody").append(template);
            };
        });
        $.ajax({
            type: "POST",
            url: '../../Admin/UpdateImageProduct',
            data: formData,
            dataType: 'json',
            contentType: false,
            processData: false,
            success: function (response) {
                alert('succes!!');
            },
            error: function (error) {
                console.log(error);
            }
        });
    });
});