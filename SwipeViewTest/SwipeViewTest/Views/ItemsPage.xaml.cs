using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SwipeViewTest.Models;
using SwipeViewTest.Views;
using SwipeViewTest.ViewModels;

namespace SwipeViewTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            viewModel = BindingContext as ItemsViewModel;
        }

        async void OnItemSelected(object sender, EventArgs args)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            viewModel.MakeRetryVisibleCommand.Execute(item);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }


        private void Retry_Invoked(object sender, System.EventArgs e)
        {
            var layout = (BindableObject)sender;
            var item = (Item)layout.BindingContext;
            viewModel.MakeRetryInvisibleCommand.Execute(item);
        }


        private void OpenSwipeMenu_Tapped(object sender, System.EventArgs e)
        {
            // open swipe view here
            //SwipeMenu.Open(); <- not sure why I cant acess this
        }
    }
}