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
    public class AreasViewModel : BaseViewModel
    {

        #region VARIABLES
        private ObservableCollection<Area> _listaAreas;
        private bool boleanoxd = true;
        #endregion

        #region CONSTRUCTOR
        public AreasViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ObtenerLista();

            MessagingCenter.Subscribe<VMCrearArea>(this, "ActualizarListaAreas", async (sender) =>
            {
                await ObtenerLista();
            });

            MessagingCenter.Subscribe<VMeditarArea>(this, "ActualizarListaAreas", async (sender) =>
            {
                await ObtenerLista();
            });
        }
        #endregion

        #region OBJETOS
        public bool Booleanoxdd
        {
            get { return boleanoxd; } 
            set { SetValue(ref boleanoxd, value); }
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
        public async Task ObtenerLista()
        {
            //http://www.aquasmart.somee.com/api/Area
            Uri RequestUri = new Uri("http://www.aquasmartt.somee.com/api/Area");
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
        public void ProcesoSimple()
        {
            
        }
        public async Task CrearAreas()
        {
            await Navigation.PushAsync(new CrearArea());
        }

        public async Task EditarArea(Area area)
        {
            await Navigation.PushAsync(new EditarArea(area));
        }

        #endregion

        #region COMANDOS
        public ICommand AgregarJardinCommand => new Command(async () => await CrearAreas());
        public ICommand VerDetalleCommand => new Command<Area>(async (a) => await EditarArea(a));
        #endregion

    }
}
