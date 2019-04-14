using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorDeMercadoApp.Core;
using ValorDeMercadoApp.Models;

namespace ValorDeMercadoApp.DAO
{
    public class ValorMercadoDAO
    {
        private int _idUsuario;
        private DbConnection _conn;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }

        public ValorMercadoDAO()
        {
            _conn = new DbConnection();
            IdUsuario = Convert.ToInt16(ConfigurationManager.AppSettings["usuario"].ToString());
        }
        /// <summary>
        /// Get parametros valor de mercado
        /// según identificador de propiedad
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <returns></returns>
        public DataSet GetParametros(int idPropiedad)
        {
            DataSet data = new DataSet();
            try
            {
                ArrayList arguments = new ArrayList()
            {
                "id_usu",
                "id_prop"
            };
                ArrayList argumentsValues = new ArrayList()
            {
                IdUsuario,
                idPropiedad
            };
                data = _conn.ExecuteSP("Propiedades_Mercado_Parametros", arguments, argumentsValues);
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLAMANDO SP (Renovacion_carga_propiedades) :{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
            return data;

        }

        /// <summary>
        /// Actualiza valor de mercado para la lista de contratos/propiedades
        /// dadas
        /// </summary>
        /// <param name="contratos"></param>
        public void ActualizaValorMercado(int idPropiedad, int valorMercado)
        {
            try
            {
                ArrayList arguments = new ArrayList()
            {
                "id_usu",
                "id_contrato",
                "valor_mercado"
            };
                ArrayList argumentsValues = new ArrayList()
            {
                IdUsuario,
                idPropiedad,
                valorMercado
            };
                _conn.ExecuteSP("Renovacion_Actualiza_Valor_Mercado", arguments, argumentsValues);
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLAMANDO SP (Renovacion_Actualiza_Valor_Mercado) :{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
        }

        /// <summary>
        /// Get Valores de mercado basado en los parametros obtenidos
        /// desde la api
        /// </summary>
        /// <param name="idPropiedadArriendo"></param>
        /// <param name="cantidad"></param>
        /// <param name="promedio"></param>
        /// <param name="minimo"></param>
        /// <param name="maximo"></param>
        /// <param name="dev_estandar"></param>
        /// <param name="var_estandar"></param>
        /// <param name="sup_total_prom"></param>
        /// <returns></returns>
        public DataSet GetValorMercado(int idPropiedadArriendo, int cantidad, string promedio, string minimo, string maximo, string dev_estandar, string var_estandar, string sup_total_prom)
        {
            DataSet data = new DataSet();
            try
            {
                ArrayList arguments = new ArrayList()
            {
                "id_usu",
                "id_prop",
                "cantidad",
                "promedio",
                "minimo",
                "maximo",
                "dev_estandar",
                "var_estandar",
                "sup_total_prom"
            };
                ArrayList argumentsValues = new ArrayList()
            {
                IdUsuario,
                idPropiedadArriendo,
                cantidad,
                promedio,
                minimo,
                maximo,
                dev_estandar,
                var_estandar,
                sup_total_prom
            };
                data = _conn.ExecuteSP("Propiedades_Mercado_Consulta", arguments, argumentsValues);
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLAMANDO SP (Renovacion_carga_propiedades) :{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
            return data;
        }
    }
}
