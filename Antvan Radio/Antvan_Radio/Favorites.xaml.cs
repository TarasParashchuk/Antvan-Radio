using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Antvan_Radio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Favorites : ContentPage
    {
        string current_title = String.Empty;
        string flag_name_song = string.Empty;
        bool result_check_element = false;
        string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "f1avori1tes.xml");

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeComponent();

            var information = Return_data.parameter;
            Device.BeginInvokeOnMainThread(() =>
            {
                FavoritesList.ItemsSource = new SaveOpenFavorites().Open_Favorites();
                control_author.Text = information.Author;
                control_song.Text = information.Song;
                control_photo.Source = information.Icon;

                if (!Return_data._flag)
                    control_playarrow.Source = "pausearrow.png";
                else
                    control_playarrow.Source = "playarrow.png";

                result_check_element = new SaveOpenFavorites().Find_Favorites(current_title);

                if (result_check_element)
                    Favorite_Button.Source = "favorite.png";
                else
                    Favorite_Button.Source = "favorite1.png";
            });
        }

        public Favorites()
        {
            InitializeComponent();

            var thread1 = new Thread(Song_Image);
            thread1.Start();
        }

        private void OnGoMain(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new MainPage());
            //Navigation.PopModalAsync(new Menu());
        }

        private bool Touch_Image(bool result_check_element)
        {
            if (!result_check_element)
            {
                var favorites = new Model_Song(Return_data.parameter.Icon, control_song.Text, control_author.Text);
                new SaveOpenFavorites(favorites).Save_Favorites();
                Favorite_Button.Source = "favorite1.png";
                result_check_element = true;
            }
            else
            {
                new SaveOpenFavorites().Remove_Favorites(control_song.Text);
                Favorite_Button.Source = "favorite.png";
                result_check_element = false;
            }
            Device.BeginInvokeOnMainThread(() => FavoritesList.ItemsSource = new SaveOpenFavorites().Open_Favorites());
            return result_check_element;
        }

        private void TouchImage(object sender, EventArgs e)
        {
            result_check_element = Touch_Image(result_check_element);
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
                        control_author.Text = information.Author;
                        control_song.Text = information.Song;
                        control_photo.Source = information.Icon;
                        current_title = information.Song;
                    });
                }
            
                result_check_element = new SaveOpenFavorites().Find_Favorites(current_title);
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (!result_check_element)
                        Favorite_Button.Source = "favorite.png";
                    else
                        Favorite_Button.Source = "favorite1.png";
                });
                Thread.Sleep(1000);
            }

        }
        
        private void OnButtonPlay(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;

            if (Return_data._flag)
            {
                new Droid.Control_player().Play();
                imageSender.Source = "pausearrow.png";
                Return_data._flag = false;
            }
            else
            {
                new Droid.Control_player().Pause();
                imageSender.Source = "playarrow.png";
                Return_data._flag = true;
            }
        }

    }
}