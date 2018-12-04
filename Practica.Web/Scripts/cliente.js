$(document).ready(function () {
    cargarTablaRemitentes();
    cargarTablaDestinatarios();
});

function cargarTablaRemitentes() {
    var table = $('#tabla-remitentes').DataTable();
    table.destroy();
    $('#tabla-remitentes').DataTable({
        'paging': true,
        'pageLength': 10,
        'lengthChange': false,
        'searching': true,
        "processing": true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
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
        "ajax": baseUrl + "Cliente/ObtenerTodos",
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "Nombre" },
            { "data": "Correo" },
            { "data": "TelefonoUno" },
            { "data": "RFC" },
        ]
    });
    activarRenglonRemitente();
}

function cargarTablaDestinatarios() {
    var table = $('#tabla-destinatarios').DataTable();
    table.destroy();
    $('#tabla-destinatarios').DataTable({
        'paging': true,
        'pageLength': 10,
        'lengthChange': false,
        'searching': true,
        "processing": true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
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
        "ajax": baseUrl + "Destinatario/ObtenerTodos",
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "Nombre" },
            { "data": "Domicilio" },
            { "data": "Telefono" },
            { "data": "Correo" }
        ]
    });
    activarRenglonDestinatario();
}


function activarRenglonRemitente() {
    var singleSelect = $('#tabla-remitentes').DataTable();
    $('#tabla-remitentes tbody').on('click', 'tr', function () {
        if ($(this).hasClass('')) {
            $(this).removeClass('active');
        }
        else {
            singleSelect.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });
}

function activarRenglonDestinatario() {
    var singleSelect = $('#tabla-destinatarios').DataTable();
    $('#tabla-destinatarios tbody').on('click', 'tr', function () {
        if ($(this).hasClass('')) {
            $(this).removeClass('active');
        }
        else {
            singleSelect.$('tr.active').removeClass('active');
            $(this).addClass('active');
        }
    });
}


function obtenerIdRemitente() {
    var table = $('#tabla-remitentes').DataTable();
    var id = 0;
    if (table.$('tr.active')[0] != undefined) {
        var selectedIndex = table.$('tr.active')[0]._DT_RowIndex
        var row = table.row(selectedIndex).data();
        id = row.Id;
    }
    return id;
}

function obtenerIdDestinatario() {
    var table = $('#tabla-destinatarios').DataTable();
    var id = 0;
    if (table.$('tr.active')[0] != undefined) {
        var selectedIndex = table.$('tr.active')[0]._DT_RowIndex
        var row = table.row(selectedIndex).data();
        id = row.Id;
    }
    return id;
}


function cargarDatosRemitente(id) {
    if (id != "" && id != 0) {
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
    return false;
}

function cargarDatosDestinatario(id) {     
    if (id != "" && id != 0) {
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
    return false;
}





function addRemitente() {
    var modalC = $("#mdContent");
    $('#mdMain').modal();
    modalC.load(baseUrl + 'Cliente/Add', {});
}

function addDestinatario() {
    var modalC = $("#mdContent");
    $('#mdMain').modal();
    modalC.load(baseUrl + 'Destinatario/Add', {});
}

function editRemitente() {

    var modalC = $("#mdContent");
    var id = obtenerIdRemitente();
    if (id != 0) {
        $('#mdMain').modal();
        modalC.load(baseUrl + 'Cliente/Edit/0' + id, function () {
            cargarDatosRemitente(id);
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }    
}

function editDestinatario() {

    var modalC = $("#mdContent");
    var id = obtenerIdDestinatario();
    if (id != 0) {
        $('#mdMain').modal();
        modalC.load(baseUrl + 'Destinatario/Edit/0' + id, function () {
            cargarDatosDestinatario(id);
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }
}



function editarRemitente() {
    var id = obtenerIdRemitente();
    var nombre = $.trim($("#nombreRemitente").val());
    var telefonoUno = $.trim($("#telefonoUnoRemitente").val());
    var telefonoDos = $.trim($("#telefonoDosRemitente").val());
    var telefonoTres = $.trim($("#telefonoTresRemitente").val());
    var correo = $.trim($("#correoRemitente").val());
    var rfc = $.trim($("#rfcRemitente").val());

    if (nombre == "" || telefonoUno == "" || correo == "" || rfc == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {

        $.ajax({
            url: baseUrl + "Cliente/Guardar",
            data: {
                id: id, nombre: nombre, telefonoUno: telefonoUno, telefonoDos: telefonoDos, telefonoTres: telefonoTres, correo: correo, rfc: rfc
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Remitente modificado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTablaRemitentes();
                }
            },
            error: function (xhr, exception) {
            }
        });

    }
}

function editarDestinatario() {
    var id = obtenerIdDestinatario();
    var nombre = $.trim($("#nombreDestinatario").val());
    var correo = $.trim($("#correoDestinatario").val());
    var telefono = $.trim($("#telefonoDestinatario").val());
    var calle = $.trim($("#calleDomicilioD").val());
    var numero = $.trim($("#numeroDomicilioD").val());
    var avenida = $.trim($("#avenidaDomicilioD").val());
    var codigoPostal = $.trim($("#cpDomicilioD").val());
    var colonia = $.trim($("#coloniaDomicilioD").val());
    var estado = $.trim($("#estadoDomicilioD").val());
    var ciudad = $.trim($("#ciudadDomicilioD").val());
    var referencia = $.trim($("#referenciaDomicilioD").val());

    if (nombre == "" || correo == "" || telefono == "" || calle == "" || numero == "" || codigoPostal == "" || colonia == "" || estado == "" || ciudad == "" || referencia == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {

        $.ajax({
            url: baseUrl + "Destinatario/Guardar",
            data: {
                id: id, nombre: nombre, correo: correo, telefono: telefono, calle: calle, numero: numero, avenida: avenida, codigoPostal: codigoPostal, colonia: colonia, estado: estado, ciudad: ciudad, referencia: referencia
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Destinatario modificado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTablaDestinatarios();
                }
            },
            error: function (xhr, exception) {
            }
        });

    }
}


function eliminarRemitente() {
    var id = obtenerIdRemitente();
    if (id != 0) {

        swal({
            title: '¿Deseas eliminar este remitente?',
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
                    url: baseUrl + "Cliente/Eliminar",
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
                                title: 'Remitente eliminado correctamente',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            cargarTablaRemitentes();
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

function eliminarDestinatario() {
    var id = obtenerIdDestinatario();
    if (id != 0) {

        swal({
            title: '¿Deseas eliminar este Destinatario?',
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
                    url: baseUrl + "Destinatario/Eliminar",
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
                                title: 'Destinatario eliminado correctamente',
                                showConfirmButton: false,
                                timer: 1500
                            })
                            cargarTablaDestinatarios();
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


function guardarDestinatario() {
    var nombre = $.trim($("#nombreDestinatario").val());
    var correo = $.trim($("#correoDestinatario").val());
    var telefonoDestinatario = $.trim($("#telefonoDestinatario").val());
    var calleDomicilioD = $.trim($("#calleDomicilioD").val());
    var numeroDomicilioD = $.trim($("#numeroDomicilioD").val());
    var avenidaDomicilioD = $.trim($("#avenidaDomicilioD").val());
    var cpDomicilioD = $.trim($("#cpDomicilioD").val());
    var coloniaDomicilioD = $.trim($("#coloniaDomicilioD").val());
    var estadoDomicilioD = $.trim($("#estadoDomicilioD").val());
    var ciudadDomicilioD = $.trim($("#ciudadDomicilioD").val());
    var referenciaDomicilioD = $.trim($("#referenciaDomicilioD").val());

    if (nombre == "" || telefonoDestinatario == "" || calleDomicilioD == "" || numeroDomicilioD == "" || correo == "" || cpDomicilioD == "" || coloniaDomicilioD == "" || 
        estadoDomicilioD == "" || ciudadDomicilioD == "" || referenciaDomicilioD == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Destinatario/Guardar",
            type: "POST",
            data: {
                id: "0", nombre: nombre, telefono: telefonoDestinatario, calle: calleDomicilioD, numero: numeroDomicilioD, avenida: avenidaDomicilioD,
                correo: correo, codigoPostal: cpDomicilioD, colonia: coloniaDomicilioD, estado: estadoDomicilioD, ciudad: ciudadDomicilioD, referencia: referenciaDomicilioD
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Destinatario registrado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTablaDestinatarios();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }

}

function guardarRemitente() {
    var nombre = $.trim($("#nombreRemitente").val());
    var telefonoUno = $.trim($("#telefonoUnoRemitente").val());
    var telefonoDos = $.trim($("#telefonoDosRemitente").val());
    var telefonoTres = $.trim($("#telefonoTresRemitente").val());
    var correo = $.trim($("#correoRemitente").val());
    var rfc = $.trim($("#rfcRemitente").val());

    if (nombre == "" || telefonoUno == "" || correo == "" || rfc == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else {
        $.ajax({
            url: baseUrl + "Cliente/Guardar",
            type: "POST",
            data: {
                id: "0", nombre: nombre, telefonoUno: telefonoUno, telefonoDos: telefonoDos, telefonoTres: telefonoTres, correo: correo, rfc: rfc
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Remitente registrado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTablaRemitentes();
                }
            },
            error: function (xhr, exception) {
            }
        });
    }
}


