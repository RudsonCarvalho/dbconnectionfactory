using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Oracle.DataAccess.Client;
using DAL.Base;

namespace DAL.Concrete.Connection
{
    public class DALOracle : DALBase
    {
        public DALOracle() { }

        public DALOracle(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
 
        public override IDbConnection GetDataProviderConnection()
        {
            return new OracleConnection();
        }

        public override IDbCommand GetDataProviderCommand()
        {
            return new OracleCommand();
        }
 
        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new OracleDataAdapter();
        }
    }
}
