$(function () {
    var cus = $.connection.realtimeHub;

    cus.client.displayNotifications = function () {
        GetCountAssignTickets(true);
    };

    $.connection.hub.start();
    GetCountAssignTickets(false);
});

function GetCountAssignTickets(init) {
    let coutTicketsAsignados = $('#cout-ticket-asignados');

    $.ajax({
        url: $("#getCountTicketsAsignados").val(),
        type: 'GET',
        datatype: 'json',
        success: function (data) {
            if (data.Status) {
                if (data.Details > 0)
                {
                    //if (init == true) {
                    //    let titulo = document.title;
                    //    let estado = true;

                    //    setInterval(function () {

                    //        if (!estado) {
                    //            document.title = titulo;
                    //        } else {
                    //            document.title = "Nueva notificación recibida";
                    //        }
                    //        estado = !estado;

                    //    }, 1000);
                    //}

                    coutTicketsAsignados.addClass('badge-danger');
                    coutTicketsAsignados.text(data.Details);
                }
                else {
                    coutTicketsAsignados.removeClass('badge-danger');
                    coutTicketsAsignados.text('');
                    coutTicketsAsignados.text('');
                }
            }
            else {
                alert(data.Message);
            }
        }
    });
}