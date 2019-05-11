// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddRow() {

    var i = 1;

    var projectIdDiv = document.getElementById("cost_item_projectId_div_0");

    var projectIdInputClone = document.getElementById("cost_item_projectId_input_0").cloneNode(true);
    projectIdInputClone.setAttribute("id", `cost_item_projectId_input_${i}`);
    projectIdInputClone.setAttribute("name", `Costs[${i}].ProjectId`);

    projectIdDiv.appendChild(projectIdInputClone);

    var selectDiv = document.getElementById("cost_item_select_div_0");

    var selectClone = document.getElementById("cost_item_select_0").cloneNode(true);
    selectClone.setAttribute("id", `cost_item_select_${i}`);
    selectClone.setAttribute("name", `Costs[${i}].CostItemId`);

    var selectValClone = document.getElementById("cost_item_select_val_0").cloneNode(true);
    selectValClone.setAttribute("id", `cost_item_select_val_${i}`);
    selectValClone.setAttribute("data-valmsg-for", `Costs[${i}].CostItemId`);

    selectDiv.appendChild(selectClone);
    selectDiv.appendChild(selectValClone);

    var dateDiv = document.getElementById("cost_item_date_div_0");

    var dateClone = document.getElementById("cost_item_date_0").cloneNode(true);
    dateClone.setAttribute("id", `cost_item_date_${i}`);
    dateClone.setAttribute("name", `Costs[${i}].DateUsed`);

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

    //var container = document.getElementById("pc_form");

    //var rowClone = document.getElementById("pc_form_row_0").cloneNode();
    //rowClone.setAttribute("id", `pc_form_row_${i}`);

    //var projectIdDivClone = document.getElementById("cost_item_projectId_div_0").cloneNode();
    //projectIdDivClone.setAttribute("id", `cost_item_projectId_div_${i}`);
    //projectIdDivClone.setAttribute("name", `Costs[${i}].ProjectId`);

    //var selectDivClone = document.getElementById("cost_item_select_div_0").cloneNode();
    //selectDivClone.setAttribute("id", `cost_item_select_div_${i}`);
    //selectDivClone.setAttribute("name", `Costs[${i}].CostItemId`);

    //var selectLabelClone = document.getElementById("cost_item_select_label_0").cloneNode(true);
    //selectLabelClone.setAttribute("id", `cost_item_select_label_${i}`);
    //selectLabelClone.setAttribute("for", `Costs_${i}__CostItemId`);
    //selectLabelClone.textContent = "Cost Item";

    //var dateDivClone = document.getElementById("cost_item_date_div_0").cloneNode();
    //dateDivClone.setAttribute("id", `cost_item_date_div_${i}`);
    //dateDivClone.setAttribute("name", `Costs[${i}].DateUsed`);

    //var dateLabelClone = document.getElementById("cost_item_date_label_0").cloneNode();
    //dateLabelClone.setAttribute("id", `cost_item_date_label_${i}`);
    //dateLabelClone.setAttribute("for", `Costs_${i}__DateUsed`);
    //dateLabelClone.textContent = "Date Used";

    //var quantityDivClone = document.getElementById("cost_item_quantity_div_0").cloneNode();
    //quantityDivClone.setAttribute("id", `cost_item_quantity_div_${i}`);
    //quantityDivClone.setAttribute("name", `Costs[${i}].Quantity`);

    //var quantityLabelClone = document.getElementById("cost_item_quantity_label_0").cloneNode();
    //quantityLabelClone.setAttribute("id", `cost_item_quantity_label_${i}`);
    //quantityLabelClone.setAttribute("for", `Costs_${i}__Quantity`);
    //quantityLabelClone.textContent = "Quantity";

    

    //projectIdDivClone.appendChild(projectIdInputClone);
    //rowClone.appendChild(projectIdDivClone);

    //selectDivClone.appendChild(selectLabelClone);
    //selectDivClone.appendChild(selectClone);
    //selectDivClone.appendChild(selectValClone);
    //rowClone.appendChild(selectDivClone);

    //dateDivClone.appendChild(dateLabelClone);
    //dateDivClone.appendChild(dateClone);
    //dateDivClone.appendChild(dateValClone);
    //rowClone.appendChild(dateDivClone);

    //quantityDivClone.appendChild(quantityLabelClone);
    //quantityDivClone.appendChild(quantityClone);
    //quantityDivClone.appendChild(quantityValClone);
    //rowClone.appendChild(quantityDivClone);

    //container.appendChild(rowClone);

    i++
}