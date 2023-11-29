// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var dropdownItems = document.querySelectorAll('#collectionButton');

dropdownItems.forEach(item => {

    

    item.addEventListener('click', async () => {

        var collectionId = item.getAttribute('data-collectionId');
        var movieId = item.getAttribute('data-movieId');

        console.log('click');
        console.log('Collection ID:', collectionId);
        console.log('Movie ID:', movieId);

        var res = await fetch(`/Movies/AddToMovieCollection?collectionId=${collectionId}&movieId=${movieId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
           
        })


            .then(response => response.json())
            .then(data => {
                // Handle the response from the server if needed
                if (data.success) {
                    console.log('Movie added to collection successfully');
                } else {
                    console.error('Error: ', data.error || 'Unknown error');
                }
               
            })
            .catch(error => {
                console.error('Error:', error);
            });
    });
});








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


