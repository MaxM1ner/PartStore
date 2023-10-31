const typeMenu = document.querySelector("#typeId");
const list = document.querySelector("#fetchedFeatures");
const selected_List = document.querySelector("#selectedFeatures");
typeMenu.addEventListener("change", async function () {  
    list.innerHTML = "";
    selected_List.innerHTML = "";
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
