using entities.models.Usuario;

using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace core.utils
{
    public static class CUtilidades
    {
        public static UsuarioSesionViewModel ObtenerUsuario(System.Security.Principal.IIdentity identity)
        {
            UsuarioSesionViewModel usuarioSesion = null;
            if (identity.IsAuthenticated)
            {
                var claimsIdentity = identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    IEnumerable<Claim> claims = claimsIdentity.Claims;
                    usuarioSesion = new UsuarioSesionViewModel()
                    {
                        usuarioId = int.Parse(claimsIdentity.FindFirst("usuarioId").Value),
                        perfilId = int.Parse(claimsIdentity.FindFirst("perfilId").Value),
                        perfilNombre = claimsIdentity.FindFirst("perfilNombre").Value,
                    };
                }
            }
            return usuarioSesion;
        }

        public static void ContrasenaCrear(string contrasena, out byte[] usuarioContrasena)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                usuarioContrasena = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
            }
        }

        public static string GetFileExtension(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";

                case "/9J/4":
                    return "jpg";

                case "AAAAF":
                    return "mp4";

                case "JVBER":
                    return "pdf";

                case "AAABA":
                    return "ico";

                case "UMFYI":
                    return "rar";

                case "E1XYD":
                    return "rtf";

                case "U1PKC":
                    return "txt";

                case "MQOWM":
                case "77U/M":
                    return "srt";

                default:
                    return string.Empty;
            }
        }
    }
}