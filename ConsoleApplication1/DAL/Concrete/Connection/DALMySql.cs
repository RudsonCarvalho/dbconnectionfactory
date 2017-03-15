using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using MySql.Data.MySqlClient;
using DAL.Base;

namespace DAL.Concrete.Connection
{
    public class DALMySql : DALBase
    {
        public DALMySql() { }

        public DALMySql(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
 
        public override IDbConnection GetDataProviderConnection()
        {
            return new MySqlConnection();
        }

        public override IDbCommand GetDataProviderCommand()
        {
            return new MySqlCommand();
        }
 
        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new MySqlDataAdapter();
        }
    }
}
