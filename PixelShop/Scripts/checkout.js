$(document).ready(function (c) {
    $('.close-checkout-1').on('click', function (c) {
        $('.cart-header').fadeOut('slow', function (c) {
            $('.cart-header').remove();
        });
    });
});


$(document).ready(function (c) {
    $('.close-checkout-2').on('click', function (c) {
        $('.cart-header2').fadeOut('slow', function (c) {
            $('.cart-header2').remove();
        });
    });
});
$(document).ready(function () {
    $(".qtycart").change(function () {
        var qty = $(this).val();
        var productId = $(this).attr('class').replace('qtycart ', '');
        $.ajax({
            type: "GET",
            url: "ShoppingCart/Update",
            data: { quantity: qty, id: productId },
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