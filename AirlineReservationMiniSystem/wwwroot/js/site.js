// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let connection = null;

setupConnection = () => {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/reservationhub")
        .build();

    connection.on("ReceiveReservationUpdate", (update) => {
        updateTable(update);
    });

    connection.on("NewReservation", function (reservation) {
        addTableRow(reservation);
        updateTable(reservation);
    });

    connection.on("finished", function () {
        connection.stop();
    });

    connection.start().then(
        () => {
            connection.invoke("JoinRoom", "Agent");
        }
    )
        .catch(err => console.error(err.toString()));


    function addTableRow(reservation) {
        var table = document.getElementById("reservations");
        var row = table.insertRow();
        var cell1 = row.insertCell();
        cell1.innerHTML = reservation.reservationId;
        var cell2 = row.insertCell();
        switch (reservation.flight.departureCity) {
            case 0:
                cell2.innerHTML = "BEOGRAD";
                break;
            case 1:
                cell2.innerHTML = "NIS";
                break;
            case 2:
                cell2.innerHTML = "KRALJEVO";
                break;
            case 3:
                cell2.innerHTML = "PRISTINA";
                break;
            default:
                cell2.innerHTML = "CITY UNKNOWN";
        }
        var cell3 = row.insertCell();
        switch (reservation.flight.destinationCity) {
            case 0:
                cell3.innerHTML = "BEOGRAD";
                break;
            case 1:
                cell3.innerHTML = "NIS";
                break;
            case 2:
                cell3.innerHTML = "KRALJEVO";
                break;
            case 3:
                cell3.innerHTML = "PRISTINA";
                break;
            default:
                cell3.innerHTML = "CITY UNKNOWN";
        }
        var cell4 = row.insertCell();
        cell4.innerHTML = new Date(reservation.flight.departureDateTime).toLocaleString('en');
        var cell5 = row.insertCell();
        cell5.innerHTML = new Date(reservation.dateOfReservation).toLocaleString('en');
        var cell6 = row.insertCell().setAttribute("class", "FreeSeats " + reservation.flight.flightId);
        var cell7 = row.insertCell();
        cell7.innerHTML = reservation.numberOfReservedSeats;
        var cell8 = row.insertCell().setAttribute("id", "Status " + reservation.reservationId);
        // switch (reservation.status) {
        //     case 0:
        //         cell8.innerHTML = "PENDING";
        //         break;
        //     case 1:
        //         cell8.innerHTML = "CONFIRMED";
        //         break;
        //     case 2:
        //         cell8.innerHTML = "DECLINED";
        //         break;
        //     default:
        //         cell8.innerHTML = "STATUS UNKNOWN";
        // }
        var cell9 = row.insertCell();
        if (reservation.status != 1) {
            cell9.innerHTML = '<a id="Approve ' + reservation.reservationId + '" class="btn btn-success btn-sm text-white approve" onClick=onApprove(' + reservation.reservationId + ',' + reservation.numberOfReservedSeats + ')' +
                ' style="display:block">Approve</a>'+
            '<a id="Cancel ' + reservation.reservationId + '" class="btn btn-danger btn-sm text-white" onClick=onCancel(' + reservation.reservationId + ')' +
                ' style="display:none">Cancel</a>';
        } else {
            cell9.innerHTML = '<a id="Cancel ' + reservation.reservationId + '" class="btn btn-danger btn-sm text-white" onClick=onCancel(' + reservation.reservationId + ')' +
                ' style="display:block">Approve</a>' +
            '<a id="Approve ' + reservation.reservationId + '" class="btn btn-success btn-sm text-white approve" onClick=onApprove(' + reservation.reservationId + ',' + reservation.numberOfReservedSeats + ')' +
                ' style="display:none">Approve</a>';
        }
    }
    function updateTable(update) {
        const statusDiv = document.getElementById("Status " + update.reservationId);
        const btnApprove = document.getElementById("Approve " + update.reservationId);
        const btnCancel = document.getElementById("Cancel " + update.reservationId);
        const freeSeatsDivs = document.getElementsByClassName("FreeSeats " + update.flight.flightId);

        console.log(update);
        console.log(update.reservationId);

        Array.prototype.forEach.call(freeSeatsDivs, element => {
            element.innerHTML = update.flight.numberOfFreeSeats;
        });
        switch (update.status) {
            case 0:
                statusDiv.innerHTML = "PENDING";
                btnApprove.style.display = 'block';
                btnCancel.style.display = 'none';
                break;
            case 1:
                statusDiv.innerHTML = "CONFIRMED";
                btnApprove.style.display = 'none';
                btnCancel.style.display = 'block';
                break;
            case 2:
                statusDiv.innerHTML = "DECLINED";
                btnApprove.style.display = 'block';
                btnCancel.style.display = 'none';
                break;
        }
        console.log(statusDiv);
    }

}

setupConnection();


// <a id="Approve @item.ReservationId" class="btn btn-success btn-sm text-white approve" onClick=onApprove(@(item.ReservationId),@(item.NumberOfReservedSeats))
// style="@(item.Status.ToString() != "CONFIRMED" ? "display:block" : "display:none")">
// Approve
// </a>
// <a id="Cancel @item.ReservationId" class="btn btn-danger btn-sm text-white" onClick=onCancel(@(item.ReservationId))
// style="@(item.Status.ToString() != "CONFIRMED" ? "display:none" : "display:block")">
// Cancel
// </a>
