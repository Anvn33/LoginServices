using Login.WebApi.Models;
using Login.WebApi.Models.Common;
using Login.WebApi.Models.Request;
using Login.WebApi.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login.WebApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppSettings _appSegttings;

        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSegttings = appSettings.Value;
        }

        public UsuarioResponse Auth(AuthRequest model)
        {
            UsuarioResponse userresponse = new UsuarioResponse();

            using (var db = new PortalContext())
            {
                var usuario = db.Usuarios.Where(u => u.Email == model.Email && 
                                                    u.Password == model.Password ).FirstOrDefault();

                if (usuario == null) return null;

                userresponse.Email = usuario.Email;
                userresponse.Token = GetToken(usuario);
            }

            return userresponse;
        }

        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSegttings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()) ,
                        new Claim(ClaimTypes.Email,usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UsuarioResponse NewUsuario(UsuarioRequest userModel)
        {
            UsuarioResponse uRespuesta = new UsuarioResponse();

            try
            {
                using (var db = new PortalContext())
                {
                    Usuario dbUsuario = new Usuario();

                    dbUsuario.Usuario1 = userModel.Usuario1;
                    dbUsuario.Email = userModel.Email;
                    dbUsuario.Password = userModel.Password;
                    dbUsuario.Status = userModel.Status;

                    db.Usuarios.Add(dbUsuario);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                //uRespuesta.Mensaje = ex.Message;
            }

            return uRespuesta;
        }


        public UsuarioResponse EditUsuario(UsuarioRequest userModel)
        {
            UsuarioResponse uRespuesta = new UsuarioResponse();

            try
            {
                using (var db = new PortalContext())
                {
                    Usuario dbUsuario = db.Usuarios.Find(userModel.Id);

                    dbUsuario.Usuario1 = userModel.Usuario1;
                    dbUsuario.Email = userModel.Email;
                    dbUsuario.Password = userModel.Password;
                    dbUsuario.Status = userModel.Status;

                    db.Entry(dbUsuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                //uRespuesta.Mensaje = ex.Message;
            }

            return uRespuesta;
        }


        public UsuarioResponse DeleteUsuario(int Id)
        {
            UsuarioResponse uRespuesta = new UsuarioResponse();

            try
            {
                using (var db = new PortalContext())
                {
                    Usuario dbUsuario = db.Usuarios.Find(Id);
                    db.Remove(dbUsuario);
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                //uRespuesta.Mensaje = ex.Message;
            }

            return uRespuesta;
        }

        public Respuesta GetUsuario()
        {
            Respuesta uRespuesta = new Respuesta();

            try
            {
                using (var db = new PortalContext())
                {
                    var list = db.Usuarios.ToList();
                    uRespuesta.Data = list;

                }
            }
            catch (Exception ex)
            {
                //uRespuesta.Mensaje = ex.Message;
            }

            return uRespuesta;
        }
    }
}
