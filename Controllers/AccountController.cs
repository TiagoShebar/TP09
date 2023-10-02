using Microsoft.AspNetCore.Mvc;

namespace TP09.Controllers;

public class HomeController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult VerificarLogin(string NombreUsuario, string Password){
        int i = 0;
        ViewBag.Igual = true;
        bool nombreUsarioIne = true;
        List<Usuario> ListaUsuarios = BD.ListarUsuarios();

        while(i < ListaUsuarios.Count && ViewBag.Igual){
           
            if(ListaUsuarios[i].NombreUsuario == NombreUsuario){
                nombreUsarioIne = true;
                if(ListaUsuarios[i].Password != Password){
                    ViewBag.Igual = false;
                }
            }
            else{
                nombreUsarioIne = false;
            }
            
            i++;
        }

        if(nombreUsarioIne && ViewBag.Igual){
            return RedirectToAction("Bienvenida, { us.NombreUsuario }");
        }
        else{
            return View("Login");
        }
    }

    public IActionResult VerificarRegistro(Usuario us){
        List<Usuario> ListaUsuarios = BD.ListarUsuarios();
        int i = 0;
        ViewBag.NUIgual = false;
        ViewBag.EIgual = false;
        while(i < ListaUsuarios.Count && !ViewBag.NUIgual && !ViewBag.PIgual){
            if(ListaUsuarios[i].NombreUsuario == us.NombreUsuario){
                ViewBag.NUIgual = true;
            }
            if(ListaUsuarios[i].Email == us.Email){
                ViewBag.EIgual = true;
            }
            i++;
        }
        if(i == ListaUsuarios.Count){
           return RedirectToAction("GuardarUsuario , { us }"); 
        }
        else{
            return View("Registro");
        }
        
    }

    public IActionResult AgregarUsuario(){
        return View();
    }

    [HttpPost] public IActionResult GuardarUsuario(Usuario us){
        BD.AgregarUsuario(us);
        return View("Login");
    }

    public IActionResult Olvide(string Email, string PreguntaPersonal)
    {
        ViewBag.PasswordOlvidado = BD.OlvideContraseña(Email, PreguntaPersonal);
        return View();
    }

    public IActionResult Bienvenida(string NombreUsuario){

        return View();
    }




}
