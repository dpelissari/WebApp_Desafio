$(document).ready(function () {

    $('.glyphicon-calendar').closest("div.date").datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        calendarWeeks: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'pt-BR'
    });

    var $solicitante = $('#Solicitante');
    var solicitantes = $solicitante.data('solicitantes') || [];
    var $listaSolicitantes = $('<ul class="solicitante-sugestoes">').hide().insertAfter($solicitante);

    $solicitante.on('input', function () {
        var termo = $solicitante.val().toLowerCase();
        $listaSolicitantes.empty();
        if (!termo) {
            $listaSolicitantes.hide();
            return;
        }
        $.each(solicitantes, function (_, nome) {
            if (nome.toLowerCase().indexOf(termo) !== -1)
                $listaSolicitantes.append($('<li>').text(nome));
        });
        $listaSolicitantes.toggle($listaSolicitantes.children().length > 0);
    });

    $listaSolicitantes.on('mousedown', 'li', function () {
        $solicitante.val($(this).text());
        $listaSolicitantes.hide();
    });

    $solicitante.on('blur', function () {
        $listaSolicitantes.hide();
    });

    $('#btnCancelar').click(function () {
        Swal.fire({
            html: "Deseja cancelar essa operação? O registro não será salvo.",
            type: "warning",
            showCancelButton: true,
        }).then(function (result) {
            if (result.value) {
                history.back();
            }
        });
    });

    $('#btnSalvar').click(function () {

        if ($('#form').valid() != true) {
            FormularioInvalidoAlert();
            return;
        }

        let chamado = SerielizeForm($('#form'));
        let url = $('#form').attr('action');

        $.ajax({
            type: "POST",
            url: url,
            data: chamado,
            success: function (result) {

                Swal.fire({
                    type: result.Type,
                    title: result.Title,
                    text: result.Message,
                }).then(function () {
                    window.location.href = config.contextPath + result.Controller + '/' + result.Action;
                });

            },
            error: function (result) {

                Swal.fire({
                    text: result.responseJSON.Message,
                    confirmButtonText: 'OK',
                    icon: 'error'
                });

            },
        });
    });

});
