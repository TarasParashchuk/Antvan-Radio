using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Antvan_Radio
{
    public class GetRadioSongInformation
    {
        public async Task Return_Information_Song()
        {
            await Task.Run(async () => {
                 var radio_inform = new RadioInformationandStreaming();
                    while (true)
                    { 
                        var rez = await radio_inform.Get_RadioInformationandStreaming();
                        if (rez != null)
                        {
                            Return_data.parameter = rez;
                            await Task.Delay(1400);
                        }
                    }
                });
        }
    }
}
