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
    public partial class MarksPage : ContentPage
    {
        public MarksPage(User u, User authorizeUser, string type)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new MarksViewModel(u, authorizeUser, type) { Navigation = this.Navigation };
        }
    }
}