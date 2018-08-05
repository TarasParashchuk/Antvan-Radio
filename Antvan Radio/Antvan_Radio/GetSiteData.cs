using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Antvan_Radio
{
    public class GetSiteData
    {
        public GetSiteData() { }

        public async Task<string> Data_Song(string url)
        {
            JsonValue json;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Method = "Get";

            using (var responce = await request.GetResponseAsync())
            {
                using (var stream = responce.GetResponseStream())
                {
                    json = await Task.Run(() => JsonObject.Load(stream));
                }
            }
            return json[0]["/non-stop"]["title"].ToString();
        }

        public string Data_Image(string title_song)
        {
            var result = String.Empty;
            var encoding = new ASCIIEncoding();
            string postData = "action=get_album_image_output&image_name=" + title_song;
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(postData);

            WebRequest request = WebRequest.Create("http://antvan-radio.com/wp-admin/admin-ajax.php");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
                stream.Write(data, 0, data.Length);

            using (var response = request.GetResponse())
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                }
            }

            return result;
        }
    }
}
