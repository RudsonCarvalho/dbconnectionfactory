using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.Common;
using DAL.Concrete;
using DAL.Base;
using DAL.Concrete.ConnectionString;
using DAL.Concrete.Connection;


namespace DAL.Factory
{
    public class DALDbFactory : DALFactory
    {
        private static DALDbFactory _DALDbFactory;

        public static DALDbFactory getInstance()
        {

            if (_DALDbFactory == null)
            {
                _DALDbFactory = new DALDbFactory();
            }

            return _DALDbFactory;
        }

        public override DALBase GetDataAccessLayer(DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                case DataProviderType.Excel:
                    return new DALOledb();
                case DataProviderType.Access:
                    return new DALOledb();
                case DataProviderType.OleDb:
                    return new DALOledb();
                case DataProviderType.Odbc:
                    return new DALOdbc();
                case DataProviderType.Oracle:
                    return new DALOracle();
                case DataProviderType.SqlServer:
                    return new DALSqlServer();
                case DataProviderType.MySql:
                    return new DALMySql();
                default:
                    throw new ArgumentException("Invalid data access layout data provider type.");
            }
        }

        public override ConnectionStringBase GetConnectionStringBuilderDataAccessLayer(DataProviderType dataProviderType)
        {
            switch (dataProviderType)
            {
                case DataProviderType.Excel:
                    return new DALConnectionOledb();
                case DataProviderType.Access:
                    return new DALConnectionOledb();
                case DataProviderType.OleDb:
                    return new DALConnectionOledb();
                case DataProviderType.Odbc:
                    return new DALConnectionOdbc();
                case DataProviderType.Oracle:
                    return new DALConnectionOracle();
                case DataProviderType.SqlServer:
                    return new DALConnectionSqlServer();
                case DataProviderType.MySql:
                    return new DALConnectionMySql();
                default:
                    throw new ArgumentException("Invalid data access layout data provider type.");
            }
        }

        public override string GetConnectionStringDataAccessLayer(DataProviderType dataProviderType, ConnectionParamDTO connectionParamDTO)
        {
            ConnectionStringBase connectionStringBase = GetConnectionStringBuilderDataAccessLayer(dataProviderType);
            DbConnectionStringBuilder builder = connectionStringBase.GetDbConnectionStringBuilder();

            String server = null;
            switch (dataProviderType)
            {
                case DataProviderType.Excel:
                    //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\myFolder\myExcel2007file.xlsx;Extended Properties="Excel 12.0 Xml;HDR=YES";"
                    builder.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=c:\\myFolder\\myOldExcelFile.xls;Extended Properties=\"Excel 8.0;HDR=YES\";";
                    builder["Extended Properties"] = (connectionParamDTO.ExcelType == ExcelType.XLS) ? "Excel 8.0;HDR=YES" : "Excel 12.0 Xml;HDR=YES";
                    builder["Data Source"] = connectionParamDTO.Database;
                    break;
                case DataProviderType.Access:
                    builder.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\myFolder\\myAccessFile.mdb;Jet OLEDB:Database Password=MyDbPassword;";
                    builder["Data Source"] = connectionParamDTO.Database;
                    builder["Password"] = connectionParamDTO.Password;
                    break;
                case DataProviderType.OleDb:
                    throw new ArgumentException("Use GetConnectionStringBuilderDataAccessLayer to GetDbConnectionStringBuilder and construct a connection String from builder.");
                case DataProviderType.Odbc:
                    throw new ArgumentException("Use GetConnectionStringBuilderDataAccessLayer to GetDbConnectionStringBuilder and construct a connection String from builder.");
                case DataProviderType.Oracle:
                    builder.ConnectionString = "user id=scott;password=tiger;Data source=oracle;";
                    server = connectionParamDTO.Server;

                    if (connectionParamDTO.Port > 0)
                    {
                        server = String.Concat(server, String.Concat(":", connectionParamDTO.Port));
                    }

                    //builder["Server"] = server;
                    builder["Data source"] = connectionParamDTO.Database;
                    builder["user Id"] = connectionParamDTO.UserId;
                    builder["password"] = connectionParamDTO.Password; ;
                    break;
                case DataProviderType.SqlServer:
                    builder.ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
                    server = connectionParamDTO.Server;

                    if (connectionParamDTO.Port > 0)
                    {
                        server = String.Concat(server, String.Concat(",", connectionParamDTO.Port));
                    }

                    builder["Server"] = connectionParamDTO.Server;
                    builder["Database"] = connectionParamDTO.Database;
                    builder["User Id"] = connectionParamDTO.UserId;
                    builder["Password"] = connectionParamDTO.Password;
                    break;
                case DataProviderType.MySql:
                    builder.ConnectionString = "Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
                    builder["Server"] = connectionParamDTO.Server;
                    builder["Port"] = connectionParamDTO.Port;
                    builder["Database"] = connectionParamDTO.Database;
                    builder["Uid"] = connectionParamDTO.UserId;
                    builder["Pwd"] = connectionParamDTO.Password;
                    break;
                default:
                    throw new ArgumentException("Use GetConnectionStringBuilderDataAccessLayer to GetDbConnectionStringBuilder from oledb or odbc and construct a connection String from builder.");
            }

            return builder.ConnectionString;
        }
            
    }
}
