using PornTokF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PornTokF.Services
{
    public static class Finder
    {
        public static async System.Threading.Tasks.Task<List<Post>> FindVideosAsync(string tags, string limit = "5", string page = "0")
        {
            /*HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=0&tags=" + tags);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            responseBody = responseBody.Replace("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>", "");
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(responseBody);
            XmlElement xRoot = xDoc.DocumentElement;


            response = await client.GetAsync("https://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=" + limit + "&pid=" + (new Random()).Next(int.Parse(xRoot.GetAttribute("count")) / 200) + "&tags=" + tags);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(Posts));
            using (TextReader sr = new StringReader(responseBody))
            {
                var s = serializer.Deserialize(sr);
                return ((Posts)s).Post;
            }*/
            HttpClient client = new HttpClient();


            string u = "https://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=" + limit + "&pid=" + page + "&tags=" + tags;
            HttpResponseMessage response = await client.GetAsync(u);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            XmlSerializer serializer = new XmlSerializer(typeof(Posts));
            using (TextReader sr = new StringReader(responseBody))
            {
                var s = serializer.Deserialize(sr);
                return ((Posts)s).Post;
            }
        }
    }
}
