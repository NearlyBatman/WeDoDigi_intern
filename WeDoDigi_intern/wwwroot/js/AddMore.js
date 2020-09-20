function addThis(event) {

    event.preventDefault()
    document.getElementById("ingredients").insertAdjacentHTML("beforeend",
        `<input class="form-control" type="textarea" id="Ingredients" name="Ingredients" /> 
            <span class="text-danger field-validation-valid" data-valmsg-for="Ingredients" data-valmsg-replace="true"></span> <br />`)
}
function addMore(event) {

    event.preventDefault()
    document.getElementById("steps").insertAdjacentHTML("beforeend",
        `<textarea class="form-control" data-val="true" data-val-required="The Steps field is required." id="Steps" name="Steps"></textarea>
        <span class="text-danger field-validation-valid" data-valmsg-for="Steps" data-valmsg-replace="true"></span> <br />`)
}