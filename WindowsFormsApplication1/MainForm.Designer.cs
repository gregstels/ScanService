namespace Mallenom.ScanNetwork.Gui
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this._dataGridCameras = new System.Windows.Forms.DataGridView();
			this._btnScan = new System.Windows.Forms.Button();
			this._toolTip = new System.Windows.Forms.ToolTip(this.components);
			this._txtMimimum = new System.Windows.Forms.TextBox();
			this._txtMaximum = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Macadress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CameraName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._btnOpen = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this._dataGridCameras)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// _dataGridCameras
			// 
			this._dataGridCameras.AllowUserToAddRows = false;
			this._dataGridCameras.AllowUserToDeleteRows = false;
			this._dataGridCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._dataGridCameras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._dataGridCameras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Port,
            this.Macadress,
            this.CameraName});
			this._dataGridCameras.Location = new System.Drawing.Point(0, 71);
			this._dataGridCameras.Name = "_dataGridCameras";
			this._dataGridCameras.ReadOnly = true;
			this._dataGridCameras.Size = new System.Drawing.Size(503, 152);
			this._dataGridCameras.TabIndex = 0;
			// 
			// _btnScan
			// 
			this._btnScan.Location = new System.Drawing.Point(12, 19);
			this._btnScan.Name = "_btnScan";
			this._btnScan.Size = new System.Drawing.Size(102, 42);
			this._btnScan.TabIndex = 1;
			this._btnScan.Text = "Сканировать";
			this._toolTip.SetToolTip(this._btnScan, "Сканировать сеть");
			this._btnScan.UseVisualStyleBackColor = true;
			this._btnScan.Click += new System.EventHandler(this.button1_Click);
			// 
			// _txtMimimum
			// 
			this._txtMimimum.Location = new System.Drawing.Point(6, 19);
			this._txtMimimum.Name = "_txtMimimum";
			this._txtMimimum.Size = new System.Drawing.Size(100, 20);
			this._txtMimimum.TabIndex = 5;
			// 
			// _txtMaximum
			// 
			this._txtMaximum.Location = new System.Drawing.Point(128, 19);
			this._txtMaximum.Name = "_txtMaximum";
			this._txtMaximum.Size = new System.Drawing.Size(100, 20);
			this._txtMaximum.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(112, 22);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(10, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "-";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this._txtMimimum);
			this.groupBox2.Controls.Add(this._txtMaximum);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Location = new System.Drawing.Point(120, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(243, 53);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Диапазон ip";
			// 
			// Address
			// 
			this.Address.DataPropertyName = "Address";
			this.Address.HeaderText = "Адрес";
			this.Address.Name = "Address";
			this.Address.ReadOnly = true;
			// 
			// Port
			// 
			this.Port.DataPropertyName = "Port";
			this.Port.HeaderText = "Порт";
			this.Port.Name = "Port";
			this.Port.ReadOnly = true;
			this.Port.Width = 40;
			// 
			// Macadress
			// 
			this.Macadress.DataPropertyName = "PhysicalAddress";
			this.Macadress.HeaderText = "MAC адрес";
			this.Macadress.Name = "Macadress";
			this.Macadress.ReadOnly = true;
			this.Macadress.Width = 120;
			// 
			// CameraName
			// 
			this.CameraName.DataPropertyName = "CameraName";
			this.CameraName.HeaderText = "Имя камеры";
			this.CameraName.Name = "CameraName";
			this.CameraName.ReadOnly = true;
			this.CameraName.Width = 200;
			// 
			// _btnOpen
			// 
			this._btnOpen.Location = new System.Drawing.Point(369, 19);
			this._btnOpen.Name = "_btnOpen";
			this._btnOpen.Size = new System.Drawing.Size(102, 42);
			this._btnOpen.TabIndex = 11;
			this._btnOpen.Text = "Открыть в браузере";
			this._toolTip.SetToolTip(this._btnOpen, "Сканировать сеть");
			this._btnOpen.UseVisualStyleBackColor = true;
			this._btnOpen.Click += new System.EventHandler(this._btnOpen_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(503, 223);
			this.Controls.Add(this._btnOpen);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this._btnScan);
			this.Controls.Add(this._dataGridCameras);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Scan Service";
			((System.ComponentModel.ISupportInitialize)(this._dataGridCameras)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _dataGridCameras;
		private System.Windows.Forms.Button _btnScan;
		private System.Windows.Forms.ToolTip _toolTip;
        private System.Windows.Forms.TextBox _txtMimimum;
        private System.Windows.Forms.TextBox _txtMaximum;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn Port;
		private System.Windows.Forms.DataGridViewTextBoxColumn Macadress;
		private System.Windows.Forms.DataGridViewTextBoxColumn CameraName;
		private System.Windows.Forms.Button _btnOpen;
	}
}

