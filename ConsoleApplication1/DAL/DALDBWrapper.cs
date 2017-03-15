using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DAL.Factory;
using DAL.Base;

namespace DAL
{
    public class DALDBWrapper
    {

        #region SimpleQuery

        /// <summary>
        /// Executa uma query e retorna a primeira coluna da primeira linha 
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static String ExecuteSimpleQuery(string sCommandSQL, DALBase DAL, IDbTransaction transaction)
        {          
            try
            {
                using (IDbConnection connection = DAL.GetDataProviderConnection())
                {
                    connection.ConnectionString = DAL.ConnectionString;

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand cmdDB = DAL.GetDataProviderCommand())
                    {
                        cmdDB.CommandText = sCommandSQL;
                        cmdDB.Connection = connection;

                        if (transaction != null)
                        {
                            cmdDB.Transaction = transaction;
                        }

                        object value = cmdDB.ExecuteScalar();
                        return (value != null) ? value.ToString() : String.Empty;
                    }
                    //disposable chama o metodo close Connection
                }
            }
            catch (Exception e)
            {
                throw new DALException("Classe: DBWrapper | Metodo: ExecuteSimpleQuery(" + sCommandSQL + ")", e);
            }    
        }

        /// <summary>
        /// Executa uma query e retorna a primeira coluna da primeira linha 
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <returns></returns>
        public static String ExecuteSimpleQuery(string sCommandSQL, DALBase DAL)
        {
            return ExecuteSimpleQuery(sCommandSQL, DAL, null);
        }

        #endregion

        #region NonQuery

        /// <summary>
        /// Executa um comando sql e retorna o nro de linhas afetadas 
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool ExecuteCommand(string sCommandSQL, DALBase DAL, IDbTransaction transaction)
        {           
            try
            {
                using (IDbConnection connection = DAL.GetDataProviderConnection())
                {
                    connection.ConnectionString = DAL.ConnectionString;

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand cmdDB = DAL.GetDataProviderCommand())
                    {
                        cmdDB.CommandText = sCommandSQL;
                        cmdDB.Connection = connection;

                        if (transaction != null)
                        {
                            cmdDB.Transaction = transaction;
                        }

                        int value = cmdDB.ExecuteNonQuery();
                        return (value > 0)? true : false;
                    }
                    //disposable chama o metodo close Connection
                }
            }
            catch (Exception e)
            {
                throw new DALException("Class: DALDBWrapper | Method: ExecuteCommand(" + sCommandSQL + ")", e);
            } 
        }

        /// <summary>
        /// Executa um comando sql e retorna o nro de linhas afetadas
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <returns></returns>
        public static bool ExecuteCommand(string sCommandSQL, DALBase DAL)
        {
            return ExecuteCommand(sCommandSQL, DAL, null);
        }

        #endregion

        #region DataTableQuery 

        /// <summary>
        /// Retorna um Datatable
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public DataTable DataTableQuery(string sCommandSQL,DALBase DAL, IDbTransaction transaction)
        {         
            try
            {
                using (IDbConnection connection = DAL.GetDataProviderConnection())
                {
                    connection.ConnectionString = DAL.ConnectionString;

                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    using (IDbCommand cmdDB = DAL.GetDataProviderCommand())
                    {
                        cmdDB.CommandText = sCommandSQL;
                        cmdDB.Connection = connection;

                        if (transaction != null)
                        {
                            cmdDB.Transaction = transaction;
                        }                        
                        
                        using (DataSet dsReturn = new DataSet())
                        {
                            IDbDataAdapter adapter = DAL.GetDataProviderDataAdapter();
                            
                            try 
                            {                                
                                adapter.SelectCommand = cmdDB;
                                adapter.Fill(dsReturn);

                                if (dsReturn != null && dsReturn.Tables.Count > 0)
                                {
                                    return dsReturn.Tables[0];
                                }
                                else
                                {
                                    return null;
                                }
                            } 
                            catch( Exception e)
                            {
                                throw e;
                            }
                            finally 
                            {
                                ((IDisposable) adapter).Dispose();
                            }                            
                        }
                    }
                    //disposable chama o metodo close Connection
                }
            }
            catch (Exception e)
            {
                throw new DALException("Classe: DBWrapper | Metodo: DataTableQuery(" + sCommandSQL + ")", e);
            }
        }

        /// <summary>
        ///  Retorna um Datatable
        /// </summary>
        /// <param name="sCommandSQL"></param>
        /// <param name="DAL"></param>
        /// <returns></returns>
        public DataTable DataTableQuery(string sCommandSQL, DALBase DAL) 
        { 
           return DataTableQuery(sCommandSQL, DAL, null) ;
        }

        #endregion


        #region PrepareSQL

        public static string PrepareSQL()
        {
            return PrepareSQL(String.Empty);
        }

        public static char PrepareSQL(bool sField)
        {
            if (sField)
                return '1';
            else
                return '0';
        }

        public static string PrepareSQL(long sField)
        {
            if (sField == 0)
                return "NULL";
            else
                return sField.ToString();
        }

        public static string PrepareSQL(string sField)
        {
            return PrepareSQL(sField, false);
        }

        public static string PrepareSQL(float sField)
        {
            if (sField == 0.0)
                return "NULL";
            else
                return PrepareSQL(sField.ToString(), true).Replace(",", ".");
        }

        public static string PrepareSQL(DateTime sField)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
            if (sField.Equals(new DateTime(0)))
                return "NULL";
            else
                return PrepareSQL(sField.ToString("yyyy-MM-dd hh:mm:ss"), false);
        }

        public static string PrepareSQL(string sField, bool isNumber)
        {
            if (String.IsNullOrEmpty(sField) || sField.Trim().Replace("'", String.Empty).Length == 0)
            {
                return "NULL";
            }
            else
            {
                if (isNumber)
                    return sField.Trim().Replace("'", "''");
                else
                {
                    return "'" + sField.Trim().Replace("'", "''") + "'";
                }
            }
        }

        #endregion

        #region Enums
        
        public enum TypeObjReturn { Datetime, Text, Number, Float, Boolean }
        public enum OptionDropDownList { All, Select }

        #endregion

        #region Validations

        public static object CheckDBField(System.Data.DataRow drItem, string sItem, TypeObjReturn typeObjectReturn)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");

            try
            {
                if (typeObjectReturn.Equals(TypeObjReturn.Datetime))
                    return drItem[sItem].Equals(DBNull.Value) ? new DateTime(0) : Convert.ToDateTime(drItem[sItem]);

                if (typeObjectReturn.Equals(TypeObjReturn.Number))
                    return drItem[sItem].Equals(DBNull.Value) ? 0 : Convert.ToInt64(drItem[sItem].ToString());

                if (typeObjectReturn.Equals(TypeObjReturn.Float))
                    return drItem[sItem].Equals(DBNull.Value) ? 0 : float.Parse(drItem[sItem].ToString());

                if (typeObjectReturn.Equals(TypeObjReturn.Text))
                    return drItem[sItem].Equals(DBNull.Value) ? String.Empty : drItem[sItem].ToString().Trim();

                if (typeObjectReturn.Equals(TypeObjReturn.Boolean))
                    return drItem[sItem].Equals(DBNull.Value) ? false : (drItem[sItem].ToString().Trim().ToUpper().Equals("1") || drItem[sItem].ToString().Trim().ToUpper().Equals("TRUE")) ? true : false;

            }
            catch (Exception e)
            {
                throw new DALException("Classe: DBWrapper | Metodo: CheckDBField(drItem, sItem, typeObjectREturn) | Mensagem: " + e.Message, e);
            }

            return String.Empty;
        }
        #endregion
    }
}
