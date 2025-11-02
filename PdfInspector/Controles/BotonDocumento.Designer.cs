namespace PdfInspector.Controles
{
    partial class BotonDocumento
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAccion = new System.Windows.Forms.Button();
            this.lbAtajo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAccion
            // 
            this.btnAccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccion.Location = new System.Drawing.Point(130, 10);
            this.btnAccion.Name = "btnAccion";
            this.btnAccion.Size = new System.Drawing.Size(120, 35);
            this.btnAccion.TabIndex = 0;
            this.btnAccion.Text = "button1";
            this.btnAccion.UseVisualStyleBackColor = true;
            // 
            // lbAtajo
            // 
            this.lbAtajo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAtajo.Location = new System.Drawing.Point(0, 10);
            this.lbAtajo.Name = "lbAtajo";
            this.lbAtajo.Size = new System.Drawing.Size(120, 30);
            this.lbAtajo.TabIndex = 1;
            this.lbAtajo.Text = "...";
            this.lbAtajo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BotonDocumento
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnAccion);
            this.Controls.Add(this.lbAtajo);
            this.Name = "BotonDocumento";
            this.Size = new System.Drawing.Size(260, 50);
            this.Load += new System.EventHandler(this.BotonDocumento_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccion;
        private System.Windows.Forms.Label lbAtajo;
    }
}
