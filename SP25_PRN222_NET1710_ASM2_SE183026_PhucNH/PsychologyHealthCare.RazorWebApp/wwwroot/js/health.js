"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/healthHub").build();

//Disable the send button until connection is established.
const createButton = document.getElementById("createButton");
const saveButton = document.getElementById("saveButton");

if (createButton) {
    createButton.disabled = true
}
if (saveButton) {
    saveButton.disabled = true
}

connection.start().then(function () {
    if (createButton) {
        createButton.disabled = false
    }
    if (saveButton) {
        saveButton.disabled = false
    }

}).catch(function (err) {
    return console.error(err.toString());
});

if (createButton) {
    createButton.addEventListener("click", function (event) {
        var dataObj = JSON.stringify({
            id: document.getElementById("AppointmentTracking_Id").value,
            name: document.getElementById("AppointmentTracking_Name").value,
            startTime: document.getElementById("AppointmentTracking_StartTime").value,
            endTime: document.getElementById("AppointmentTracking_EndTime").value,
            createdDate: document.getElementById("AppointmentTracking_CreatedDate").value,
            updatedDate: document.getElementById("AppointmentTracking_UpdatedDate").value,
            rating: document.getElementById("AppointmentTracking_Rating").value,
            holder: document.getElementById("AppointmentTracking_Holder").value,
            address: document.getElementById("AppointmentTracking_Address").value,
            type: document.getElementById("AppointmentTracking_Type").value,
            systemStatus: document.getElementById("AppointmentTracking_SystemStatus").value,
            programTrackingId: document.getElementById("AppointmentTracking_ProgramTrackingId").value
        });

        console.log(dataObj)

        connection.invoke("CreateAppointmentTracking", dataObj).catch(function (err) {
            return console.error(err.toString())
        })
        // Check if the element exists before updating it

        event.preventDefault();
    });
}

connection.on("Create_Appointment", function (appointmentTracking) {
    console.log("append");
    console.log(appointmentTracking);

    var tr = `
        <tr id="${appointmentTracking.id}">
            <td>${appointmentTracking.name}</td>
            <td>${appointmentTracking.startTime}</td>
            <td>${appointmentTracking.endTime}</td>
            <td>${formatDate(appointmentTracking.createdDate)}</td>
            <td>${formatDate(appointmentTracking.updatedDate)}</td>
            <td>${appointmentTracking.rating}</td>
            <td>${appointmentTracking.holder}</td>
            <td>${appointmentTracking.address}</td>
            <td>${appointmentTracking.type}</td>
            <td>${appointmentTracking.systemStatus}</td>
            <td>${appointmentTracking.programTracking.name}</td>
            <td>
                <a href="AppointmentTracking/Edit?id=${appointmentTracking.id}">Edit</a> |
                <a href="AppointmentTracking/Details?id=${appointmentTracking.id}">Details</a> |
                <a href="AppointmentTracking/Delete?id=${appointmentTracking.id}">Delete</a>
            </td>
        </tr>
    `;
    $("#appointmentList").append(tr);
});

if (saveButton) {
    saveButton.addEventListener("click", function (event) {

        var dataObj = JSON.stringify({
            id: document.getElementById("AppointmentTracking_Id").value,
            name: document.getElementById("AppointmentTracking_Name").value,
            startTime: document.getElementById("AppointmentTracking_StartTime").value,
            endTime: document.getElementById("AppointmentTracking_EndTime").value,
            createdDate: document.getElementById("AppointmentTracking_CreatedDate").value,
            updatedDate: document.getElementById("AppointmentTracking_UpdatedDate").value,
            rating: document.getElementById("AppointmentTracking_Rating").value,
            holder: document.getElementById("AppointmentTracking_Holder").value,
            address: document.getElementById("AppointmentTracking_Address").value,
            type: document.getElementById("AppointmentTracking_Type").value,
            systemStatus: document.getElementById("AppointmentTracking_SystemStatus").value,
            programTrackingId: document.getElementById("AppointmentTracking_ProgramTrackingId").value
        });

        console.log(dataObj);

        connection.invoke("UpdateAppointment", dataObj).catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    });
}

connection.on("Update_Appointment", function (appointmentTracking) {
    console.log("Update Appointment ID: ", appointmentTracking.id);

    var existingRow = $("#appointmentList").find(`tr[id='${appointmentTracking.id}']`);

    if (existingRow.length > 0) {
        existingRow.find("td:eq(0)").text(appointmentTracking.name);
        existingRow.find("td:eq(1)").text(appointmentTracking.startTime);
        existingRow.find("td:eq(2)").text(appointmentTracking.endTime);
        existingRow.find("td:eq(3)").text(formatDate(appointmentTracking.createdDate));
        existingRow.find("td:eq(4)").text(formatDate(appointmentTracking.updatedDate));
        existingRow.find("td:eq(5)").text(appointmentTracking.rating);
        existingRow.find("td:eq(6)").text(appointmentTracking.holder);
        existingRow.find("td:eq(7)").text(appointmentTracking.address);
        existingRow.find("td:eq(8)").text(appointmentTracking.type);
        existingRow.find("td:eq(9)").text(appointmentTracking.systemStatus);
        existingRow.find("td:eq(10)").text(appointmentTracking.programTracking.name);
    }
});


connection.on("Delete_Appointment", function (id) {
    console.log("Deleted Appointment ID:", id);

    $("#appointmentList").find(`tr[id='${id}']`).remove()
})



function formatDate(dateString) {
    let date = new Date(dateString);
    return date
        .toLocaleString('en-GB')
        .replace(/\//g, '-') // Change "/" to "-"
        .replace(',', '');   // Remove the comma
}
