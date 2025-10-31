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
            this.components = new System.ComponentModel.Container();
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.checkAuto = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelArchivos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.panelBotonera = new System.Windows.Forms.Panel();
            this.pdfVisor = new PdfiumViewer.PdfViewer();
            this.listViewPartes = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelPdf = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNotificacion = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEliminarTabla = new System.Windows.Forms.Button();
            this.timerNotificacion = new System.Windows.Forms.Timer(this.components);
            this.infoDocControl = new PdfInspector.Controles.InfoDocumentoControl();
            this.btnSig = new PdfInspector.Controles.BotonDocumento();
            this.btnComplete = new PdfInspector.Controles.BotonDocumento();
            this.btnCancel = new PdfInspector.Controles.BotonDocumento();
            this.btnFin = new PdfInspector.Controles.BotonDocumento();
            this.panelSuperior.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.panelBotonera.SuspendLayout();
            this.panelPdf.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panelSuperior.Controls.Add(this.checkAuto);
            this.panelSuperior.Controls.Add(this.btnSig);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(543, 50);
            this.panelSuperior.TabIndex = 0;
            // 
            // checkAuto
            // 
            this.checkAuto.AutoSize = true;
            this.checkAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAuto.Location = new System.Drawing.Point(384, 12);
            this.checkAuto.Name = "checkAuto";
            this.checkAuto.Size = new System.Drawing.Size(152, 28);
            this.checkAuto.TabIndex = 5;
            this.checkAuto.Text = "Auto Siguiente";
            this.checkAuto.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelArchivos
            // 
            this.flowLayoutPanelArchivos.AutoScroll = true;
            this.flowLayoutPanelArchivos.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.flowLayoutPanelArchivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelArchivos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelArchivos.Location = new System.Drawing.Point(0, 50);
            this.flowLayoutPanelArchivos.Name = "flowLayoutPanelArchivos";
            this.flowLayoutPanelArchivos.Size = new System.Drawing.Size(543, 533);
            this.flowLayoutPanelArchivos.TabIndex = 1;
            this.flowLayoutPanelArchivos.WrapContents = false;
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panelControl.Controls.Add(this.btnComplete);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Controls.Add(this.btnFin);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 583);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(543, 390);
            this.panelControl.TabIndex = 3;
            // 
            // panelBotonera
            // 
            this.panelBotonera.BackColor = System.Drawing.SystemColors.Window;
            this.panelBotonera.Controls.Add(this.flowLayoutPanelArchivos);
            this.panelBotonera.Controls.Add(this.panelSuperior);
            this.panelBotonera.Controls.Add(this.panelControl);
            this.panelBotonera.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBotonera.Location = new System.Drawing.Point(0, 0);
            this.panelBotonera.Name = "panelBotonera";
            this.panelBotonera.Size = new System.Drawing.Size(543, 973);
            this.panelBotonera.TabIndex = 8;
            // 
            // pdfVisor
            // 
            this.pdfVisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfVisor.Location = new System.Drawing.Point(0, 0);
            this.pdfVisor.Name = "pdfVisor";
            this.pdfVisor.Size = new System.Drawing.Size(973, 973);
            this.pdfVisor.TabIndex = 6;
            // 
            // listViewPartes
            // 
            this.listViewPartes.AllowColumnReorder = true;
            this.listViewPartes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colDoc,
            this.colInicio,
            this.colFin});
            this.listViewPartes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPartes.FullRowSelect = true;
            this.listViewPartes.GridLines = true;
            this.listViewPartes.HideSelection = false;
            this.listViewPartes.Location = new System.Drawing.Point(3, 3);
            this.listViewPartes.Name = "listViewPartes";
            this.listViewPartes.Size = new System.Drawing.Size(721, 116);
            this.listViewPartes.TabIndex = 7;
            this.listViewPartes.UseCompatibleStateImageBehavior = false;
            this.listViewPartes.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "No";
            this.colId.Width = 114;
            // 
            // colDoc
            // 
            this.colDoc.Text = "Tipo Documento";
            this.colDoc.Width = 300;
            // 
            // colInicio
            // 
            this.colInicio.Text = "Pag Inicio";
            this.colInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colInicio.Width = 150;
            // 
            // colFin
            // 
            this.colFin.Text = "Pag Fin";
            this.colFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFin.Width = 150;
            // 
            // panelPdf
            // 
            this.panelPdf.BackColor = System.Drawing.SystemColors.Window;
            this.panelPdf.Controls.Add(this.tableLayoutPanel2);
            this.panelPdf.Controls.Add(this.tableLayoutPanel1);
            this.panelPdf.Controls.Add(this.pdfVisor);
            this.panelPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPdf.Location = new System.Drawing.Point(543, 0);
            this.panelPdf.Name = "panelPdf";
            this.panelPdf.Size = new System.Drawing.Size(973, 973);
            this.panelPdf.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.infoDocControl, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblNotificacion, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(973, 50);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // lblNotificacion
            // 
            this.lblNotificacion.AutoSize = true;
            this.lblNotificacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotificacion.Location = new System.Drawing.Point(489, 0);
            this.lblNotificacion.Name = "lblNotificacion";
            this.lblNotificacion.Size = new System.Drawing.Size(481, 50);
            this.lblNotificacion.TabIndex = 5;
            this.lblNotificacion.Text = "label1";
            this.lblNotificacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNotificacion.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.82014F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.17986F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.listViewPartes, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEliminarTabla, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 851);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(973, 122);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // btnEliminarTabla
            // 
            this.btnEliminarTabla.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEliminarTabla.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarTabla.Location = new System.Drawing.Point(767, 40);
            this.btnEliminarTabla.Name = "btnEliminarTabla";
            this.btnEliminarTabla.Size = new System.Drawing.Size(166, 42);
            this.btnEliminarTabla.TabIndex = 8;
            this.btnEliminarTabla.Text = "Eliminar Documeno";
            this.btnEliminarTabla.UseVisualStyleBackColor = true;
            this.btnEliminarTabla.Click += new System.EventHandler(this.btnEliminarTabla_Click);
            // 
            // timerNotificacion
            // 
            this.timerNotificacion.Interval = 2500;
            this.timerNotificacion.Tick += new System.EventHandler(this.timerNotificacion_Tick);
            // 
            // infoDocControl
            // 
            this.infoDocControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoDocControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoDocControl.Location = new System.Drawing.Point(4, 3);
            this.infoDocControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.infoDocControl.Name = "infoDocControl";
            this.infoDocControl.PaginaActual = 0;
            this.infoDocControl.PaginaInicio = 0;
            this.infoDocControl.Size = new System.Drawing.Size(478, 44);
            this.infoDocControl.TabIndex = 4;
            this.infoDocControl.TipoDocumento = "";
            // 
            // btnSig
            // 
            this.btnSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSig.Location = new System.Drawing.Point(3, 0);
            this.btnSig.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSig.Name = "btnSig";
            this.btnSig.Size = new System.Drawing.Size(525, 50);
            this.btnSig.TabIndex = 4;
            this.btnSig.Tag = "Siguiente";
            this.btnSig.TeclaAtajo = System.Windows.Forms.Keys.Tab;
            this.btnSig.Texto = "Siguiente";
            // 
            // btnComplete
            // 
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.Location = new System.Drawing.Point(0, 330);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(528, 48);
            this.btnComplete.TabIndex = 2;
            this.btnComplete.Tag = "Completar";
            this.btnComplete.TeclaAtajo = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
            this.btnComplete.Texto = "Completar";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(4, 268);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(524, 45);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "Cancelar";
            this.btnCancel.TeclaAtajo = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Space)));
            this.btnCancel.Texto = "Cancelar";
            // 
            // btnFin
            // 
            this.btnFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFin.Location = new System.Drawing.Point(4, 194);
            this.btnFin.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnFin.Name = "btnFin";
            this.btnFin.Size = new System.Drawing.Size(524, 43);
            this.btnFin.TabIndex = 0;
            this.btnFin.Tag = "Finalizar";
            this.btnFin.TeclaAtajo = System.Windows.Forms.Keys.Space;
            this.btnFin.Texto = "Fin Documento";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1516, 973);
            this.Controls.Add(this.panelPdf);
            this.Controls.Add(this.panelBotonera);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visor de Documentos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelControl.ResumeLayout(false);
            this.panelBotonera.ResumeLayout(false);
            this.panelPdf.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelArchivos;
        private System.Windows.Forms.Panel panelControl;
        private Controles.BotonDocumento btnSig;
        private Controles.BotonDocumento btnFin;
        private Controles.BotonDocumento btnCancel;
        private Controles.BotonDocumento btnComplete;
        private System.Windows.Forms.Panel panelBotonera;
        private Controles.InfoDocumentoControl infoDocControl;
        private PdfiumViewer.PdfViewer pdfVisor;
        private System.Windows.Forms.ListView listViewPartes;
        private System.Windows.Forms.Panel panelPdf;
        private System.Windows.Forms.ColumnHeader colDoc;
        private System.Windows.Forms.ColumnHeader colInicio;
        private System.Windows.Forms.ColumnHeader colFin;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnEliminarTabla;
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.CheckBox checkAuto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblNotificacion;
        private System.Windows.Forms.Timer timerNotificacion;
    }
}