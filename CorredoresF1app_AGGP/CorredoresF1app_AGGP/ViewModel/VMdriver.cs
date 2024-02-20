using CorredoresF1app_AGGP.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace CorredoresF1app_AGGP.ViewModel
{
    public class VMdriver : BaseViewModel
    {
        #region VARIABLES
        private string _Nombre;
        private int _Number;
        private string _Team;
        #endregion

        #region CONSTRUCTOR
        public VMdriver(INavigation navigation) 
        { 
            Navigation = navigation;
        }
        #endregion

        #region OBJETOS
        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }
        public int Numero
        {
            get { return _Number; }
            set { SetValue(ref _Number, value); }
        }
        public string Equipo
        {
            get { return _Team; }
            set { SetValue(ref _Team, value); }
        }

        #endregion

        #region PROCESOS
        public async Task Insertar()
        {
            var driverModel = new DriverModel
            {
                Name = Nombre,
                Number = Numero,
                Team = Equipo
            };
            Uri RequestUri = new Uri("http://www.alexmarindinastia.somee.com/api/drivers");

            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(driverModel);
            var contentJson = new StringContent(json, Encoding.UTF8, "aplication/json");
            var response = await client.PostAsync(RequestUri, contentJson);
            if(response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Mensaje", "Registrado", "Ok");    
            }
            else
            {
                await DisplayAlert("Mensaje", "No se registro", "Ok");
            }
        }
        #endregion

        #region COMANDOS
        public ICommand InsertarCorredor => new Command(async () => await Insertar());
        #endregion
    }
}
