using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Antvan_Radio.Droid
{
    [Service]
    [IntentFilter(new[] { ActionPlay, ActionPause })]
    public class Thread_radio : Service
    {
        public const string ActionPlay = "com.xamarin.action.PLAY";
        public const string ActionPause = "com.xamarin.action.PAUSE";
        public bool paused;
        public bool _paused;

        MediaPlayer player, _player;
        public void IntializePlayer()
        {
            player = new MediaPlayer();
            player.SetAudioStreamType(Stream.Music);
            _paused = true;
        }

        private void Play()
        {
            var thread1 = new Thread(async () =>
            {
                if (_paused && player != null)
                {
                    _player.Start();
                    Thread.Sleep(5000);
                    paused = true;
                    _paused = false;
                }

                if (paused && player != null)
                {
                    paused = false;
                    player.Start();
                    return;
                }

                if (player == null)
                {
                    IntializePlayer();
                }

                if (player.IsPlaying)
                    return;

                try
                {
                    _player = new MediaPlayer();
                    var fd = Android.App.Application.Context.Assets.OpenFd("zastavka.mp3");
                    _player.SetDataSource(fd.FileDescriptor);
                    _player.Prepare();
                    /*await player.SetDataSourceAsync("https://www.soundhelix.com/examples/mp3/SoundHelix-Song-1.mp3");
                    player.PrepareAsync();*/
                    var fd1 = Android.App.Application.Context.Assets.OpenFd("live.m3u");
                    _player.SetDataSource(fd1.FileDescriptor);
                    _player.Prepare();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to start playback: " + ex);
                }
            }); thread1.Start();
        }

        private void Pause()
        {
            if (player == null)
                return;

            if (_player.IsPlaying)
            {
                _player.Pause();
                if (player.IsPlaying)
                {
                    player.Pause();
                    paused = true;
                }
                return;   
            }

            if (player.IsPlaying)
            {
                player.Pause();
                paused = true;
                return;
            }

        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            switch (intent.Action)
            {
                case ActionPlay: Play(); break;
                case ActionPause: Pause(); break;
            }
            return StartCommandResult.NotSticky;
        }
    }
}
