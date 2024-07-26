var dotnetReference;

export function init(dotNetHelper) {
    dotnetReference = dotNetHelper;
    dotnetReference.invokeMethodAsync("Paginar", 1);
}

export function definirPaginacao(parametros) {
    const quantidadeDePaginas = parametros[0];

    var ulPaginacao = document.getElementById('paginacao');

    if (quantidadeDePaginas <= 0) {
        console.log("ZERO");
        ulPaginacao.style = 'visibility: hidden';
        return;
    }

    ulPaginacao.style = 'visibility: visible';
    var conteudo = '<li class="page-item"><a href="javascript:void(0);" class="page-link" id="primeira-pagina")"><i class="ki-solid ki-double-left fs-4"></i></a></li>';

    for (var i = 1; i <= quantidadeDePaginas; i++) {
        conteudo += '<li class="page-item m-1" id="pag' + i + '" data-custom="ermano"><a href="javascript:void(0);" class="page-link link-paginacao">' + i + '</a></li>';
    }

    conteudo += '<li class="page-item"><a href="javascript:void(0);" class="page-link" id="ultima-pagina")"><i class="ki-solid ki-double-right fs-4"></i></a></li>';
    
    ulPaginacao.innerHTML = conteudo;

    const primeiraPagina = document.getElementById('primeira-pagina');
    primeiraPagina.addEventListener('click', e => {
        e.preventDefault();
        paginar(1);
    });

    const itemPaginacao = document.getElementsByClassName('link-paginacao');
    for (let i = 0; i < itemPaginacao.length; i++) {
        itemPaginacao[i].addEventListener('click', e => {
            e.preventDefault();
            paginar(itemPaginacao[i].textContent);
        });
    }

    const ultimaPagina = document.getElementById('ultima-pagina');
    ultimaPagina.addEventListener('click', e => {
        e.preventDefault();
        paginar(quantidadeDePaginas);
    });

    definirSelecaoDosItensDaPaginacao(1);
    //definirDialerQuantidadeDeImpressoes();
}

export function definirDialerQuantidadeDeImpressoes() {
    var element = document.querySelector("#quantidade_de_impressoes");
    var dialer = new KTDialer(element, { min: 1, max: 100, step: 1 });
    dialer.on('kt.dialer.changed', function () {
        dotnetReference.invokeMethodAsync("DefinirQuantidadeDeImpressoes", parseInt(dialer.getValue()));

    });
}

export function paginar(pagina) {
    removerSelecaoDosItensDaPaginacao();
    definirSelecaoDosItensDaPaginacao(pagina);

    dotnetReference.invokeMethodAsync("Paginar", parseInt(pagina));
}

function removerSelecaoDosItensDaPaginacao() {
    var itens = document.querySelectorAll("ul.pagination li");
    itens.forEach((element) => {
        element.classList.remove('active');
    });
}

function definirSelecaoDosItensDaPaginacao(pagina) {
    var element = document.getElementById('pag' + pagina);
    element.classList.add('active')
}

//export function alertaDeConfirmacao(titulo, mensagem) {
//    return new Promise(resolve => {
//        Swal.fire({
//            title: titulo,
//            text: mensagem,
//            icon: "question",
//            buttonsStyling: false,
//            showCancelButton: true,
//            confirmButtonText: "Sim",
//            cancelButtonText: 'Cancelar',
//            showClass: {
//                popup: 'animated fadeInDown faster'
//            },
//            customClass: {
//                confirmButton: "btn btn-primary",
//                cancelButton: 'btn btn-danger'
//            }
//        }).then((resultado) => {
//            console.log(resultado.isConfirmed);
//            resolve(resultado.isConfirmed);
//        });
//    });
//}

//export function alerta() {
//}

//export function alertaToast(titulo, mensagem) {
//    toastr.options = {
//        "closeButton": true,
//        "debug": false,
//        "newestOnTop": false,
//        "progressBar": false,
//        "positionClass": "toastr-bottom-center",
//        "preventDuplicates": false,
//        "onclick": null,
//        "showDuration": "300",
//        "hideDuration": "500",
//        "timeOut": "3000",
//        "extendedTimeOut": "1000",
//        "showEasing": "swing",
//        "hideEasing": "linear",
//        "showMethod": "fadeIn",
//        "hideMethod": "fadeOut"
//    };

//    toastr.success(mensagem, titulo);
//}

export function abrirModal(id) {
    const element = document.getElementById(id);

    if (element) {
        const modal = new bootstrap.Modal(element);
        modal.show();
    }
}

export function definirIndicadorDeProgressoNoBotao(id, indicar) {
    var botao = document.querySelector("#" + id);
    if (indicar) {
        botao.setAttribute("data-kt-indicator", "on");
    }
    else {
        botao.removeAttribute("data-kt-indicator");
    }
}