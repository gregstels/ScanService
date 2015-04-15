using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Mallenom.ScanNetwork.Core;

namespace Mallenom.ScanNetwork.Gui
{
	public sealed partial class MainForm : Form
	{
		private readonly BindingList<CameraData> _bindingList;
		private readonly ScanServiceConfigration _scanServiceConfigration;
		public MainForm()
		{
			InitializeComponent();

			Font = SystemFonts.MessageBoxFont;
			BackColor = SystemColors.Window;

			_scanServiceConfigration = new ScanServiceConfigration();
			_bindingList = new BindingList<CameraData>();


			_txtMimimum.Text = _scanServiceConfigration.Minimum.ToString();
			_txtMaximum.Text = _scanServiceConfigration.Maximum.ToString();

			_dataGridCameras.AutoGenerateColumns = false;
			_dataGridCameras.DataSource = _bindingList;
			_dataGridCameras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize = Size;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			_bindingList.Clear();

			IPAddress minimum;
			IPAddress maximum;

			if(!IPAddress.TryParse(_txtMimimum.Text, out minimum))
			{
				MessageBox.Show(
					this,
					@"Неправильно указан минимальный адрес.",
					@"Scan Service",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				return;
			}

			if(!IPAddress.TryParse(_txtMaximum.Text, out maximum))
			{
				MessageBox.Show(
					this,
					@"Неправильно указан максимальный адрес.",
					@"Scan Service",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);

				return;
			}

			_scanServiceConfigration.Minimum = minimum;
			_scanServiceConfigration.Maximum = maximum;
		   
			using(var scanner = new ScanService(_scanServiceConfigration))
			{
				var list = await scanner.ScanNetworkAsync();

				if(list == null || list.Count == 0)
				{
					MessageBox.Show(
							this,
							@"Видеокамеры не найдены.",
							@"Scan Service",
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
					return;
				}

				foreach(var ipAddressData in list)
				{
					if(ipAddressData == null)
					{
						return;
					}
					 
					_bindingList.Add(ipAddressData);
				}
			}
		}

		private void _btnOpen_Click(object sender, EventArgs e)
		{
			if(_dataGridCameras.SelectedRows.Count > 0)
			{
				var ipAddress = _dataGridCameras.SelectedRows[0].DataBoundItem as CameraData;

				if(ipAddress == null)
				{
					return;
				}

				var builder = new StringBuilder();
				builder.AppendFormat("http://{0}/", ipAddress.Address);
				var uri = new Uri(builder.ToString());

				Process.Start(uri.ToString());
			}
		}
	}
}
