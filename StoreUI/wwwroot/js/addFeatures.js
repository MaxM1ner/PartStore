const addBtn = document.querySelector("#add-features");
const remBtn = document.querySelector("#remove-features");
const list = document.querySelector("#fetchedFeatures");
const selectedList = document.querySelector("#selectedFeatures");

addBtn.addEventListener("click", async function () {
    let selected = [];

    for (let i = 0; i < list.selectedOptions.length; i++) {
        selected[i] = list.selectedOptions[i];
    }

    for (var i = 0; i < selected.length; i++) {
        selectedList.append(selected[i]);
    }  
});

remBtn.addEventListener("click", async function () {
    let selected = [];

    for (let i = 0; i < selectedList.selectedOptions.length; i++) {
        selected[i] = selectedList.selectedOptions[i];
    }

    for (var i = 0; i < selected.length; i++) {
        list.append(selected[i]);
    }
});