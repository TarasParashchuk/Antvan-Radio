using Android.App;
using Android.Content;

namespace Antvan_Radio.Droid
{
    public class Control_player
    {
        //Notification_player Notification_player = new Notification_player();
        Intent intent = new Intent(Application.Context, typeof(Thread_radio));

        public void Play()
        {
            intent.SetAction(Thread_radio.ActionPlay);
            PendingIntent pendingIntent = PendingIntent.GetService(Application.Context, 1, intent, PendingIntentFlags.UpdateCurrent);
            pendingIntent.Send();
            //Notification_player.Updater_Notification();
        }

        public void Pause()
        {
            intent.SetAction(Thread_radio.ActionPause);
            PendingIntent pendingIntent = PendingIntent.GetService(Application.Context, 1, intent, PendingIntentFlags.UpdateCurrent);
            pendingIntent.Send();
            //Notification_player.Updater_Notification();
        }
    }
}