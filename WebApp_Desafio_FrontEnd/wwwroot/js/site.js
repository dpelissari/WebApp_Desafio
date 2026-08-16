function FormularioInvalidoAlert(form) {
    let mensagensDeErro = $("span.text-danger.field-validation-error");
    if (form) {
        mensagensDeErro = form.find("span.text-danger.field-validation-error");
    }
    let msg = "";
    let errElem = {};
    for (var i = 0; i < mensagensDeErro.length; i++) {
        if (mensagensDeErro[i].children.length > 0) {
            msg = mensagensDeErro[i].children[0].innerHTML;
            errElem = mensagensDeErro[i].children[0];
            break;
        } else if (mensagensDeErro[i].innerHTML) {
            msg = mensagensDeErro[i].innerHTML;
            errElem = mensagensDeErro[i];
        }
    }
    if (msg) {
        Swal.fire({
            type: "warning",
            title: "Atenção",
            text: msg,
        }).then(function () {
            if (errElem) {
                let id = errElem.id.replace("-error", "");
                setTimeout(function () {
                    try {
                        $("#" + id).focus();
                    } catch (e) {
                    }
                }, 500);
            }
        });
    }
}

function SerielizeForm(form) {
    let json = {};
    let serArr = form.serializeArray();

    $.each(serArr, function (i, field) {
        if (json[field.name] == undefined) {
            json[field.name] = field.value || '';
        }
    });
    return json;
}
