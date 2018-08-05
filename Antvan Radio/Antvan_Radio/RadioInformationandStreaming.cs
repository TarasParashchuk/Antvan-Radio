using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Antvan_Radio
{
    public class RadioInformationandStreaming
    {
        string current_title = string.Empty;
        Model_Song data = null;

        public RadioInformationandStreaming() { }

        public async Task<Model_Song> Get_RadioInformationandStreaming()
        {
            var title = await Check_Information();

            if (current_title != title)
            {
                current_title = title;
                data = Get_New_Information(title);
            }

            return data;
        }

        public async Task<string> Check_Information()
        {
            var full_title = new GetSiteData().Data_Song("http://195.123.218.74:8000/info_2.xsl");
            return await full_title;
        }    

        public Model_Song Get_New_Information(string title)
        {
            if (title != string.Empty)
            {
                var img = string.Empty;
                
                var str_title = title.Substring(1, title.Length - 2).Replace(" - ", "-");
                var str = str_title.Split('-');

                var song = str[1].Trim();

                dynamic stuff = JObject.Parse(new GetSiteData().Data_Image(str_title));
                if (stuff["status"].ToString() == "error" || stuff["data"].ToString() == null)
                    img = "image.jpg";
                else img = stuff["data"].ToString();

                return new Model_Song( img, str[1].Trim(), str[0].Trim());
            }
            else
            {
                return null;
            }
        }
    }
}

