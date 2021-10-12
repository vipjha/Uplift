﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tbldData').DataTable({
        "ajax": {
            "url": "/admin/category/GetAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "name", "width": "50%" },
            { "data": "displayOrder", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class = "text-center">
                               <a href="/Admin/category/Upsert/${data}" class='btn btn-success text-white' style='cursor:pointer;width:100px;'>                         
                                <i class='far fa-edit'></i>Edit</a>
                                &nbsp;
                                <a class='btn btn-danger text-white' style='cursor:pointer;width:100px;' onclick=Delete('/admin/category/Delete/'+${data}) >
                                <i class='far fa-trash-alt'></i>Delete</a>
                            </div> `;
                }, "width": "30%"
            }
        ],
        "language": {
            "emptyTable": "No records found."
        },
        "width": "100%"
    });
}

function Delete(url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the content!",
        type: "warning",
        showCancleButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message)
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}

function ShowMessage(msg) {

}