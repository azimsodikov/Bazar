// Write your Javascript code.

$("#nav-shopping-cart").on("click", function (evt) {
    evt.preventDefault();
    $(".cart--floating").slideToggle();
})
$(".add_bag").on("click", function (e) {
    $(".cart--floating").slideToggle();
})
$(".search").on("click", function (e) {
    e.preventDefault();
    $(".search-box").fadeToggle();
})
