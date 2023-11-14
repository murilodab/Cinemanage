// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#trailer").on('hide.bs.modal', function (e) {
    $("#trailer iframe").attr("src", $("#trailer iframe").attr("src"));
});

const multipleItemCarousel = document.querySelector('#upcomingCarousel')



if (window.matchMedia("(min-width:576px)").matches) {

    const carousel = new bootstrap.Carousel(multipleItemCarousel, {
        interval: false
    });

    var carouselWidth = $('.carousel-inner')[0].scrollWidth;
    var cardWidth = $('.carousel-item').width();
    var scrollPosition = 0;

    $('.carousel-control-next').on('click', function () {
        if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
            console.log('next');
            scrollPosition = scrollPosition + cardWidth;
            $('.carousel-inner').animate({ scrollLeft: scrollPosition },
                600);
        }
    });

    $('.carousel-control-prev').on('click', function () {
        if (scrollPosition > 0) {
            console.log('prev');
            scrollPosition = scrollPosition - cardWidth;
            $('.carousel-inner').animate({ scrollLeft: scrollPosition },
                600);
        }
    });

    $('.carousel').on('touchstart', function (event) {
        const xClick = event.originalEvent.touches[0].pageX;
        $(this).one('touchmove', function (event) {
            const xMove = event.originalEvent.touches[0].pageX;
            const sensitivityInPx = 5;

            if (Math.floor(xClick - xMove) > sensitivityInPx) {
                if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
                    console.log('next');
                    scrollPosition = scrollPosition + cardWidth;
                    $('.carousel-inner').animate({ scrollLeft: scrollPosition },
                        600);
                }
            }
            else if (Math.floor(xClick - xMove) < -sensitivityInPx) {
                if (scrollPosition > 0) {
                    console.log('prev');
                    scrollPosition = scrollPosition - cardWidth;
                    $('.carousel-inner').animate({ scrollLeft: scrollPosition },
                        600);
                }
            }

        });

        $(this).on('touchend', function () {
            $(this).off('touchmove');
        });
    });



} else {
    $(multipleItemCarousel).addClass('slide');
}



