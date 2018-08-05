using Android.Content;
using Android.Media;
using Antvan_Radio.Droid;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Antvan_Radio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string flag_name_song = string.Empty;
        bool result_check_element = false;
        private ToolbarItem Favorite_Button;

        public MainPage()
        {
            InitializeComponent();
            var thread1 = new Thread(Song_Image);
            thread1.Start();

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Return_data._flag)
                    button_play.Source = "play.png";
                else
                    button_play.Source = "pause.png";
            });

            Favorite_Button = new ToolbarItem { Icon = null };
            this.ToolbarItems.Add(Favorite_Button);
            Favorite_Button.Clicked += OnFavorite_ButtonClicked;
            name_song.Text = "Загрузка...";
            name_author.Text = "Загрузка...";
            photo.Source = "image.jpg";
        }

        private void OnFavorite_ButtonClicked(object sender, EventArgs e)
        {
            if (!result_check_element)
            {
                var favorites = new Model_Song(Return_data.parameter.Icon, name_song.Text, name_author.Text);
                new SaveOpenFavorites(favorites).Save_Favorites();
                Favorite_Button.Icon = "favorite1.png";
                result_check_element = true;
            }
            else
            {
                new SaveOpenFavorites().Remove_Favorites(name_song.Text);
                Favorite_Button.Icon = "favorite.png";
                result_check_element = false;
            }
        }

        protected void Song_Image()
        {
            var current_title = String.Empty;
            while (true)
            {
                var information = Return_data.parameter;
                if (information != null && information.Song != current_title)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        name_author.Text = information.Author;
                        name_song.Text = information.Song;
                        photo.Source = information.Icon;
                        result_check_element = new SaveOpenFavorites().Find_Favorites(information.Song);
                        if (!result_check_element)
                            Favorite_Button.Icon = "favorite.png";
                        else
                            Favorite_Button.Icon = "favorite1.png";
                        current_title = information.Song;
                    });
                    Thread.Sleep(60000);
                }
                else Thread.Sleep(1000);
            }

        }
        private void OnButtonPlay(object sender, EventArgs args)
        {
            var imageSender = (Xamarin.Forms.Image)sender;
 
            if (Return_data._flag)
            {
                imageSender.Source = "pause.png";
                new Droid.Control_player().Play();
                Return_data._flag = false;
            }
            else
            {
                imageSender.Source = "play.png";
                new Droid.Control_player().Pause();
                Return_data._flag = true;
            }

        }

        private void Advertising_Image(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("https://timati.black-star.ru/?utm_source=allsocial&utm_medium=social&utm_campaign=anons#timing"));
        }
    }
}
