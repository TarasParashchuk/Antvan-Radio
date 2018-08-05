using System.Threading.Tasks;

namespace Antvan_Radio.iOS
{
    public class BackgroundTaskInf
    {
        public void Start()
        {
            Task.Run(async () =>
            {
                 await new GetRadioSongInformation().Return_Information_Song();
            });
        }     
    }
}