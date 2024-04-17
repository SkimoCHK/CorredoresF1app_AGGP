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
    public class VMlogin : BaseViewModel
    {
        #region VARIABLES
        private string _email;
        private string _password;
        #endregion
        #region CONTRUCTOR
        public VMlogin(INavigation navigation)
        {
            Navigation = navigation;
            LoginCommand = new Command(async () => await LoginUsuario());

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
        public async Task IraMenu()
        {
            await Navigation.PushAsync(new MenuHamburguesa());
        }
        public async Task IraRegistro()
        {
            await Navigation.PushAsync(new Registrarse());
        }
        public async Task ContraOlvidada()
        {
            await Navigation.PushAsync(new ContraseñaOlvidada());
        }
        public async Task LoginUsuario()
        {
            var objusuario = new UserModel()
            {
                Email = _email,
                Password = _password,
            };
            try
            {

                var autenticacion = new FirebaseAuthProvider(new FirebaseConfig(DBconn.WepApyAuthentication));
                var authuser = await autenticacion.SignInWithEmailAndPasswordAsync(objusuario.Email.ToString(), objusuario.Password.ToString());
                string obternertoken = authuser.FirebaseToken;

                IraMenu();

                


            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("Advertencia", "Los datos introducidos son incorrectos o el usuario se encuentra inactivo.", "Aceptar");
                //await App.Current.MainPage.DisplayAlert("Advertencia", ex.Message, "Aceptar");
            }
        }
        #endregion
        #region COMANDOS
        public Command LoginCommand { get; }
        public ICommand Menu => new Command(async () => await IraMenu());
        public ICommand Contraseña => new Command(async () => await ContraOlvidada());
        public ICommand Registro => new Command(async () => await IraRegistro());
        #endregion                                                                                                                       
    }
}
