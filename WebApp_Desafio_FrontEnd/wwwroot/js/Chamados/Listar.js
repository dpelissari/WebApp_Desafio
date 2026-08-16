$(document).ready(function () {

    var table = $('#dataTables-Chamados').DataTable({
        paging: false,
        ordering: false,
        info: false,
        searching: false,
        processing: true,
        serverSide: true,
        autoWidth: false,
        ajax: config.contextPath + 'Chamados/Datatable',
        columns: [
            { data: 'ID' },
            { data: 'Assunto' },
            { data: 'Solicitante' },
            { data: 'Departamento' },
            { data: 'DataAberturaWrapper', title: 'Data Abertura' },
        ],
    });

    $('#dataTables-Chamados tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#dataTables-Chamados tbody').on('dblclick', 'tr', function () {
        var data = table.row(this).data();
        if (!data || !data.ID) {
            return;
        }

        window.location.href = config.contextPath + 'Chamados/Editar/' + data.ID;
    });

    $('#btnRelatorio').click(function () {
        window.location.href = config.contextPath + 'Chamados/Report';
    });

    $('#btnAdicionar').click(function () {
        window.location.href = config.contextPath + 'Chamados/Cadastrar';
    });

    $('#btnEditar').click(function () {
        var data = table.row('.selected').data();
        if (!data) {
            Swal.fire({ type: "warning", text: "Selecione um registro para continuar." });
            return;
        }

        window.location.href = config.contextPath + 'Chamados/Editar/' + data.ID;
    });

    $('#btnExcluir').click(function () {
        var data = table.row('.selected').data();
        if (!data) {
            Swal.fire({ type: "warning", text: "Selecione um registro para continuar." });
            return;
        }

        Swal.fire({
            text: "Tem certeza de que deseja excluir " + data.Assunto + " ?",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {

            if (result.value) {
                $.ajax({
                    url: config.contextPath + 'Chamados/Excluir/' + data.ID,
                    type: 'DELETE',
                    contentType: 'application/json',
                    error: function (result) {

                        Swal.fire({
                            text: result.responseJSON.Message,
                            confirmButtonText: 'OK',
                            icon: 'error'
                        });

                    },
                    success: function (result) {

                        Swal.fire({
                            type: result.Type,
                            title: result.Title,
                            text: result.Message,
                        }).then(function () {
                            table.draw();
                        });
                    }
                });
            }

        });
    });

});