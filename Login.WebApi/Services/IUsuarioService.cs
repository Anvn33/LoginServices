using Login.WebApi.Models;
using Login.WebApi.Models.Request;
using Login.WebApi.Models.Response;

namespace Login.WebApi.Services
{
    public interface IUsuarioService
    {
        UsuarioResponse Auth(AuthRequest model);
        UsuarioResponse NewUsuario(UsuarioRequest model);
        UsuarioResponse EditUsuario(UsuarioRequest model);
        UsuarioResponse DeleteUsuario(int Id);
        Respuesta GetUsuario();
    }
}
