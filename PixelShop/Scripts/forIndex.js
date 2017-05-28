
$(window).load(function () {
    $("#flexiselDemo1").flexisel({
        visibleItems: 4,
        animationSpeed: 1000,
        autoPlay: false,
        autoPlaySpeed: 3000,
        pauseOnHover: true,
        enableResponsiveBreakpoints: true,
        responsiveBreakpoints: {
            portrait: {
                changePoint: 480,
                visibleItems: 1
            },
            landscape: {
                changePoint: 640,
                visibleItems: 2
            },
            tablet: {
                changePoint: 768,
                visibleItems: 3
            }
        }
    });

});



$(document).ready(function () {
    $(".dropdown").hover(
        function () {
            $('.dropdown-menu', this).stop(true, true).slideDown("fast");
            $(this).toggleClass('open');
        },
        function () {
            $('.dropdown-menu', this).stop(true, true).slideUp("fast");
            $(this).toggleClass('open');
        }
    );
});

$(document).ready(function () {
    $('#horizontalTab').easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true   // 100% fit in a container
    });
});



w3ls1.render();

w3ls1.cart.on('w3sb1_checkout', function (evt) {
    var items, len, i;

    if (this.subtotal() > 0) {
        items = this.items();

        for (i = 0, len = items.length; i < len; i++) {
            items[i].set('shipping', 0);
            items[i].set('shipping2', 0);
        }
    }
});


$('.example1').wmuSlider();


function checkPass() {

    var pass1 = document.getElementById('password_old');
    var pass2 = document.getElementById('password_new');
    var passwordOld = document.getElementById('passOld');
    var hoten = document.getElementById("display_name");

    var message = document.getElementById('confirmMessage');
    var mess = document.getElementById('mess');
    var tenmess = document.getElementById('tenmess');

    var goodColor = "#66cc66";
    var badColor = "#ff6666";

    if (hoten == null || hoten.value.length == 0) {
        hoten.style.backgroundColor = badColor;
        tenmess.style.color = badColor;
        tenmess.innerHTML = "Họ tên không được trống!"
        $(':input[type="submit"]').prop('disabled', true);
    }
    else {
        hoten.style.backgroundColor = goodColor;
        tenmess.style.color = goodColor;
        tenmess.innerHTML = "OK!";
        var btnupdate = document.getElementById('btncapnhat');
        $(':input[type="submit"]').prop('disabled', false);
    }
    
    if (pass1.value != passwordOld.value) {
        pass1.style.backgroundColor = badColor;
        mess.style.color = badColor;
        mess.innerHTML = "Mật khẩu cũ không chính xác!"
        $(':input[type="submit"]').prop('disabled', true);
    }
    else {
        pass1.style.backgroundColor = goodColor;
        mess.style.color = goodColor;
        mess.innerHTML = "OK!"



        if (pass2.value.length >= 6) {

            if (pass2.value == pass1.value) {
                pass2.style.backgroundColor = badColor;
                message.style.color = badColor;
                message.innerHTML = "Mật khẩu mới không được giống với mật khẩu cũ"
                $(':input[type="submit"]').prop('disabled', true);

            } else {

                pass2.style.backgroundColor = goodColor;
                message.style.color = goodColor;
                message.innerHTML = "OK!";
                var btnupdate = document.getElementById('btncapnhat');
                $(':input[type="submit"]').prop('disabled', false);
            }

        } else {

            pass2.style.backgroundColor = badColor;
            message.style.color = badColor;
            message.innerHTML = "Mật khẩu phải có ít nhất 6 kí tự!"
            $(':input[type="submit"]').prop('disabled', true);

        }
    }
}
