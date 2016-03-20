window.noCollapse = function (e) {
    e.stopPropagation();
}

$('.collapse').on('show.bs.collapse', function () {
    $('.collapse.in').collapse('hide');
});