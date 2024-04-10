using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CorredoresF1app_AGGP.Model;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Input;
using CorredoresF1app_AGGP.View;
using System.IO;
using Xamarin.Essentials;
using SkiaSharp;

namespace CorredoresF1app_AGGP.ViewModel
{
    public class VMCrearArea : BaseViewModel
    {
        #region VARIABLES
        private string _Nombre;
        private string _Imagen;
        private ElectroValvula _Valvula;
        private SensorHumedad _Sensor;

        ObservableCollection<ElectroValvula> _listaValvulas;
        ObservableCollection<SensorHumedad> _listaSensores;
        ObservableCollection<Area> _listaAreas;


        private bool _VerSensores;
        private bool _VerActuadores;
        #endregion

        #region CONSTRUCTOR
        public VMCrearArea(INavigation navigation)
        {
            Navigation = navigation;
            ObtenerSensores();
            ObtenerValvulas();
            VerSensores = false;
            VerActuadores = false;
        }
        #endregion

        #region OBJETOS
        public ObservableCollection<Area> ListaAreas
        {
            get { return _listaAreas; }
            set
            {
                _listaAreas = value;
                OnPropertyChanged();
            }
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

        public async Task InsertarArea()
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

                var requestUri = "http://www.aquasmart.somee.com/api/Area";

                var client = new HttpClient();
                var response = await client.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Mensaje", "Área insertada correctamente", "Ok");
                    MessagingCenter.Send(this, "ActualizarListaAreas");
                    await Volver();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Error al insertar el área: {response.StatusCode} - {errorMessage}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
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
                        await RedimensionarImagen(stream, ms, 854, 480); // Redimensionar a 480p
                        Imagen = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async Task RedimensionarImagen(Stream imagenOriginal, Stream imagenRedimensionada, int ancho, int alto)
        {
            try
            {
                using (SKBitmap bitmap = SKBitmap.Decode(imagenOriginal))
                {
                    int nuevoAncho, nuevoAlto;

                    if (bitmap.Width > bitmap.Height)
                    {
                        nuevoAncho = ancho;
                        nuevoAlto = bitmap.Height * ancho / bitmap.Width;
                    }
                    else
                    {
                        nuevoAlto = alto;
                        nuevoAncho = bitmap.Width * alto / bitmap.Height;
                    }

                    using (SKBitmap resizedBitmap = bitmap.Resize(new SKImageInfo(nuevoAncho, nuevoAlto), SKFilterQuality.High))
                    {
                        using (SKImage imageResized = SKImage.FromBitmap(resizedBitmap))
                        {
                            using (SKData data = imageResized.Encode())
                            {
                                using (Stream output = data.AsStream())
                                {
                                    await output.CopyToAsync(imagenRedimensionada);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al redimensionar la imagen: {ex.Message}");
            }
        }



        #endregion


        #region COMANDOS
        public ICommand SeleccionarImagenCommand => new Command(async () => await SeleccionarImagen());

        public ICommand MostrarSensoresCommand => new Command(MostrarSensores);
        public ICommand MostrarActuadoresCommand => new Command(MostrarActuadores);
        public ICommand Crearcommand => new Command(async () => await InsertarArea());

        #endregion
    }
}
