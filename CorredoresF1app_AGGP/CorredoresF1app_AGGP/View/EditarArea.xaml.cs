using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorredoresF1app_AGGP.ViewModel;
using CorredoresF1app_AGGP.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace CorredoresF1app_AGGP.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarArea : ContentPage
	{
		string server;
		Int32 port;
		TcpClient tcpClient;
		Byte[] bytes;
		NetworkStream stream;
		string entrada = "";
		bool conectando= false;
		bool conectado = false;
		

		public EditarArea(Area area)
		{
			InitializeComponent ();
			BindingContext = new VMeditarArea(Navigation, area);

			server = "192.168.137.112";
			port = 8888;

			sw0.IsEnabled = false;

			Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
			{
				if (conectando == false)
				{
					_ = IniciarConecte();
				}
				if (conectado == true)
				{
					sw0.IsEnabled = true;
				}

				return true;
			});



        }

		public async Task IniciarConecte()
		{
			lblStatus.Text = "Conectando...";
			tcpClient = new TcpClient();
			conectando = true;
			await tcpClient.ConnectAsync(server,port);
			stream = tcpClient.GetStream();
			lblStatus.Text = "Conectado";
		}
		int a = 0;
		string message;
        private void sw0_Toggled(object sender, ToggledEventArgs e)
        {
			if (sw0.IsToggled)
			{
				a = 1;
			}
			else
			{
				a = 0;
			}
			message = "0" + a + Environment.NewLine;
			Console.WriteLine("Valvula:" + message);

			bytes = System.Text.Encoding.ASCII.GetBytes(message);
			stream.Write(bytes, 0, bytes.Length);
        }
    }
}