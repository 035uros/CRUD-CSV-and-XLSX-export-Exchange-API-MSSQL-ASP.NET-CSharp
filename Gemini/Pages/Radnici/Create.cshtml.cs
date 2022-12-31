using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Gemini.Pages.Radnici
{

    public class CreateModel : PageModel
    {
        public RadniciInfo radnikInfo = new RadniciInfo();
        public String errorMsg = "";
        public String okMsg = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
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
                    String sql = "INSERT INTO clients (name, lastname, address, bruto_plata, position) VALUES (@name, @lastname, @address, @brutosalary, @role); ";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", radnikInfo.name);
                        command.Parameters.AddWithValue("@lastname", radnikInfo.lastname);
                        command.Parameters.AddWithValue("@address", radnikInfo.address);
                        command.Parameters.AddWithValue("@brutosalary", Convert.ToDouble(radnikInfo.bruto_salary));
                        command.Parameters.AddWithValue("@role", radnikInfo.role);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }

            }
            catch (Exception e)
            {
                errorMsg = e.Message;
                return;
            }


            radnikInfo.name = "";
            radnikInfo.lastname = "";
            radnikInfo.address = "";
            radnikInfo.bruto_salary = "";
            radnikInfo.role = "";
            okMsg = "Radnik uspesno dodat!";

            Response.Redirect("/Radnici/Index");

        }
    }
}