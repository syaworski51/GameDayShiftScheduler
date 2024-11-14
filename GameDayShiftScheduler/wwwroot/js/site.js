function AddNewShift() {
    $("#new-shifts").append('<partial name="../Partials/NewShiftCard.cshtml" />');
}

function TogglePasswordFields() {
    const passwordFields = $("#password-fields");
    const passwordField = $("#password");
    const confirmPasswordField = $("#confirm-password");

    if ($('input[name="OneTimePasswordOption"]:checked').val() === "Create Password Manually") {
        passwordFields.show();
    }
    else {
        passwordFields.hide();
        passwordField.val("");
        confirmPasswordField.val("");
    }
}

$('input[name="OneTimePasswordOption"]').change(TogglePasswordFields);