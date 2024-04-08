using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using CorredoresF1app_AGGP.Model;
using System.Text;
using System.Threading.Tasks;
using CorredoresF1app_AGGP.View;
using CorredoresF1app_AGGP.ViewModel;
using System.Windows.Input;
using Xamarin.Forms;


namespace CorredoresF1app_AGGP.ViewModel
{
    class VMHistorialRiego : BaseViewModel
    {
        #region VARIABLES
        private ObservableCollection<RiegoEvent> _historial;
        public Area _area {  get; set; }
        #endregion

        #region CONSTRUCTOR
        public VMHistorialRiego(INavigation navigation, Area area)
        {
            Navigation = navigation;
            _area = area;
            ObtenerHistorial();
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<RiegoEvent> Historial
        {
            get { return _historial; }
            set 
            { 
                SetValue(ref _historial, value);
                OnpropertyChanged();
            }
            
        }
        #endregion

        #region PROCESOS
        public async Task ObtenerHistorial()
        {
            Uri RequestUri = new Uri($"http://www.aquasmart.somee.com/api/Area/obtener-historial{_area.id}");
            var client = new HttpClient();
            var response = await client.GetAsync(RequestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Historial = JsonConvert.DeserializeObject<ObservableCollection<RiegoEvent>>(content);
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al cargar el historial", "Ok");
            }
        }

        #endregion

        #region COMANDOS

        #endregion
    }
}
