var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/books/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {"data": "ReservationId", "width": "10%"},
            {"data": "flight.departureCity", "width": "10%"},
            {"data": "flight.destinationCity", "width": "10%"},
            {"data": "flight.departureDateTime", "width": "01%"},
            {"data": "dateOfReservation", "width": "01%"},
            {"data": "flight.numberOfFreeSeats", "width": "10%"},
            {"data": "numberOfReservedSeats", "width": "10%"},
            {"data": "status", "width": "10%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                    <a id="Approve @item.ReservationId" class="btn btn-success btn-sm text-white approve" onClick=onApprove(@(item.ReservationId),@(item.NumberOfReservedSeats))>Approve</a>
                                                        &nbsp;
                    <a id="Cancel @item.ReservationId" class="btn btn-danger btn-sm text-white" onClick=onCancel(@(item.ReservationId))>Cancel</a>
                        </div>`;
                }, "width": "40%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

const onApprove = (reservationId, numberOfSeats) => {

    console.log(reservationId);
    console.log(numberOfSeats);
    fetch("/Reservation/ApproveReservation",
        {
            method: "POST",
            body: JSON.stringify({ReservationId: parseInt(reservationId), NumberOfSeats: parseInt(numberOfSeats)}),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text()).catch(e => console.log(e))
        .then(id => {
            if (id == "No free seats available") {
                alert("No free seats available");
            } else {
                console.log(id);
                console.log("Rez id:" + reservationId);
                console.log(numberOfSeats);
                connection.invoke("JoinRoom", "Agent");
                connection.invoke("GetUpdateForReservation", parseInt(id));
            }

        });
}
const onCancel = (reservationId) => {

    console.log(reservationId);
    fetch("/Reservation/CancelReservation",
        {
            method: "POST",
            body: JSON.stringify(parseInt(reservationId)),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => {
            console.log("Rez id:" + reservationId);
            connection.invoke("JoinRoom", "Agent");
            connection.invoke("GetUpdateForReservation", parseInt(id));
        }).catch(e => console.log(e));

}