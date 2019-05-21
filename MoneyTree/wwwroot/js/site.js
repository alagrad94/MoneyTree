function AddProjectCostRow() {

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

    selectDiv.appendChild(selectClone);

    var dateDiv = document.getElementById("cost_item_date_div_0");

    var dateClone = document.getElementById("cost_item_date_0").cloneNode(true);
    dateClone.setAttribute("id", `cost_item_date_${i}`);
    dateClone.setAttribute("name", `Costs[${i}].DateUsed`);

    dateDiv.appendChild(dateClone);

    var quantityDiv = document.getElementById("cost_item_quantity_div_0");

    var quantityClone = document.getElementById("cost_item_quantity_0").cloneNode(true);
    quantityClone.setAttribute("id", `cost_item_quantity_${i}`);
    quantityClone.setAttribute("name", `Costs[${i}].Quantity`);

    quantityDiv.appendChild(quantityClone);

    var removeButtonDiv = document.getElementById("pc_form_removeButton_div_0");
    var removeButton = document.createElement("button");
    removeButton.classList.add("btn", "btn-dark");
    removeButton.setAttribute("type", "button");
    removeButton.setAttribute("id", `removeButton_${i}`);
    removeButton.addEventListener("click", RemoveProjectCostRow);

    var buttonText = document.createTextNode("Remove Cost");

    removeButton.appendChild(buttonText);
    removeButtonDiv.appendChild(removeButton);

    var pcForm = document.getElementById("pc_form");

    var customSelectScript = document.createElement("script");
    customSelectScript.setAttribute("id", `customSelect_${i}`);
    customSelectScript.type = "text/javascript";
    customSelectScript.innerHTML = `$(document).ready(function () {
    $("#cost_item_select_${i}").customselect({ search: true,hoveropen: false });});`

    pcForm.appendChild(customSelectScript);

    i++
}

function RemoveProjectCostRow(event) {

    var idNumber = event.target.id.split("_").pop();

    var projectIdToRemove = document.getElementById(`cost_item_projectId_input_${idNumber}`);
    var selectToRemove = document.getElementById(`cost_item_select_${idNumber}`);
    var parentToRemove = selectToRemove.parentElement;
    var dateToRemove = document.getElementById(`cost_item_date_${idNumber}`);
    var quantityToRemove = document.getElementById(`cost_item_quantity_${idNumber}`);
    var buttonToRemove = document.getElementById(`removeButton_${idNumber}`);
    var scriptToRemove = document.getElementById(`customSelect_${idNumber}`);

    projectIdToRemove.remove();
    selectToRemove.remove();
    dateToRemove.remove();
    quantityToRemove.remove();
    buttonToRemove.remove();
    parentToRemove.remove();
    scriptToRemove.remove();
}

function AddCustomCostRow(){

   var ids = $('select').map(function () {
        var idNumber = this.id.split("_").pop();
        return idNumber;
    }).get().sort((a, b) => b - a);

   var i = parseInt(ids[0]) +1;

   var projectIdDiv = document.getElementById("custom_projectId_div_0");

   var projectIdInputClone = document.getElementById("custom_projectId_input_0").cloneNode(true);
   projectIdInputClone.setAttribute("id", `custom_projectId_input_${i}`);
   projectIdInputClone.setAttribute("name", `CustomCosts[${i}].ProjectId`);

   projectIdDiv.appendChild(projectIdInputClone);

   var categoryDiv = document.getElementById("custom_category_div_0");

   var categoryInputClone = document.getElementById("custom_category_input_0").cloneNode(true);
   categoryInputClone.setAttribute("id", `custom_category_input_${i}`);
   categoryInputClone.setAttribute("name", `CustomCosts[${i}].Category`);

   categoryDiv.appendChild(categoryInputClone);

   var itemDiv = document.getElementById("custom_item_div_0");

   var itemInputClone = document.getElementById("custom_item_input_0").cloneNode(true);
   itemInputClone.setAttribute("id", `custom_item_input_${i}`);
   itemInputClone.setAttribute("name", `CustomCosts[${i}].ItemName`);

   itemDiv.appendChild(itemInputClone);

   var unitDiv = document.getElementById("custom_unit_div_0");

   var unitInputClone = document.getElementById("custom_unit_input_0").cloneNode(true);
   unitInputClone.setAttribute("id", `custom_unit_input_${i}`);
   unitInputClone.setAttribute("name", `CustomCosts[${i}].UnitOfMeasure`);

   unitDiv.appendChild(unitInputClone);

   var cpuDiv = document.getElementById("custom_cpu_div_0");

   var cpuInputClone = document.getElementById("custom_cpu_input_0").cloneNode(true);
   cpuInputClone.setAttribute("id", `custom_cpu_input_${i}`);
   cpuInputClone.setAttribute("name", `CustomCosts[${i}].CostPerUnit`);

   cpuDiv.appendChild(cpuInputClone);

   var quantityDiv = document.getElementById("custom_quantity_div_0");

   var quantityInputClone = document.getElementById("custom_quantity_input_0").cloneNode(true);
   quantityInputClone.setAttribute("id", `custom_quantity_input_${i}`);
   quantityInputClone.setAttribute("name", `CustomCosts[${i}].Quantity`);

   quantityDiv.appendChild(quantityInputClone);

   var dateDiv = document.getElementById("custom_date_div_0");

   var dateInputClone = document.getElementById("custom_date_input_0").cloneNode(true);
   dateInputClone.setAttribute("id", `custom_date_input_${i}`);
   dateInputClone.setAttribute("name", `CustomCosts[${i}].DateUsed`);

   dateDiv.appendChild(dateInputClone);

   var removeButtonDiv = document.getElementById("cpc_form_removeButton_div_0");
   var removeButton = document.createElement("button");
   removeButton.classList.add("btn", "btn-dark");
   removeButton.setAttribute("type", "button");
   removeButton.setAttribute("id", `cpc_removeButton_${i}`);
   removeButton.addEventListener("click", RemoveCustomCostRow);

   var buttonText = document.createTextNode("Remove Cost");

   removeButton.appendChild(buttonText);

   removeButtonDiv.appendChild(removeButton);
   
   i++

}

function RemoveCustomCostRow(event) {

    var idNumber = event.target.id.split("_").pop();

    var projectIdToRemove = document.getElementById(`custom_projectId_input_${idNumber}`);
    var categoryToRemove = document.getElementById(`custom_category_input_${idNumber}`);
    var itemToRemove = document.getElementById(`custom_item_input_${idNumber}`);
    var unitDivToRemove = document.getElementById(`custom_unit_input_${idNumber}`);
    var cpuDivToRemove = document.getElementById(`custom_cpu_input_${idNumber}`);
    var quantityToRemove = document.getElementById(`custom_quantity_input_${idNumber}`);
    var dateToRemove = document.getElementById(`custom_date_input_${idNumber}`);
    var removeButtonToRemove = document.getElementById(`cpc_removeButton_${idNumber}`);

    projectIdToRemove.remove();
    categoryToRemove.remove();
    itemToRemove.remove();
    unitDivToRemove.remove();
    cpuDivToRemove.remove();
    quantityToRemove.remove();
    dateToRemove.remove();
    removeButtonToRemove.remove();
}


