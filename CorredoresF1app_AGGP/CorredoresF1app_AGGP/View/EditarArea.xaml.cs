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
using Newtonsoft.Json;
using System.Net.Http;


namespace CorredoresF1app_AGGP.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditarArea : ContentPage
	{
	
		

		public EditarArea(Area area)
		{
			InitializeComponent ();
			BindingContext = new VMeditarArea(Navigation, area);

        }
			
        private async void sw0_Toggled(object sender, ToggledEventArgs e)
        {
            bool isOn = sw0.IsToggled;

            string url = "http://aquasmartx.somee.com/api/Area/actualizar-status"; // Replace with your actual API URL
            var client = new HttpClient();

            AreaUpdateDTO updateDTO = new AreaUpdateDTO
            {
                id = "6619531d7c202c5b37e47a31", // Replace with the ID of the area you want to control
                Status = isOn
            };

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(updateDTO), Encoding.UTF8, "application/json");
                var response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // Show success message (optional)
                }
                else
                {
                    // Handle error (optional)
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (optional)
            }
        }
    }
}