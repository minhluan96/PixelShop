
							$(window).load(function() {
							    $("#flexiselDemo1").flexisel({
							        visibleItems: 4,
							        animationSpeed: 1000,
							        autoPlay: false,
							        autoPlaySpeed: 3000,
							        pauseOnHover: true,
							        enableResponsiveBreakpoints: true,
							        responsiveBreakpoints: {
							            portrait: {
							                changePoint:480,
							                visibleItems: 1
							            },
							            landscape: {
							                changePoint:640,
							                visibleItems:2
							            },
							            tablet: {
							                changePoint:768,
							                visibleItems: 3
							            }
							        }
							    });

							});



            $(document).ready(function(){
                $(".dropdown").hover(
                    function() {
                        $('.dropdown-menu', this).stop( true, true ).slideDown("fast");
                        $(this).toggleClass('open');
                    },
                    function() {
                        $('.dropdown-menu', this).stop( true, true ).slideUp("fast");
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

    