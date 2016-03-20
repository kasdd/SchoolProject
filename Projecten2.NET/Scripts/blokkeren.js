var dateToday = new Date();

$(document).ready(function () {

    function changeBeschikbaar(date, naam) {
        $.get("/Blokkering/GetBeschikbaar", { 'datum': date, 'naam': naam }, function (aantal) {
            $("#beschikbaar").text(function () {
                return aantal;
            });
        })
    };

    $(document).ready(function () {
        $('.date').not('.hasDatePicker').datepicker({
            dateFormat: 'dd/mm/yy',
            beforeShowDay: disableAllButMonday,
            minDate: dateToday,
            showOn: 'button',
            onSelect: function (dateText) {
                $(this).change();
                changeBeschikbaar(this.value, naam);
            }
        }).next('button').button({
            icons: {
                primary: "ui-icon-calendar"
            }, text: false
        });
    });;
    function disableAllButMonday(date) {

        var day = date.getDay();
        // If day == 1 then it is Monday
        if (day == 1) {
            return [true];
        } else {
            return [false];
        }
    }

    function callback() {
        $('#hidden').hide();
    }
    function show() {
        $('#hidden').show();
    };;
    $("#meer").click(function () {
        show();
    });
    $("#minder").click(function () {
        callback();
    });
    $('#hidden').hide();

    $.datepicker.regional['nl'] = {
        clearText: 'Effacer', clearStatus: '',
        closeText: 'sluiten', closeStatus: 'Onveranderd sluiten ',
        prevText: '<vorige', prevStatus: 'Zie de vorige maand',
        nextText: 'volgende>', nextStatus: 'Zie de volgende maand',
        currentText: 'Huidige', currentStatus: 'Bekijk de huidige maand',
        monthNames: ['januari', 'februari', 'maart', 'april', 'mei', 'juni',
        'juli', 'augustus', 'september', 'oktober', 'november', 'december'],
        monthNamesShort: ['jan', 'feb', 'mrt', 'apr', 'mei', 'jun',
        'jul', 'aug', 'sep', 'okt', 'nov', 'dec'],
        monthStatus: 'Bekijk een andere maand', yearStatus: 'Bekijk nog een jaar',
        weekHeader: 'Sm', weekStatus: '',
        dayNames: ['zondag', 'maandag', 'dinsdag', 'woensdag', 'donderdag', 'vrijdag', 'zaterdag'],
        dayNamesShort: ['zo', 'ma', 'di', 'wo', 'do', 'vr', 'za'],
        dayNamesMin: ['zo', 'ma', 'di', 'wo', 'do', 'vr', 'za'],
        dayStatus: 'Gebruik DD als de eerste dag van de week', dateStatus: 'Kies DD, MM d',
        dateFormat: 'dd/mm/yy', firstDay: 1,
        initStatus: 'Kies een datum', isRTL: false
    };
    $.datepicker.setDefaults($.datepicker.regional['nl']);
});
