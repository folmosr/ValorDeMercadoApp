using Microsoft.Practices.EnterpriseLibrary.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ValorDeMercadoApp.Core;
using ValorDeMercadoApp.DAO;
using ValorDeMercadoApp.Models;

namespace ValorDeMercadoApp.BLL
{
    public class ValorMercadoBLL
    {
        private string _urlBase;
        private string _clientToken;

        public string UrlBase { get => _urlBase; set => _urlBase = value; }

        private ValorMercadoDAO _valorMercadoDAO;

        public string ClientToken { get => _clientToken; set => _clientToken = value; }

        public ValorMercadoBLL()
        {
            ClientToken = ConfigurationManager.AppSettings["clientTokenApi"].ToString();
            UrlBase = ConfigurationManager.AppSettings["apiVMercado"].ToString();
            _valorMercadoDAO = new ValorMercadoDAO();
        }

        public void ActualizaValorDeMercado(IEnumerable<ContratroModel> contratos)
        {
            try
            {
                foreach (ContratroModel contrato in contratos) {
                    _valorMercadoDAO.ActualizaValorMercado(contrato.IdContrato, Convert.ToInt32(contrato.Mercado.Valor_arr_prom_pe.Replace(".", String.Empty)));
                    Core.Logger.Instance.LogWriter.Write(new LogEntry()
                    {
                        Message = String.Format("ACTUALIZADO VALOR DE MERCADO CONTRATO N°{0}, PROPIEDAD N° {1}, VALOR {2}", contrato.IdContrato, contrato.IdPropiedadArriendo, contrato.Mercado.Valor_arr_prom_pe),
                        Categories = new List<string> { "General" },
                        Priority = 1,
                        ProcessName = Core.Logger.PROCESS_NAME
                    });
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry()
                {
                    Message = String.Format("EXCEPCION EN EL PROCESO DE ACTUALIZAR VALOR DE MERCADO {0}", ex.Message),
                    Categories = new List<string> { "General" },
                    Priority = 1,
                    ProcessName = Core.Logger.PROCESS_NAME
                });
            }
        }

        public void GetValoresMercado(IEnumerable<ContratroModel> contratos)
        {
            try
            {
                foreach (ContratroModel contrato in contratos)
                {
                    if (contrato.Data.Count(x=>(x.cantidad > 0 && 
                                                !string.IsNullOrEmpty(x.promedio) &&
                                                !string.IsNullOrEmpty(x.minimo) &&
                                                !string.IsNullOrEmpty(x.maximo) &&
                                                !string.IsNullOrEmpty(x.dev_estandar) &&
                                                !string.IsNullOrEmpty(x.var_estandar) &&
                                                !string.IsNullOrEmpty(x.sup_total_prom)
                                                )
                                           ) >  0) {
                        var data = contrato.Data.First();
                        DataSet dataMercado = _valorMercadoDAO.GetValorMercado(contrato.IdPropiedadArriendo,
                                                                                data.cantidad,
                                                                                data.promedio,
                                                                                data.minimo,
                                                                                data.maximo,
                                                                                data.dev_estandar,
                                                                                data.var_estandar,
                                                                                data.sup_total_prom);
                        if ((dataMercado != null) && ((dataMercado.Tables.Count > 0) && (dataMercado.Tables[0].Rows.Count > 0)))
                        {
                            DataRowCollection rows = dataMercado.Tables[0].Rows;
                            foreach (DataRow row in rows)
                            {
                                var valorMercado = new MercadoModel()
                                {
                                    Valor_arr_max_pe = row["valor_arr_max_pe"].ToString(),
                                    Valor_arr_max_uf = row["valor_arr_max_uf"].ToString(),
                                    Valor_arr_min_pe = row["valor_arr_min_pe"].ToString(),
                                    Valor_arr_min_uf = row["valor_arr_min_uf"].ToString(),
                                    Valor_arr_prom_pe = (row["valor_arr_prom_pe"].ToString() == "") ? "0" : row["valor_arr_prom_pe"].ToString(),
                                    Valor_arr_prom_uf = row["valor_arr_prom_uf"].ToString(),
                                    Valor_desv_estandar = row["valor_desv_estandar"].ToString(),
                                    Valor_m2_desv_estandar = row["valor_m2_desv_estandar"].ToString(),
                                    Valor_m2_max = row["valor_m2_max"].ToString(),
                                    Valor_m2_minimo = row["valor_m2_minimo"].ToString(),
                                    Valor_m2_prom = row["valor_m2_prom"].ToString(),
                                    Valor_max = row["valor_max"].ToString(),
                                    Valor_minimo = row["valor_minimo"].ToString(),
                                    Valor_prom = row["valor_prom"].ToString()
                                };
                                contrato.Mercado = valorMercado;
                                Core.Logger.Instance.LogWriter.Write(new LogEntry()
                                {
                                    Message = String.Format(
                                        "VALORES MERCADO PARA LA PROPIEDAD N° {0} :" + Environment.NewLine +
                                        "Valor_arr_max_pe : {1}" + Environment.NewLine +
                                        "Valor_arr_max_uf : {2}" + Environment.NewLine +
                                        "Valor_arr_min_pe : {3}" + Environment.NewLine +
                                        "Valor_arr_min_uf : {4}" + Environment.NewLine +
                                        "Valor_arr_prom_pe : {5}" + Environment.NewLine +
                                        "Valor_arr_prom_uf : {6}" + Environment.NewLine +
                                        "Valor_desv_estandar : {7}" + Environment.NewLine +
                                        "Valor_m2_desv_estandar : {8}" + Environment.NewLine +
                                        "Valor_m2_max : {9}" + Environment.NewLine +
                                        "Valor_m2_minimo : {10}" + Environment.NewLine +
                                        "Valor_m2_prom : {11}" + Environment.NewLine +
                                        "Valor_max : {12}" + Environment.NewLine +
                                        "Valor_minimo : {13}" + Environment.NewLine +
                                        "Valor_prom : {14}" + Environment.NewLine
                                        ,
                                        contrato.IdPropiedadArriendo,
                                        valorMercado.Valor_arr_max_pe,
                                        valorMercado.Valor_arr_max_uf,
                                        valorMercado.Valor_arr_min_pe,
                                        valorMercado.Valor_arr_min_uf,
                                        valorMercado.Valor_arr_prom_pe,
                                        valorMercado.Valor_arr_prom_uf,
                                        valorMercado.Valor_desv_estandar,
                                        valorMercado.Valor_m2_desv_estandar,
                                        valorMercado.Valor_m2_max,
                                        valorMercado.Valor_m2_minimo,
                                        valorMercado.Valor_m2_prom,
                                        valorMercado.Valor_max,
                                        valorMercado.Valor_minimo,
                                        valorMercado.Valor_prom
                                     ),
                                    Categories = new List<string> { "General" },
                                    Priority = 1,
                                    ProcessName = Core.Logger.PROCESS_NAME
                                });
                            }
                        }
                        else
                        {
                            Core.Logger.Instance.LogWriter.Write(new LogEntry()
                            {
                                Message = String.Format("SIN VALORES VALORES MERCADO PARA LA PROPIEDAD:{0}", contrato.IdPropiedadArriendo.ToString()),
                                Categories = new List<string> { "General" },
                                Priority = 1,
                                ProcessName = Core.Logger.PROCESS_NAME
                            });
                        }
                    }
                    else
                    {
                        Core.Logger.Instance.LogWriter.Write(new LogEntry()
                        {
                            Message = String.Format("PARA EL CONTRATO N° :{0}, N° PROPIEDAD {1} NO SE HAYARON VALOR DE MERCADO VALIDO ", contrato.IdContrato, contrato.IdPropiedadArriendo),
                            Categories = new List<string> { "General" },
                            Priority = 1,
                            ProcessName = Core.Logger.PROCESS_NAME
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry()
                {
                    Message = String.Format("EXCEPTION PROCESO OBTENCION VALORES MERCADO :{0}", ex.Message),
                    Categories = new List<string> { "General" },
                    Priority = 1,
                    ProcessName = Core.Logger.PROCESS_NAME
                });
            }
        }

        public async Task GetParametrosAsync(IEnumerable<ContratroModel> contratos)
        {
            foreach (ContratroModel contrato in contratos)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(UrlBase);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string JSONParameters = JsonConvert.SerializeObject(contrato.ParametrosMercado);
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(string.Format("?cliente={0}&data={1}", ClientToken, JSONParameters));
                        if (response.IsSuccessStatusCode)
                        {
                            var stream = await response.Content.ReadAsStreamAsync();
                            var r = Deserialize.DeserializeJsonFromStream<ContratroModel>(stream);
                            contrato.Data = r.Data;
                            Core.Logger.Instance.LogWriter.Write(new LogEntry()
                            {
                                Message = String.Format("HACIENDO LLAMALDO A API:{0}, PROPIEDAD: {1} PARAMETROS:{2}, RESULT:{3}", UrlBase, contrato.IdPropiedadArriendo,
                                                            JSONParameters, JsonConvert.SerializeObject(contrato.Data)),
                                Categories = new List<string> { "General" },
                                Priority = 1,
                                ProcessName = Core.Logger.PROCESS_NAME
                            });
                        }
                        else
                        {
                            Core.Logger.Instance.LogWriter.Write(new LogEntry()
                            {
                                Message = String.Format("HACIENDO LLAMALDO A API:{0}, PROPIEDAD: {1} PARAMETROS:{2}, RESULT:{3}", UrlBase, contrato.IdPropiedadArriendo,
                                JSONParameters, response.ReasonPhrase),
                                Categories = new List<string> { "General" },
                                Priority = 1,
                                ProcessName = Core.Logger.PROCESS_NAME
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Core.Logger.Instance.LogWriter.Write(new LogEntry()
                        {
                            Message = String.Format("HACIENDO LLAMALDO A API:{0}, PROPIEDAD: {1} PARAMETROS:{2}, ERROR:{3}", UrlBase, contrato.IdPropiedadArriendo,
                            JSONParameters, ex.Message),
                            Categories = new List<string> { "General" },
                            Priority = 1,
                            ProcessName = Core.Logger.PROCESS_NAME
                        });
                    }
                }
            }
        }

    }
}
