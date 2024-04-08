﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorredoresF1app_AGGP.Model;
using CorredoresF1app_AGGP.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorredoresF1app_AGGP.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HistorialRiego : ContentPage
	{
		public HistorialRiego(Area area)
		{
			InitializeComponent();
			BindingContext = new VMHistorialRiego(Navigation, area);
		}
	}
}