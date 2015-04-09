using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mallenom.ScanNetwork.Core;

namespace Mallenom.ScanNetwork.Gui
{
	public sealed partial class MainForm : Form
	{
		private readonly BindingList<IpAddressData> _bindingList;
		private readonly IScanService _scanService;
		private readonly Progress<IpAddressData> _progress;
		private readonly ScanServiceConfigration _scanServiceConfigration;
        
		public MainForm()
		{
			InitializeComponent();

			Font = SystemFonts.MessageBoxFont;
			BackColor = SystemColors.Window;

			_scanServiceConfigration = new ScanServiceConfigration();
			_bindingList = new BindingList<IpAddressData>();
			_scanService = new ScanService(_scanServiceConfigration);
			_progress = new Progress<IpAddressData>();
			_progress.ProgressChanged += AddressAdded;



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
            
            
			//await _scanService.MethodAsync(_progress);
            
			var list = await _scanService.ScanNetworkAsync();

			if(list == null) return;
			
			foreach(var ipAddressData in list)
			{
				_bindingList.Add(ipAddressData);
			}
		}
	}
}
