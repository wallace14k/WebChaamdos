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
    public class ListasChamadosModel : PageModel
    {

        public List<ChamadosViewModel> Chamados { get; set; }
        public async Task OnGet()
        {
            MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;user id=root;database=bdwallace;password=123456789");
            await mySqlConnection.OpenAsync();

            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM chamados";

            MySqlDataReader reader = mySqlCommand.ExecuteReader();

            Chamados = new List<ChamadosViewModel>();

            while (await reader.ReadAsync())
            {
                Chamados.Add(new ChamadosViewModel
                {
                Usuario = reader.GetString(1),
                Filial = reader.GetString(2),
                IdChamados = reader.GetInt32(0),
                Pdv = reader.GetString(3),
                Defeito = reader.GetString(4),
                Descricao = reader.GetString(5),
                Data = reader.GetDateTime(6),
                });
            }
        }
    }

    public class ChamadosViewModel
    {
        public string Usuario { get; set; }
        public string Filial { get; set; }
        public int IdChamados { get; set; }
        public string Pdv { get; set; }
        public string Defeito { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

    }
}
