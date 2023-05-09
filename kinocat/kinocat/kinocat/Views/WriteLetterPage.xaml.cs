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
	public partial class WriteLetterPage : ContentPage
	{
        public WriteLetterPage(Serial film, User user)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.BindingContext = new WriteLetterViewModel(film, user) { Navigation = this.Navigation };
        }
    }
}