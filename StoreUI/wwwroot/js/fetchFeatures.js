const typeMenu = document.querySelector("#typeId");
const list = document.querySelector("#fetchedFeatures");
const selectedList = document.querySelector("#selectedFeatures");
const addBtn = document.querySelector("#add-features");
const remBtn = document.querySelector("#remove-features");
const productForm = document.querySelector("#productForm");

document.addEventListener('DOMContentLoaded', function () {
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
    typeMenu.addEventListener("change", async function () {
        list.innerHTML = "";
        selectedList.innerHTML = "";
        let fetchString = `GetFeatures/${typeMenu.options[typeMenu.selectedIndex].value}`;
        var res = await fetch(fetchString, { credentials: "include" });
        const jsonResponse = await res.json();
        const elements = jsonResponse.map(o => ({ name: `${o.name}: ${o.value}`, id: o.id }));
        for (let id in elements) {
            const option = document.createElement('option');
            option.value = elements[id].id;
            option.text = elements[id].name;
            list.append(option);
        }
    });
    productForm.addEventListener("submit", async function () {
        let selected = selectedList.getElementsByTagName('option')

        for (let i = 0; i < selected.length; i++) {
            selected[i].selected = 'selected';
        }
    });
});

