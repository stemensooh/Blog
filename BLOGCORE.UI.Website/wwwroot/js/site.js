function IniciarLoading() {
    let timerInterval
    Swal.fire({
        title: 'Auto close alert!',
        html: 'I will close in <b></b> milliseconds.',
        timer: 30000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                const content = Swal.getContent()
                if (content) {
                    const b = content.querySelector('b')
                    if (b) {
                        b.textContent = Swal.getTimerLeft()
                    }
                }
            }, 100)
        },
        willClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            console.log('I was closed by the timer')
        }
    })
}

function MensajeGrowl(Mensaje) {
    DetenerLoading();

    $.toast({
        heading: '¡Alerta!',
        text: Mensaje,
        position: 'top-right',
        stack: true,
        showHideTransition: 'fade',
        icon: 'error'
    });
}

function MensajeSesionFinalizadaConfirmacionRedireccion(url) {
    Swal.fire({
        title: 'Alerta',
        text: 'La sesión ha caducado',
        type: 'error',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url + "/?returnUrl=" + window.location.pathname;
    });
}

function DetenerLoading() {
    Swal.close();
}

function MsgAjaxError(result) {
    
    var msg = "Existió un error, favor intentelo de nuevo.";
    var UrlBaseSite = $("#UrlBaseSite").val();

    console.log(UrlBaseSite);

    try {
        if (result.status === 401) {
            MensajeSesionFinalizadaConfirmacionRedireccion(UrlBaseSite);
        } else if (result.status === 403) {
            MensajeGrowl('El usuario no tiene permiso para acceder a este contenido');
        } else {
            MensajeGrowl(msg);
        }
    } catch (e) {
        MensajeGrowl(e);
    }
}

function MensajeErrorSwal(mensaje, modulo) {
    Swal.fire({
        type: "error",
        title: modulo,
        html: mensaje
    });
}


function EliminarRegistro(urlAction, urlRedirect, ID) {

    Swal.fire({
        title: '¿Esta seguro que desea eliminar?',
        //text: "Esta seguro que desea eliminar",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Si',
        cancelButtonColor: '#d33',
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: urlAction,
                type: 'POST',
                cache: false,
                data: { ID: ID},
                success: function (result) {
                    if (result.estado) {
                        MensajeExitosoSwalConfirmacionRedireccion(result.mensaje, "", urlRedirect);
                    } else {
                        MensajeErrorSwal(result.mensaje, "");
                    }
                },
                error: function (jqXHR, textStatus, error) {
                    MsgAjaxError(jqXHR);
                }
            })
        }
    })
}

function MensajeExitosoSwalConfirmacionRedireccion(Mensaje, modulo, url) {
    Swal.fire({
        title: modulo,
        html: Mensaje,
        icon: 'success',
        confirmButtonText: 'OK',
        allowOutsideClick: false,
    }).then(function () {
        window.location.href = url;
    });

}