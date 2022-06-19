using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Turnos.Models;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace Turnos.Controllers
{
    public class LoginController : Controller
    {
        private readonly TurnosContext _context;
        public LoginController(TurnosContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }                
        
        //public IActionResult Login(string usuario, string password)        
        [HttpPost]
        [ValidateAntiForgeryToken]   
        public IActionResult Login(Login login)        
        {
            if (ModelState.IsValid){
                string passwordEncriptado = Encriptar(login.Password);
                var loginUsuario = _context.Login.Where(l => l.Usuario == login.Usuario && l.Password == passwordEncriptado).FirstOrDefault();
                if (loginUsuario != null) 
                {                           
                    HttpContext.Session.SetString("usuario", loginUsuario.Usuario);                
                    return RedirectToAction("Index","Home");                                                            
                
                }else{
                    ViewData["errorLogin"] = "Los datos ingresados son incorrectos.";
                    return View("Index");
                }
            }
            return View("Index");
        }            

        public string Encriptar (string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {                
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)                
                    stringbuilder.Append(bytes[i].ToString("x2"));
                
                return stringbuilder.ToString();
            }
        }    

        public IActionResult Logout()
        {                                   
            HttpContext.Session.Clear();
            return View("Index");
        } 
    }       
}