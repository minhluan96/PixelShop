
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
$(document).ready(function () {
    $(".qtycart").change(function () {
        var qty = $(this).val();
        var price = parseInt($(this).parent().find('#price').text());
        var productId = $(this).attr('class').replace('qtycart ', '');
        var t = $(this).parent().find('#total');
        var str = ".close-checkout." + productId;
        var elementminicart = $('.minicart').find(str).parent();
        elementminicart.find('#qty').text("Số lượng: " + qty);
        elementminicart.find('.total').text("Tổng tiền: " + qty * price);
        t.text("Tổng tiền: " + qty * price);
        var sum = 0;
        $('.minicart').find('.total').each(function (index, value) {
            sum += parseInt($(this).text().replace('Tổng tiền: ', ''));
        });
        $('.minicart').find('#totalorder').text("Tổng tiền hóa đơn: " + sum);
        $.ajax({
            type: "GET",
            url: "ShoppingCart/Update",
            data: { quantity: qty, id: productId },
            dataType: "html"
        });
    });
});
$(document).on('click', ".close-checkout", function () {
        $(this).parent().fadeOut('slow', function (c) {
            $(this).remove();
        });
        var totalcart = parseInt($("#totalorder").text().replace("Tổng tiền hóa đơn: ", ""));
        var productId = $(this).attr('class').replace('close-checkout ', '');
        var elementqty = $(document).find("#qty").first();
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
        }
        $.ajax({
            method: "POST",
            url: "MenuComponent/GetProduct",
            dataType: "json",
            data: { id: productId },
            success: function (data) {
                totalcart = totalcart - (data.GiaBan * qty);
                $("#totalorder").text("Tổng tiền hóa đơn: " + totalcart);
                $("#checkout_total").text(totalcart);
                $("#checkout_total1").text(totalcart);
            },
            error: function () {
                alert("Thêm giỏ hàng thất bại");
            }
        });
        $.ajax({
            type: "GET",
            url: "ShoppingCart/Delete",
            data: { id: productId },
            dataType: "html"
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
        var totalcart = parseInt($("#totalorder").text().replace("Tổng tiền hóa đơn: ", ""));
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
            var total = qty * price;
            totalcart = totalcart + price;
            $("#totalorder").text("Tổng tiền hóa đơn: " + totalcart);
            qty = qty + 1;
            element.parent().find(".total").first().text("Tổng tiền: " + total);
            elementqty.text("Số lượng: " + qty);
        }
        else {
            var name, id, img;
            var gia;
            id = productId;
            $.ajax({
                method: "POST",
                url: "MenuComponent/GetProduct",
                dataType: "json",
                data: { id: productId },
                success: function (data) {
                    var item = data;
                    name = data.TenSP;
                    img = data.HinhHienThi;
                    gia = data.GiaBan;
                    totalcart = totalcart + data.GiaBan;
                    $("#totalorder").text("Tổng tiền hóa đơn: " + totalcart);
                    var template = '<div class="cart-header"><div class="close-checkout ' + productId + '"> </div><div class="cart-sec simpleCart_shelfItem" style="margin-right:40px;"><div class="cart-item cyc"><img src="' + img + '" class="sbmincart-img" alt="" /></div><div class="cart-item-info">                        <p class="sbmincart-name">' + name + '</p>                        <p id="price" style="display:none;">' + gia + '</p>                        <p id="qty">Số lượng: 1</p>                        <div class="delivery" style="margin-top:10px;">                            <p class="total">Tổng tiền: ' + gia + '</p>                            <div class="clearfix"></div>                        </div>                    </div>                    <div class="clearfix"></div>                </div>                <hr />            </div>';
                    $(".minicart").prepend(template);
                },
                error: function(){
                    alert("Thêm giỏ hàng thất bại");
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