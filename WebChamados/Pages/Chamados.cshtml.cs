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
    
    public class ChamadosModel : PageModel
    {
        [Required(ErrorMessage = "É obrigatorio informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }


        [Required(ErrorMessage = "É obrigatorio informar a Filial")]
        [BindProperty(SupportsGet = true)]
        public string Filial { get; set; }

        [Required(ErrorMessage ="Numero do Chamado necessario")]
        [BindProperty(SupportsGet =true)]
        public int IdChamados { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar o PDV")]
        [BindProperty(SupportsGet = true)]
        public string Pdv { get; set; }
        [Required(ErrorMessage = "É obrigatorio informar o Defeito")]
        [BindProperty(SupportsGet = true)]
        public string Defeito { get; set; }

        [Required(ErrorMessage = "É obrigatorio informar o Descrição")]
        [BindProperty(SupportsGet = true)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Data da abertura obrigatoria")]
        [BindProperty(SupportsGet = true)]
        public DateTime Data { get; set; }

        public void OnGet()
        {
        }

        public void DataAbertura(DateTime data)
        {           
            data = DateTime.Now;
            Data = data;
        }


        public async Task<JsonResult> OnPostAsync()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"INSERT INTO chamados (username, filial, idchamados, pdv, defeito, descricao, data) VALUES ('{Usuario}', '{Filial}', '{IdChamados}', '{Pdv}','{Defeito}','{Descricao}', '{Data}')";

            await mySqlCommand.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Chamado registrado!" });
        }
    }
}
