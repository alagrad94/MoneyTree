// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddRow() {

    var ids = $('select').map(function () {
        var idNumber = this.id.split("_").pop();
        return idNumber;
    }).get().sort((a, b) => b - a);

    var i = parseInt(ids[0]) +1;

    var projectIdDiv = document.getElementById("cost_item_projectId_div_0");

    var projectIdInputClone = document.getElementById("cost_item_projectId_input_0").cloneNode(true);
    projectIdInputClone.setAttribute("id", `cost_item_projectId_input_${i}`);
    projectIdInputClone.setAttribute("name", `Costs[${i}].ProjectId`);

    projectIdDiv.appendChild(projectIdInputClone);

    var selectDiv = document.getElementById("cost_item_select_div_0");

    var selectClone = document.getElementById("cost_item_select_0").cloneNode(true);
    selectClone.setAttribute("id", `cost_item_select_${i}`);
    selectClone.setAttribute("name", `Costs[${i}].CostItemId`);
    selectClone.setAttribute("aria-describedby", `cost_item_select_${i}-error`);

    var selectValClone = document.getElementById("cost_item_select_val_0").cloneNode(true);
    selectValClone.setAttribute("id", `cost_item_select_val_${i}`);
    selectValClone.setAttribute("data-valmsg-for", `Costs[${i}].CostItemId`);

    selectDiv.appendChild(selectClone);
    selectDiv.appendChild(selectValClone);

    var dateDiv = document.getElementById("cost_item_date_div_0");

    var dateClone = document.getElementById("cost_item_date_0").cloneNode(true);
    dateClone.setAttribute("id", `cost_item_date_${i}`);
    dateClone.setAttribute("name", `Costs[${i}].DateUsed`);
    dateClone.setAttribute("aria-describedby", `cost_item_date_${i}-error`);

    var dateValClone = document.getElementById("cost_item_date_val_0").cloneNode(true);
    dateValClone.setAttribute("id", `cost_item_date_val_${i}`);
    dateValClone.setAttribute("data-valmsg-for", `Costs[${i}].DateUsed`);

    dateDiv.appendChild(dateClone);
    dateDiv.appendChild(dateValClone);

    var quantityDiv = document.getElementById("cost_item_quantity_div_0");

    var quantityClone = document.getElementById("cost_item_quantity_0").cloneNode(true);
    quantityClone.setAttribute("id", `cost_item_quantity_${i}`);
    quantityClone.setAttribute("name", `Costs[${i}].Quantity`);

    var quantityValClone = document.getElementById("cost_item_quantity_val_0").cloneNode(true);
    quantityValClone.setAttribute("id", `cost_item_quantity_val_${i}`);
    quantityValClone.setAttribute("data-valmsg-for", `Costs[${i}].Quantity`);

    quantityDiv.appendChild(quantityClone);
    quantityDiv.appendChild(quantityValClone);

    i++
}