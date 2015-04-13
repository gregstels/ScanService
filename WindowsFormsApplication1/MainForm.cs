using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using Mallenom.ScanNetwork.Core;

namespace Mallenom.ScanNetwork.Gui
{
	public sealed partial class MainForm : Form
	{
		private readonly BindingList<IpAddressData> _bindingList;
		private readonly Progress<IpAddressData> _progress;
		private readonly ScanServiceConfigration _scanServiceConfigration;
		public MainForm()
		{
			InitializeComponent();

			Font = SystemFonts.MessageBoxFont;
			BackColor = SystemColors.Window;

			_scanServiceConfigration = new ScanServiceConfigration();
			_bindingList = new BindingList<IpAddressData>();
			_progress = new Progress<IpAddressData>();
			_progress.ProgressChanged += AddressAdded;

			_txtMimimum.Text = _scanServiceConfigration.Minimum.ToString();
			_txtMaximum.Text = _scanServiceConfigration.Maximum.ToString();

			dataGridView1.DataSource = _bindingList;
		}

		private void AddressAdded(object sender, IpAddressData e)
		{
			_bindingList.Add(e);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize = Size;
		}

		protected override void OnClosed(EventArgs e)
		{
			_progress.ProgressChanged -= AddressAdded;

			base.OnClosed(e);
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

				if(list == null) return;

				foreach(var ipAddressData in list)
				{
					_bindingList.Add(ipAddressData);
				}
			}
		}
	}
}
