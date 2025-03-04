using BL;
using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario)
        {
            _usuario = usuario; 
        }


        public IActionResult GetAll()
        {
            ML.Usuario usuario= new ML.Usuario();
            ML.Result result = _usuario.GetAll();
            if (result.Objects != null)
            {
                usuario.Usuarios = result.Objects;

            }
            else
            {
                ViewBag.Error = result.ErrorMessage;
            }

            return View(usuario);
        }

        public IActionResult Formulario(int? IdUsuario)
        {
            ML.Usuario Usuario = new ML.Usuario();
            if (IdUsuario != null)
            {
                ML.Result result = _usuario.GetById(IdUsuario.Value);
                if (result.Correct)
                {
                    Usuario = (ML.Usuario)result.Object;
                }
            }
            else
            {
            }
            return View(Usuario);
        }

        [HttpPost]
        public IActionResult Formulario(ML.Usuario usuario, IFormFile ImgUsuario)
        {
            if (ImgUsuario != null && ImgUsuario.Length > 0) // si el input tiene nueva imagen
            {
                using (var target = new MemoryStream())
                {
                    //Convierte en arreglo de bytes 
                    ImgUsuario.CopyTo(target);
                    byte[] data = target.ToArray();
                    usuario.Foto = data;
                }
            }
            if (usuario.IdUsuario == 0)
            {
                ML.Result result = _usuario.Add(usuario);
            }
            else
            {
                ML.Result result = _usuario.Update(usuario);
            }

            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            if (usuario.IdUsuario > 0)
            {

            }

            ML.Result result = _usuario.Delete(usuario);
            return RedirectToAction("GetAll");
        }
    }
}
