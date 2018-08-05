using System;
using System.Threading;
using Xamarin.Forms;

namespace Antvan_Radio
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new Menu();
        }

		protected override void OnStart ()
		{
            Return_data._flag = true;
            Return_data.first_flag = true;
            //Return_data._control = new Droid.Control_player();
        }

        private void DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
    }
}
