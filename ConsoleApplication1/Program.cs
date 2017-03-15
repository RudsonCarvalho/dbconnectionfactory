using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL;
using DAL.Factory;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string choice = string.Empty, dbProvider = string.Empty;
            bool done = false;
            do
            {
                Console.Clear();
                Console.WriteLine("\t Selecione um banco de dados\n");
                Console.WriteLine("\t 1. Access / OleDb");
                Console.WriteLine("\t 2. Odbc");
                Console.WriteLine("\t 3. Oracle");
                Console.WriteLine("\t 4. Sql");
                Console.WriteLine("\t 5. MySql");
                Console.WriteLine("\t 6. Excel \n");
                Console.WriteLine("===============================================");
                Console.Write("\t Entre com a selecao (0 para sair) : ");
                choice = Console.ReadLine();

                ConnectionParamDTO connectionParamDTO = null;

                switch (choice)
                {
                    case "0" :
                        done = true;
                        break;
 
                    case "1":
                        dbProvider = "Access";
                        connectionParamDTO = new ConnectionParamDTO(null, "C:\\Temp\\database\\DatabaseTeste.accdb", null, null, 0);
                        break;
 
                    case "2":
                        dbProvider = "Odbc";
                        break;
 
                    case "3":
                        dbProvider = "Oracle";
                        connectionParamDTO = new ConnectionParamDTO("localhost", "XE", "system", "Pa$$w0rd", 1521);
                        break;
 
                    case "4":
                        dbProvider = "SqlServer";
                        connectionParamDTO = new ConnectionParamDTO(".", "db_teste", "sa", "Pa$$w0rd", 1433);
                        break;
                    
                    case "5":
                        dbProvider = "MySql";
                        connectionParamDTO = new ConnectionParamDTO("dbmy0101.whservidor.com", "shopalianc_1", "shopalianc_1", "UH3112br", 3306);
                        break;

                    case "6":
                        dbProvider = "Excel";
                        connectionParamDTO = new ConnectionParamDTO(null, "C:\\Temp\\database\\planilha.xlsx", null, null, 0);
                        break;
                }

                if (!done)
                {
                    Console.WriteLine("================================");
                    DataProviderType provider = (DataProviderType)Enum.Parse(typeof(DataProviderType), dbProvider);

                    var DAL = DALDbFactory.getInstance().GetDataAccessLayer(provider);

                    //monta a connection string para o banco de dados
                    DAL.ConnectionString = DALDbFactory.getInstance().GetConnectionStringDataAccessLayer(provider, connectionParamDTO);
                    
                    DALDBWrapper.ExecuteSimpleQuery("update usuario set nome = 'Simone'", DAL);                   
                   

                    Console.WriteLine(DAL.TestOpenConnection());
                    Console.ReadKey();
                }
                

            } while (!done);
        }
    }
}
