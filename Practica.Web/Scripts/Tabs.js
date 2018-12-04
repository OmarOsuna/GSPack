$(document).ready(function () {
    //Initialize tooltips
    $('.nav-tabs > li a[title]').tooltip();

    //Wizard
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

        var $target = $(e.target);

        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });

    $(".next-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        $active.next().removeClass('disabled');
        nextTab($active);

    });
    $(".prev-step").click(function (e) {

        var $active = $('.wizard .nav-tabs li.active');
        prevTab($active);

    });
});

function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();
}
function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}








var nombreRemitente = $.trim($("#nombreRemitente").val());
var telefonoUnoRemitente = $.trim($("#telefonoUnoRemitente").val());
var telefonoDosRemitente = $.trim($("#telefonoDosRemitente").val());
var telefonoTresRemitente = $.trim($("#telefonoTresRemitente").val());
var correoRemitente = $.trim($("#correoRemitente").val());
var rfcRemitente = $.trim($("#rfcRemitente").val());


var nombreDestinatario = $.trim($("#nombreDestinatario").val());
var telefonoDestinatario = $.trim($("#telefonoDestinatario").val());
var correoDestinatario = $.trim($("#correoDestinatario").val());


var calleDomicilioD = $.trim($("#calleDomicilioD").val());
var numeroDomicilioD = $.trim($("#numeroDomicilioD").val());
var avenidaDomicilioD = $.trim($("#avenidaDomicilioD").val());
var cpDomicilioD = $.trim($("#cpDomicilioD").val());
var coloniaDomicilioD = $.trim($("#coloniaDomicilioD").val());
var estadoDomicilioD = $.trim($("#estadoDomicilioD").val());
var ciudadDomicilioD = $.trim($("#ciudadDomicilioD").val());
var referenciaDomicilioD = $.trim($("#referenciaDomicilioD").val());
var destinatarioDos = $.trim($("#destinatarioDos").val());


var tipoPaquete = $.trim($("#tipoPaquete").val());
var pesoPaquete = $.trim($("#pesoPaquete").val());
var tamanoPaquete = $.trim($("#tamanoPaquete").val());
var descripcionPaquete = $.trim($("#descripcionPaquete").val());



var fechaEntrega = $.trim($("#fechaEntrega").val());
var precioPaquete = $.trim($("#precioPaquete").val());
var folioPaquete = $.trim($("#folioPaquete").val());
var numeroRastreo = $.trim($("#numeroRastreo").val());