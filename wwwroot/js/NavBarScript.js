$(document).ready(function () {
    $("#menu-btn").click(function () {
        $(".navbar").toggleClass("active");
        $(".msg-cart").removeClass("active");
        $(".filter-content").removeClass("active");
        $(".search-specification").removeClass("active");
        $(".search-form-aksesore").removeClass("active");
        $(".search-form-celesa").removeClass("active");
        $(".search-form-cipa").removeClass("active");
    });
});