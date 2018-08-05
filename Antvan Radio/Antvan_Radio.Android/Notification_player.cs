using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Graphics;

namespace Antvan_Radio.Droid
{
    public class Notification_player
    {
        public NotificationCompat.Builder builder;
        public Intent intent = new Intent(Application.Context, typeof(Thread_radio));
        public Notification_player() { }

        public void Create_Notification()
        {
            builder = new NotificationCompat.Builder(Application.Context)
                .SetContentTitle(Return_data.parameter.Song)
                .SetContentText(Return_data.parameter.Author)
                .SetSmallIcon(Resource.Drawable.Flurry_Google_Music);

            AddActionButtons(Return_data._flag);
            builder.SetOngoing(Return_data._flag);
            builder.SetVisibility(1);

            Notification notification = builder.Build();
            const int notificationId = 0;
            NotificationManagerCompat.From(Application.Context).Notify(notificationId, notification);
        }

        private void AddActionButtons(bool mediaIsPlaying)
        {
            builder.MActions.Clear();
            builder.AddAction(mediaIsPlaying
                ? GenerateActionCompat(Resource.Drawable.pausearrow, "Pause", Thread_radio.ActionPause)
                : GenerateActionCompat(Resource.Drawable.playarrow, "Play", Thread_radio.ActionPlay));
        }

        public void Updater_Notification()
        {
            if (builder != null)
            {
                AddActionButtons(Return_data._flag);
                builder.SetOngoing(Return_data._flag);
                NotificationManagerCompat.From(Application.Context).Notify(1, builder.Build());
            }
            else Create_Notification();
        }

        private NotificationCompat.Action GenerateActionCompat(int icon, String title, String intentAction)
        {
            intent.SetAction(intentAction);
            PendingIntentFlags flags = PendingIntentFlags.UpdateCurrent;

            PendingIntent pendingIntent = PendingIntent.GetService(Application.Context, 1, intent, flags);
            return new NotificationCompat.Action.Builder(icon, title, pendingIntent).Build();
        }

    }

}