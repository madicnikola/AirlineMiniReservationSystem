// Modify code below for AJAX call to controller for Flight Reservation Request

// var buttonsSize = document.querySelectorAll(".approve").length;
//
//
// for (var i = 0; i < buttonsSize ; i++) {
//     document.querySelectorAll(".approve")[i].addEventListener("click", e => {
//         e.preventDefault();
//         const reservationId = document.querySelectorAll(".reservationId")[i].innerHTML.toString().trim();
//         const numberOfSeats = document.querySelectorAll(".numberOfReservedSeats")[i].innerHTML.toString().trim();
//         const status = document.querySelectorAll(".status")[i].innerHTML.toString().trim();
//         console.log("Rez id:" + reservationId);
//         console.log(numberOfSeats);
//         console.log(status);
//        
//         fetch("/Reservation/ApproveReservation",
//             {
//                 method: "POST",
//                 body: JSON.stringify({ ReservationId: parseInt(reservationId), NumberOfReservedSeats: parseInt(numberOfSeats)}),
//                 headers: {
//                     'content-type': 'application/json'
//                 }
//             })
//             .then(response => response.text())
//             .then(id => {
//                 console.log(id);
//                 connection.invoke("JoinRoom", "Agent");
//                 connection.invoke("GetUpdateForReservation", parseInt(id));
//                 toastr.success("Reservation request sent");
//             });
//     });
// }

const onApprove = (reservationId,numberOfSeats) => {

    console.log(reservationId);
    console.log(numberOfSeats);
    fetch("/Reservation/ApproveReservation",
        {
            method: "POST",
            body: JSON.stringify({ ReservationId: parseInt(reservationId), NumberOfSeats: parseInt(numberOfSeats)}),
            headers: {
                'content-type': 'application/json'
            }
        })
        .then(response => response.text())
        .then(id => {
            console.log(id);
            console.log("Rez id:"+ reservationId);
            console.log(numberOfSeats);
            connection.invoke("JoinRoom", "Agent");
            connection.invoke("GetUpdateForReservation", parseInt(id));
            toastr.success("Reservation request sent");
        }).catch(e => console.log(e));
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
            console.log("Rez id:"+ reservationId);
            connection.invoke("JoinRoom", "Agent");
            connection.invoke("GetUpdateForReservation", parseInt(id));
            toastr.success("Reservation request sent");
        }).catch(e => console.log(e));
}

// btns.forEach.call(btns, function addClickListener(btn) {
//     btn.addEventListener('click', e => {
//         // code here to handle click
//         e.preventDefault();
//         const reservationId = parseInt(document.getElementById("reservationId").value);
//         const numberOfSeats = parseInt(document.getElementById("numberOfReservedSeats").value);
//         console.log(reservationId);
//         console.log(numberOfSeats);
//
//         fetch("/Reservation/ApproveReservation",
//             {
//                 method: "POST",
//                 body: JSON.stringify({ ReservationId: reservationId, NumberOfReservedSeats: numberOfSeats}),
//                 headers: {
//                     'content-type': 'application/json'
//                 }
//             })
//             .then(response => response.text())
//             .then(id => {
//                 console.log(id);
//                 connection.invoke("JoinRoom", "Agent");
//                 connection.invoke("GetUpdateForReservation", parseInt(id));
//                 toastr.success("Reservation request sent");
//             });
//     });
// });
