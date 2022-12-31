using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using Newtonsoft.Json;

namespace Gemini.Pages.Radnici
{
    public class indexModel : PageModel
    {
        public List<RadniciInfo> listaRadnika = new List<RadniciInfo>();

        public void OnGet()
        {
            String csv = Request.Query["csv"];
            String xlsx = Request.Query["xlsx"];

            try
            {
                String connectionString = "Data Source=DESKTOP-AV5CJI4;Initial Catalog=gemini;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RadniciInfo radnikInfo = new RadniciInfo();
                                radnikInfo.id = "" + reader.GetInt32(0);
                                radnikInfo.name = reader.GetString(1);
                                radnikInfo.lastname = reader.GetString(2);
                                radnikInfo.address = reader.GetString(3);
                                radnikInfo.bruto_salary = "" + reader.GetDouble(4);
                                radnikInfo.createdat = reader.GetDateTime(5).ToString();
                                radnikInfo.role = reader.GetString(6);


                                listaRadnika.Add(radnikInfo);
                            }
                            if(csv == "da")
                            {
                                exportCsv();
                            }else if(xlsx == "da")
                            {
                                exportExcel();
                            }
                            
                        }
                    }

                    connection.Close();
                }


            }
            catch (Exception ex)
            {
            }
        }
        public void exportCsv()
        {
            string headerLine = string.Join(",", listaRadnika[0].GetType().GetProperties().Select(p => p.Name));
            var dataLines = from emp in listaRadnika
                            let dataline = string.Join(",", emp.GetType().GetProperties().Select(p => p.GetValue(emp)))
                            select dataline;
            var csvdata = new List<string>();
            csvdata.Add(headerLine);
            csvdata.AddRange(dataLines);

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string csvpath = Path.Combine(basePath, "radnici.csv");         //fajl je kreiran Debug/netcoreapp2.1 folderu
            System.IO.File.WriteAllLines(csvpath, csvdata);
        }

        public void exportExcel()
        {
            var workbook = new XLWorkbook();
            workbook.AddWorksheet("Radnici");
            var ws = workbook.Worksheet("Radnici");

            ws.Cell("A" + 1.ToString()).Value = "Ime";
            ws.Cell("B" + 1.ToString()).Value = "Prezime";
            ws.Cell("C" + 1.ToString()).Value = "Adresa";
            ws.Cell("D" + 1.ToString()).Value = "Pozicija";
            ws.Cell("E" + 1.ToString()).Value = "Bruto plata";

            int row = 2;
            foreach (var item in listaRadnika)
            {
                ws.Cell("A" + row.ToString()).Value = item.name;
                ws.Cell("B" + row.ToString()).Value = item.lastname;
                ws.Cell("C" + row.ToString()).Value = item.address;
                ws.Cell("D" + row.ToString()).Value = item.role;
                ws.Cell("E" + row.ToString()).Value = item.bruto_salary;
                row++;
            }
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string excelpath = Path.Combine(basePath, "radnici.xlsx");         //fajl je kreiran Debug/netcoreapp2.1 folderu
            workbook.SaveAs(excelpath);
        }

        public double convertRSD(string rsd, string valuta)
        {
            String URLString = $"https://v6.exchangerate-api.com/v6/4255d481937f8b13715078c9/pair/RSD/" +valuta + "/" + rsd;

            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(URLString);
                API_Obj CurrencyFrom = JsonConvert.DeserializeObject<API_Obj>(json);
                double rate = CurrencyFrom.conversion_result;
                return rate;
            }
            
        }

    }



    public class RadniciInfo
    {
        public String id { get; set; }
        public String name { get; set; }
        public String lastname { get; set; }
        public String address { get; set; }
        public String bruto_salary { get; set; }
        public String createdat { get; set; }
        public String role { get; set; }
    }

    public class API_Obj
    {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public string time_zone { get; set; }
        public string time_last_update { get; set; }
        public string time_next_update { get; set; }
        public double conversion_rate { get; set; }
        public double conversion_result { get; set; }
    }

}