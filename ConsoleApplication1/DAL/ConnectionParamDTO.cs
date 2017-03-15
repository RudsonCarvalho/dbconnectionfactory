using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ConnectionParamDTO
    {
        public ConnectionParamDTO() { }

        public ConnectionParamDTO(String server, String database, String userId, String password, int port)
        {
            this.Server = server;
            this.Database = database;
            this.UserId = userId;
            this.Password = password;
            this.Port = port;
        }

        public String Server { get; set; }

        public String Database { get; set; }

        public String UserId { get; set; }

        public String Password { get; set; }

        public int Port { get; set; }

        public ExcelType ExcelType 
        {
            get
            {
                if (Database == null)
                {
                    throw new ArgumentNullException("Database");
                }
                else 
                {
                    return (Database.ToLower().Contains("xlsx") ? ExcelType.XLSX : ExcelType.XLS);
                }
             } 
        }
    }

    public enum ExcelType { XLS, XLSX }
}
