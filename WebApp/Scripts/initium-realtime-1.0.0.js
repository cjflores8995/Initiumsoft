$(function () {
    var hub = $.connection.realtimeHub;

    hub.client.broadcastNotif = function (total) {
        GetCountAssignTickets(total);


    };

    hub.client.broadcastNotif2 = function (total) {
        GetCountAssignTickets(total);
    };

    $.connection.hub.start()
        .done(function () {
            hub.server.getNotification();
        })
        .fail(function () {
            console.error('No se pudo conectar!');
        });

});

function GetCountAssignTickets(total) {
    let coutTicketsAsignados = $('#cout-ticket-asignados');

    if (coutTicketsAsignados.length > 0) {
        if (total > 0) {
            coutTicketsAsignados.addClass('badge-danger');
            coutTicketsAsignados.text(total);
        }
        else {
            coutTicketsAsignados.removeClass('badge-danger');
            coutTicketsAsignados.text('');
        }
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
