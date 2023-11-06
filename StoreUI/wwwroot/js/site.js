// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}
// Write your JavaScript code.


    // For PC_BUILDER////////////////
function totalPriceInfo() {
    const arrPrice = [];
    for (let i = 0; i < 7; i++) {

        const dataPriceElement = document.getElementById(`price${i + 1}`);
        if (dataPriceElement) {
            const dataPrice = dataPriceElement.innerText;
            if (dataPrice) {
                arrPrice.push(parseInt(dataPrice.replace('$', '')));
            }
        }
    }
    let totalPrice = 0;

    for (const price of arrPrice) {
        totalPrice += price;
    }

    const dataTotalPrice = document.getElementById('totalPrice');
    dataTotalPrice.textContent = '$' + totalPrice;
}




function addComponent(id, number) {

    const cellToModify = document.getElementById(`${id}${number}`);
    const newCellContent = `

              <td>
                <div class="comp-img-block">
                  <img src="https://s.ek.ua/jpg/1863791.jpg" alt="" />
                </div>
              </td>
              <td>
                <h3 class="comp-title">
                  Intel® Core™ i9-14900KF, 24 Cores & 32 Threads Unlocked Gaming
                  Processor
                </h3>
              </td>
              <td>
                <p class="comp-price" id="price${number}">$322</p>
              </td>
              <td>
                <a href="" class="btn-conp-link">link</a>
              </td>
              <td class="td-remove">
                <button type="button" class="btn-remove-comp" onclick="removeComponent('${id}',${number})" >remove</button>
              </td>
            
    `;
    cellToModify.outerHTML = newCellContent;

    totalPriceInfo();

}
function removeComponent(id, number) {
    const cellToModify = document.getElementById(`${id}`);
    const titleComponent = cellToModify.querySelector(".pc-builder-page-link").innerText;
    const newCellContent = `
    <tr class="item" id="${id}">
              <td scope="row" class="component">
                <a href="" class="pc-builder-page-link">${titleComponent}</a>
              </td>
              <td class="td-addComp" id="${id}${number}" colspan="5">
                <button
                  type="button"
                  class="btn-add-comp"
                  onclick="addComponent('${id}',${number})"
                >
                  + Add 
                </button>
              </td>
            </tr>`;

    cellToModify.outerHTML = newCellContent;
    totalPriceInfo();

}