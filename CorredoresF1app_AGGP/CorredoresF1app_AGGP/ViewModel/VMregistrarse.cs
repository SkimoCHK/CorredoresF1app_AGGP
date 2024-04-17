using CorredoresF1app_AGGP.Conexion;
using CorredoresF1app_AGGP.Model;
using CorredoresF1app_AGGP.View;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.ViewModel
{
    public class VMregistrarse : BaseViewModel
    {
        #region VARIABLES
        private string _email;
        private string _password;
        #endregion
        #region CONTRUCTOR
        public VMregistrarse(INavigation navigation)
        {
            Navigation = navigation;
            RegisterCommand = new Command(async () => await RegisterUsuario());
        }
        #endregion
        #region OBJETOS
        public string Email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetValue(ref _password, value); }
        }
        #endregion
        #region PROCESOS
        public async Task Login()
        {
            await Navigation.PushAsync(new Login());
        }
        public async Task IraLogin()
        {
            await Navigation.PushAsync(new Login());
        }
        public async Task RegisterUsuario()
        {
            var objusuario = new UserModel()
            {
                Email = Email,
                Password = Password,
            };

            try
            {
                var autenticacion = new FirebaseAuthProvider(new FirebaseConfig(DBconn.WepApyAuthentication));
                var resultado = await autenticacion.CreateUserWithEmailAndPasswordAsync(objusuario.Email, objusuario.Password);

                // Si llegamos aquí, el usuario se registró correctamente
                // Puedes manejar el flujo de tu aplicación según sea necesario, como mostrar una pantalla de confirmación o redirigir al usuario a otra página

                await App.Current.MainPage.DisplayAlert("Éxito", "¡Registro exitoso!", "Aceptar");
                IraLogin();
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante el registro
                await App.Current.MainPage.DisplayAlert("Error", $"Error durante el registro: {ex.Message}", "Aceptar");
            }
        }
        #endregion
        #region COMANDOS
        public Command RegisterCommand { get; }
        public ICommand Loginu => new Command(async () => await Login());
        public ICommand Logind => new Command(async () => await IraLogin());
        #endregion
    }
}
