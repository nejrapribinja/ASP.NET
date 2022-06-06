using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD_dws.Pages
{
    public class DodajModel : PageModel
    {
        public ImenikPodaci imenikPodaci = new ImenikPodaci();
        public String greska = "";
        public String uspjeh = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            imenikPodaci.ime = Request.Form["ime"];
            imenikPodaci.prezime = Request.Form["prezime"];
            imenikPodaci.broj = Request.Form["broj"];
            imenikPodaci.adresa = Request.Form["adresa"];

            if (imenikPodaci.ime.Length == 0 || imenikPodaci.prezime.Length == 0  ||
               imenikPodaci.broj.Length == 0 || imenikPodaci.adresa.Length == 0)
            {
                greska = "Sva polja moraju biti popunjena";
                return;
            }

            try
            {
                String konekcijaString = "Data Source=.\\tew_sqlexpress;Initial Catalog=crud;Integrated Security=True";
                using (SqlConnection konekcija = new SqlConnection(konekcijaString))
                {
                    konekcija.Open();
                    String sql = "INSERT INTO imenik " +
                                 "(ime, prezime, broj, adresa) VALUES " +
                                 "(@ime, @prezime, @broj, @adresa);";

                    using (SqlCommand komanda = new SqlCommand(sql, konekcija))
                    {
                        komanda.Parameters.AddWithValue("@ime", imenikPodaci.ime);
                        komanda.Parameters.AddWithValue("@prezime", imenikPodaci.prezime);
                        komanda.Parameters.AddWithValue("@broj", imenikPodaci.broj);
                        komanda.Parameters.AddWithValue("@adresa", imenikPodaci.adresa);

                        komanda.ExecuteNonQuery();
                    }
                }

            } catch(Exception ex)
            {
                greska = ex.Message;
                return;
            }


            imenikPodaci.ime = ""; 
            imenikPodaci.prezime = "";
            imenikPodaci.broj = "";
            imenikPodaci.adresa = "";
            uspjeh = "Uspješno ste dodali kontakt!";

            Response.Redirect("/");

        }
    }
}
