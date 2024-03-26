using LruCacheNet;
using PornTokF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PornTokF.Services
{
    public static class Finder
    {
        public static List<string> TagsList = new StreamReader(new Posts().GetType().Assembly.GetManifestResourceStream("PornTok.tags.txt")).ReadToEnd().Split('\n').Select(y => y.Split('~')[1]).ToList();
        public static async Task<List<Post>> FindPostsByNameAsync(string tags, string limit = "5", string page = "0")
        {
            tags = tags + SafeModeService.SafeTags;
            try
            {
                HttpClient client = new HttpClient();


                string u = "https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&limit=" + limit + "&pid=" + page + "&tags=" + tags;
                string st2 = u;
                HttpResponseMessage response = await client.GetAsync(u);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                string st1 = responseBody;
                XmlSerializer serializer = new XmlSerializer(typeof(Posts));
                using (TextReader sr = new StringReader(responseBody))
                {
                    var s = serializer.Deserialize(sr);
                    return ((Posts)s).Post;
                }
            }
            catch (Exception)
            {
                return new List<Post>();
            }
        }
        public static async Task<Post> FindPostsByIdsAsync(int ids, string limit = "5", string page = "0")
        {
            try
            {
                HttpClient client = new HttpClient();


                string u = "https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&limit=" + limit + "&pid=" + page + "&id=" + ids.ToString();
                string st2 = u;
                HttpResponseMessage response = await client.GetAsync(u);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                string st1 = responseBody;
                XmlSerializer serializer = new XmlSerializer(typeof(Posts));
                using (TextReader sr = new StringReader(responseBody))
                {
                    var s = serializer.Deserialize(sr);
                    return ((Posts)s).Post.First();
                }
            }
            catch (Exception)
            {
                return new Post();
            }
        }
        public static LruCache<string, string> CountCash = new LruCache<string, string>();
        public static int GetPostCounts(string tags)
        {

            tags = tags + SafeModeService.SafeTags;
            if (CountCash.ContainsKey(tags))
            {
                string rez = CountCash.Get(tags);
                return int.Parse(rez);
            }
            else
            {
                try
                {
                    HttpClient client = new HttpClient();

                    string u = "https://api.rule34.xxx/index.php?page=dapi&s=post&q=index&tags=" + tags;

                    HttpResponseMessage response = client.GetAsync(u).GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    XmlSerializer serializer = new XmlSerializer(typeof(Posts));
                    using (TextReader sr = new StringReader(responseBody))
                    {
                        var s = serializer.Deserialize(sr);
                        CountCash.Add(tags, ((Posts)s).Count);
                        return int.Parse(((Posts)s).Count);
                    }
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }
    }
}
