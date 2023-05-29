AOS.init();
$(document).ready(function () {
    $(".loading").hide();

    $(document).ajaxStart(function () {
        $(".loading").show();
    });
    $(document).ajaxStop(function () {
        $(".loading").hide();
    });
});

$('.overlay').click(function () {
    $('.hamburger').removeClass('is-active');
    $('.header-mb').removeClass('active');
    $('.overlay').removeClass(' active');
});

function IndexJS() {
    $(window).scroll(function () {
        var header = $(".header");

        if ($(window).scrollTop() > 300) {
            header.addClass("active");
        } else {
            header.removeClass("active");
        }
    });

    // Kiểm tra vị trí cuộn khi trang tải lại
    $(window).on("load", function () {
        var header = $("#header");

        if ($(window).scrollTop() > 300) {
            header.addClass("active");
        }
    });

    $('.room-slick').slick({
        arrows: false,
        dots: true,
        infinite: true,
        autoplay: false,
        autoplaySpeed: 2000,
        slidesToShow: 3,
        variableWidth: true,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: false
                }
            },
            {
                breakpoint: 600,
                settings: {
                    variableWidth: true,
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 425,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });

    $('.slick-feedback').slick({
        slidesToShow: 3,
        slidesToScroll: 1,
        autoplay: true,
        dots: true,
        autoplaySpeed: 2000,
        prevArrow: "<button type='button' class='slick-prev pull-left'><i class='fa fa-chevron-left' aria-hidden='true'></i></button>",
        nextArrow: "<button type='button' class='slick-next pull-right'><i class='fa fa-chevron-right' aria-hidden='true'></i></button>",
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 1
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
        ]
    });

    $('.service-cat').slick({
        slidesToShow: 1,
        slidesToScroll: 1,
        arrows: false,
        fade: true,
        asNavFor: '.slider'
    });
    $('.slider').slick({
        slidesToShow: 2,
        slidesToScroll: 1,
        dots: false,
        autoplay: true,
        autoplaySpeed: 2000,
        asNavFor: '.service-cat',
        focusOnSelect: true,
        prevArrow: "<button type='button' class='slick-prev pull-left'><i class='fa fa-chevron-left' aria-hidden='true'></i></button>",
        nextArrow: "<button type='button' class='slick-next pull-right'><i class='fa fa-chevron-right' aria-hidden='true'></i></button>",
        responsive: [
            {
                breakpoint: 768,
                settings: {
                    slidesToShow: 1
                }
            }
        ],
        cssEase: 'cubic-bezier(0.455, 0.03, 0.515, 0.955)'
    });

}
$('.room-main').slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
    asNavFor: '.room-nav'
});
$('.room-nav').slick({
    slidesToShow: 5 ,
    slidesToScroll: 1,
    asNavFor: '.room-main',
    dots: false,
    focusOnSelect: true,
    prevArrow: "<button type='button' class='slick-prev pull-left'><i class='fa fa-chevron-left' aria-hidden='true'></i></button>",
    nextArrow: "<button type='button' class='slick-next pull-right'><i class='fa fa-chevron-right' aria-hidden='true'></i></button>",
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
                infinite: true,
                dots: false
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 425,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 1
            }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
    ]

});
$(".contact-form").on("submit", function (e) {
    e.preventDefault();
    $.post("/Home/ContactForm", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Contact successfully sent",
                text: data.msg,
                icon: "success"
            });
            $(".contact-form").trigger("reset");
        } else {
            $.toast({
                heading: "Failed to send",
                text: data.msg,
                icon: "error"
            });
        }
    });
});
$('#order-form').submit(function (e) {
    e.preventDefault();
    $.post("/Home/OderDetail", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Contact successfully sent",
                text: data.msg,
                icon: "success"
            });
            $("#order-form").trigger("reset");
        } else {
            $.toast({
                heading: "Failed to send",
                text: data.msg,
                icon: "error"
            });
        }
    });
});
$(".subcribe-form").on("submit", function (e) {
    e.preventDefault();
    $.post("/Home/SubcribeForm", $(this).serialize(), function (data) {
        if (data.status) {
            $.toast({
                heading: "Sign up to receive news successfully",
                text: data.msg,
                icon: "success"
            });
            $(".subcribe-form").trigger("reset");
        } else {
            $.toast({
                heading: "Subscription failed",
                text: data.msg,
                icon: "error"
            });
        }
    });
});

function updateSlickDotsWidth() {
    var slideCount = $('.room-slick').slick('getSlick').slideCount;
    var slickDotsWidth = slideCount * 30; // Thay đổi 30 thành chiều rộng mỗi dot

    $('.slick-dots').css('width', slickDotsWidth + 'px');
}

// Gọi hàm updateSlickDotsWidth khi carousel thay đổi slide
$('.room-slick').on('afterChange', function () {
    updateSlickDotsWidth();
});

// Gọi hàm updateSlickDotsWidth khi trang web được tải
$(window).on('load', function () {
    updateSlickDotsWidth();
});
function Show() {
    $('.box-search').addClass('active');
}
function Hide() {
    $('.box-search').removeClass('active');
}
function showMb() {
    $('.menu-mb').addClass('active');
    $('.overlay').addClass(' active');
}
function HidenMb() {
    $('.menu-mb').removeClass('active');
    $('.overlay').removeClass(' active');
}
function CloseMenu() {
    $('.menu-mb').removeClass('active');
    $('.overlay').removeClass(' active');
}

var backToTopDiv = $('.backtop');
backToTopDiv.on('click', function (e) {
    e.preventDefault(); // Ngăn chặn hành vi mặc định khi nhấp vào phần tử
    $('html, body').animate({ scrollTop: 0 }, 'slow');
});
$(window).scroll(function () {
    if ($(this).scrollTop() > 200) {
        backToTopDiv.fadeIn(); // Hiển thị phần tử "Back to Top"
    } else {
        backToTopDiv.fadeOut(); // Ẩn phần tử "Back to Top"
    }
});
$('.play-img').click(function () {
    $.fancybox({
        href: $(this).attr('href')
        // Tùy chỉnh kích thước và giao diện Fancybox tại đây
    });
    return false;
});