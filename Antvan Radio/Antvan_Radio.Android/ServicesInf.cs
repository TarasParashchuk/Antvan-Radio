using Android.App;
using Android.Content;
using Android.OS;
using System.Threading.Tasks;
namespace Antvan_Radio.Droid
{
    [Service]
    public class ServicesInf : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Task.Run(async () =>
            {
                await new GetRadioSongInformation().Return_Information_Song();
            });
            return StartCommandResult.NotSticky;
        }
    }
}