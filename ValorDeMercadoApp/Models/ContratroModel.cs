using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValorDeMercadoApp.Models
{
    public class ContratroModel
    {
        #region Private

        private int _idContrato;
        private int _idPropiedadArriendo;
        private string _compPago;
        private string _diasRenueva;
        private DateTime _fechaTermino;
        private int _idEstado;
        private string _estado;
        private string _mesAno;
        private string _nota;
        private string _obsRenueva;
        private string _porcentajeDoc;
        private string _renueva;
        private string _valorActual;
        private string _valorSugerido;
        private string _valorRenovacion;
        private string _direccion;
        private MercadoModel _mercado;
        private VMercadoPModel _parametrosMercado;
        private List<DataVMercadoResultModel> _data;

        #endregion Private 

        #region Public

        public int IdContrato { get => _idContrato; set => _idContrato = value; }
        public int IdPropiedadArriendo { get => _idPropiedadArriendo; set => _idPropiedadArriendo = value; }
        public string CompPago { get => _compPago; set => _compPago = value; }
        public string DiasRenueva { get => _diasRenueva; set => _diasRenueva = value; }
        public DateTime FechaTermino { get => _fechaTermino; set => _fechaTermino = value; }
        public int IdEstado { get => _idEstado; set => _idEstado = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string MesAno { get => _mesAno; set => _mesAno = value; }
        public string Renueva { get => _renueva; set => _renueva = value; }
        public string ValorActual { get => _valorActual; set => _valorActual = value; }
        public string ValorSugerido { get => _valorSugerido; set => _valorSugerido = value; }
        public string ValorRenovacion { get => _valorRenovacion; set => _valorRenovacion = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Nota { get => _nota; set => _nota = value; }
        public string ObsRenueva { get => _obsRenueva; set => _obsRenueva = value; }
        public string PorcentajeDoc { get => _porcentajeDoc; set => _porcentajeDoc = value; }
        public VMercadoPModel ParametrosMercado { get => _parametrosMercado; set => _parametrosMercado = value; }
        public List<DataVMercadoResultModel> Data { get => _data; set => _data = value; }
        public MercadoModel Mercado { get => _mercado; set => _mercado = value; }

        #endregion Public
    }
}
