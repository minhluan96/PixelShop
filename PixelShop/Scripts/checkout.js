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
        $.ajax({
            type: "GET",
            url: "ShoppingCart/Update",
            data: { quantity: $(".qtycart").val(), id: $(".idProduct").text() },
            dataType: "html"
        });
    });
});