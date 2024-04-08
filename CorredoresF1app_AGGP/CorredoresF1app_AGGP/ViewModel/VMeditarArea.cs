using CorredoresF1app_AGGP.Model;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using CorredoresF1app_AGGP.View;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.ViewModel
{
    class VMeditarArea : BaseViewModel
    {

        #region VARIABLES
        private string _Id;
        private string _Nombre;
        private string _Imagen;
        private ElectroValvula _Valvula;
        private SensorHumedad _Sensor;
        ObservableCollection<ElectroValvula> _listaValvulas;
        ObservableCollection<SensorHumedad> _listaSensores;
        public Area _Area { get; set; }

        private bool _VerSensores;
        private bool _VerActuadores;
        #endregion

        #region CONSTRUCTOR
        public VMeditarArea(INavigation navigation, Area area)
        {
            Navigation = navigation;
            _Area = area;
            VerSensores = false;
            VerActuadores = false;
            CargarDatosArea(area);
        }
        #endregion

        #region OBJETOS

        public string Id
        {
            get { return _Id; }
            set { SetValue(ref _Id, value); }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { SetValue(ref _Nombre, value); }
        }
        public string Imagen
        {
            get { return _Imagen; }
            set { SetValue(ref _Imagen, value); }
        }
        public ElectroValvula Valvula
        {
            get { return _Valvula; }
            set { SetValue(ref _Valvula, value); }
        }
        public SensorHumedad Sensor
        {
            get { return _Sensor; }
            set { SetValue(ref _Sensor, value); }
        }

        public ObservableCollection<ElectroValvula> ListaValvulas
        {
            get { return _listaValvulas; }
            set
            {
                SetValue(ref _listaValvulas, value);
                OnpropertyChanged();
            }
        }

        public ObservableCollection<SensorHumedad> ListaSensores
        {
            get { return _listaSensores; }
            set
            {
                SetValue(ref _listaSensores, value);
                OnpropertyChanged();
            }
        }
        public bool VerSensores
        {
            get => _VerSensores;
            set
            {
                _VerSensores = value;
                OnPropertyChanged();
            }
        }
        public bool VerActuadores
        {
            get => _VerActuadores;
            set
            {
                _VerActuadores = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PROCESOS
        public async Task ObtenerValvulas()
        {
            Uri RequestUri = new Uri("http://www.aquasmart.somee.com/api/ElectroValvula");
            var client = new HttpClient();
            var response = await client.GetAsync(RequestUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListaValvulas = JsonConvert.DeserializeObject<ObservableCollection<ElectroValvula>>(content);
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al cargar la lista de areas", "Ok");
            }
        }
        public async Task ObtenerSensores()
        {
            Uri RequestUri = new Uri("http://www.aquasmart.somee.com/api/SensorHumedad");
            var client = new HttpClient();
            var response = await client.GetAsync(RequestUri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListaSensores = JsonConvert.DeserializeObject<ObservableCollection<SensorHumedad>>(content);
            }
            else
            {
                await DisplayAlert("Mensaje", "Error al cargar la lista de areas", "Ok");
            }
        }

        private void CargarDatosArea(Area area)
        {
            Id = area.id;
            Nombre = area.Nombre;
            Imagen = area.Imagen;
            Valvula = area.electroValvula;
            Sensor = area.sensorHumedad;
            VerSensores = false;
            VerActuadores = false;
            ObtenerValvulas();
            ObtenerSensores();
        }
        private async Task SeleccionarImagen()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Seleccionar imagen"
                });

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        Imagen = Convert.ToBase64String(ms.ToArray());

                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task EditarArea()
        {
       
            try
            {

                AreaDTO nuevaArea = new AreaDTO()
                {
                    Nombre = Nombre,
                    Imagen = Imagen,
                    refSensor = Sensor.Id,
                    refValvula = Valvula.Id,
                };


                var json = JsonConvert.SerializeObject(nuevaArea);
                var content = new StringContent(json, Encoding.UTF8, "application/json");


                var requestUri = "http://www.aquasmart.somee.com/api/Area/" + Id;


                var client = new HttpClient();
                var response = await client.PutAsync(requestUri, content);


                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Mensaje", "Área actualizada correctamente", "Ok");
                    MessagingCenter.Send(this, "ActualizarListaAreas");
                    await Volver();
                }
                else
                {
                    await DisplayAlert("Mensaje", "Error al actualizar el área", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }
        public async Task Eliminar()
        {
            try
            {
                var confirmarEliminar = await DisplayAlert("Confirmar", "¿Estás seguro de que deseas eliminar esta área?", "Sí", "No");
                if (confirmarEliminar)
                {
                    var requestUri = "http://www.aquasmart.somee.com/api/Area/" + Id;
                    var client = new HttpClient();
                    var response = await client.DeleteAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Mensaje", "Área eliminada correctamente", "Ok");
                        MessagingCenter.Send(this, "ActualizarListaAreas");
                        await Volver();
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Error al eliminar el área", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("MENSAJE", "No estes chingando entonces pendejo", "vales verga");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", $"error al eliminar{ex.Message}", "Ok");
            } 
        }


        private void MostrarSensores()
        {
            VerSensores = !VerSensores;
        }
        private void MostrarActuadores()
        {
            VerActuadores = !VerActuadores;
        }

        private async Task Volver()
        {
            Navigation.PopAsync();
        }

        public async Task Historial(Area area)
        {
            await Navigation.PushAsync(new HistorialRiego(_Area));
        }


        #endregion


        #region COMANDOS
        public ICommand SeleccionarImagenCommand => new Command(async () => await SeleccionarImagen());
        public ICommand MostrarSensoresCommand => new Command(MostrarSensores);
        public ICommand MostrarActuadoresCommand => new Command(MostrarActuadores);
        public ICommand Crearcommand => new Command(async () => await EditarArea());
        public ICommand Historialcommand => new Command<Area>(async (h) => await Historial(h));
        public ICommand Deletecommand => new Command(async () => await Eliminar());

        #endregion

    }
}
