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
	public partial class StatPage : ContentPage
	{
        public StatPage(User authorizeUser)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new StatViewModel(authorizeUser) { Navigation = this.Navigation };
        }
    }
}