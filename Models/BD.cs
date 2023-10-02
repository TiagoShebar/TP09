using System.Data.SqlClient;
using Dapper;

static class BD {
    private static string _connectionString = 
        @"Server=localhost;DataBase=TP09;Trusted_Connection=True;";
    
    public static void AgregarUsuario(Usuario us){
        string SQL = "INSERT INTO Usuarios(NombreUsuario, Password, Nombre, Email, Telefono) VALUES (@pIdUsuario, @pNombreUsuario, @pPassword, @pNombre, @pEmail, @pTelefono)";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            db.Execute(SQL, new{pNombreUsuario = us.NombreUsuario, pPassword = us.Nombre, pEmail = us.Email, pTelefono = us.Telefono});
        }
    }

    public static List<Usuario> ListarUsuarios(){
        List<Usuario> ListaUsuarios;
        string SQL = "SELECT * FROM Usuarios";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            ListaUsuarios = db.Query<Usuario>(SQL).ToList();
        }
        return ListaUsuarios;
    }


    public static string OlvideContraseña(string Email, string RespuestaPersonal, int NumeroPregunta){
        string Password = null;

        string SQL = "SELECT * FROM Usuarios WHERE Email = @pEmail AND CONCAT(RespuestaPersonal,@pNumeroPregunta) = @pRespuestaPersonal";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            Usuario usuario = db.QueryFirstOrDefault<Usuario>(SQL, new{ pEmail = Email,  pRespuestaPersonal = PreguntaPersonal1, pPreguntaPersonal = PreguntaPersonal2, pPreguntaPersonal = PreguntaPersonal3 });

            if (usuario != null) {
            Password = usuario.Password;
            }
        }

        return Password;
    }

    public static Usuario EncontrarUsuario(string NombreUsuario){
        string SQL = "SELECT * FROM Usuarios WHERE NombreUsuario = @pNombreUsuario";
        using(SqlConnection db = new SqlConnection(_connectionString)){
            Usuario usuario = db.QueryFirstOrDefault<Usuario>(SQL, new{ pNombreUsuario = NombreUsuario });
        }

        return usuario;
    }
    
}