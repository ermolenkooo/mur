using kinocat.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace kinocat.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}