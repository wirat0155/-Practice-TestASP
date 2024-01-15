$(document).ready(function () {
    $('#tblData').DataTable({
        searching: false,
        dom: 'Bfrtip',
        buttons: [
            'csv', 'excel', 'pdf', 'print'
        ]
    });
});