
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
$(document).ready(function () {
    $(".close-checkout").click(function () {
        $(this).parent().fadeOut('slow', function (c) {
            $(this).remove();
        });
        if ($('.cart-header').length==1) {
            $('.sbmincart-footer').fadeOut('slow', function (c) {
                $('.sbmincart-footer').remove();
            });
        }
        var productId = $(this).attr('class').replace('close-checkout ', '');
        $.ajax({
            type: "GET",
            url: "ShoppingCart/Delete",
            data: {id: productId },
            dataType: "html"
        });
    });
});
$(document).ready(function () {
    $(".btnaddcart").click(function () {
        var productId = $(this).attr('class').replace('btnaddcart ', '');
        $.ajax({
            type: "GET",
            url: "ShoppingCart/OrderNow",
            data: { id: productId },
            dataType: "html"
        });
    });
});