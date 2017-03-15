using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Base;

namespace DAL.Factory
{
    public enum DataProviderType
    {
        Access, Odbc, OleDb, Oracle, SqlServer, MySql, Excel
    }

    public abstract class DALFactory
    {
        public abstract DALBase GetDataAccessLayer(DataProviderType dataProviderType);

        public abstract ConnectionStringBase GetConnectionStringBuilderDataAccessLayer(DataProviderType dataProviderType);        
        
        public abstract String GetConnectionStringDataAccessLayer(DataProviderType dataProviderType, ConnectionParamDTO connectionParamDTO);
        
    }
}
