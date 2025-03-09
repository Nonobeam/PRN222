"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/healthHub").build();

//Disable the send button until connection is established.
const createButton = document.getElementById("createButton");
const saveButton = document.getElementById("saveButton");
const createButton = document.getElementById("deleteButton");

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
            id: document.getElementById("SurveyQuest_Id").value,
            surveyId: document.getElementById("SurveyQuest_SurveyId").value,
            type: document.getElementById("SurveyQuest_Type").value,
            text: document.getElementById("SurveyQuest_Text").value,
            isRequired: document.getElementById("SurveyQuest_IsRequired").checked,
            order: document.getElementById("SurveyQuest_Order").value,
            minValue: document.getElementById("SurveyQuest_MinValue").value,
            maxValue: document.getElementById("SurveyQuest_MaxValue").value,
        });

        console.log(dataObj)


        connection.invoke("Create_Appointment", dataObj).catch(function (err) {
            return console.error(err.toString())
        })
        // Check if the element exists before updating it

        event.preventDefault();
    });
}

connection.on("Create_Appointment", function (appointmentTracking) {

    console.log(appointmentTracking);

    var tr = `
        <tr id="${appointmentTracking.id}">
            <td>${appointmentTracking.name}</td>
            <td>${formatDate(appointmentTracking.startTime)}</td>
            <td>${formatDate(appointmentTracking.endTime)}</td>
            <td>${formatDate(appointmentTracking.createdDate)}</td>
            <td>${appointmentTracking.updatedDate ? formatDate(appointmentTracking.updatedDate) : 'N/A'}</td>
            <td>${appointmentTracking.rating}</td>
            <td>${appointmentTracking.holder}</td
            <td>${appointmentTracking.address}</td>
            <td>${appointmentTracking.type}</td>
            <td>${appointmentTracking.systemStatus}</td>
            <td>${appointmentTracking.programTracking ? appointmentTracking.programTracking.name : 'N/A'}</td>
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
            id: document.getElementById("Id").value,
            name: document.getElementById("Name").value,
            startTime: document.getElementById("StartTime").value,
            endTime: document.getElementById("EndTime").value,
            createdDate: document.getElementById("CreatedDate").value,
            updatedDate: document.getElementById("UpdatedDate").value,
            rating: document.getElementById("Rating").value,
            holder: document.getElementById("Holder").value,
            address: document.getElementById("Address").value,
            type: document.getElementById("Type").value,
            systemStatus: document.getElementById("SystemStatus").value,
            programTrackingId: document.getElementById("ProgramTrackingId").value
        });

        console.log(dataObj);

        connection.invoke("Update_Appointment", dataObj).catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    });
}

connection.on("Update_SurveyQuest", function (surveyQuest) {
    console.log("Update survey quest ID: ", surveyQuest.id)

    var existingRow = $("#appointmentList").find(`tr[id='${surveyQuest.id}']`)

    if (existingRow.length > 0) {
        existingRow.find("td:eq(0)").text(surveyQuest.type)
        existingRow.find("td:eq(1)").text(surveyQuest.text)
        existingRow.find("td:eq(2)").text(surveyQuest.isRequired ? "Yes" : "No");
        existingRow.find("td:eq(3)").text(surveyQuest.order)
        existingRow.find("td:eq(4)").text(surveyQuest.minValue)
        existingRow.find("td:eq(5)").text(surveyQuest.maxValue)
        existingRow.find("td:eq(6)").text(formatDate(surveyQuest.createdTime))
        existingRow.find("td:eq(7)").text(formatDate(new Date()));
        existingRow.find("td:eq(8)").text(surveyQuest.survey.title)
    }

})

connection.on("Delete_SurveyQuest", function (id) {
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
