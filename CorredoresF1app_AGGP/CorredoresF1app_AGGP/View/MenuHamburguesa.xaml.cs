using CorredoresF1app_AGGP.Model;
using CorredoresF1app_AGGP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CorredoresF1app_AGGP.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuHamburguesa : MasterDetailPage
    {
        public MenuHamburguesa()
        {
            InitializeComponent();
            BindingContext = new VMburger(Navigation);
            Elmenu();
        }

        public void Elmenu()
        {
            Detail = new NavigationPage(new Reportes());
            List<Menu> menu = new List<Menu>
            {
                //new Menu{ Pagina= new Inicio(),Nombre="Inicio", Icono="https://i.ibb.co/M7y1WLh/casita.png"},
                new Menu{ Pagina= new Reportes (),Nombre="Informe general", Icono="https://i.ibb.co/TrNjZyY/estadisticas.png"},
                new Menu{ Pagina= new Areas(),Nombre="Áreas", Icono="https://i.ibb.co/d600tff/hierba.png",},
                new Menu{ Pagina= new Ubicaciones(),Nombre="Ubicaciones", Icono="https://i.ibb.co/52WBW5t/despertador.png"},
            };
            ListMenu.ItemsSource = menu;
        }
        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var menu = e.SelectedItem as Menu;
            if (menu != null)
            {
                IsPresented = false;
                Detail = new NavigationPage(menu.Pagina);
            }
        }
        public class Menu
        {
            public string Nombre
            {
                get;
                set;
            }

            public ImageSource Icono
            {
                get;
                set;
            }

            public Page Pagina
            {
                get;
                set;
            }
        }
    }
}