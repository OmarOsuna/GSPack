$(document).ready(function () {

});




function existe() {
    var folio = $('#idRastreo').val();

    if (folio != "") {
        $.ajax({
            url: baseUrl + "Orden/Existe",
            type: "GET",
            data: {
                folio: folio
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modalC = $("#mdContent");
                    $('#mdMain').modal();
                    modalC.load(baseUrl + 'Orden/ListaStatus/0' + folio, function () {
                        cargarDatos();
                    });
                }
                else {
                    var modalC = $("#mdContent");
                    $('#mdMain').modal();
                    modalC.load(baseUrl + 'Orden/Error', function () {
                    });
                }
            },
            error: function (xhr, exception) {
            }
        });
    }  

}

function cargarDatos() {

    var folio = $('#idRastreo').val();
    if (folio != "") {
        $.ajax({
            url: baseUrl + "Orden/ObtenerPorFolio",
            data: { folio: folio },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#idOrden").val(data.Id);
                $("#nombreRemitente").text(data.Cliente.Nombre)
                $("#correoRemitente").text(data.Cliente.Correo);
                $("#numeroRemitente").text(data.Cliente.TelefonoUno);
                var fechaEntrega = new Date(parseInt(data.FechaEntrega.substr(6)));
                $("#fechaEntrega").text(fechaEntrega);
                var fechaSalida = new Date(parseInt(data.FechaSalida.substr(6)));
                $("#fechaSalida").text(fechaSalida);
                $("#costo").text(data.Precio);

                marcarPuntos(data.Id);
            }
        });
    }

    var table = $('#tabla-estatus').DataTable();
    table.destroy();
    $('#tabla-estatus').DataTable({
        'paging': true,
        'pageLength': 10,
        'lengthChange': false,
        'searching': false,
        'ordering': false,
        'info': false,
        'autoWidth': false,
        "processing": false,
        'language': {
            paginate: {
                next: 'Siguiente',
                previous: 'Anterior',
                last: 'Último',
                first: 'Primero'
            },
            info: 'Mostrando _START_ a _END_ de _TOTAL_ resultados',
            infoEmpty: '0 Resgistros',
            emptyTable: 'No hay registros',
            search: 'Buscar: '
        },

        "ajax": {
            url: baseUrl + "Orden/ObtenerStatusDTOFolio",
            data: { folio: folio }
        },
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "TipoEstatu" },
            { "data": "Descripcion" },
            { "data": "Fecha" },
            { "data": "Lugar" }
        ]
    });


   

}


function marcarPuntos(id) {
    /// <reference path="home.js" />
    jQuery(function ($) {
        // Asynchronously Load the map API 
        var script = document.createElement('script');
        script.src = "https://maps.googleapis.com/maps/api/js?sensor=false&callback=initialize";
        document.body.appendChild(script);
    });

    console.log(id);
    if (id != "" && id != 0) {
        $.ajax({
            url: baseUrl + "Orden/ObtenerStatus",
            data: { id: id },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var map;
                var bounds = new google.maps.LatLngBounds();
                var mapOptions = {
                    mapTypeId: 'roadmap'
                };
                map = new google.maps.Map(document.getElementById("map_canvas"), mapOptions);
                map.setTilt(45);
                var infoWindow = new google.maps.InfoWindow(), marker, i;
                var i = 0;
                $.each(data, function (index, value) {
                    $.each(value, function (index, status) {
                        var tipoStatu;
                        if (status.TipoEstatu == 1) {
                            tipoStatu = "Pendiente"
                        }
                        if (status.TipoEstatu == 2) {
                            tipoStatu = "Entregado"
                        }
                        if (status.TipoEstatu == 3) {
                            tipoStatu = "Cancelado"
                        }
                        var thisDate = new Date(parseInt(status.Fecha.substr(6)));
                        var position = new google.maps.LatLng(status.Latitud, status.Longitud);
                        bounds.extend(position);
                        marker = new google.maps.Marker({
                            position: position,
                            map: map,
                            title: status.Descripcion
                        });
                        google.maps.event.addListener(marker, 'click', (function (marker, i) {
                            return function () {
                                infoWindow.setContent('<div class="info_content"><h3>' + status.Ciudad + ' ' + status.Estado + '</h3><p>' + status.Descripcion + ' (' + tipoStatu + ')</p><p>' + thisDate + '</p></div>');
                                infoWindow.open(map, marker);
                            }
                        })(marker, i));
                        map.fitBounds(bounds);
                        i++;


                    });
                });
                var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function (event) {
                    this.setZoom(6);
                    google.maps.event.removeListener(boundsListener);
                });
            }
        });
    }

}



