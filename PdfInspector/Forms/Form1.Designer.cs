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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.checkAuto = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelArchivos = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listViewPartes = new System.Windows.Forms.ListView();
            this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInicio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGroup = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbDel1 = new System.Windows.Forms.ToolStripButton();
            this.tsbDelAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.panelBotonera = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelPdf = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gdViewer1 = new GdPicture.GdViewer();
            this.timerNotificacion = new System.Windows.Forms.Timer(this.components);
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.inactividadTimer = new System.Windows.Forms.Timer(this.components);
            this.timerLabel = new System.Windows.Forms.Timer(this.components);
            this.infoDocControl = new PdfInspector.Controles.InfoDocumentoControl();
            this.lbTotalPag = new PdfInspector.Controles.InfoDocumentoControl();
            this.btnFin = new PdfInspector.Controles.BotonDocumento();
            this.btnComplete = new PdfInspector.Controles.BotonDocumento();
            this.btnCancel = new PdfInspector.Controles.BotonDocumento();
            this.btnSig = new PdfInspector.Controles.BotonDocumento();
            this.panelSuperior.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panelBotonera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panelPdf.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
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
            this.checkAuto.Location = new System.Drawing.Point(374, 0);
            this.checkAuto.Name = "checkAuto";
            this.checkAuto.Size = new System.Drawing.Size(109, 50);
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
            this.flowLayoutPanelArchivos.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelArchivos.Name = "flowLayoutPanelArchivos";
            this.flowLayoutPanelArchivos.Size = new System.Drawing.Size(481, 169);
            this.flowLayoutPanelArchivos.TabIndex = 1;
            this.flowLayoutPanelArchivos.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(481, 188);
            this.panel1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(481, 161);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewPartes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 135);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Identificacion";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listViewPartes
            // 
            this.listViewPartes.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listViewPartes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewPartes.CheckBoxes = true;
            this.listViewPartes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colId,
            this.colDoc,
            this.colInicio,
            this.colFin,
            this.colGroup});
            this.listViewPartes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewPartes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewPartes.FullRowSelect = true;
            this.listViewPartes.GridLines = true;
            this.listViewPartes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewPartes.HideSelection = false;
            this.listViewPartes.Location = new System.Drawing.Point(3, 3);
            this.listViewPartes.Name = "listViewPartes";
            this.listViewPartes.Size = new System.Drawing.Size(467, 129);
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
            // colGroup
            // 
            this.colGroup.Text = "Grupo";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(475, 127);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Estadísticas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep30) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep45) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(3, 3);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(469, 121);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDel1,
            this.tsbDelAll,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(481, 27);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbDel1
            // 
            this.tsbDel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDel1.Image = global::PdfInspector.Properties.Resources.delete_icon;
            this.tsbDel1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel1.Name = "tsbDel1";
            this.tsbDel1.Size = new System.Drawing.Size(24, 24);
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
            this.tsbDelAll.Size = new System.Drawing.Size(24, 24);
            this.tsbDelAll.Text = "toolStripButton2";
            this.tsbDelAll.ToolTipText = "Eliminar todos";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::PdfInspector.Properties.Resources.stats_chart_sharp_icon;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton1.Text = "Actualizar mis estadísticas";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::PdfInspector.Properties.Resources.Pictogrammers_Material_Link_variant_plus_48;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton2.Text = "Vincular documentos";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::PdfInspector.Properties.Resources.Pictogrammers_Material_Link_variant_remove_48;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton3.Text = "Eliminar Vinculos";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // panelBotonera
            // 
            this.panelBotonera.BackColor = System.Drawing.SystemColors.Window;
            this.panelBotonera.Controls.Add(this.splitContainer1);
            this.panelBotonera.Controls.Add(this.panelSuperior);
            this.panelBotonera.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBotonera.Location = new System.Drawing.Point(0, 0);
            this.panelBotonera.Name = "panelBotonera";
            this.panelBotonera.Size = new System.Drawing.Size(483, 614);
            this.panelBotonera.TabIndex = 8;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelArchivos);
            this.splitContainer1.Panel1MinSize = 2;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(483, 564);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 9;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.btnFin);
            this.splitContainer2.Panel1.Controls.Add(this.btnComplete);
            this.splitContainer2.Panel1.Controls.Add(this.btnCancel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Size = new System.Drawing.Size(483, 387);
            this.splitContainer2.SplitterDistance = 193;
            this.splitContainer2.TabIndex = 0;
            // 
            // panelPdf
            // 
            this.panelPdf.BackColor = System.Drawing.SystemColors.Control;
            this.panelPdf.Controls.Add(this.tableLayoutPanel1);
            this.panelPdf.Controls.Add(this.gdViewer1);
            this.panelPdf.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPdf.Location = new System.Drawing.Point(483, 0);
            this.panelPdf.Name = "panelPdf";
            this.panelPdf.Size = new System.Drawing.Size(601, 614);
            this.panelPdf.TabIndex = 7;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.infoDocControl, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalPag, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(601, 50);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // gdViewer1
            // 
            this.gdViewer1.AnimateGIF = false;
            this.gdViewer1.BackColor = System.Drawing.Color.Black;
            this.gdViewer1.BackgroundImage = null;
            this.gdViewer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gdViewer1.ContinuousViewMode = true;
            this.gdViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gdViewer1.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityBicubicHQ;
            this.gdViewer1.DisplayQualityAuto = false;
            this.gdViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdViewer1.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.gdViewer1.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.gdViewer1.EnabledProgressBar = true;
            this.gdViewer1.EnableMenu = true;
            this.gdViewer1.EnableMouseWheel = true;
            this.gdViewer1.ForceScrollBars = false;
            this.gdViewer1.ForceTemporaryModeForImage = false;
            this.gdViewer1.ForceTemporaryModeForPDF = false;
            this.gdViewer1.ForeColor = System.Drawing.Color.Black;
            this.gdViewer1.Gamma = 1F;
            this.gdViewer1.HQAnnotationRendering = true;
            this.gdViewer1.IgnoreDocumentResolution = false;
            this.gdViewer1.KeepDocumentPosition = false;
            this.gdViewer1.Location = new System.Drawing.Point(0, 0);
            this.gdViewer1.LockViewer = false;
            this.gdViewer1.MagnifierHeight = 90;
            this.gdViewer1.MagnifierWidth = 160;
            this.gdViewer1.MagnifierZoomX = 2F;
            this.gdViewer1.MagnifierZoomY = 2F;
            this.gdViewer1.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.gdViewer1.MouseMode = GdPicture.ViewerMouseMode.MouseModePan;
            this.gdViewer1.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.gdViewer1.Name = "gdViewer1";
            this.gdViewer1.OptimizeDrawingSpeed = false;
            this.gdViewer1.PdfDisplayFormField = true;
            this.gdViewer1.PdfEnableLinks = true;
            this.gdViewer1.PDFShowDialogForPassword = true;
            this.gdViewer1.RectBorderColor = System.Drawing.Color.Black;
            this.gdViewer1.RectBorderSize = 1;
            this.gdViewer1.RectIsEditable = true;
            this.gdViewer1.RegionsAreEditable = true;
            this.gdViewer1.ScrollBars = true;
            this.gdViewer1.ScrollLargeChange = ((short)(50));
            this.gdViewer1.ScrollSmallChange = ((short)(1));
            this.gdViewer1.SilentMode = true;
            this.gdViewer1.Size = new System.Drawing.Size(601, 614);
            this.gdViewer1.TabIndex = 0;
            this.gdViewer1.Zoom = 1D;
            this.gdViewer1.ZoomCenterAtMousePosition = false;
            this.gdViewer1.ZoomMode = GdPicture.ViewerZoomMode.ZoomMode100;
            this.gdViewer1.ZoomStep = 25;
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
            this.statusStrip2.Location = new System.Drawing.Point(0, 614);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1084, 22);
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(99, 17);
            this.statusLabel.Text = "Pdf Splitter V1.5.2";
            // 
            // inactividadTimer
            // 
            this.inactividadTimer.Interval = 3600000;
            this.inactividadTimer.Tick += new System.EventHandler(this.inactividadTimer_Tick);
            // 
            // timerLabel
            // 
            this.timerLabel.Interval = 5000;
            this.timerLabel.Tick += new System.EventHandler(this.timerLabel_Tick);
            // 
            // infoDocControl
            // 
            this.infoDocControl.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.infoDocControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.infoDocControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.infoDocControl.Location = new System.Drawing.Point(306, 6);
            this.infoDocControl.Margin = new System.Windows.Forms.Padding(6);
            this.infoDocControl.Name = "infoDocControl";
            this.infoDocControl.PaginaActual = 0;
            this.infoDocControl.PaginaInicio = 0;
            this.infoDocControl.Size = new System.Drawing.Size(289, 38);
            this.infoDocControl.TabIndex = 4;
            this.infoDocControl.TipoDocumento = "";
            this.infoDocControl.TotalPaginas = 0;
            // 
            // lbTotalPag
            // 
            this.lbTotalPag.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lbTotalPag.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbTotalPag.Location = new System.Drawing.Point(4, 4);
            this.lbTotalPag.Margin = new System.Windows.Forms.Padding(4);
            this.lbTotalPag.Name = "lbTotalPag";
            this.lbTotalPag.PaginaActual = 0;
            this.lbTotalPag.PaginaInicio = 0;
            this.lbTotalPag.Size = new System.Drawing.Size(292, 42);
            this.lbTotalPag.TabIndex = 5;
            this.lbTotalPag.TipoDocumento = null;
            this.lbTotalPag.TotalPaginas = 0;
            // 
            // btnFin
            // 
            this.btnFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFin.Location = new System.Drawing.Point(3, 6);
            this.btnFin.Margin = new System.Windows.Forms.Padding(6);
            this.btnFin.Name = "btnFin";
            this.btnFin.Size = new System.Drawing.Size(456, 72);
            this.btnFin.TabIndex = 0;
            this.btnFin.Tag = "Finalizar";
            this.btnFin.TeclaAtajo = System.Windows.Forms.Keys.Space;
            this.btnFin.Texto = "Documento Fin";
            // 
            // btnComplete
            // 
            this.btnComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComplete.Location = new System.Drawing.Point(8, 135);
            this.btnComplete.Margin = new System.Windows.Forms.Padding(6);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(451, 70);
            this.btnComplete.TabIndex = 2;
            this.btnComplete.Tag = "Completar";
            this.btnComplete.TeclaAtajo = System.Windows.Forms.Keys.None;
            this.btnComplete.Texto = "Completar Separación";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(8, 63);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(451, 65);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "Cancelar";
            this.btnCancel.TeclaAtajo = System.Windows.Forms.Keys.Escape;
            this.btnCancel.Texto = "Documento Cancelar";
            // 
            // btnSig
            // 
            this.btnSig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSig.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSig.Location = new System.Drawing.Point(0, 0);
            this.btnSig.Margin = new System.Windows.Forms.Padding(6);
            this.btnSig.Name = "btnSig";
            this.btnSig.Size = new System.Drawing.Size(374, 50);
            this.btnSig.TabIndex = 4;
            this.btnSig.Tag = "Siguiente";
            this.btnSig.TeclaAtajo = System.Windows.Forms.Keys.Tab;
            this.btnSig.Texto = "Siguiente";
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelBotonera.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panelPdf.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelArchivos;
        private Controles.BotonDocumento btnSig;
        private Controles.BotonDocumento btnFin;
        private Controles.BotonDocumento btnCancel;
        private Controles.BotonDocumento btnComplete;
        private System.Windows.Forms.Panel panelBotonera;
        private Controles.InfoDocumentoControl infoDocControl;
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
        private GdPicture.GdViewer gdViewer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controles.InfoDocumentoControl lbTotalPag;
        private System.Windows.Forms.Timer inactividadTimer;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ColumnHeader colGroup;
        private System.Windows.Forms.Timer timerLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
    }
}