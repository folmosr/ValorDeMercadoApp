using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorDeMercadoApp.Core;
using ValorDeMercadoApp.DAO;

namespace ValorDeMercadoApp.BLL
{
    public class ContratoBLL
    {
        ContratoDAO _contratoDAO;
        ParametroMercadoBLL _valorMercadoBLL;

        public ContratoBLL()
        {
            _contratoDAO = new ContratoDAO();
            _valorMercadoBLL = new ParametroMercadoBLL();
        }

        public IEnumerable<Models.ContratroModel> GetContratos()
        {
            List<Models.ContratroModel> contratoList = new List<Models.ContratroModel>();
            try
            {
                DataSet data = _contratoDAO.GetContratros();
                if ((data != null) && (data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
                {
                    DataRowCollection rows = data.Tables[0].Rows;
                    foreach (DataRow item in rows)
                    {
                        var contrato = new Models.ContratroModel()
                        {
                            IdContrato = Convert.ToInt32(item["id_contrato"].ToString()),
                            IdPropiedadArriendo = Convert.ToInt32(item["id_prop_arriendo"].ToString()), //6167
                            CompPago = (item["comp_pago"] == null) ? "0" : item["comp_pago"].ToString(),
                            DiasRenueva = (item["dias_renueva"] == null) ? "0" : item["dias_renueva"].ToString(),
                            FechaTermino = Convert.ToDateTime(item["termino_contrato"]),
                            IdEstado = (string.IsNullOrEmpty(item["id_estado"].ToString())) ? 0 : Convert.ToInt32(item["id_estado"].ToString()),
                            Estado = (item["estado"] == null) ? "0" : item["estado"].ToString(),
                            MesAno = (item["mes_ano"] == null) ? "0" : item["mes_ano"].ToString(),
                            Nota = item["nota"].ToString(),
                            ObsRenueva = item["obs_renueva"].ToString(),
                            PorcentajeDoc = item["porc_doc"].ToString(),
                            Renueva = (item["renueva"] == null) ? "0" : item["renueva"].ToString(),
                            ValorActual = (item["valor_actual"] == null) ? "0" : item["valor_actual"].ToString().Replace(",", "."),
                            ValorSugerido = (item["valor_sugerido"] == null) ? "0" : item["valor_sugerido"].ToString().Replace(",", "."),
                            ValorRenovacion = (item["valor_renovacion"] == null || item["valor_renovacion"].ToString() == "") ? "0" : item["valor_renovacion"].ToString().Substring(0, item["valor_renovacion"].ToString().Length - 2).Replace(",", ""),
                            Direccion = item["direccion"].ToString()
                        };
                        contrato.ParametrosMercado = _valorMercadoBLL.GetParametrosVMercado(contrato.IdPropiedadArriendo);
                        contratoList.Add(contrato);
                        Core.Logger.Instance.LogWriter.Write(new LogEntry()
                        {
                            Message = String.Format(
                                "CONTRATO DE ARRIENDO N°:{0} ENCONTRADO, PARAMETROS:" + Environment.NewLine +
                                "Comuna:{1}\n" + Environment.NewLine +
                                "Region:{2}\n" + Environment.NewLine +
                                "Dormitorios:{3}\n" + Environment.NewLine +
                                "Proc:{4}\n" + Environment.NewLine +
                                "Negocio:{5}\n" + Environment.NewLine +
                                "Propiedad:{6}\n" + Environment.NewLine +
                                "Xexp:{7}\n" + Environment.NewLine +
                                "Xini:{8}\n" + Environment.NewLine +
                                "Yexp:{9}\n" + Environment.NewLine +
                                "Yini:{10}\n" + Environment.NewLine +
                                "Baños:{11}\n"
                                , contrato.IdPropiedadArriendo.ToString()
                                , contrato.ParametrosMercado.comunaSP
                                , contrato.ParametrosMercado.regionSP
                                , contrato.ParametrosMercado.dormitoriosSP
                                , contrato.ParametrosMercado.proc
                                , contrato.ParametrosMercado.tipo_negocioSP
                                , contrato.ParametrosMercado.tipo_propiedadSP
                                , contrato.ParametrosMercado.XExp
                                , contrato.ParametrosMercado.XIni
                                , contrato.ParametrosMercado.YExp
                                , contrato.ParametrosMercado.YIni
                                , contrato.ParametrosMercado.banosSP
                        ),
                            Categories = new List<string> { "General" },
                            Priority = 1,
                            ProcessName = Core.Logger.PROCESS_NAME
                        });

                    }
                }
                else
                {
                    Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = "SIN CONTRATOS A PROCESAR", Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("LLENANDO MODELO (Propiedades):{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
            return contratoList;
        }
    }
}
