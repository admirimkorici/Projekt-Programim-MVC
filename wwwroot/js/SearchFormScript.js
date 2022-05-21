$(document).ready(function () {
    $("#search-btn").click(function () {
        $(".search-form").toggleClass("active");
        $(".msg-cart").removeClass("active");
        $(".filter-content").removeClass("active");
        $(".navbar").removeClass("active");
    });
});