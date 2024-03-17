using CorredoresF1app_AGGP.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.ViewModel
{
    public class VMcontraseñaOlvidada : BaseViewModel
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public VMcontraseñaOlvidada(INavigation navigation)
        {
            Navigation = navigation;

        }
        #endregion

        #region OBJETOS

        #endregion

        #region PROCESOS
        public async Task Volver()
        {
            await DisplayAlert("Mensaje", "Se ha enviado un codigo de recuperación a su email", "Aceptar");
            await Navigation.PushAsync(new Login());
        }
        #endregion
        #region COMANDOS
        public ICommand Iniciar => new Command(async () => await Volver());

        #endregion
    }
}
