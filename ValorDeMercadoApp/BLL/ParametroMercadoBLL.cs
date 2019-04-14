using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using ValorDeMercadoApp.DAO;
using ValorDeMercadoApp.Models;

namespace ValorDeMercadoApp.BLL
{
    public class ParametroMercadoBLL
    {
        private ValorMercadoDAO _parametroVMercadoDAO;


        public ParametroMercadoBLL()
        {
            _parametroVMercadoDAO = new ValorMercadoDAO();
        }

        public VMercadoPModel GetParametrosVMercado(int idPropiedad)
        {
            VMercadoPModel model = new VMercadoPModel();
            try
            {
                DataSet data = _parametroVMercadoDAO.GetParametros(idPropiedad);
                if ((data != null) && ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0)))
                {
                    DataRowCollection rows = data.Tables[0].Rows;
                    foreach (DataRow row in rows)
                    {
                        model.banosSP = row["banosSP"].ToString();
                        model.comunaSP = row["comunaSP"].ToString();
                        model.dormitoriosSP = row["dormitoriosSP"].ToString();
                        model.proc = row["proc"].ToString();
                        model.regionSP = row["regionSP"].ToString();
                        model.tipo_negocioSP = row["tipo_negociosp"].ToString();
                        model.tipo_propiedadSP = row["tipo_propiedadSP"].ToString();
                        model.XExp = row["xexp"].ToString().Replace(",", ".");
                        model.XIni = row["xini"].ToString().Replace(",", ".");
                        model.YExp = row["yexp"].ToString().Replace(",", ".");
                        model.YIni = row["yini"].ToString().Replace(",", ".");

                    }                    
                }
                else
                {
                    Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("SIN PARAMETROS DE MERCADO PARA LA PROPIEDAD {0}", idPropiedad.ToString()), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLENANDO MODELO (ParaMetros de Mercado):{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
            return model;
        }
    }
}
