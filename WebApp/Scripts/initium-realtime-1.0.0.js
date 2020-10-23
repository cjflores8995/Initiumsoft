$(function () {
    var hub = $.connection.realtimeHub;

    hub.client.broadcastNotif = function (total) {
        GetCountAssignTickets(total, false);


    };

    hub.client.broadcastNotif2 = function (total) {
        GetCountAssignTickets(total, true);
    };

    $.connection.hub.start()
        .done(function () {
            console.log('Hub conectado!');

            hub.server.getNotification();
        })
        .fail(function () {
            console.error('No se pudo conectar!');
        });

    //$.connection.hub.start();

    //GetCountAssignTickets(total);
});

function GetCountAssignTickets(total, moreActions) {
    let coutTicketsAsignados = $('#cout-ticket-asignados');

    if (total > 0) {

        if (moreActions) {
            let titulo = document.title;
            let estado = true;

            setInterval(function () {

                if (!estado) {
                    document.title = titulo;
                } else {
                    document.title = "Nueva notificación recibida";
                }
                estado = !estado;

            }, 1000);
        }

        coutTicketsAsignados.addClass('badge-danger');
        coutTicketsAsignados.text(total);

        $('#new-ticket').text('Un nuevo ticket ha sido asignado para tu atención.');
        jQuery('#ticket-alert-message').toast('show');
    }
    else {
        coutTicketsAsignados.removeClass('badge-danger');
        coutTicketsAsignados.text('');
    }
}

function updateNotifications() {
    $.ajax({
        url: '/Logic/UpdateStatusNotif',
        type: 'GET',
        datatype: 'json',
        beforeSend: function () {
            let coutTicketsAsignados = $('#cout-ticket-asignados');
            coutTicketsAsignados.removeClass('badge-danger');
            coutTicketsAsignados.text('');
        },
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.error(err.responseText);
        }
    });
}
