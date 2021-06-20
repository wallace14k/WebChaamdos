
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebChamados.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json( new { Msg = " Usuario logado"});
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogarAsync(string username, string senha, bool manterLogado)
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM usuario WHERE username = '{username}' AND senha = '{senha}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();
           
                if(await reader.ReadAsync())
                {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                List<Claim> direitosAcessos = new List<Claim> {

                    new Claim(ClaimTypes.NameIdentifier,id.ToString()),
                    new Claim(ClaimTypes.Name,name)
                };
                var identity = new ClaimsIdentity(direitosAcessos, "identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity});

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = manterLogado,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    }) ;


                    return Json(new { Msg = "Usuario logado com sucesso!" });
                }           
            return Json(new { Msg = "Usuario nao encontrado!" });

            await mySqlConnection.CloseAsync();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
