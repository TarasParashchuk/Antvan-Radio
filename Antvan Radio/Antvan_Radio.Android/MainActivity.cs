using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System.Threading.Tasks;

namespace Antvan_Radio.Droid
{
	[Activity (Label = "Antvan_Radio", Icon = "@drawable/icon", Theme="@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar; 

			base.OnCreate (bundle);
            
            /*
                Notification.Builder builder = new Notification.Builder(Application.Context)
                        .SetContentTitle("1")
                        .SetContentText("2")
                        .SetSmallIcon(Resource.Drawable.play);


                // Build the notification:
                Notification notification = builder.Build();

                // Get the notification manager:
                /*NotificationManager notificationManager =
                    GetSystemService(Context.NotificationService) as NotificationManager;

                // Publish the notification:
                const int notificationId = 0;
                Android.Support.V4.App.NotificationManagerCompat.From(Application.Context).Notify(notificationId, notification);
            
            */
            global::Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new Antvan_Radio.App ());
		}
    }

}

