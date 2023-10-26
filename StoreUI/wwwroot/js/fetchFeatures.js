const typeMenu = document.querySelector("#typeId");
typeMenu.addEventListener("change", async function () {
    const list = document.querySelector("#fetchedFeatures");
    list.innerHTML = "";
    let fetchString = `GetFeatures/${typeMenu.options[typeMenu.selectedIndex].value}`;
    var res = await fetch(fetchString);
    const jsonResponse = await res.json();
    const elements = jsonResponse.map(o => ({ name: `${o.name}: ${o.value}`, id: o.id }));

    for (let id in elements) {
        const option = document.createElement('option');
        option.value = elements[id].id;
        option.text = elements[id].name;
        list.append(option); 
    }
});