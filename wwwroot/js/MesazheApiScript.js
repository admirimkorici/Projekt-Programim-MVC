var uri = "/api/WebApi";
$(document).ready(function () {
    $.getJSON(uri).done(function (data) {
        $(".msg-cart").html("");
        //if (data.length != 0) {
            $.each(data, function (key, item) {
                $(".msg-cart").append("<a class='msg-link' href='/Mesazhe/Details/" + item.id + "/'><div class='box'><h2>" + item.emri + "</h2> <div class='content'><div class='email'>" +
                    item.email + "</div> <div class='Subjekti'>" + item.subjekti + "</div></div><hr>");
            });
        //}
        /*else {
            $(".msg-cart").append("<h3 style='text-align: center;' class='font-weight-light'>Nuk ka mesazhe te palexuara</h3>")
        }*/
        $(".msg-cart").append("<a href='/Mesazhe/Index/' class='btn'>Shiko te gjitha mesazhet</a>")
    });
});