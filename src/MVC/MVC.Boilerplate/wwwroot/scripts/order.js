$(document).ready(function () {

    $("#test-registers").DataTable({
        "lengthMenu": [2,4,8,10,20],
        autoWidth: true,
        processing: true,
        serverSide: true,
        paging: true,
        searching: { regex: true },
        ordering: true,
        ajax: {
            url: "/Order/LoadTable",
            type: "GET",
            contentType: "application/json",
            dataType: "json"
        },
        columns: [
            { data: "id", name: "Id" },
            { data: "orderTotal", name: "OrderTotal" },
            {
                data: "orderPlaced",
                name: "OrderPlaced",
                type: 'datetime',
                "render": function (value) {
                    if (value === null) return "";
                    return moment(value).format('DD/MM/YYYY');
                }
            }
        ]
    });
});