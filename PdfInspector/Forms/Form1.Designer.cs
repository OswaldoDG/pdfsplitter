namespace PdfInspector
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btnCargarDocumentos = new System.Windows.Forms.Button();
            this.txtIdsDocumentos = new System.Windows.Forms.TextBox();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.flowLayoutPanelArchivos = new System.Windows.Forms.FlowLayoutPanel();
            this.pdfViewer = new PdfiumViewer.PdfViewer();
            this.panelSuperior.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.Controls.Add(this.btnCargarDocumentos);
            this.panelSuperior.Controls.Add(this.txtIdsDocumentos);
            this.panelSuperior.Controls.Add(this.lblInstrucciones);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(800, 50);
            this.panelSuperior.TabIndex = 0;
            // 
            // btnCargarDocumentos
            // 
            this.btnCargarDocumentos.Location = new System.Drawing.Point(500, 13);
            this.btnCargarDocumentos.Name = "btnCargarDocumentos";
            this.btnCargarDocumentos.Size = new System.Drawing.Size(120, 23);
            this.btnCargarDocumentos.TabIndex = 2;
            this.btnCargarDocumentos.Text = "Cargar Documentos";
            this.btnCargarDocumentos.UseVisualStyleBackColor = true;
            this.btnCargarDocumentos.Click += new System.EventHandler(this.btnCargarDocumentos_Click);
            // 
            // txtIdsDocumentos
            // 
            this.txtIdsDocumentos.Location = new System.Drawing.Point(280, 15);
            this.txtIdsDocumentos.Name = "txtIdsDocumentos";
            this.txtIdsDocumentos.Size = new System.Drawing.Size(200, 20);
            this.txtIdsDocumentos.TabIndex = 1;
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.AutoSize = true;
            this.lblInstrucciones.Location = new System.Drawing.Point(12, 18);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(258, 13);
            this.lblInstrucciones.TabIndex = 0;
            this.lblInstrucciones.Text = "Ingrese los IDs de los documentos (separados por coma):";
            // 
            // flowLayoutPanelArchivos
            // 
            this.flowLayoutPanelArchivos.AutoScroll = true;
            this.flowLayoutPanelArchivos.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanelArchivos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelArchivos.Location = new System.Drawing.Point(0, 50);
            this.flowLayoutPanelArchivos.Name = "flowLayoutPanelArchivos";
            this.flowLayoutPanelArchivos.Size = new System.Drawing.Size(250, 400);
            this.flowLayoutPanelArchivos.TabIndex = 1;
            this.flowLayoutPanelArchivos.WrapContents = false;
            // 
            // pdfViewer
            // 
            this.pdfViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfViewer.Location = new System.Drawing.Point(250, 50);
            this.pdfViewer.Name = "pdfViewer";
            this.pdfViewer.Size = new System.Drawing.Size(550, 400);
            this.pdfViewer.TabIndex = 2;
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pdfViewer);
            this.Controls.Add(this.flowLayoutPanelArchivos);
            this.Controls.Add(this.panelSuperior);
            this.Name = "AppForm";
            this.Text = "Visor de Documentos";
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.Button btnCargarDocumentos;
        private System.Windows.Forms.TextBox txtIdsDocumentos;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelArchivos;
        private PdfiumViewer.PdfViewer pdfViewer;
    }
}