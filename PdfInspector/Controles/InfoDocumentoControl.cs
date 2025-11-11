using System.Windows.Forms;

namespace PdfInspector.Controles
{
    public partial class InfoDocumentoControl : UserControl
    {
        private string _tipoDocumento = "";
        private int _paginaInicio = 0;
        private int _paginaActual = 0;
        private int _totalPaginas = 0;
        private bool _mostrarSoloTotal = false;

        public InfoDocumentoControl()
        {
            InitializeComponent();
        }

        public int PaginaActual
        {
            get { return _paginaActual; }
            set { _paginaActual = value; renderLabel(); }
        }

        public int PaginaInicio
        {
            get { return _paginaInicio; }
            set { _paginaInicio = value; renderLabel(); }
        }

        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; renderLabel(); }
        }

        public int TotalPaginas
        {
            get { return _totalPaginas; }
            set { _totalPaginas = value; renderLabel(); }
        }

        public void ActualizarInfo(string tipo, int paginaInicio, int paginaActual)
        {
            _tipoDocumento = tipo;
            _paginaInicio = paginaInicio;
            _paginaActual = paginaActual;
            _mostrarSoloTotal = false;
            renderLabel();
        }

        public void ActualizarTotal(int totalPaginas)
        {
            _totalPaginas = totalPaginas;
            _mostrarSoloTotal = true;
            renderLabel();
        }

        private void renderLabel()
        {
            if (_mostrarSoloTotal)
            {
                this.lblDocInfo.Text = $"Total de páginas: {_totalPaginas}";
                return;
            }   
            if (string.IsNullOrEmpty(_tipoDocumento))
            {
                this.lblDocInfo.Text = $"Página {_paginaActual}";
            }
            else
            {
                this.lblDocInfo.Text = $"Tipo: {_tipoDocumento} de {_paginaInicio} a {_paginaActual}";
            }
        }
    }
}
