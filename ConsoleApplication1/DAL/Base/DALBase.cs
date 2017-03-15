using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Data;

namespace DAL.Base
{
    public abstract class DALBase
    {
        private string strConnectionString;
        private IDbConnection connection;
        private IDbCommand command;
        private IDbTransaction transaction;
 
        public string ConnectionString
        {
            get
            {
              if (strConnectionString == string.Empty || strConnectionString.Length == 0)
                    throw new ArgumentException("Invalid connection string.");
                
              return strConnectionString;
            }
            set
            { strConnectionString = value; }
        }
 
        protected DALBase() { }
 
        public abstract IDbConnection GetDataProviderConnection();
        
        public abstract IDbCommand GetDataProviderCommand();
        
        public abstract IDbDataAdapter GetDataProviderDataAdapter();
         
        #region Database Teste
 
        public string TestOpenConnection()
        {
            string Response = string.Empty;
            try
            {
                connection = GetDataProviderConnection(); // instantiate a connection object
                connection.ConnectionString = this.ConnectionString;

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open(); // open connection
                }

                Response = ((System.Reflection.MemberInfo)(connection.GetType())).Name + "Open Successfully";               
            }
            catch (Exception e)
            {
                Response = "Unable to Open " + ((System.Reflection.MemberInfo)(connection.GetType())).Name;
                Response = String.Concat(Response, System.Environment.NewLine + e.Message);
            }
            finally 
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }

            return Response;
        }
 
        #endregion
    }
}
