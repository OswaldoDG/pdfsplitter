namespace PdfInspector
{
    partial class Form1
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pdfDocumentViewer1 = new Spire.PdfViewer.Forms.PdfDocumentViewer();
            this.button1 = new System.Windows.Forms.Button();
            this.pdfViewer1 = new PdfiumViewer.PdfViewer();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.txtPdf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInicioPag = new System.Windows.Forms.Button();
            this.btnFinPag = new System.Windows.Forms.Button();
            this.txtNombreParte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCrearPartes = new System.Windows.Forms.Button();
            this.progressBarPartes = new System.Windows.Forms.ProgressBar();
            this.btnRevision = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pdfDocumentViewer1
            // 
            this.pdfDocumentViewer1.AutoScroll = true;
            this.pdfDocumentViewer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.pdfDocumentViewer1.FormFillEnabled = false;
            this.pdfDocumentViewer1.Location = new System.Drawing.Point(327, 52);
            this.pdfDocumentViewer1.MultiPagesThreshold = 60;
            this.pdfDocumentViewer1.Name = "pdfDocumentViewer1";
            this.pdfDocumentViewer1.OnRenderPageExceptionEvent = null;
            this.pdfDocumentViewer1.PageLayoutMode = Spire.PdfViewer.Forms.PageLayoutMode.SinglePageContinuous;
            this.pdfDocumentViewer1.Size = new System.Drawing.Size(26, 332);
            this.pdfDocumentViewer1.TabIndex = 0;
            this.pdfDocumentViewer1.Text = "pdfDocumentViewer1";
            this.pdfDocumentViewer1.Threshold = 60;
            this.pdfDocumentViewer1.ViewerMode = Spire.PdfViewer.Forms.PdfViewerMode.PdfViewerMode.MultiPage;
            this.pdfDocumentViewer1.ZoomFactor = 1F;
            this.pdfDocumentViewer1.ZoomMode = Spire.PdfViewer.Forms.ZoomMode.Default;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pdfViewer1
            // 
            this.pdfViewer1.Location = new System.Drawing.Point(431, 34);
            this.pdfViewer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pdfViewer1.Name = "pdfViewer1";
            this.pdfViewer1.Size = new System.Drawing.Size(610, 457);
            this.pdfViewer1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(146, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(58, 39);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDescargar
            // 
            this.btnDescargar.Location = new System.Drawing.Point(189, 68);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(75, 20);
            this.btnDescargar.TabIndex = 4;
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.UseVisualStyleBackColor = true;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
            // 
            // txtPdf
            // 
            this.txtPdf.Location = new System.Drawing.Point(30, 68);
            this.txtPdf.Name = "txtPdf";
            this.txtPdf.Size = new System.Drawing.Size(148, 20);
            this.txtPdf.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ingrese Id del Pdf:";
            // 
            // btnInicioPag
            // 
            this.btnInicioPag.Location = new System.Drawing.Point(29, 272);
            this.btnInicioPag.Name = "btnInicioPag";
            this.btnInicioPag.Size = new System.Drawing.Size(75, 23);
            this.btnInicioPag.TabIndex = 7;
            this.btnInicioPag.Text = "Inicio";
            this.btnInicioPag.UseVisualStyleBackColor = true;
            this.btnInicioPag.Click += new System.EventHandler(this.btnInicioPag_Click);
            // 
            // btnFinPag
            // 
            this.btnFinPag.Location = new System.Drawing.Point(129, 272);
            this.btnFinPag.Name = "btnFinPag";
            this.btnFinPag.Size = new System.Drawing.Size(75, 23);
            this.btnFinPag.TabIndex = 8;
            this.btnFinPag.Text = "Fin";
            this.btnFinPag.UseVisualStyleBackColor = true;
            this.btnFinPag.Click += new System.EventHandler(this.btnFinPag_Click);
            // 
            // txtNombreParte
            // 
            this.txtNombreParte.Location = new System.Drawing.Point(29, 227);
            this.txtNombreParte.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombreParte.Name = "txtNombreParte";
            this.txtNombreParte.Size = new System.Drawing.Size(176, 20);
            this.txtNombreParte.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 209);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Nombre de la Parte:";
            // 
            // btnCrearPartes
            // 
            this.btnCrearPartes.Location = new System.Drawing.Point(29, 319);
            this.btnCrearPartes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCrearPartes.Name = "btnCrearPartes";
            this.btnCrearPartes.Size = new System.Drawing.Size(178, 19);
            this.btnCrearPartes.TabIndex = 11;
            this.btnCrearPartes.Text = "Crear Partes";
            this.btnCrearPartes.UseVisualStyleBackColor = true;
            this.btnCrearPartes.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBarPartes
            // 
            this.progressBarPartes.Location = new System.Drawing.Point(29, 360);
            this.progressBarPartes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressBarPartes.Name = "progressBarPartes";
            this.progressBarPartes.Size = new System.Drawing.Size(178, 19);
            this.progressBarPartes.TabIndex = 12;
            // 
            // btnRevision
            // 
            this.btnRevision.Location = new System.Drawing.Point(29, 139);
            this.btnRevision.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRevision.Name = "btnRevision";
            this.btnRevision.Size = new System.Drawing.Size(235, 19);
            this.btnRevision.TabIndex = 13;
            this.btnRevision.Text = "Establecer Estado En Revisión";
            this.btnRevision.UseVisualStyleBackColor = true;
            this.btnRevision.Click += new System.EventHandler(this.btnRevision_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 556);
            this.Controls.Add(this.btnRevision);
            this.Controls.Add(this.progressBarPartes);
            this.Controls.Add(this.btnCrearPartes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNombreParte);
            this.Controls.Add(this.btnFinPag);
            this.Controls.Add(this.btnInicioPag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPdf);
            this.Controls.Add(this.btnDescargar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pdfViewer1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pdfDocumentViewer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Spire.PdfViewer.Forms.PdfDocumentViewer pdfDocumentViewer1;
        private System.Windows.Forms.Button button1;
        private PdfiumViewer.PdfViewer pdfViewer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.TextBox txtPdf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnInicioPag;
        private System.Windows.Forms.Button btnFinPag;
        private System.Windows.Forms.TextBox txtNombreParte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCrearPartes;
        private System.Windows.Forms.ProgressBar progressBarPartes;
        private System.Windows.Forms.Button btnRevision;
    }
}

