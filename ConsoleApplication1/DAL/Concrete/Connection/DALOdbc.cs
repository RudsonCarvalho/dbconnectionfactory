using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Odbc;
using DAL.Base;

namespace DAL.Concrete.Connection
{
    public class DALOdbc : DALBase
    {        
        public DALOdbc() { }

        public DALOdbc(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new OdbcConnection();
        }

        public override IDbCommand GetDataProviderCommand()
        {
            return new OdbcCommand();
        }

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new OdbcDataAdapter();
        }
    }
}
