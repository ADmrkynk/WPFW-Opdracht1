document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("contact-form");
    const nameInput = document.getElementById("name");
    const emailInput = document.getElementById("email");
    const messageInput = document.getElementById("message");
    const successMessage = document.getElementById ("form-success");

    if (!form) return

    function showError(input, message) {
        const errorSpan = document.getElementById(`${input.id}-error`);
        if (errorSpan) {
            errorSpan.textContent = message;
        }
        input.classList.add("input-error");
        input.setAttribute("aria-invalid", "true");
    }

    function clearError(input) {
        const errorSpan = document.getElementById(`${input.id}-error`);
        if (errorSpan) {
            errorSpan.textContent = "";
        }
        input.classList.remove("input-error");
        input.setAttribute("aria-invalid", "false");
    }

    form.addEventListener("submit", (event) => {
        event.preventDefault();

        let isValid = true;
        clearError(nameInput);
        clearError(emailInput);
        clearError(messageInput);
        successMessage.textContent = "";

        if (nameInput.value.trim().length < 2) {
            showError(nameInput, "Vul alstublieft een naam in van minimaal 2 tekens.");
            isValid = false;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(emailInput.value.trim())) {
            showError(emailInput, "Vul een geldig e-mailadres in (bijv. naam@voorbeeld.nl).");
            isValid = false;
        }

        if (messageInput.value.trim().length < 10) {
            showError (messageInput, "Het bericht moet minimaal 10 tekens lang zijn.");
            isValid = false;
        }

        if (isValid) {
            successMessage.textContent = "Bedankt voor uw bericht! Ik neem zo snel mogelijk contact je op."
            form.reset();
        }
    });
    nameInput.addEventListener ("input", () => clearError(nameInput));
    emailInput.addEventListener ("input", () => clearError(emailInput));
    messageInput.addEventListener ("input", () => clearError(messageInput));
});