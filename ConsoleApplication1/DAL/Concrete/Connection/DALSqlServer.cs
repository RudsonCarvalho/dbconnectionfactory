using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using DAL.Base;

namespace DAL.Concrete.Connection
{
    public class DALSqlServer : DALBase
    {
        public DALSqlServer() { }

        public DALSqlServer(string connectionString) 
        {
            this.ConnectionString = connectionString;
        }
 
        public override IDbConnection GetDataProviderConnection()
        {
            return new SqlConnection();
        }

        public override IDbCommand GetDataProviderCommand()
        {
            return new SqlCommand();
        }
 
        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
 
