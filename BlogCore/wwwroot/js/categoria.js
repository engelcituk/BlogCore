let datatable;

$(document).ready( ()=> {
    cargarDatatable();
});

function cargarDatatable() {
    datatable = $("#tblCategorias").DataTable({
        "ajax": {
            "url": "/admin/categorias/GetAll",
            "type": "GET",
            "datatype":"json"
        },
        "columns": [
            { "data": "id", "width": "5%" },
            { "data": "nombre", "width": "50%" },
            { "data": "orden", "width": "20%" },
            {
                "data": "id",
                "render": (data) => {
                    return `<div class="text-center">
                                <a href='/Admin/Categorias/Edit/${data}' class='btn btn-success text-white' style='cursor:pointer; '> <i class='fas fa-edit'></i></a>&nbsp
                                <button onclick=Delete("/Admin/Categorias/Delete/${data}")  class='btn  btn-danger text-white' style='cursor:pointer; '><i class='fas fa-trash'></i></button>
                            </idv>`;
                },"width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No hay registros" 
        },
        "width":"100%"
    });
}

function Delete(url) {
    swal({
        title: "¿Está seguro de borrar?",
        text: "Este contenido no se podrá recuperar",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sí, borrar",
        closeOnConfirm: true
    },
    function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    datatable.ajax.reload();
                } else {
                    toastr.error(data.message);
                }
            }
        });
    });
}