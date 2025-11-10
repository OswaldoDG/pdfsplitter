namespace PdfInspector.Controles
{
    partial class InfoDocumentoControl
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
            this.lblDocInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblDocInfo
            // 
            this.lblDocInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDocInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocInfo.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblDocInfo.Location = new System.Drawing.Point(0, 0);
            this.lblDocInfo.Name = "lblDocInfo";
            this.lblDocInfo.Size = new System.Drawing.Size(150, 50);
            this.lblDocInfo.TabIndex = 0;
            this.lblDocInfo.Text = "...";
            this.lblDocInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InfoDocumentoControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.lblDocInfo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InfoDocumentoControl";
            this.Size = new System.Drawing.Size(150, 50);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDocInfo;
    }
}
