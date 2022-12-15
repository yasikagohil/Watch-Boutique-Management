using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Yazley_watch_boutique.Models
{
    public class selectMenModel
    {
        public DataSet GetMenProduct()
        {
            SqlConnection cn = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=C:\Users\Akshay\source\repos\yazley_watch_boutique\Yazley_watch_boutique\App_Data\yazley watch boutique.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework");
            SqlCommand cmd = new SqlCommand("Select * From product Where gender='Men'", cn);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
    }
}