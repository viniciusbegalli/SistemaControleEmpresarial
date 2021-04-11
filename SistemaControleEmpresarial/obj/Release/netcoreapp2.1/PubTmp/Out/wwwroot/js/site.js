// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
/*
$(document).on('click', '#navbarHeadDropdownMenuLink', function (e) {
    $("#navbarDropdownMenuLink").removeClass("dropdown-menu collapse in");//.addClass("collapse");
});*/

function confirmaDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmaDeleteSpan = 'confirmaDeleteSpan_' + uniqueId;
    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmaDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmaDeleteSpan).hide();
    }
}

function voltarPagina() {
    history.go(-1);
}

function MascaraTelefone(telefone) {
    setTimeout(function () {
        var v = mTelefone(telefone.value);
        if (v != telefone.value) {
            telefone.value = v;
        }
    }, 1);
}

function ValidaTelefone(telefone) {
    setTimeout(function () {
        var v = telefone.value.length;
        if (v == 1) {
            telefone.value = "";
        }
    }, 1);
}

function mTelefone(v) {
    var r = v.replace(/\D/g, "");
    r = r.replace(/^0/, "");
    if (r.length > 10) {
        r = r.replace(/^(\d\d)(\d{5})(\d{4}).*/, "($1) $2-$3");
    } else if (r.length > 5) {
        r = r.replace(/^(\d\d)(\d{4})(\d{0,4}).*/, "($1) $2-$3");
    } else if (r.length > 2) {
        r = r.replace(/^(\d\d)(\d{0,5})/, "($1) $2");
    } else {
        r = r.replace(/^(\d*)/, "($1");
    }
    return r;
}

function MascaraCPF(CPF) {
    setTimeout(function () {
        var v = mCPF(CPF.value);
        if (v != CPF.value) {
            CPF.value = v;
        }
    }, 1);
}

function mCPF(v) {
    var r = v.replace(/\D/g, "");
    if (r.length > 9) {
        r = r.replace(/^(\d\d\d)(\d{3})(\d{3})(\d{2}).*/, "$1.$2.$3-$4");
    } else if (r.length > 6) {
        r = r.replace(/^(\d\d\d)(\d{3})(\d{0,3}).*/, "$1.$2.$3");
    } else if (r.length > 3) {
        r = r.replace(/^(\d\d\d)(\d{0,3})/, "$1.$2");
    } else {
        r = r.replace(/^(\d*)/, "$1");
    }
    return r;
}

function ValidarCPF(Objcpf) {
    var cpf = Objcpf.value;
    exp = /\.|\-/g
    cpf = cpf.toString().replace(exp, "");
    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;

    for (i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);

    var digitoGerado = (soma1 * 10) + soma2;
    if (digitoGerado != digitoDigitado)
        alert('CPF Invalido!');
}

$(document).on('click', '#navbarHeadDropdownMenuLink', function (e) {
    if (!$(e.target).is('a:not(".dropdown-toggle")')) {
        $('#navbarDropdownMenuLink').collapse('hide');
    }
});

$(document).on('click', '#navbarHeadDropdownMenuLink2', function (e) {
    if (!$(e.target).is('a:not(".dropdown-toggle")')) {
        $('#navbarDropdownMenuLink2').collapse('hide');
    }
});
