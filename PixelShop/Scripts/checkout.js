
$(document).ready(function () {
    $('.user-cart')
.on('mouseover', function (event) {
    $('.minicart').show();
})
.on('mouseout', function (event) {
    $('.minicart').hide();
});
});
$(document).ready(function () {
    $('.minicart')
.on('mouseover', function (event) {
    $('.minicart').show();
})
.on('mouseout', function (event) {
    $('.minicart').hide();
});
});
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
}
$(document).ready(function () {
    $(".qtycart").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            return false;
        }
    });
    $('.qtycart').on('focusin', function () {
        $(this).data('val', $(this).val());
    });
    $(".qtycart").change(function () {
        var prev = $(this).data('val');
        var current = $(this).val();
        var count;
        var productId = $(this).attr('class').replace('qtycart ', '');
        var count;
        var elementqtycart = $(this);
        $.ajax({
            method: "POST",
            url: "/MenuComponent/GetProduct",
            dataType: "json",
            data: { id: productId },
            success: function (data) {
                count = data.SoLuongTon;
                if (current > count) {
                    elementqtycart.val(prev);
                    alert("Sản phẩm có tại cửa hàng không đủ cung cấp cho khách hàng!!!");
                }
                else if(current < 1){
                    elementqtycart.val(prev);
                    alert("Số lượng nhập không hợp lệ. Vui lòng nhập lại!!!");
                }
                else {
                    var qty = elementqtycart.val();
                    var price = parseInt(elementqtycart.parent().find('#price').text());
                    var t = elementqtycart.parent().find('#total');
                    var str = ".close-checkout." + productId;
                    var elementminicart = $('.minicart').find(str).parent();
                    var qtyold = parseInt(elementminicart.find('#qty').text().replace("Số lượng: ", ""));
                    var count = parseInt($(".user-numproduct").text());
                    count = parseInt(count) - parseInt(qtyold) + parseInt(qty);
                    $(".user-numproduct").text(count);
                    elementminicart.find('#qty').text("Số lượng: " + qty);
                    var tt = qty * price;
                    elementminicart.find('.total').text("Tổng tiền: " + numberWithCommas(tt) + " VNĐ");
                    t.text("Tổng tiền: " + numberWithCommas(tt) + " VNĐ");
                    var sum = 0;
                    $('.minicart').find('.total').each(function (index, value) {
                        var replace1 = $(this).text().replace('Tổng tiền: ', '');
                        var replace2 = replace1.substring(0, replace1.length - 4);
                        var lsStr = replace2.split(".");
                        var tstr = "";
                        for (var i = 0 ; i < lsStr.length; i++) {
                            tstr = tstr + lsStr[i];
                        }
                        var sumitem = parseInt(tstr);
                        sum += sumitem;
                    });
                    $('.minicart').find('#totalorder').text("Tổng tiền hóa đơn: " + numberWithCommas(sum) + " VNĐ");
                    $("#checkout_total").text(numberWithCommas(sum) + " VNĐ");
                    $("#checkout_total1").text(numberWithCommas(sum) + " VNĐ");
                    $.ajax({
                        type: "GET",
                        url: "ShoppingCart/Update",
                        data: { quantity: qty, id: productId },
                        dataType: "html"
                    });
                }
            },
            error: function () {
            }
        });
    });
});
$(document).on('click', ".close-checkout", function () {
        $(this).parent().fadeOut('slow', function (c) {
            $(this).remove();
        });
        var replace1 = $("#totalorder").text().replace("Tổng tiền hóa đơn: ", "");
        var replace2 = replace1.substring(0, replace1.length - 4);
        var lsStr = replace2.split(".");
        var tstr = "";
        for (var i = 0 ; i < lsStr.length; i++) {
            tstr = tstr + lsStr[i];
        }
        var totalcart = parseInt(tstr);
        var productId = $(this).attr('class').replace('close-checkout ', '');

        var elementqty = $(".minicart").find('.close-checkout.' + productId).parent().find("#qty").first();
        var qty = parseInt(elementqty.text().replace("Số lượng: ", ""));
        var count = parseInt($(".user-numproduct").text());
        count = count - qty;
        $(".user-numproduct").text(count);

        $(document).find('.close-checkout.' + productId).each(function () {
            $(this).parent().fadeOut('slow', function (c) {
                $(this).remove();
            });
        });
        if (count == 0) {
            $('.null-cart').each(function (i, obj) {
                $(this).fadeIn(2000, function (c) {
                    $(this).show();
                });
            });
            $("#btnpayment").fadeOut(1000, function (c) {
                $(this).hide();
            });
        }
        $.ajax({
            method: "POST",
            url: "MenuComponent/GetProduct",
            dataType: "json",
            data: { id: productId },
            success: function (data) {
                totalcart = totalcart - (data.GiaBan * qty);
                $("#totalorder").text("Tổng tiền hóa đơn: " + numberWithCommas(totalcart) + " VNĐ");
                $("#checkout_total").text(numberWithCommas(totalcart) + " VNĐ");
                $("#checkout_total1").text(numberWithCommas(totalcart) + " VNĐ");
            },
            error: function () {
                
            }
        });
        $.ajax({
            type: "GET",
            url: "/ShoppingCart/Delete",
            data: { id: productId },
            dataType: "html",
            success: function (data) {
                
            }
        });      
    });
function flyToElement(flyer, flyingTo) {
    var $func = $(this);
    var divider = 3;
    var flyerClone = $(flyer).clone();
    $(flyerClone).css({ position: 'absolute', top: $(flyer).offset().top + "px", left: $(flyer).offset().left + "px", opacity: 1, 'z-index': 1000 });
    $('body').append($(flyerClone));
    var gotoX = $(flyingTo).offset().left + ($(flyingTo).width() / 2) - ($(flyer).width() / divider) / 2;
    var gotoY = $(flyingTo).offset().top + ($(flyingTo).height() / 2) - ($(flyer).height() / divider) / 2;

    $(flyerClone).animate({
        opacity: 0.4,
        left: gotoX,
        top: gotoY,
        width: $(flyer).width() / divider,
        height: $(flyer).height() / divider
    }, 1000,
    function () {
        $(flyingTo).fadeOut('fast', function () {
            $(flyingTo).fadeIn('fast', function () {
                $(flyerClone).fadeOut('fast', function () {
                    $(flyerClone).remove();
                });
            });
        });
    });
}
$(document).ready(function () {
    $(".btnaddcart").click(function () {
        $('html, body').animate({
            'scrollTop': $(".user-cart").position().top
        });
        $('.null-cart').each(function (i, obj) {
            $(this).hide();
        });
        var replace1 = $("#totalorder").text().replace("Tổng tiền hóa đơn: ", "");
        var replace2 = replace1.substring(0, replace1.length - 4);
        var lsStr = replace2.split(".");
        var tstr = "";
        for (var i = 0 ; i < lsStr.length; i++) {
            tstr = tstr + lsStr[i];
        }
        var totalcart = parseInt(tstr);
        //Select item image and pass to the function
        var itemImg = $(this).parent().parent().find('img').eq(0);
        var count = parseInt($(".user-numproduct").text());
        count = count + 1;
        $(".user-numproduct").text(count);
        flyToElement($(itemImg), $('.user-cart'));
        var productId = $(this).attr('class').replace('btnaddcart ', '');
        if ($(".minicart").find("." + productId).length > 0) {
            var element = $(".minicart").find("." + productId).first();
            var elementqty = element.parent().find("#qty").first();
            var qty = parseInt(elementqty.text().replace("Số lượng: ", ""));
            var price = parseInt(element.parent().find("#price").first().text());
            qty = qty + 1;
            var total = qty * price;
            totalcart = totalcart + price;
            $("#totalorder").text("Tổng tiền hóa đơn: " + numberWithCommas(totalcart) + " VNĐ");
            element.parent().find(".total").first().text("Tổng tiền: " + numberWithCommas(total) + " VNĐ");
            elementqty.text("Số lượng: " + qty);
        }
        else {
            var name, id, img;
            var gia, totaltemp;
            id = productId;
            $.ajax({
                method: "POST",
                url: "/MenuComponent/GetProduct",
                dataType: "json",
                data: { id: productId },
                success: function (data) {
                    var item = data;
                    name = data.TenSP;
                    img = data.HinhHienThi;
                    gia = data.GiaBan;
                    totaltemp = totalcart + data.GiaBan;
                    totalcart = numberWithCommas(totaltemp) + " VNĐ";
                    $("#totalorder").text("Tổng tiền hóa đơn: " + totalcart);
                    var template = '<div class="cart-header"><div class="close-checkout ' + productId + '"> </div><div class="cart-sec simpleCart_shelfItem" style="margin-right:40px;"><div class="cart-item cyc"><img src="' + img + '" class="sbmincart-img" alt="" /></div><div class="cart-item-info">                        <p class="sbmincart-name" style="font-weight: bold; font-size: 13px;">' + name + '</p>                        <p id="price" style="display:none;">' + gia + '</p>                        <p id="qty">Số lượng: 1</p>                        <div class="delivery" style="margin-top:10px;">                            <p class="total" style="font-weight: bold; font-size:12px;">Tổng tiền: ' + numberWithCommas(gia) + " VNĐ" + '</p>                            <div class="clearfix"></div>                        </div>                    </div>                    <div class="clearfix"></div>                </div>                <hr />            </div>';
                    $(".minicart").prepend(template);
                },
                error: function(){
                }
            });
            
            
        }
        $.ajax({
            type: "GET",
            url: "../../ShoppingCart/OrderNow",
            data: { id: productId },
            dataType: "html"

        });
    });
});

