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
                Chamados.Add( new ChamadosViewModel
                {
                    Id = reader.GetInt32(0),
                    Usuario = reader.GetString(2),
                    Filial = reader.GetString(3),
                    IdChamados = reader.GetInt32(1),
                    Pdv = reader.GetString(4),
                    Defeito = reader.GetString(5),
                    Descricao = reader.GetString(6),
                    Data = reader.GetDateTime(7),

                });
            }

            await mySqlConnection.CloseAsync();
        }
    }

    public class ChamadosViewModel :SlaRetorno
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Filial { get; set; }
        public int IdChamados { get; set; }
        public string Pdv { get; set; }
        public string Defeito { get; set; }
        public string Descricao { get; set; }
        public string Sla { get; set; }
        public DateTime dataVen { get => Data.AddHours(8); }


    }
    public class SlaRetorno
    {
        public DateTime Data { get; set; }
        public DateTime Dataatual { get => DateTime.Now; }
    }
}
     





    






