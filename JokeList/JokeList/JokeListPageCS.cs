using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace JokeList
{
    public class JokeListPageCS : ContentPage
    {
        ListView listView;

        public JokeListPageCS()
        {
            Title = "Joke List";

            var toolbarItem = new ToolbarItem
            {
                Text = "+"
            };
            toolbarItem.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new JokeListItemCS
                {
                    BindingContext = new JokeItem()
                });
            };
            ToolbarItems.Add(toolbarItem);

            listView = new ListView
            {
                Margin = new Thickness(20),
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label
                    {
                        VerticalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.StartAndExpand
                    };
                    label.SetBinding(Label.TextProperty, "Joke");

                    var tick = new Image
                    {
                        Source = ImageSource.FromFile("check.png"),
                        HorizontalOptions = LayoutOptions.End
                    };
                    tick.SetBinding(VisualElement.IsVisibleProperty, "Delete");

                    var stackLayout = new StackLayout
                    {
                        Margin = new Thickness(20, 0, 0, 0),
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Children = { label, tick }
                    };

                    return new ViewCell { View = stackLayout };
                })
            };
            listView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem != null)
                {
                    await Navigation.PushAsync(new JokeListItemCS
                    {
                        BindingContext = e.SelectedItem as JokeItem
                    });
                }
            };

            Content = listView;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtJokeId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }
    }
}