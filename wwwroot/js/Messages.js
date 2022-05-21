$(document).ready(function () {
    $("#msg-btn").click(function () {
        $(".msg-cart").toggleClass("active");
        $(".navbar").removeClass("active");
        $(".search-specification").removeClass("active");
        $(".search-form-aksesore").removeClass("active");
        $(".search-form-celesa").removeClass("active");
        $(".filter-content").removeClass("active");
        $(".search-form-cipa").removeClass("active");
    });
});