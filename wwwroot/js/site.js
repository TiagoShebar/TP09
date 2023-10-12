function ValidarFormularioOlvide(event) {
    event.preventDefault();

    let passwordInput = document.getElementsByName("NewPassword")[0].value;

    if (VerificarPassword(passwordInput)) {
        document.getElementById('registroForm').submit();
    }
}


function ValidarFormularioRegistro(event) {
    event.preventDefault();

    let passwordInput = document.getElementsByName("Password")[0].value;
    let confirmPasswordInput = document.getElementsByName("Password2")[0].value;

    if (VerificarPassword(passwordInput) && VerificarIgualdad(confirmPasswordInput)) {
        document.getElementById('registroForm').submit();
    }
}

function VerificarPassword(password) {
    let largo = password.length >= 8;
    let letraMayuscula = /[A-Z]/.test(password);
    let caracterEspecial = /[!@#$%^&*(),.?":{}|<>]/.test(password);

    let mensajeElement = document.getElementById('mensajePassword');

    if (largo && letraMayuscula && caracterEspecial) {
        mensajeElement.innerHTML = "La contraseña es válida";
        mensajeElement.style.color = "green";
        return true;
    } else {
        mensajeElement.innerHTML = "La contraseña debe tener al menos 8 caracteres, una letra mayúscula y un carácter especial.";
        mensajeElement.style.color = "red";
        return false;
    }
}

function VerificarIgualdad(confirmPassword) {
    let password = document.getElementsByName("Password")[0].value;
    let mensajeElement = document.getElementById('mensajePassword2');

    if (password === confirmPassword) {
        mensajeElement.innerHTML = "Las contraseñas coinciden.";
        mensajeElement.style.color = "green";
        return true;
    } else {
        mensajeElement.innerHTML = "Las contraseñas no coinciden.";
        mensajeElement.style.color = "red";
        return false;
    }
}
