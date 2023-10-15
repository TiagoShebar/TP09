using System;
using System.Data.SqlClient;
using Dapper;

public static class BD {
    private static string _connectionString = 
    @"Server=localhost;DataBase=TP09;Trusted_Connection=True;";

   public static bool AgregarUsuario(Usuario us)
    {
        Usuario encontrado = null;
        string SQL = "INSERT INTO Usuarios(NombreUsuario, Password, Nombre, Email, Telefono, IdPregunta, Respuesta) VALUES (@pNombreUsuario, HASHBYTES('MD5',@pPassword), @pNombre, @pEmail, @pTelefono, @pIdPregunta, @pRespuesta)";
        string SQL2 = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario OR Email = @pEmail";
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            encontrado = db.QueryFirstOrDefault<Usuario>(SQL2, new{ pNombreUsuario = us.NombreUsuario, pEmail = us.Email });
            if (encontrado != null)
            {
                return false;
            }
            else
            {
                db.Execute(SQL, new { pNombreUsuario = us.NombreUsuario, pPassword = us.Password, pNombre = us.Nombre, pEmail = us.Email, pTelefono = us.Telefono, pIdPregunta = us.IdPregunta, pRespuesta = us.Respuesta });
                return true;
            }
        }
    }

    public static bool OlvidePassword(string Email, int IdPregunta, string Respuesta, string NewPassword)
    {

        string SQL = "SELECT * FROM Usuarios WHERE Email = @pEmail AND IdPregunta = @pIdPregunta AND Respuesta= @pRespuesta";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            Usuario usuario = db.QueryFirstOrDefault<Usuario>(SQL, new{ pEmail = Email, pIdPregunta = IdPregunta, pRespuesta = Respuesta });
            if(usuario != null){
                SQL = "Update Usuarios Set Password = HASHBYTES('MD5',@pNewPassword) Where IdUsuario = @pIdUsuario";
                db.Execute(SQL,new { pNewPassword = NewPassword, pIdUsuario = usuario.IdUsuario});
                return true;
            }
            else{
                return false;
            }
        }
    }

    public static Usuario EncontrarUsuario(string NombreUsuario, string Password){
        Password = Password;
        string SQL = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario AND Password = HASHBYTES('MD5',@pPassword)";
        Usuario usuario = null;
        using(SqlConnection db = new SqlConnection(_connectionString)){
            usuario = db.QueryFirstOrDefault<Usuario>(SQL, new{ pNombreUsuario = NombreUsuario,  pPassword = Password});
        }

        return usuario;
    }


    public static List<Pregunta> ListarPreguntas(){
        List<Pregunta> ListaPreguntas;
        string SQL = "SELECT * FROM Preguntas";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            ListaPreguntas = db.Query<Pregunta>(SQL).ToList();
        }
        return ListaPreguntas;
    }

   
}