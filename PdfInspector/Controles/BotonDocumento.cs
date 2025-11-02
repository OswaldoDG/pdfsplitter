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
    public partial class BotonDocumento : UserControl
    {
        public event EventHandler BotonPresionado;

        private Keys _teclaAtajo = Keys.None;
        private string _Texto;

        public BotonDocumento()
        {
            InitializeComponent();

            this.Load += BotonDocumento_Load;
            this.btnAccion.Click += BtnAccion_Click;
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }

        [Category("Comportamiento")]
        [Description("Tecla rápida asociada al botón.")]
        public Keys TeclaAtajo
        {
            get => _teclaAtajo;
            set
            {
                _teclaAtajo = value;
                lbAtajo.Text = $"[{_teclaAtajo}]";
            }
        }

        [Category("Apariencia")]
        [Description("Texto que se mostrará en el botón principal.")]
        public string Texto
        {
            get { return _Texto; }
            set
            {
                _Texto = value;
                this.btnAccion.Text = value;
            }
        }

        [Category("Datos")]
        [Description("Etiqueta o valor asociado al botón.")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new object Tag
        {
            get => base.Tag;
            set
            {
                base.Tag = value;
                btnAccion.Tag = value;
            }
        }

        private void BotonDocumento_Load(object sender, EventArgs e)
        {
            this.KeyDown += BotonDocumento_KeyDown;
            this.btnAccion.KeyDown += BotonDocumento_KeyDown;
            this.lbAtajo.KeyDown += BotonDocumento_KeyDown;
        }

        private void BtnAccion_Click(object sender, EventArgs e)
        {
            BotonPresionado?.Invoke(this, EventArgs.Empty);
        }

        private void BotonDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == _teclaAtajo)
            {
                BtnAccion_Click(this, EventArgs.Empty);
            }
        }



        //protected override void OnResize(EventArgs e)
        //{
        //    base.OnResize(e);

        //    int margen = 10;

        //    btnAccion.Size = new Size(this.Width - 60, this.Height - margen);
        //    btnAccion.Location = new Point(0, 0);

        //    lbAtajo.Location = new Point(btnAccion.Right + 5, (this.Height - lbAtajo.Height) / 2);
        //}

        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == _teclaAtajo)
            {
                BtnAccion_Click(this, EventArgs.Empty);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void PerformClick()
        {
            BtnAccion_Click(this, EventArgs.Empty);
        }

        private void BotonDocumento_Load_1(object sender, EventArgs e)
        {

        }
    }
}
