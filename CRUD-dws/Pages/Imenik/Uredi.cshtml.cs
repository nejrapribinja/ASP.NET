using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRUD_dws.Pages.Imenik
{
    public class UrediModel : PageModel
    {
        public ImenikPodaci imenikPodaci = new ImenikPodaci();
        public String greska = "";
        public String uspjeh = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String konekcijaString = "Data Source=.\\tew_sqlexpress;Initial Catalog=crud;Integrated Security=True";

                using (SqlConnection konekcija = new SqlConnection(konekcijaString))
                {
                    konekcija.Open();
                    String sql = "SELECT * FROM imenik WHERE id=@id";
                    using (SqlCommand komanda = new SqlCommand(sql, konekcija))
                    {
                        komanda.Parameters.AddWithValue("id", id);
                        using (SqlDataReader citac = komanda.ExecuteReader())
                        {
                            while (citac.Read())
                            {
                                imenikPodaci.id = "" + citac.GetInt32(0);
                                imenikPodaci.ime = citac.GetString(1);
                                imenikPodaci.prezime = citac.GetString(2);
                                imenikPodaci.broj = citac.GetString(3);
                                imenikPodaci.adresa = citac.GetString(4);

                            }
                        }
                    }
                }
                } catch (Exception ex)
            {
                greska = ex.Message;
            }
        }

        public void OnPost()
        {
            imenikPodaci.id = Request.Form["id"];
            imenikPodaci.ime = Request.Form["ime"];
            imenikPodaci.prezime = Request.Form["prezime"];
            imenikPodaci.broj = Request.Form["broj"];
            imenikPodaci.adresa = Request.Form["adresa"];

            if (imenikPodaci.id.Length == 0 || imenikPodaci.ime.Length == 0 || imenikPodaci.prezime.Length == 0 ||
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
                    String sql = "UPDATE imenik " +
                                 "SET ime=@ime, prezime=@prezime, broj=@broj, adresa=@adresa " +
                                 "WHERE id=@id;";

                    using (SqlCommand komanda = new SqlCommand(sql, konekcija))
                    {
                        komanda.Parameters.AddWithValue("@ime", imenikPodaci.ime);
                        komanda.Parameters.AddWithValue("@prezime", imenikPodaci.prezime);
                        komanda.Parameters.AddWithValue("@broj", imenikPodaci.broj);
                        komanda.Parameters.AddWithValue("@adresa", imenikPodaci.adresa);
                        komanda.Parameters.AddWithValue("@id", imenikPodaci.id);

                        komanda.ExecuteNonQuery();
                    }
                }


            }
            catch (Exception ex)
            {
                greska = ex.Message;
                return;
            }

            Response.Redirect("/");
        }
    }
}
