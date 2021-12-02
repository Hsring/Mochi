using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ShoppingCart.Pages.Product
{
    public class MochiProductModel : PageModel
    {
        public void OnGet()
        {
            Search();
        }

        private void AddLogError(Exception ex)
        {
            throw new NotImplementedException();
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
                sc.CommandText = "SELECT*,CONVERT(varchar(10),Date,120)Dates FROM MochiProduct ORDER BY Seq";
                sda.Fill(moc);
            }
            ViewData["Mochi"] = moc;
            //return rTB;
        }
     
        public void OnPostQuery() {
            //Search();
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
                            row["Date"] = dr["Date"].ToString();
                            row["Code"] = dr["Code"].ToString();
                            row["Qty"] = dr["Qty"].ToString();
                            row["Cost"] = dr["Cost"].ToString();
                            row["Seq"] = dr["Seq"].ToString();
                            row["Name"] = dr["Name"].ToString();
                            row["Remark"] = dr["Remark"].ToString();
                            row["Status"] = dr["Status"].ToString();
                            //Data.Add(rows);
                        }
                        ViewData["row"] = row;
                    }
                    catch (Exception ex) {
                        ViewData["LOG"] = ex.ToString();

                    }
                    finally
                    {

                        sc.Connection.Close();
                    }
                }
            }
        }

        public void OnPostInsert()
        {
            if (Request.Form.ContainsKey("Qty") && Request.Form.ContainsKey("Name") && Request.Form.ContainsKey("Cost"))
            {
                using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
                {
                    try
                    {
                        sc.Connection.Open();
                        sc.CommandText = "INSERT INTO MochiProduct(ID,Date,Code,Name,Qty,Cost,Seq,Status,Remark)VALUES(@ID,@Date,@Code,@Name,@Qty,@Cost,@Seq,@Status,@Remark)";
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("ID", Guid.NewGuid().ToString().ToLower()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Date", Request.Form["Date"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Code", Request.Form["Code"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Name", Request.Form["Name"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Qty", Request.Form["Qty"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Cost", Request.Form["Cost"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Seq", Request.Form["Seq"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Status", Request.Form["Status"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Remark", Request.Form["Remark"].ToString()));
                        sc.ExecuteNonQuery();
                    }
                    finally
                    {
                        sc.Connection.Close();
                    }
                    Search();
                }
            }
        }
        //public void OnPostUpdate()
        //{
        //    if (Request.Form.ContainsKey("ID"))
        //    {
        //        string[] ids = Request.Form["ID"].ToString().Split(',');
        //        string[] Codes = Request.Form["Code"].ToString().Split(',');
        //        string[] Names = Request.Form["Name"].ToString().Split(',');
        //        string[] Remarks = Request.Form["Remark"].ToString().Split(',');
        //        for (int i = 0; i < ids.Length; i++)
        //        {
        //            using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
        //            {
        //                try
        //                {
        //                    sc.Connection.Open();
        //                    sc.CommandText = "UPDATE MochiProduct SET Code=@Code,Name=@Name,Remark=@Remark WHERE ID=@ID;";
        //                    sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("ID", ids[i]));
        //                    sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Code", Codes[i]));
        //                    sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Name", Names[i]));
        //                    sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Remark", Remarks[i]));
        //                    sc.ExecuteNonQuery();
        //                }
        //                finally
        //                {
        //                    sc.Connection.Close();
        //                }
        //            }
        //        }

        //    }
        //    Search();
        //}
        public void OnPostUpdate()
        {
            if (Request.Form.ContainsKey("ID"))
            {
                    using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
                    {
                        try
                        {
                            sc.Connection.Open();
                            sc.CommandText = "UPDATE MochiProduct SET Code=@Code,Name=@Name,Remark=@Remark,Date=@Date,Qty=@Qty,Cost=@Cost,Seq=@Seq,Status=@Status WHERE ID=@ID;";
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("ID", Request.Form["ID"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Date", Request.Form["Date"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Code", Request.Form["Code"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Name", Request.Form["Name"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Qty", Request.Form["Qty"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Cost", Request.Form["Cost"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Seq", Request.Form["Seq"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Status", Request.Form["Status"].ToString()));
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("Remark", Request.Form["Remark"].ToString()));
                        sc.ExecuteNonQuery();
                        }
                        finally
                        {
                            sc.Connection.Close();
                        }
                    Search();
                }
            }
        }
        public void OnPostDelete()
        {
            if (Request.Form.ContainsKey("ID"))
            {
                using (Microsoft.Data.SqlClient.SqlCommand sc = new Microsoft.Data.SqlClient.SqlCommand("", new Microsoft.Data.SqlClient.SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection"))))
                {
                    try
                    {
                        sc.Connection.Open();
                        sc.CommandText = " DELETE MochiProduct WHERE ID=@ID; ";
                        sc.Parameters.Add(new Microsoft.Data.SqlClient.SqlParameter("ID", Request.Form["ID"].ToString()));
                        sc.ExecuteNonQuery();

                    }
                    finally
                    {
                        sc.Connection.Close();
                    }
                }
            }
            Search();
        }

    }
}
