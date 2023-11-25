using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace pro4.Models
{
    public class data_prop
    {
        public string datab { get; set;}
        public string datay { get; set;}
        public string cbcd { get; set; }
        public string dataclip { get; set; }

       // page index separation for loaddata page

      //  public string year { get; set; }
        public string pagesize { get; set; }
        public string pageindex { get; set; }
    }

    public class drop_down
    {
        /*  public string g_date { get; set; }
          public string g_day { get; set; }
          public string g_docs { get; set; }
          public string g_receipts { get; set; }
          public string g_payments { get; set; }
          public string g_balance { get; set; }*/
        public string gl_code { get; set; }
        public string gl_name { get; set; }
        public string gl_cbcd { get; set; }
        public string gl_budamt { get; set; }
        public string gl_yopbal { get; set; }
        public string gl_tdamt { get; set; }
    }


    public class datalink
    {

        /*
        public string datacheck(string datab,string datay)
        {
            prop.datab = datab;
            prop.datay = datay;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[prop.datab].ConnectionString);
                var qry1 = @"select * from '" + prop.datay +"'";
                SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);  //return output as list format
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                if (dt1.Rows.Count > 0)
                {
                  //  prop.datab = datab;
                    prop.datay = datay;
                    return "Valid Table";
                }
                else
                {
                    return "Invalid Table";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        */
        data_prop prop = new data_prop();
        public DataTable List1(string datasql)
        {
            
            if (datasql == "sqlconn2")
            {
                prop.dataclip = "HTClipper";
            }
            else if(datasql == "sqlconn3")
            {
                prop.dataclip = "WVGClipper";
            }
            else 
            {
                prop.dataclip = "Spinning";
            }
            prop.datab = datasql;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["sqlconn1"].ConnectionString);
            try
            {
                var qry1 = @"select * from "+prop.dataclip+""; // HT_Clipper_Test table 
                SqlDataAdapter da = new SqlDataAdapter(qry1, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public Tuple<DataTable, int> tbody(string datab,string datay,string cbcd,string Pagesize, string Pageindex)
        {
            prop.datab = datab;
            prop.datay = datay;
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings[prop.datab].ConnectionString);

            //data_prop propth = new data_prop();

            try
            {
               // propth.year = year;
                prop.pagesize = Pagesize;
                prop.pageindex = Pageindex;
                prop.cbcd = cbcd;

                var qry = @" select * from (select ROW_NUMBER() over (order by GL_CODE) as rowno, " +
                    "GL_CODE,GL_NAME,GL_CBCD, GL_BUDAMT ,GL_YOPBAL,GL_TDAMT from  " + prop.datay + " Where GL_CBCD='"+prop.cbcd+"')" +
                   "  as count Where rowno>=(convert(int," + prop.pagesize + ")*(convert(int," + prop.pageindex + ")-1)+1)" +
                  "  AND rowno<= (convert(int," + prop.pagesize + ") * convert(int, " + prop.pageindex + ")) ORDER BY rowno  " +

                         "  select count(*) count from " + prop.datay + " Where GL_CBCD='" + prop.cbcd + "'";

                


                SqlDataAdapter thda = new SqlDataAdapter(qry, con2);
                DataSet thdt = new DataSet();
                thda.Fill(thdt);
                List<drop_down> list1 = new List<drop_down>();
                for (int i = 0; i < thdt.Tables[0].Rows.Count; i++)
                {
                    drop_down proptb = new drop_down();
                    // proptb.row = " ";
                    /*    proptb.g_date = thdt.Tables[0].Rows[i]["GL_CODE"].ToString();
                        proptb.g_day = thdt.Tables[0].Rows[i]["GL_NAME"].ToString();
                        proptb.g_docs = thdt.Tables[0].Rows[i]["GL_GLCD"].ToString();
                        proptb.g_receipts = thdt.Tables[0].Rows[i]["GL_Receipts"].ToString();
                        proptb.g_payments = thdt.Tables[0].Rows[i]["SL_Payments"].ToString();
                        proptb.g_balance = thdt.Tables[0].Rows[i]["SL_balance"].ToString(); */

                    proptb.gl_code = thdt.Tables[0].Rows[i]["GL_CODE"].ToString();
                    proptb.gl_name = thdt.Tables[0].Rows[i]["GL_NAME"].ToString();
                    proptb.gl_cbcd = thdt.Tables[0].Rows[i]["GL_CBCD"].ToString();
                    proptb.gl_budamt = thdt.Tables[0].Rows[i]["GL_BUDAMT"].ToString();
                    proptb.gl_yopbal = thdt.Tables[0].Rows[i]["GL_YOPBAL"].ToString();
                    proptb.gl_tdamt = thdt.Tables[0].Rows[i]["GL_TDAMT"].ToString();

                    list1.Add(proptb);
                }

                return Tuple.Create(thdt.Tables[0], Convert.ToInt32(thdt.Tables[1].Rows[0]["count"]));


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}