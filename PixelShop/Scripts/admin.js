$(document).ready(function () {
    $("#submitnew_img").click(function () {
        $("#imagesfile").click();
    });
    $("#imagesfile").change(function (e) {
        var files = e.target.files;

            $.each(files, function (i, file) {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function (e) {
                    var path = e.target.result;
                    var template = ' <tr><td><a href="" class="fancybox-button" data-rel="fancybox-button"><input type="hidden" name="statusimgSP" value="new" /><input type="hidden" name="imgSP" value="' + path + '"/><img type="image"  class="img-responsive" src="' + path + '"  alt=""></a></td><td><label><input type="radio" name="imgKey" value="' + path + '"' + path + ' ? "checked" : "")>Ảnh bìa</label>                                                    </td>          <td>                                                        <a href="javascript:;" class="btn default btn-sm">                                                            <i class="fa fa-times"></i> Xóa                                                        </a>                                                               </td>                                                </tr>';
                    $("#tableImage tbody").append(template);
                };
            });
        
    });
});