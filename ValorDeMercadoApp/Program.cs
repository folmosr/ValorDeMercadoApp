using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValorDeMercadoApp.BLL;
using ValorDeMercadoApp.Core;

namespace ValorDeMercadoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ContratoBLL _contratoBLL = new ContratoBLL();
                ValorMercadoBLL _valorMercadoBLL = new ValorMercadoBLL();

                IEnumerable<Models.ContratroModel> contratos = _contratoBLL.GetContratos();
                _valorMercadoBLL.GetParametrosAsync(contratos).Wait();
                _valorMercadoBLL.GetValoresMercado(contratos);
                _valorMercadoBLL.ActualizaValorDeMercado(contratos);
            }
            catch (Exception ex)
            {
                Core.Logger.Instance.LogWriter.Write(new LogEntry() { Message = String.Format("INICIANDO VALOR DE MERCADO APP:{0}", ex.Message), Categories = new List<string> { "General" }, Priority = 1, ProcessName = Core.Logger.PROCESS_NAME });
            }
        }
    }
}
