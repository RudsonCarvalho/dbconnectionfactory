using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Base;

namespace DAL.Concrete.ConnectionString
{
    public class DALConnectionOracle : ConnectionStringBase
    {
        public override System.Data.Common.DbConnectionStringBuilder GetDbConnectionStringBuilder()
        {
            return new Oracle.DataAccess.Client.OracleConnectionStringBuilder();
        }
    }
}
