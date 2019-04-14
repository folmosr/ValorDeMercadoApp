using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Core
{
    /// <summary>
    /// Summary description for BaseDAO
    /// </summary>
    public class DbConnection
    {
        private SqlConnection conn;

        /// <summary>
        /// Ejecución Store Procedure
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parametros"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public DataSet ExecuteSP(string spName, ArrayList parametros, ArrayList valores)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                string str = this.StringConn();

                using (this.conn = new SqlConnection(str))
                {
                    this.conn.StatisticsEnabled = false;

                    using (SqlCommand cmd = new SqlCommand(spName, this.conn))
                    {
                        for (int i = 0; i < parametros.Count; i++)
                        {
                            cmd.Parameters.Add(new SqlParameter(parametros[i].ToString(), valores[i].ToString()));
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("EJECUTANDO SP:{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Logger.PROCESS_NAME });
            }
            return ds;
        }

        /// <summary>
        /// Conexión a Base de datos
        /// </summary>
        /// <returns></returns>
        public string StringConn()
        {
            string conexionBaseDatos = null;
            try
            {
                conexionBaseDatos = ConfigurationManager.ConnectionStrings["Corretaje"].ToString();
                return conexionBaseDatos;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("OBTENIENDO CONNECTIONSTRING:{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Logger.PROCESS_NAME });
            }
            return conexionBaseDatos;
        }

    }
}
