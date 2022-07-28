const montoInput = document.querySelector("#monto");
const montoLabel = document.querySelector("#montoLabel");
const rolSelect = document.querySelector("#rol-select");

document.addEventListener('DOMContentLoaded', () => {
    // To hide after loading the view
    montoLabel.classList.add("d-none");
    montoInput.setAttribute("type", "hidden");

    rolSelect.onchange = (e) => {

        if (e.target.value === "1") {
            montoLabel.classList.add("d-none");
            montoInput.setAttribute("type", "hidden");
        } else {
            montoLabel.classList.remove("d-none");
            montoInput.setAttribute("type", "text");
        }
    }
});