using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Models
{
    public class DataVMercadoResultModel
    {
        public int cantidad { get; set; }
        public string promedio { get; set; }
        public string minimo { get; set; }
        public string maximo { get; set; }
        public string dev_estandar { get; set; }
        public string var_estandar { get; set; }
        public string sup_total_prom { get; set; }
    }
}
