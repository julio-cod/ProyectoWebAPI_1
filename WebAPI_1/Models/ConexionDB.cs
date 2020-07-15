using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebAPI_1.Models
{
    public class ConexionDB
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

   

    }
}