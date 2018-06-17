// Write your JavaScript code.
function readURL(input) {
    if (input.files && input.files[0]) {

        var imgElementId = $("#" + input.id).next("img").attr('id');

        reader.onload = function (e) {
            $('#' + imgElementId).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}
var reader = new FileReader();

$("#YellowImgUpload, #WhiteImgUpload, #RoseImgUpload, #FirstImgUpload, #SecondImgUpload")
    .change(function () {
        var reader = new FileReader();
        readURL(this);
    });


$(document).ready(() => {
    $('#example').DataTable({
        "ajax": {
            "url": "/home/AllJewelries",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [{
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            { "width": "10%", "targets": 1 }],
        "columns": [
            { "data": "id" },
            {
                "render": function (data, type, full, meta) { return '<img src= /images/shop/' + full.photo + '.jpg' + ' style="height:100px; width:100px">'; }
            },
            { "data": "jewelId" },
            { "data": "title" },
            { "data": "price" },
            { "data": "orderInFirstPage" },
            {
                "render": function (data, type, full, meta) { return '<a class="btn btn-info" href="/home/Jewelry/' + full.id + '">Edit</a><a class="btn btn-danger" href="/home/GetAllJewelries" onclick = DeleteData(' +'"'+ (full.id) +'"'+ ') ">Delete</a>'; }
            }
        ]
    });
});

function DeleteData(_jewelId) {
    if(confirm('Are you sure you want to delete.....'))
    {
        Delete(_jewelId);
    }
    else {
        return false
    }
};

function Delete(_jewelId) {
    $.get("Delete/"+_jewelId, (data) => {
        if(!data){
            alert('something goes wrong');
        }else{
            oTable = $('#example').DataTable();  
            oTable.draw();  
        }
    });
}