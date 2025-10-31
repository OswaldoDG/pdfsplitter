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

        [Category("Datos PDF")]
        [Description("Tipo de documento que se está visualizando.")]
        public string TipoDocumento
        {
            get => _tipoDocumento;
            set
            {
                _tipoDocumento = value;
                lbTipoDoc.Text = value;
            }
        }

        [Category("Datos PDF")]
        [Description("Página donde inicia el documento.")]
        public int PaginaInicio
        {
            get => _paginaInicio;
            set
            {
                _paginaInicio = value;
                lbPagInicio.Text = value.ToString();
            }
        }

        [Category("Datos PDF")]
        [Description("Página actual que se está visualizando.")]
        public int PaginaActual
        {
            get => _paginaActual;
            set
            {
                _paginaActual = value;
                lbPagFinal.Text = value.ToString();
            }
        }

        public void ActualizarInfo(string tipo, int paginaInicio, int paginaActual)
        {
            TipoDocumento = tipo;
            PaginaInicio = paginaInicio;
            PaginaActual = paginaActual;
        }
    }
}
