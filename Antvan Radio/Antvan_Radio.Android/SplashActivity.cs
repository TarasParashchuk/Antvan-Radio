using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;

namespace Antvan_Radio.Droid
{
    [Activity(Theme = "@style/Theme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            base.OnCreate(savedInstanceState);
            
            var thread1 = new Thread(delegate () {
                var intent1 = new Intent(Thread_radio.ActionPlay);
                StartService(intent1);
            });
            thread1.Start();
            var thread2 = new Thread(delegate () {
                var intent = new Intent(this, typeof(ServicesInf));
            StartService(intent);
            });
            thread2.Start();
            
            StartActivity(typeof(MainActivity));
        }
    }

    
}
