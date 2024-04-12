using CorredoresF1app_AGGP.Model;
using CorredoresF1app_AGGP.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.ViewModel
{
    internal class VMIntermedio : BaseViewModel
    {

        #region VARIABLES
        public Area _area {  get; set; }

        #endregion

        #region CONSTRUCTOR
        public VMIntermedio(INavigation navigation, Area area) 
        {
            Navigation = navigation;         
            _area = area;
        }
        #endregion

        #region OBJETOS

        #endregion

        #region PROCESOS
        public async Task Historial(Area area)
        {
            await Navigation.PushAsync(new HistorialRiego(_area));
        }

        public async Task Editar(Area area)
        {
            await Navigation.PushAsync(new EditarArea(_area));
        }

        #endregion

        #region COMANDOS
        public ICommand Historialcommand => new Command<Area>(async (h) => await Historial(h));
        public ICommand Editarcommand => new Command<Area>(async (a) => await Editar(a));

        #endregion

    }
}
