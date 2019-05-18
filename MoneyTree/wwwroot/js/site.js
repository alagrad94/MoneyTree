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
    removeButton.addEventListener("click", RemoveRow);

    var buttonText = document.createTextNode("Remove Cost");

    removeButton.appendChild(buttonText);

    removeButtonDiv.appendChild(removeButton);

    i++

    var costFormRow = document.getElementById("pc_form_row");
}

function RemoveRow(event) {

    var idNumber = event.target.id.split("_").pop();

    var projectIdToRemove = document.getElementById(`cost_item_projectId_input_${idNumber}`);
    var selectToRemove = document.getElementById(`cost_item_select_${idNumber}`);
    var dateToRemove = document.getElementById(`cost_item_date_${idNumber}`);
    var quantityToRemove = document.getElementById(`cost_item_quantity_${idNumber}`);
    var buttonToRemove = document.getElementById(`removeButton_${idNumber}`);

    projectIdToRemove.remove();
    selectToRemove.remove();
    dateToRemove.remove();
    quantityToRemove.remove();
    buttonToRemove.remove();
}