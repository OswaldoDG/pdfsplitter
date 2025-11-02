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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.checkAuto = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelArchivos = new System.Windows.Forms.FlowLayoutPanel();
            this.panelControl = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewPartes = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDel1 = new System.Windows.Forms.ToolStripButton();
            this.tsbDelAll = new System.Windows.Forms.ToolStripButton();
            this.panelBotonera = new System.Windows.Forms.Panel();
            this.pdfVisor = new PdfiumViewer.PdfViewer();
            this.panelPdf = new System.Windows.Forms.Panel();
            this.timerNotificacion = new System.Windows.Forms.Timer(this.components);
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoDocControl = new PdfInspector.Controles.InfoDocumentoControl();
            this.btnSig = new PdfInspector.Controles.BotonDocumento();
            this.btnComplete = new PdfInspector.Controles.BotonDocumento();
            this.btnCancel = new PdfInspector.Controles.BotonDocumento();
            this.btnFin = new PdfInspector.Controles.BotonDocumento();
            this.panelSuperior.SuspendLayout();
            this.panelControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panelBotonera.SuspendLayout();
            this.panelPdf.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panelSuperior.Controls.Add(this.btnSig);
            this.panelSuperior.Controls.Add(this.checkAuto);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(483, 50);
            this.panelSuperior.TabIndex = 0;
            // 
            // checkAuto
            // 
            this.checkAuto.AutoSize = true;
            this.checkAuto.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkAuto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkAuto.Location = new System.Drawing.Point(351, 0);
            this.checkAuto.Name = "checkAuto";
            this.checkAuto.Size = new System.Drawing.Size(132, 50);
            this.checkAuto.TabIndex = 5;
            this.checkAuto.Text = "Automático";
            this.checkAuto.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelArchivos
            // 
            this.flowLayoutPanelArchivos.AutoScroll = true;
            this.flowLayoutPanelArchivos.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelArchivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelArchivos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelArchivos.Location = new System.Drawing.Point(0, 50);
            this.flowLayoutPanelArchivos.Name = "flowLayoutPanelArchivos";
            this.flowLayoutPanelArchivos.Size = new System.Drawing.Size(483, 220);
            this.flowLayoutPanelArchivos.TabIndex = 1;
            this.flowLayoutPanelArchivos.WrapContents = false;
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panelControl.Controls.Add(this.panel1);
            this.panelControl.Controls.Add(this.btnComplete);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Controls.Add(this.btnFin);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 270);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(483, 340);
            this.panelControl.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewPartes);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 180);
            this.panel1.TabIndex = 8;
            // 
            // listViewPartes
            // 
            this.listViewPartes.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listViewPartes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPartes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colDoc,
            this.colInicio,
            this.colFin});
            this.listViewPartes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPartes.FullRowSelect = true;
            this.listViewPartes.GridLines = true;
            this.listViewPartes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPartes.HideSelection = false;
            this.listViewPartes.Location = new System.Drawing.Point(0, 31);
            this.listViewPartes.Name = "listViewPartes";
            this.listViewPartes.Size = new System.Drawing.Size(483, 149);
            this.listViewPartes.TabIndex = 7;
            this.listViewPartes.UseCompatibleStateImageBehavior = false;
            this.listViewPartes.View = System.Windows.Forms.View.Details;
            // 
            // colId
            // 
            this.colId.Text = "#";
            this.colId.Width = 114;
            // 
            // colDoc
            // 
            this.colDoc.Text = "Tipo";
            this.colDoc.Width = 300;
            // 
            // colInicio
            // 
            this.colInicio.Text = "Inicio";
            this.colInicio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colInicio.Width = 150;
            // 
            // colFin
            // 
            this.colFin.Text = "Fin";
            this.colFin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colFin.Width = 150;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDel1,
            this.tsbDelAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(483, 31);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDel1
            // 
            this.tsbDel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDel1.Image = global::PdfInspector.Properties.Resources.delete_icon;
            this.tsbDel1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel1.Name = "tsbDel1";
            this.tsbDel1.Size = new System.Drawing.Size(29, 28);
            this.tsbDel1.Text = "toolStripButton1";
            this.tsbDel1.ToolTipText = "Eliminar 1";
            this.tsbDel1.Click += new System.EventHandler(this.tsbDel1_Click);
            // 
            // tsbDelAll
            // 
            this.tsbDelAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelAll.Image = global::PdfInspector.Properties.Resources.deleteall_icon;
            this.tsbDelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelAll.Name = "tsbDelAll";
            this.tsbDelAll.Size = new System.Drawing.Size(29, 28);
            this.tsbDelAll.Text = "toolStripButton2";
            this.tsbDelAll.ToolTipText = "Eliminar todos";
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
            this.panelBotonera.Size = new System.Drawing.Size(483, 610);
            this.panelBotonera.TabIndex = 8;
            // 
            // pdfVisor
            // 
            this.pdfVisor.BackColor = System.Drawing.SystemColors.Window;
            this.pdfVisor.DefaultPrintMode = PdfiumViewer.PdfPrintMode.ShrinkToMargin;
            this.pdfVisor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pdfVisor.Location = new System.Drawing.Point(0, 50);
            this.pdfVisor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pdfVisor.Name = "pdfVisor";
            this.pdfVisor.ShowBookmarks = false;
            this.pdfVisor.ShowToolbar = false;
            this.pdfVisor.Size = new System.Drawing.Size(601, 560);
            this.pdfVisor.TabIndex = 6;
            this.pdfVisor.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            // 
            // panelPdf
            // 
            this.panelPdf.BackColor = System.Drawing.SystemColors.Control;
            this.panelPdf.Controls.Add(this.pdfVisor);
            this.panelPdf.Controls.Add(this.infoDocControl);
            this.panelPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPdf.Location = new System.Drawing.Point(483, 0);
            this.panelPdf.Name = "panelPdf";
            this.panelPdf.Size = new System.Drawing.Size(601, 610);
            this.panelPdf.TabIndex = 7;
            // 
            // timerNotificacion
            // 
            this.timerNotificacion.Interval = 2500;
            this.timerNotificacion.Tick += new System.EventHandler(this.timerNotificacion_Tick);
            // 
            // statusStrip2
            // 
            this.statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip2.Location = new System.Drawing.Point(0, 610);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1084, 26);
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(114, 20);
            this.statusLabel.Text = "Pdf Splitter V1.0";
            // 
            // infoDocControl
            // 
            this.infoDocControl.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.infoDocControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoDocControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoDocControl.Location = new System.Drawing.Point(0, 0);
            this.infoDocControl.Margin = new System.Windows.Forms.Padding(6);
            this.infoDocControl.Name = "infoDocControl";
            this.infoDocControl.PaginaActual = 0;
            this.infoDocControl.PaginaInicio = 0;
            this.infoDocControl.Size = new System.Drawing.Size(601, 50);
            this.infoDocControl.TabIndex = 4;
            this.infoDocControl.TipoDocumento = "";
            // 
            // btnSig
            // 
            this.btnSig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSig.Location = new System.Drawing.Point(0, 0);
            this.btnSig.Margin = new System.Windows.Forms.Padding(6);
            this.btnSig.Name = "btnSig";
            this.btnSig.Size = new System.Drawing.Size(351, 50);
            this.btnSig.TabIndex = 4;
            this.btnSig.Tag = "Siguiente";
            this.btnSig.TeclaAtajo = System.Windows.Forms.Keys.Tab;
            this.btnSig.Texto = "Siguiente";
            // 
            // btnComplete
            // 
            this.btnComplete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.Location = new System.Drawing.Point(0, 100);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(6);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(483, 48);
            this.btnComplete.TabIndex = 2;
            this.btnComplete.Tag = "Completar";
            this.btnComplete.TeclaAtajo = System.Windows.Forms.Keys.Return;
            this.btnComplete.Texto = "Completar Separación";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(0, 50);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(483, 45);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "Cancelar";
            this.btnCancel.TeclaAtajo = System.Windows.Forms.Keys.Space;
            this.btnCancel.Texto = "Documento Cancelar";
            // 
            // btnFin
            // 
            this.btnFin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFin.Location = new System.Drawing.Point(0, 0);
            this.btnFin.Margin = new System.Windows.Forms.Padding(6);
            this.btnFin.Name = "btnFin";
            this.btnFin.Size = new System.Drawing.Size(483, 50);
            this.btnFin.TabIndex = 0;
            this.btnFin.Tag = "Finalizar";
            this.btnFin.TeclaAtajo = System.Windows.Forms.Keys.Space;
            this.btnFin.Texto = "Documento Fin";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1084, 636);
            this.Controls.Add(this.panelPdf);
            this.Controls.Add(this.panelBotonera);
            this.Controls.Add(this.statusStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDFSplitter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelBotonera.ResumeLayout(false);
            this.panelPdf.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ColumnHeader colId;
        private System.Windows.Forms.CheckBox checkAuto;
        private System.Windows.Forms.Timer timerNotificacion;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbDel1;
        private System.Windows.Forms.ToolStripButton tsbDelAll;
    }
}