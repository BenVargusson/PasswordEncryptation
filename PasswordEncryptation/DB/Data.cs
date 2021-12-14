using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PasswordEncryptation.DB
{
    class Data
    {

        private SqlConnectionStringBuilder cadenaConexion;
        private SqlConnection sqlConnection;
        private SqlCommand sqlCommand;
        private SqlDataReader response;

        public Data(string servidor, string bd, string user, string pass)
        {
            cadenaConexion = new SqlConnectionStringBuilder();
            cadenaConexion.DataSource = servidor;
            cadenaConexion.InitialCatalog = bd;
            cadenaConexion.UserID = user;
            cadenaConexion.Password = pass;


        }


    }
}
