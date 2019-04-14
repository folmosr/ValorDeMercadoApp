using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Models
{
    public class VMercadoPModel
    {
        private string _banosSP;
        private string _comunaSP;
        private string _dormitoriosSP;
        private string _proc;
        private string _regionSP;
        private string _tipo_negocioSP;
        private string _tipo_propiedadSP;
        private string _xexp;
        private string _xini;
        private string _yexp;
        private string _yini;

        public string banosSP { get => _banosSP; set => _banosSP = value; }
        public string comunaSP { get => _comunaSP; set => _comunaSP = value; }
        public string dormitoriosSP { get => _dormitoriosSP; set => _dormitoriosSP = value; }
        public string proc { get => _proc; set => _proc = value; }
        public string regionSP { get => _regionSP; set => _regionSP = value; }
        public string tipo_negocioSP { get => _tipo_negocioSP; set => _tipo_negocioSP = value; }
        public string XExp { get => _xexp; set => _xexp = value; }
        public string XIni { get => _xini; set => _xini = value; }
        public string YExp { get => _yexp; set => _yexp = value; }
        public string YIni { get => _yini; set => _yini = value; }
        public string tipo_propiedadSP { get => _tipo_propiedadSP; set => _tipo_propiedadSP = value; }
    }
}
