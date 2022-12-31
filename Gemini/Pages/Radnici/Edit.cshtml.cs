using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace Gemini.Pages.Radnici
{
    public class EditModel : PageModel
    {
        public RadniciInfo radnikInfo = new RadniciInfo();
        public String errorMsg = "";
        public String okMsg = "";
        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {

                String connectionString = "Data Source=DESKTOP-AV5CJI4;Initial Catalog=gemini;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                radnikInfo.id = "" + reader.GetInt32(0);
                                radnikInfo.name = reader.GetString(1);
                                radnikInfo.lastname = reader.GetString(2);
                                radnikInfo.address = reader.GetString(3);
                                radnikInfo.bruto_salary = "" + reader.GetDouble(4);
                                radnikInfo.role = "" + reader.GetString(6);

                            }
                        }
                    }
                }


            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
        }

        public void OnPost()
        {
            radnikInfo.id = Request.Form["id"];
            radnikInfo.name = Request.Form["name"];
            radnikInfo.lastname = Request.Form["lastname"];
            radnikInfo.address = Request.Form["address"];
            radnikInfo.bruto_salary = Request.Form["brutosalary"];
            radnikInfo.role = Request.Form["role"];

            if (radnikInfo.name.Length == 0 || radnikInfo.lastname.Length == 0 || radnikInfo.address.Length == 0 || radnikInfo.bruto_salary.Length == 0 || radnikInfo.role.Length == 0)
            {
                errorMsg = "Sva polja su neophodna";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-AV5CJI4;Initial Catalog=gemini;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clients SET name=@name, lastname=@lastname, address=@address, bruto_plata=@bruto_plata, position=@role WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", radnikInfo.name);
                        command.Parameters.AddWithValue("@lastname", radnikInfo.lastname);
                        command.Parameters.AddWithValue("@address", radnikInfo.address);
                        command.Parameters.AddWithValue("@bruto_plata", Convert.ToDouble(radnikInfo.bruto_salary));
                        command.Parameters.AddWithValue("@id", radnikInfo.id);
                        command.Parameters.AddWithValue("@role", radnikInfo.role);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return;
            }

            Response.Redirect("/Radnici/Index");
        }
    }
}