using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.Curewell.DAL
{
    public static class SqlConnectionString
    {
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["CurewellCon"].ConnectionString;
        }
    }
}
