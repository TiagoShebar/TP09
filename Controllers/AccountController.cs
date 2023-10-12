using Microsoft.AspNetCore.Mvc;

namespace TP09.Controllers;

public class AccountController : Controller
{
    public IActionResult Login()
    {  
        ViewBag.ErrorLogin = null;
        return View();
    }

    public IActionResult ValidarLogin(string NombreUsuario, string Password)
    {
        Usuario usuario = BD.EncontrarUsuario(NombreUsuario, Password);
        if (usuario == null)
        {
            ViewBag.ErrorLogin = "Nombre de usuario o contraseña incorrectas";
            return View("Login");
        }
        else
        {
            ViewBag.Usuario = usuario;
            return View("Bienvenida");
        }
    }



    public IActionResult Registro(){
        ViewBag.ListaPreguntas = BD.ListarPreguntas();
        ViewBag.NombreUsuarioExisteOMail = null;
        return View();
    }

    [HttpPost]
        public IActionResult GuardarUsuario(Usuario us)
        {
            if (!BD.AgregarUsuario(us)) 
            {
                ViewBag.NombreUsuarioExisteOMail = "Ya existe el nombre de usuario o el mail está vinculado a una cuenta";
                ViewBag.ListaPreguntas = BD.ListarPreguntas();
                return View("Registro");
            }
            else
            {
                return View("Login");
            }
        }


    public IActionResult Olvide(){
        ViewBag.ListaPreguntas = BD.ListarPreguntas();
        return View();
    }

    public IActionResult ValidarOlvido(string Email, int IdPregunta, string Respuesta, string NewPassword)
    {
        if(BD.OlvidePassword(Email, IdPregunta, Respuesta, NewPassword)){
            return View("Login");
        }
        else{
            ViewBag.ErrorOlvidado = "Error. No hay ninguna cuenta registrada con ese mail, o la respuesta es incorrecta";
            ViewBag.ListaPreguntas = BD.ListarPreguntas();
            return View("Olvide");
        }
    }

}
