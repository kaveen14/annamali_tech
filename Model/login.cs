using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace pro4.Models
{
    public class Loginprop
    {
        public int id { get; set; }
        //public string username { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
        public string editda { get; set; }
        public string editby { get; set; }
        public string result { get; set; }

        public string year { get; set; }

        public string pagesize { get; set; }

        public string pageindex { get; set; }
    }
    public class login
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconn1"].ConnectionString);
        public Loginprop Logincheckup(int id, string pass)
        {
            // var Result1 = " ";
            try
            {
                var qry1 = @"select * from Table_3 where id='" + id + "'and pass='" + pass + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);  //return output as list format
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                Loginprop prop = new Loginprop();
                if (dt1.Rows.Count > 0)
                {
                    prop.id = Convert.ToInt32(dt1.Rows[0]["id"]);
                    prop.name = dt1.Rows[0]["name"].ToString();
                    prop.pass = dt1.Rows[0]["pass"].ToString();
                    //  prop.year = dt2.Rows[0]["tname"].ToString();
                    prop.result = "Valid";
                    return prop;
                }
                else
                {
                    prop.result = "Invalid username and password !";
                    return prop;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}