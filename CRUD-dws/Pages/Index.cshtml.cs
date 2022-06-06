using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD_dws.Pages
{
    public class IndexModel : PageModel
    {
        public List<ImenikPodaci> listaImenik = new List<ImenikPodaci>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                String konekcijaString = "Data Source=.\\tew_sqlexpress;Initial Catalog=crud;Integrated Security=True";

                using (SqlConnection konekcija = new SqlConnection(konekcijaString))
                {
                    konekcija.Open();
                    String sql = "SELECT * FROM imenik";
                    using (SqlCommand komanda = new SqlCommand(sql, konekcija))
                    {
                        using (SqlDataReader citac = komanda.ExecuteReader())
                        {
                            while (citac.Read())
                            {
                                ImenikPodaci imenikPodaci = new ImenikPodaci();
                                imenikPodaci.id = "" + citac.GetInt32(0);
                                imenikPodaci.ime = citac.GetString(1);
                                imenikPodaci.prezime = citac.GetString(2);
                                imenikPodaci.broj = citac.GetString(3);
                                imenikPodaci.adresa = citac.GetString(4);

                                listaImenik.Add(imenikPodaci);
                            }
                        }
                    }
                }

            } catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
    }

    public class ImenikPodaci
    {
        public String id;
        public String ime;
        public String prezime;
        public String broj;
        public String adresa;
    }
}