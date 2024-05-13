using Login.WebApi.Models;
using Login.WebApi.Models.Request;
using Login.WebApi.Models.Response;
using Login.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Login.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model )
        {
            Respuesta respuesta = new Respuesta();
            var userresponse = _usuarioService.Auth(model);

            if(userresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o Contraseña invalido.";
                return BadRequest(respuesta);
            }

            respuesta.Exito = 1;
            respuesta.Data = userresponse;

            return Ok(respuesta);
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser([FromBody] UsuarioRequest userModel)
        {
            Respuesta uRespuesta = new Respuesta();
        
            var newuser = _usuarioService.NewUsuario(userModel);

            if (newuser == null)
            {
                uRespuesta.Exito = 0;
                return Ok(uRespuesta);
            }

            uRespuesta.Exito = 1;
            uRespuesta.Data = uRespuesta;

            return Ok(uRespuesta);

        }


        [HttpPut("EditUser")]
        public IActionResult EditUser([FromBody] UsuarioRequest userModel)
        {
            Respuesta uRespuesta = new Respuesta();

            var newuser = _usuarioService.EditUsuario(userModel);

            if (newuser == null)
            {
                uRespuesta.Exito = 0;
                return Ok(uRespuesta);
            }

            uRespuesta.Exito = 1;
            uRespuesta.Data = uRespuesta;

            return Ok(uRespuesta);

        }


        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser([FromBody] UsuarioRequest userModel)
        {
            Respuesta uRespuesta = new Respuesta();

            var deluser = _usuarioService.DeleteUsuario(userModel.Id);

            if (deluser == null)
            {
                uRespuesta.Exito = 0;
                return Ok(uRespuesta);
            }

            uRespuesta.Exito = 1;
            uRespuesta.Data = uRespuesta;

            return Ok(uRespuesta);

        }


        [HttpGet("GetUser")]
        public IActionResult GetUser()
        {
            Respuesta uRespuesta = new Respuesta();

            var getuser = _usuarioService.GetUsuario();

            if (getuser == null)
            {
                uRespuesta.Exito = 0;
                return Ok(uRespuesta);
            }

            uRespuesta.Exito = 1;
            uRespuesta.Data = uRespuesta;

            return Ok(uRespuesta);

        }
    }
}
