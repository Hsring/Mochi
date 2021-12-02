using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ShoppingCart.Pages.User
{
    public class UserModel : PageModel
    {

        public void OnGet()
        {
            Dictionary<string, object> row = new Dictionary<string, object>();
            using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
            {

                try
                {
                    List<Dictionary<string, object>> Data = new List<Dictionary<string, object>>();

                    sc.Connection.Open();
                    sc.CommandText = "SELECT * FROM MochiProduct";

                    var dr = sc.ExecuteReader();
                    // Dictionary<string, object> row = new Dictionary<string, object>();

                    while (dr.Read())
                    {


                        row["ID"] = dr["ID"].ToString();
                        row["Code"] = dr["Code"].ToString();
                        row["Name"] = dr["Name"].ToString();
                        row["Remark"] = dr["Remark"].ToString();
                        row["Status"] = dr["Status"].ToString();
                        Data.Add(row);
                    }
                    ViewData["K"] = Data;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    sc.Connection.Close();
                }
                Search();
            }
        }
        public IActionResult OnPostAJ()
        {
            return new JsonResult(Request.Form["Key"]);
        }
        public void Search()
        {
            System.Data.DataTable moc = new System.Data.DataTable();
            using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
            {
                Microsoft.Data.SqlClient.SqlDataAdapter sda = new Microsoft.Data.SqlClient.SqlDataAdapter(sc);
                sc.CommandText = "SELECT * FROM MochiProduct";
                sda.Fill(moc);
            }
            ViewData["Mochi"] = moc;
            //return rTB;
        }
        public void OnPostQuery()
        {
            if (Request.Form.ContainsKey("ID"))
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
                {

                    try
                    {
                        //List<Dictionary<string, object>> Data = new List<Dictionary<string, object>>();

                        sc.Connection.Open();
                        sc.CommandText = "SELECT * FROM MochiProduct WHERE ID=@ID";
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("ID", Request.Form["ID"].ToString()));
                        var dr = sc.ExecuteReader();
                        //Dictionary<string, object> row = new Dictionary<string, object>();

                        if (dr.Read())
                        {
                            row["ID"] = dr["ID"].ToString();
                            row["Code"] = dr["Code"].ToString();
                            row["Name"] = dr["Name"].ToString();
                            row["Remark"] = dr["Remark"].ToString();
                            row["Status"] = dr["Status"].ToString();
                            //Data.Add(rows);
                        }
                        ViewData["row"] = row;
                    }
                    finally
                    {
                        sc.Connection.Close();
                    }
                }
            }
        }

    }
}
