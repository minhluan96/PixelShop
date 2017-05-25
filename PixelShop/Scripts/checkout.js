
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
        var productId = $(this).attr('class').replace('close-checkout ', '');
        $(document).find('.close-checkout.' + productId).each(function () {
            $(this).parent().fadeOut('slow', function (c) {
                $(this).remove();
            });
        });

        if (($(document).find('.account-in').length == 1 && $(document).find('.cart-header').length == 2) || $(document).find('.cart-header').length == 1) {
            $('.sbmincart-footer').fadeOut('slow', function (c) {
                $('.sbmincart-footer').remove();
            });
        }

        $.ajax({
            type: "GET",
            url: "ShoppingCart/Delete",
            data: { id: productId },
            dataType: "html"
        });
    });
$(document).ready(function () {
    $(".btnaddcart").click(function () {
        var productId = $(this).attr('class').replace('btnaddcart ', '');
        if ($(".minicart").find("." + productId).length > 0) {
            var element = $(".minicart").find("." + productId).first();
            var elementqty = element.parent().find("#qty").first();
            var qty = parseInt(elementqty.text().replace("Số lượng: ", ""));
            var price = parseInt(element.parent().find("#price").first().text());
            var total = qty * price;
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
                data: { id: productId },
                success: function (data) {
                    var item = data.split("%");
                    name = item[0];
                    img = item[1];
                    gia = item[2];
                    var template = '<div class="cart-header"><div class="close-checkout ' + productId + '"> </div><div class="cart-sec simpleCart_shelfItem" style="margin-right:40px;"><div class="cart-item cyc"><img src="' + img + '" class="sbmincart-img" alt="" /></div><div class="cart-item-info">                        <p class="sbmincart-name">' + name + '</p>                        <p id="price" style="display:none;">' + gia + '</p>                        <p id="qty">Số lượng: 1</p>                        <div class="delivery" style="margin-top:10px;">                            <p class="total">Tổng tiền:' + gia + '</p>                            <div class="clearfix"></div>                        </div>                    </div>                    <div class="clearfix"></div>                </div>                <hr />            </div>';
                    $(".minicart").prepend(template);
                },
                error: function(){
                    console.log(Error);
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