using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace WebChamados.Pages
{
    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage ="É obrigatorio informar o nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }


        [Required(ErrorMessage = "É obrigatorio informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "É obrigatorio informar o Senha")]
        [BindProperty(SupportsGet = true)]
        public string Senha { get; set; }
        


        public void OnGet()
        {
        }
        public async Task<JsonResult> OnPostAsync()
        {
            

            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"INSERT INTO usuario (nome, username, senha) VALUES ('{Nome}' , ' {Usuario}', '{Senha}')";

            await mySqlCommand.ExecuteReaderAsync();
                     
            return new JsonResult(new { Msg = "Usuario Cadastrado!" });

            
        }
    }
}
