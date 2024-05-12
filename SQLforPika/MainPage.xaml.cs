using SQLforPika; // Replace with the actual namespace of your class library

namespace SQLforPika
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();

            changeBGToRed.BackgroundColor = Colors.Blue;
            changeBGToRed.TextColor = Colors.White;

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        }
        async void Add_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                { Name = nameEntry.Text });

                nameEntry.Text = "";
                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }


        }
        Person lastSelection;
        Person pika;




        void collectionView_SelectionChanged(System.Object sender, SelectionChangedEventArgs e)
        {
            lastSelection = e.CurrentSelection[0] as Person;

            nameEntry.Text = lastSelection.Name;
        }





        async void Update_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                lastSelection.Name = nameEntry.Text;


                await App.Database.UpdatePersonAsync(lastSelection);

                collectionView.ItemsSource = await App.Database.GetPeopleAsync();
            }
        }

        async void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                await App.Database.DeletePersonAsync(lastSelection);

                collectionView.ItemsSource = await App.Database.GetPeopleAsync();

                nameEntry.Text = "";

            }
        }

        private async void PutAll_Clicked(object sender, EventArgs e)
        {

            if (lastSelection != null)
            {
                await this.DisplayAlert("Thông báo", "Bạn đã thêm tất cả!", "OK");
            }
        }

        private async void sort_Clicked(System.Object sender, System.EventArgs e)
        {
            if (lastSelection != null)
            {
                pika = lastSelection;
                Delete_Clicked(sender, e);
                //await App.Database.SortData(new Person { Name = pika.Name });
                Add_Clicked(sender, e);

            }
        }

        private void Change_Clicked(object sender, EventArgs e)
        {
            (sender as Button).ChangeBGToRed();
        }

        public int i = 0;
        private void changeBGToRed_Clicked(object sender, EventArgs e)
        {

            if (i == 0)
            {
                changeBGToRed.BackgroundColor = Colors.Red;
                i = 1;
            }
            else
            {
                changeBGToRed.BackgroundColor = Colors.Blue;
                i = 0;
            }


        }
    }

    public static class EmbeddedExtensions
    { public static void ChangeBGToRed(this Button button) => button.BackgroundColor = Colors.Red;}

}
