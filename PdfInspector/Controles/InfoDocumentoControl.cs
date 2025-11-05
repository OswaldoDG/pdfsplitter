using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfInspector.Controles
{
    public partial class InfoDocumentoControl : UserControl
    {
        private string _tipoDocumento = "";
        private int _paginaInicio = 0;
        private int _paginaActual = 0;

        public InfoDocumentoControl()
        {
            InitializeComponent();
        }

        public int PaginaActual
        {
            get { return _paginaActual; }
            set { 
                _paginaActual = value;
                renderLabel();
            }
        }

        private int _PaginaInicio;

        public int PaginaInicio
        {
            get { return _PaginaInicio; }
            set { _PaginaInicio = value; renderLabel(); }
        }


        private string _TipoDocumento;

        public string TipoDocumento
        {
            get { return _TipoDocumento; }
            set { _TipoDocumento = value; renderLabel(); }
        }


        public void ActualizarInfo(string tipo, int paginaInicio, int paginaActual)
        {
            _tipoDocumento = tipo;
            _paginaInicio = paginaInicio;   
            _paginaActual = paginaActual;   
            renderLabel();
        }

        private void renderLabel()
        {
            if (string.IsNullOrEmpty(_tipoDocumento))
            {
                this.lblDocInfo.Text = $"Pagina {_paginaActual}";
            }
            else
            {

                this.lblDocInfo.Text = $"Tipo: {_tipoDocumento} de {_paginaInicio} a {_paginaActual}";
            }
        }
    }
}
