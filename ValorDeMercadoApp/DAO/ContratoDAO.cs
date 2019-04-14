using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using ValorDeMercadoApp.Core;

namespace ValorDeMercadoApp.DAO
{
    public class ContratoDAO
    {
        private int _idUsuario;
        private int _dias;
        private int _idEstado;
        private int _idPropiedad;

        private DbConnection _conn;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public int Dias { get => _dias; set => _dias = value; }
        public int IdEstado { get => _idEstado; set => _idEstado = value; }
        public int IdPropiedad { get => _idPropiedad; set => _idPropiedad = value; }

        public ContratoDAO()
        {
            _conn = new DbConnection();
            Dias = Convert.ToInt16(ConfigurationManager.AppSettings["dias"].ToString());
            IdEstado = Convert.ToInt16(ConfigurationManager.AppSettings["estado"].ToString());
            IdUsuario = Convert.ToInt16(ConfigurationManager.AppSettings["usuario"].ToString());
            IdPropiedad = Convert.ToInt16(ConfigurationManager.AppSettings["propiedad"].ToString());
        }

        /// <summary>
        /// Get Contratos
        /// </summary>
        /// <param name="dias"></param>
        /// <param name="idEstado"></param>
        /// <param name="idProp"></param>
        /// <returns></returns>
        public DataSet GetContratros()
        {
            DataSet data = new DataSet();
            try
            {
                ArrayList arguments = new ArrayList()
            {
                "id_usu",
                "id_estado",
                "dias",
                "id_prop"
            };
                ArrayList argumentsValues = new ArrayList()
            {
                IdUsuario,
                IdEstado,
                Dias,
                IdPropiedad
            };
                data = _conn.ExecuteSP("Renovacion_carga_propiedades", arguments, argumentsValues);
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLAMANDO SP (Renovacion_carga_propiedades) :{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
            return data;
        }
    }
}
