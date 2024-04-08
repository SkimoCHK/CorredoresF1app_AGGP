using System;
using CorredoresF1app_AGGP.Model;
using CorredoresF1app_AGGP.View;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace CorredoresF1app_AGGP.ViewModel
{
    internal class VMReportes : BaseViewModel
    {
        #region VARIABLES
        private double _porcentaje;
        private ObservableCollection<Area> _listaAreas;
        #endregion

        #region CONSTRUCTOR
        public VMReportes(INavigation navigation) 
        {
            Navigation = navigation;
            ObtenerPorcentaje();
            ObtenerLista();
        }
        #endregion

        #region OBJETOS
        public double Porcentaje
        {
            get { return _porcentaje; }
            set { SetValue(ref _porcentaje, value); }

        }
        public ObservableCollection<Area> ListaAreas
        {
            get { return _listaAreas; }
            set
            {
                SetValue(ref _listaAreas, value);
                OnpropertyChanged();
            }

        }
        #endregion

        #region PROCESOS
        public async Task ObtenerPorcentaje()
        {
            Uri RequestUri = new Uri("http://www.aquasmart.somee.com/api/ElectroValvula/status");
            var client = new HttpClient();
            var response = await client.GetAsync(RequestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Porcentaje = JsonConvert.DeserializeObject<double>(content);
            }
            else
            {
                await DisplayAlert("Mensaje", "Eror al obtener el porcentaje", "Ok");
            }
            
        }

        public async Task ObtenerLista()
        {
            //http://www.aquasmart.somee.com/api/Area
            Uri RequestUri = new Uri("http://www.aquasmart.somee.com/api/Area/obtener-areas");
            var client = new HttpClient();
            var response = await client.GetAsync(RequestUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListaAreas = JsonConvert.DeserializeObject<ObservableCollection<Area>>(content);
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al cargar la lista de areas", "Ok");
            }
        }
        #endregion

        #region COMANDOS
        public ICommand ActualizarCommand => new Command(async() => await ObtenerPorcentaje());

        #endregion
    }
}
