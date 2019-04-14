using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Models
{
    public class ValorMercadoModel
    {
        private int _idPropiedad;
        private int _valorMercado;

        public int IdPropiedad { get => _idPropiedad; set => _idPropiedad = value; }
        public int ValorMercado { get => _valorMercado; set => _valorMercado = value; }
    }
}
