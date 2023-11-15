// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#trailer").on('hide.bs.modal', function (e) {
    $("#trailer iframe").attr("src", $("#trailer iframe").attr("src"));
});


var upcomingCarousel = document.getElementById('upcomingCarousel');
var nowPlayingCarousel = document.getElementById('nowPlayingCarousel');
var popularCarousel = document.getElementById('popularCarousel');
var topRatedCarousel = document.getElementById('topRatedCarousel');

//var carouselWidth = $('.carousel-inner')[0].scrollWidth;
//var cardWidth = $('.carousel-item').width();
//var scrollPosition = 0;

carouselControl(upcomingCarousel);
carouselControl(nowPlayingCarousel);
carouselControl(popularCarousel);
carouselControl(topRatedCarousel);


function carouselControl(carousel) {

    var carouselWidth = $('.carousel-inner')[0].scrollWidth;
    var cardWidth = $('.carousel-item').width();
    var scrollPosition = 0;

    $(carousel).find('.carousel-control-next').on('click', function () {
        if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
            console.log('next');
            scrollPosition = scrollPosition + cardWidth;
            $(carousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
                350);
        }
    });
    $(carousel).find('.carousel-control-prev').on('click', function () {
        if (scrollPosition > 0) {
            console.log('prev');
            scrollPosition = scrollPosition - cardWidth;
            $(carousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
                350);
        }
    });

    $(carousel).on('touchstart', function (event) {
        const xClick = event.originalEvent.touches[0].pageX;
        $(this).one('touchmove', function (event) {
            const xMove = event.originalEvent.touches[0].pageX;
            const sensitivityInPx = 5;

            if (Math.floor(xClick - xMove) > sensitivityInPx) {
                if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
                    console.log('next');
                    scrollPosition = scrollPosition + cardWidth;
                    $(carousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
                        350);
                }
            }
            else if (Math.floor(xClick - xMove) < -sensitivityInPx) {
                if (scrollPosition > 0) {
                    console.log('prev');
                    scrollPosition = scrollPosition - cardWidth;
                    $(carousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
                        350);
                }
            }

        });

        $(this).on('touchend', function () {
            $(this).off('touchmove');
        });
    });

}

/*--------------upcomingCarousel Controls----------------------*/

//$(upcomingCarousel).find('.carousel-control-next').on('click', function () {
//    if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
//        console.log('next');
//        scrollPosition = scrollPosition + cardWidth;
//        $(upcomingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//            350);
//    }
//});
//$(upcomingCarousel).find('.carousel-control-prev').on('click', function () {
//    if (scrollPosition > 0) {
//        console.log('prev');
//        scrollPosition = scrollPosition - cardWidth;
//        $(upcomingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//            350);
//    }
//});

//$(upcomingCarousel).on('touchstart', function (event) {
//    const xClick = event.originalEvent.touches[0].pageX;
//    $(this).one('touchmove', function (event) {
//        const xMove = event.originalEvent.touches[0].pageX;
//        const sensitivityInPx = 5;

//        if (Math.floor(xClick - xMove) > sensitivityInPx) {
//            if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
//                console.log('next');
//                scrollPosition = scrollPosition + cardWidth;
//                $(upcomingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//                    350);
//            }
//        }
//        else if (Math.floor(xClick - xMove) < -sensitivityInPx) {
//            if (scrollPosition > 0) {
//                console.log('prev');
//                scrollPosition = scrollPosition - cardWidth;
//                $(upcomingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//                    350);
//            }
//        }

//    });

//    $(this).on('touchend', function () {
//        $(this).off('touchmove');
//    });
//});

/*--------------nowPlayingCarousel Controls----------------------*/

//$(nowPlayingCarousel).find('.carousel-control-next').on('click', function () {
//    if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
//        console.log('next');
//        scrollPosition = scrollPosition + cardWidth;
//        $(nowPlayingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//            350);
//    }
//});
//$(nowPlayingCarousel).find('.carousel-control-prev').on('click', function () {
//    if (scrollPosition > 0) {
//        console.log('prev');
//        scrollPosition = scrollPosition - cardWidth;
//        $(nowPlayingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//            350);
//    }
//});

//$(nowPlayingCarousel).on('touchstart', function (event) {
//    const xClick = event.originalEvent.touches[0].pageX;
//    $(this).one('touchmove', function (event) {
//        const xMove = event.originalEvent.touches[0].pageX;
//        const sensitivityInPx = 5;

//        if (Math.floor(xClick - xMove) > sensitivityInPx) {
//            if (scrollPosition < (carouselWidth - (cardWidth * 4))) {
//                console.log('next');
//                scrollPosition = scrollPosition + cardWidth;
//                $(nowPlayingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//                    350);
//            }
//        }
//        else if (Math.floor(xClick - xMove) < -sensitivityInPx) {
//            if (scrollPosition > 0) {
//                console.log('prev');
//                scrollPosition = scrollPosition - cardWidth;
//                $(nowPlayingCarousel).find('.carousel-inner').animate({ scrollLeft: scrollPosition },
//                    350);
//            }
//        }

//    });

//    $(this).on('touchend', function () {
//        $(this).off('touchmove');
//    });
//});