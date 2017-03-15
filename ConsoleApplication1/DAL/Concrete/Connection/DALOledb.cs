using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.OleDb;
using DAL.Base;

namespace DAL.Concrete.Connection
{
    public class DALOledb : DALBase
    {
        public DALOledb() { }

        public DALOledb(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public override IDbConnection GetDataProviderConnection()
        {
            return new OleDbConnection();
        }

        public override IDbCommand GetDataProviderCommand()
        {
            return new OleDbCommand();
        }

        public override IDbDataAdapter GetDataProviderDataAdapter()
        {
            return new OleDbDataAdapter();
        }
    }
}
