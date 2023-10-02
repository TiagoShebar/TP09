function VerificarPassword(password){
    let largo = false;
    let caracterEspecial = false;
    let letraMayuscula = false;
    for(let i = 0; i<password.length(); i++){
        if(!letraMayuscula){
            if(isLowerCase(password[i])){
                letraMayuscula = true;
            }
        }
        else if(!caracterEspecial){
            if(contieneCaracterEspecial(password[i])){
                caracterEspecial = true;
            }
        }
    }
    if(i > LargoMinimo){
        largo = true;
    }

    return largo == caracterEspecial == letraMayuscula;
   
}


function contieneCaracterEspecial(char) {
    var expresionRegular = /[!@#$%^&*(),.?":{}|<>]/;
    return expresionRegular.test(char);
}


function VerificarRegistro(){
    
}