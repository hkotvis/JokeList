using Xamarin.Forms;

namespace JokeList
{
    public class JokeListItemCS : ContentPage
    {
        public JokeListItemCS()
        {
            Title = "Joke Item";

            var jokeEntry = new Entry();
            jokeEntry.SetBinding(Entry.TextProperty, "Joke");

            var punchlineEntry = new Entry();
            punchlineEntry.SetBinding(Entry.TextProperty, "Punchline");

            var deleteSwitch = new Switch();
            deleteSwitch.SetBinding(Switch.IsToggledProperty, "Delete");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var jokeItem = (JokeItem)BindingContext;
                await App.Database.SaveItemAsync(jokeItem);
                await Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var jokeItem = (JokeItem)BindingContext;
                await App.Database.DeleteItemAsync(jokeItem);
                await Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += async (sender, e) =>
            {
                await Navigation.PopAsync();
            };

            Content = new StackLayout
            {
                Margin = new Thickness(20),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children =
                {
                    new Label { Text = "Joke" },
                    jokeEntry,
                    new Label { Text = "Punchline" },
                    punchlineEntry,
                    new Label { Text = "Delete" },
                    deleteSwitch,
                    saveButton,
                    deleteButton,
                    cancelButton
                }
            };
        }
    }
}