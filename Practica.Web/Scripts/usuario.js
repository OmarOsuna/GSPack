$(document).ready(function () {
    cargarTabla();
});

function cargarTabla() {
    var table = $('#tabla-usuarios').DataTable(); 
    $("#tabla-usuarios tr").css('cursor', 'pointer');
    table.destroy();
    $('#tabla-usuarios').DataTable({
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
        "ajax": baseUrl + "Usuario/ObtenerTodos",
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "Nombre" },
            { "data": "Correo" },
            { "data": "Cuenta" },
            { "data": "Perfil" },
        ]
    });
    activarRenglon();
}

function activarRenglon() {
    var singleSelect = $('#tabla-usuarios').DataTable();    
    $('#tabla-usuarios tbody').on('click', 'tr', function () {
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
    var table = $('#tabla-usuarios').DataTable();
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
            url: baseUrl + "Usuario/ObtenerPorId",
            data: { id: id },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#nombre").val(data.Nombre);
                $("#correo").val(data.Correo);
                $("#cuenta").val(data.Cuenta);
                $("#perfil").val(data.Perfil);
            }
        });
    }
    return false;
}



function add() {
    var modalC = $("#mdContent");
    $('#mdMain').modal();
    modalC.load(baseUrl + 'Usuario/Add', {});
}

function edit() {
    var modalC = $("#mdContent");
    var id = obtenerId();
    if (id != 0) {
        $('#mdMain').modal();
        modalC.load(baseUrl + 'Usuario/Edit/0' + id, function () {
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

function eliminar() {
    var id = obtenerId();
    if (id != 0) {

        swal({
            title: '¿Deseas eliminar este usuario?',
            text: "No podrás revertir esto!",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            cancelButtonText: 'No, Cancelar',
            confirmButtonText: 'Si, Eliminar'
            
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: baseUrl + "Usuario/Eliminar",
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
                                title: 'Usuario eliminado correctamente',
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

function editar() {
    var id = obtenerId();
    var nombre = $.trim($("#nombre").val());
    var correo = $.trim($("#correo").val());
    var cuenta = $.trim($("#cuenta").val());
    var perfil = $.trim($("#perfil").val());


    if (nombre == "" || correo == "" || cuenta == "" || perfil == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })
    } else {
        $.ajax({
            url: baseUrl + "Usuario/Guardar",
            data: {
                id: id, nombre: nombre, correo: correo, cuenta: cuenta, password: " ", perfil: perfil
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Usuario modificado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTabla();
                }
                else {
                    swal(
                        'Correo Existente',
                        'Este correo ya existe,',
                        'error'
                    )
                }
            },
            error: function (xhr, exception) {
            }
        });

    }
}

function guardar() {
    var nombre = $.trim($("#nombre").val());
    var correo = $.trim($("#correo").val());
    var cuenta = $.trim($("#cuenta").val());
    var password = $.trim($("#password").val());
    var repetirPassword = $.trim($("#repetirPassword").val());
    var perfil = $.trim($("#perfil").val());

    if (nombre == "" || correo == "" || cuenta == "" || password == "" || repetirPassword == "" || perfil == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else if (password == repetirPassword) {
        $.ajax({
            url: baseUrl + "Usuario/Guardar",
            type: "POST",
            data: {
                id: "0", nombre: nombre, correo: correo, cuenta: cuenta, password: password, perfil: perfil
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Usuario registrado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    modal.modal("hide");
                    cargarTabla();
                }
                else {
                    swal(
                        'Correo Existente',
                        'Este correo ya existe,',
                        'error'
                    )
                }
            },
            error: function (xhr, exception) {
            }
        });
    }
    else {
        swal({
            position: 'center',
            type: 'error',
            title: 'Las contraseñas no coinciden',
            showConfirmButton: false,
            timer: 1500
        })
    }
}



function pass() {
    var modalC = $("#mdContent");
    var id = obtenerId();
    if (id != 0) {
        $('#mdMain').modal();
        modalC.load(baseUrl + 'Usuario/Pass/0' + id, function () {
        });
    } else {
        swal(
            'Registro no seleccionado!',
            'Favor de seleccionar un registro',
            'error'
        )
    }
}

function editarPassword() {
    var id = obtenerId();
    var password = $.trim($("#password").val());
    var repetirPassword = $.trim($("#repetirPassword").val());

    if (password == "" || repetirPassword == "") {
        swal({
            position: 'center',
            type: 'error',
            title: 'Favor de llenar todos los campos',
            showConfirmButton: false,
            timer: 1500
        })

    } else if (password == repetirPassword) {
        $.ajax({
            url: baseUrl + "Usuario/CambiarPassword",
            type: "POST",
            data: {
                id: id, password: password
            },
            cache: false,
            traditional: true,
            success: function (data) {
                if (data === "true") {
                    var modal = $("#mdMain");
                    swal({
                        position: 'top-end',
                        type: 'success',
                        title: 'Contraseña modificada correctamente',
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
    else {
        swal({
            position: 'center',
            type: 'error',
            title: 'Las contraseñas no coinciden',
            showConfirmButton: false,
            timer: 1500
        })
    }

}