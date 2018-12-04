$(document).ready(function () {
    cargarTabla();
});

function loadRemitente(id) {
    $.ajax({
        url: baseUrl + "Cliente/ObtenerPorId",
        data: { id: id },
        type: 'GET',
        dataType: 'json',
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#nombreRemitente").val(data.Nombre);
            $("#telefonoUnoRemitente").val(data.TelefonoUno);
            $("#telefonoDosRemitente").val(data.TelefonoDos);
            $("#telefonoTresRemitente").val(data.TelefonoTres);
            $("#correoRemitente").val(data.Correo);
            $("#rfcRemitente").val(data.RFC);
        }
    });
}

function loadDestinatario(id) {
    $.ajax({
        url: baseUrl + "Destinatario/ObtenerPorId",
        data: { id: id },
        type: 'GET',
        dataType: 'json',
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#nombreDestinatario").val(data.Nombre);
            $("#correoDestinatario").val(data.Correo);
            $("#telefonoDestinatario").val(data.Telefono);
            $("#calleDomicilioD").val(data.Domicilio.Calle);
            $("#numeroDomicilioD").val(data.Domicilio.Numero);
            $("#avenidaDomicilioD").val(data.Domicilio.Avenida);
            $("#coloniaDomicilioD").val(data.Domicilio.Colonia);
            $("#cpDomicilioD").val(data.Domicilio.CodigoPostal);
            $("#estadoDomicilioD").val(data.Domicilio.Estado);
            $("#ciudadDomicilioD").val(data.Domicilio.Ciudad);
            $("#referenciaDomicilioD").val(data.Domicilio.Referencia);

        }
    });
}

function cargarTabla() {
    var table = $('#tabla-ordenes').DataTable();
    table.destroy();
    $('#tabla-ordenes').DataTable({
        'paging': true,
        'pageLength': 10,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
        "processing": true,
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

        "ajax": baseUrl + "Orden/ObtenerTodosDTO",
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "Folio" },
            { "data": "FechaHoraSalida" },
            { "data": "FechaEntrega" },
            { "data": "Remitente" },
            { "data": "Destinatario" },
            { "data": "DomicilioEntrega" },
            { "data": "Precio" }
        ]
    });
    activarRenglon();
}

function activarRenglon() {
    var singleSelect = $('#tabla-ordenes').DataTable();
    $('#tabla-ordenes tbody').on('click', 'tr', function () {
        if ($(this).hasClass('')) {
            $(this).removeClass('active');
        }
        else {
            singleSelect.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });
}

function activarRenglonStatus() {
    var singleSelect = $('#tabla-estatus').DataTable();
    $('#tabla-estatus tbody').on('click', 'tr', function () {
        if ($(this).hasClass('')) {
            $(this).removeClass('active');
        }
        else {
            singleSelect.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });
}

function obtenerId() {
    var table = $('#tabla-ordenes').DataTable();
    var id = 0;
    if (table.$('tr.active')[0] != undefined) {
        var selectedIndex = table.$('tr.active')[0]._DT_RowIndex
        var row = table.row(selectedIndex).data();
        id = row.Id;
    }
    return id;
}

function obtenerIdStatus() {
    var table = $('#tabla-estatus').DataTable();
    var id = 0;
    if (table.$('tr.active')[0] != undefined) {
        var selectedIndex = table.$('tr.active')[0]._DT_RowIndex
        var row = table.row(selectedIndex).data();
        id = row.Id;
    }
    return id;
}

function cargarDatos(id) {
    if (id != "" && id != 0) {
        $.ajax({
            url: baseUrl + "Orden/ObtenerPorId",
            data: { id: id },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#nombreRemitente").val(data.Cliente.Nombre);
                $("#telefonoUnoRemitente").val(data.Cliente.TelefonoUno);
                $("#telefonoDosRemitente").val(data.Cliente.TelefonoDos);
                $("#telefonoTresRemitente").val(data.Cliente.TelefonoTres);
                $("#correoRemitente").val(data.Cliente.Correo);
                $("#rfcRemitente").val(data.Cliente.RFC);

                $("#nombreDestinatario").val(data.Destinatario.Nombre);
                $("#telefonoDestinatario").val(data.Destinatario.Telefono);
                $("#correoDestinatario").val(data.Destinatario.Correo);

                $("#calleDomicilioD").val(data.Destinatario.Domicilio.Calle);
                $("#numeroDomicilioD").val(data.Destinatario.Domicilio.Numero);
                $("#avenidaDomicilioD").val(data.Destinatario.Domicilio.Avenida);
                $("#cpDomicilioD").val(data.Destinatario.Domicilio.CodigoPostal);
                $("#coloniaDomicilioD").val(data.Destinatario.Domicilio.Colonia);
                $("#estadoDomicilioD").val(data.Destinatario.Domicilio.Estado);
                $("#referenciaDomicilioD").val(data.Destinatario.Domicilio.Referencia);
                $("#ciudadDomicilioD").val(data.Destinatario.Domicilio.Ciudad);
                $("#destinatarioDos").val(data.DestinatarioDos);

                $("#tipoPaquete").val(data.Paquete.TipoEnvio);
                $("#pesoPaquete").val(data.Paquete.Peso);
                $("#tamanoPaquete").val(data.Paquete.Tamano);
                $("#descripcionPaquete").val(data.Paquete.Descripcion);

                var thisDate = new Date(parseInt(data.FechaEntrega.substr(6)));
                $("#fechaEntrega").val(thisDate.toLocaleString());
                $("#precioPaquete").val(data.Precio);
            }
        });
    }
    return false;
}

function cargarStatus(id) {
    if (id != "" && id != 0) {
        var tipoEstatu;
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
                url: baseUrl + "Orden/ObtenerStatusDTO",
                data: { id: id }
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
    activarRenglonStatus();
}

function cargarDatosStatus(id) {
    if (id != "" && id != 0) {
        $.ajax({
            url: baseUrl + "Estatu/ObtenerPorId",
            data: { id: id },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#idStatus").val(data.Id);
                $("#tipoStatus").val(data.TipoEstatu);
                $("#descripcionStatus").val(data.Descripcion);
                $("#estadoStatus").val(data.Estado);
                $("#ciudadStatus").val(data.Ciudad);
                $('#map').locationpicker({
                    radius: 0,
                    mapOptions: {
                        zoom: 6
                    },
                    location: {
                        latitude: data.Latitud,
                        longitude: data.Longitud,


                    },
                    inputBinding: {
                        latitudeInput: $('#latitudStatus'),
                        longitudeInput: $('#longitudStatus')
                    }

                });

            }
        });
    }
    return false;
}



function add() {
    var modalC = $("#mdContent");
    $('#mdMain').modal();
    modalC.load(baseUrl + 'Orden/Add', {});
}

function edit() {

    var modalC = $("#mdContent");
    var id = obtenerId();
    if (id != 0) {
        $('#mdMain').modal();
        modalC.load(baseUrl + 'Orden/Edit/0' + id, function () {
            cargarDatos(id);
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }

}

function addStatus() {
    var modalC = $("#mdContentStatus");
    $('#mdMainStatus').modal();
    modalC.load(baseUrl + 'Estatu/Add', {});
}

function editStatus() {
    var modalC = $("#mdContentStatus");
    var id = obtenerIdStatus();
    if (id != 0) {
        $('#mdMainStatus').modal();
        modalC.load(baseUrl + 'Estatu/Edit/0' + id, function () {
            cargarDatosStatus(id)
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }

}

function viewStatus() {
    var modalC = $("#mdContentStatus");
    var id = obtenerId();
    if (id != 0) {
        $('#mdMainStatus').modal();
        modalC.load(baseUrl + 'Orden/Vista/0' + id, function () {
            cargarStatus(id);
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }


}

function viewMap() {
    var id = obtenerId();
    var modalC = $("#mdContentStatus");
    var id = obtenerId();
    if (id != 0) {
        $('#mdMainStatus').modal();
        modalC.load(baseUrl + 'Estatu/Map/0' + id, function () {
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }


}

function guardarStatus() {
    var longitudStatus = $("#longitudStatus").val();
    var latitudStatus = $("#latitudStatus").val();
    var idOrden = obtenerId();
    var tipoEstatu = $.trim($("#tipoStatus").val());
    var descripcionStatus = $.trim($("#descripcionStatus").val());
    var estadoStatus = $.trim($("#estadoStatus").val());
    var ciudadStatus = $.trim($("#ciudadStatus").val());

    if (tipoEstatu == "" || descripcionStatus == "" || estadoStatus == "" || ciudadStatus == "") {

        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Estatu/Guardar",
            type: "POST",
            data: {
                id: "0", descripcion: descripcionStatus, tipoEstatu: tipoEstatu, estado: estadoStatus, ciudad: ciudadStatus, longitud: longitudStatus, latitud: latitudStatus, idOrden: idOrden
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Estatus creado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    viewStatus();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }

}

function editarStatus() {
    var longitudStatus = $("#longitudStatus").val();
    var latitudStatus = $("#latitudStatus").val();
    var idOrden = obtenerId();
    var idStatus = $("#idStatus").val();
    var tipoEstatu = $.trim($("#tipoStatus").val());
    var descripcionStatus = $.trim($("#descripcionStatus").val());
    var estadoStatus = $.trim($("#estadoStatus").val());
    var ciudadStatus = $.trim($("#ciudadStatus").val());

    if (tipoEstatu == "" || descripcionStatus == "" || estadoStatus == "" || ciudadStatus == "") {

        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Estatu/Guardar",
            type: "POST",
            data: {
                id: idStatus, descripcion: descripcionStatus, tipoEstatu: tipoEstatu, estado: estadoStatus, ciudad: ciudadStatus, longitud: longitudStatus, latitud: latitudStatus, idOrden: idOrden
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Estatus creado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    viewStatus();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }

}

function eliminarStatus() {
    var id = obtenerIdStatus();
    if (id != 0) {

        swal({
            title: '¿Deseas eliminar este Estatus?',
            text: "No podrás revertir esto!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'No, Cancelar',
            confirmButtonText: 'Si, Eliminar'

        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: baseUrl + "Estatu/Eliminar",
                    data: {
                        id: id
                    },
                    cache: false,
                    traditional: true,
                    success: function (data) {
                        if (data === "true") {
                            swal({
                                position: 'top-end',
                                type: 'success',
                                title: 'Estatus eliminado correctamente',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            viewStatus();
                        }
                    },
                    error: function (xhr, exception) {
                    }
                });

            }
        })

    }
    else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }
}



function eliminar() {
    var id = obtenerId();
    if (id != 0) {

        swal({
            title: '¿Deseas eliminar esta orden?',
            text: "No podrás revertir esto!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            cancelButtonText: 'No, Cancelar',
            confirmButtonText: 'Si, Eliminar'

        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: baseUrl + "Orden/Eliminar",
                    data: {
                        id: id
                    },
                    cache: false,
                    traditional: true,
                    success: function (data) {
                        if (data === "true") {
                            swal({
                                position: 'top-end',
                                type: 'success',
                                title: 'Orden eliminada correctamente',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            cargarTabla();
                        }
                    },
                    error: function (xhr, exception) {
                    }
                });

            }
        })

    }
    else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }
}

function guardarOrden() {

    var idRemitente = $("#idRemitente").val();
    var idDestinatario = $("#idDestinatario").val();

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

    if (idRemitente == null || idRemitente == "") {
        idRemitente = 0;
    }

    if (idDestinatario == null || idDestinatario == "") {
        idDestinatario = 0;
    }

    if (nombreRemitente == "" || telefonoUnoRemitente == "" || correoRemitente == "" || rfcRemitente == "" || nombreDestinatario == "" || telefonoDestinatario == "" || telefonoDestinatario == "" || correoDestinatario == "" || calleDomicilioD == "" || numeroDomicilioD == "" || cpDomicilioD == "" || coloniaDomicilioD == "" || estadoDomicilioD == "" || ciudadDomicilioD == "" || referenciaDomicilioD == "" || destinatarioDos == "" || tipoPaquete == "" || descripcionPaquete == "" || fechaEntrega == "" || precioPaquete == "") {

        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Orden/Guardar",
            type: "POST",
            data: {
                id: "0", nombreRemitente: nombreRemitente, telefonoUnoRemitente: telefonoUnoRemitente, telefonoDosRemitente: telefonoDosRemitente, telefonoTresRemitente: telefonoTresRemitente, correoRemitente: correoRemitente, rfcRemitente: rfcRemitente, nombreDestinatario: nombreDestinatario, telefonoDestinatario: telefonoDestinatario, correoDestinatario: correoDestinatario, calleDomicilioD: calleDomicilioD, numeroDomicilioD: numeroDomicilioD, avenidaDomicilioD: avenidaDomicilioD, cpDomicilioD: cpDomicilioD, coloniaDomicilioD: coloniaDomicilioD, estadoDomicilioD: estadoDomicilioD, ciudadDomicilioD: ciudadDomicilioD, referenciaDomicilioD: referenciaDomicilioD, destinatarioDos: destinatarioDos, tipoPaquete: tipoPaquete, pesoPaquete: pesoPaquete, tamanoPaquete: tamanoPaquete, descripcionPaquete: descripcionPaquete, fechaEntrega: fechaEntrega, precioPaquete: precioPaquete, idRemitente: idRemitente, idDestinatario: idDestinatario
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Orden creada correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTabla();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }
}

function editarOrden() {
    var id = obtenerId();
    var idRemitente = $("#idRemitente").val();
    var idDestinatario = $("#idDestinatario").val();

    if (idRemitente == null || idRemitente == "") {
        idRemitente = 0;
    }

    if (idDestinatario == null || idDestinatario == "") {
        idDestinatario = 0;
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


    if (nombreRemitente == "" || telefonoUnoRemitente == "" || correoRemitente == "" || rfcRemitente == "" || nombreDestinatario == "" || telefonoDestinatario == "" || telefonoDestinatario == "" || correoDestinatario == "" || calleDomicilioD == "" || numeroDomicilioD == "" || cpDomicilioD == "" || coloniaDomicilioD == "" || estadoDomicilioD == "" || ciudadDomicilioD == "" || referenciaDomicilioD == "" || destinatarioDos == "" || tipoPaquete == "" || descripcionPaquete == "" || fechaEntrega == "" || precioPaquete == "") {

        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Orden/Guardar",
            type: "POST",
            data: {
                id: id, nombreRemitente: nombreRemitente, telefonoUnoRemitente: telefonoUnoRemitente, telefonoDosRemitente: telefonoDosRemitente, telefonoTresRemitente: telefonoTresRemitente, correoRemitente: correoRemitente, rfcRemitente: rfcRemitente, nombreDestinatario: nombreDestinatario, telefonoDestinatario: telefonoDestinatario, correoDestinatario: correoDestinatario, calleDomicilioD: calleDomicilioD, numeroDomicilioD: numeroDomicilioD, avenidaDomicilioD: avenidaDomicilioD, cpDomicilioD: cpDomicilioD, coloniaDomicilioD: coloniaDomicilioD, estadoDomicilioD: estadoDomicilioD, ciudadDomicilioD: ciudadDomicilioD, referenciaDomicilioD: referenciaDomicilioD, destinatarioDos: destinatarioDos, tipoPaquete: tipoPaquete, pesoPaquete: pesoPaquete, tamanoPaquete: tamanoPaquete, descripcionPaquete: descripcionPaquete, fechaEntrega: fechaEntrega, precioPaquete: precioPaquete, idRemitente: idRemitente, idDestinatario: idDestinatario
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Orden editada correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTabla();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }




}







