function addIng(event) {
    event.preventDefault();
    let ing = document.createElement("p");
    var output = '<input type="text" name="ingredients" placeholder="Ingredient" required/> '
    ing.innerHTML += output;
    document.getElementById("ingredients").appendChild(ing);
}


function addStep(event) {
    event.preventDefault();
    let listElement = document.createElement("li");
    let removeStep = '<textarea name="steps" placeholder="First step" required />';
    listElement.innerHTML += removeStep;
    document.getElementById("description_steps").appendChild(listElement);
}
function removeIng(event) {
    event.preventDefault();
    var ingElement = document.getElementById("ingredients").lastElementChild;
    ingElement.parentNode.removeChild(ingElement);
}


function removeStep(event) {
    event.preventDefault();
    var stepElement = document.getElementById("description_steps").lastElementChild;
    stepElement.parentNode.removeChild(stepElement)
}