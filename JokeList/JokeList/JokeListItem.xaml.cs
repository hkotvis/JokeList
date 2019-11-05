using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace JokeList
{
    public partial class JokeListItem : ContentPage
    {
        public JokeListItem()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var jokeItem = (JokeItem)BindingContext;
            await App.Database.SaveItemAsync(jokeItem);
            await Navigation.PopAsync();
        }

         async void OnDeleteClicked(object sender, EventArgs e)
        {
            var jokeItem = (JokeItem)BindingContext;
            await App.Database.DeleteItemAsync(jokeItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}