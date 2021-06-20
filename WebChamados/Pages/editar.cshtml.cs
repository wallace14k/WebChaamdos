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
    public class editarModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar a Filial")]
        [BindProperty(SupportsGet = true)]
        public string Filial { get; set; }

        [Required(ErrorMessage = "Numero do Chamado necessario")]
        [BindProperty(SupportsGet = true)]
        public string IdChamados { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar o PDV")]
        [BindProperty(SupportsGet = true)]
        public string Pdv { get; set; }
        [Required(ErrorMessage = "É obrigatorio informar o Defeito")]
        [BindProperty(SupportsGet = true)]
        public string Defeito { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar o Descrição")]
        [BindProperty(SupportsGet = true)]
        public string Descricao { get; set; }
        public async Task OnGet()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT* FROM chamados WHERE idchamados='{IdChamados}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            if (await reader.ReadAsync())
            {
                Id = reader.GetInt32(0);
                Usuario = reader.GetString(2);
                Filial = reader.GetString(3);
                IdChamados = reader.GetString(1);
                Pdv = reader.GetString(4);
                Defeito = reader.GetString(5);
                Descricao = reader.GetString(6);

            }
            await mySqlConnection.CloseAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"UPDATE chamados  SET username = '{Usuario}' , filial = '{Filial}' , idchamados = '{IdChamados}' , pdv = '{Pdv}' , defeito = '{Defeito}' , descricao ='{Descricao}' WHERE idchamados = {IdChamados}";
            //UPDATE chamados`SET  idchamados = '{IdChamados}', username = '{}', `filial` = '{}', `pdv` = '{}', `defeito` = '{}', `descricao` = ' {}' WHERE idchamados = '{IdChamados}';
            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            return new JsonResult(new { Msg = "Chamado editado! " });

        }

        public async Task<IActionResult> OnGetApagar()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"DELETE FROM chamados WHERE idchamados = '{IdChamados}'";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            return new JsonResult(new { Msg = "Chamado Excluido!" });

            await mySqlConnection.CloseAsync();
        }
    }
}
