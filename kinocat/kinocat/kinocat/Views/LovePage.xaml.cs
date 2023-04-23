using kinocat.Models;
using kinocat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace kinocat.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LovePage : ContentPage
    {
        public LovePage(User u, User authorizeUser, string type)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new LoveViewModel(u, authorizeUser, type) { Navigation = this.Navigation };
        }
    }
}