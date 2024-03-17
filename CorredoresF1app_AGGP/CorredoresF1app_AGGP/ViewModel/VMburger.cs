﻿using CorredoresF1app_AGGP.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CorredoresF1app_AGGP.ViewModel
{
    public class VMburger : BaseViewModel
    {
        #region VARIABLES

        #endregion

        #region CONSTRUCTOR
        public VMburger(INavigation navigation)
        {
            Navigation = navigation;

        }
        #endregion

        #region OBJETOS

        #endregion

        #region PROCESOS
        public async Task RegLo()
        {
            await Navigation.PushAsync(new Login());
        }
        #endregion
        #region COMANDOS
        public ICommand Volver => new Command(async () => await RegLo());

        #endregion
    }
}
