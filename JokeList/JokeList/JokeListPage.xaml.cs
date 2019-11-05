using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Xamarin.Forms;

namespace JokeList
{
    public partial class JokeListPage : ContentPage
    {
        public JokeListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtJokeId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JokeListItem
            {
                BindingContext = new JokeItem()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new JokeListItem
                {
                    BindingContext = e.SelectedItem as JokeItem
                });
            }
        }
    }
}