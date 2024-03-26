using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LruCacheNet;


namespace PornTokF.Services
{
    public static class UserIDCaching
    {
        private static string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UsersCache");
        private static LruCache<string, string> cachingSystem = new LruCache<string, string>();
        public static void Init() 
        {
            try
            {
                foreach (var s in File.ReadAllLines(p))
                {
                    if (s.Contains(';'))
                    {
                        cachingSystem[s.Split(';')[0]] = s.Split(';')[1];
                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(p);
            }
        }
        public static async System.Threading.Tasks.Task<string> LoadUserNameAsync(string UserId, string PostId)
        {
            string rez = LoadUserNameOnlyFromCashe(UserId);
            if (rez != "")
                return rez;

            try
            {
                //var url = "https://api.rule34.xxx/index.php?page=post&s=view&id=" + PostId;
                //var web = new HtmlWeb();
                //var document = await web.LoadFromWebAsync(url);
                //var container = document.DocumentNode.Descendants("a").FirstOrDefault(y => y?.GetAttributeValue("href", "")?.Contains("index.php?page=account&amp;s=profile&amp;uname=") == true);
                //rez = container.InnerHtml.Trim();
                //cachingSystem[UserId] = rez;

                //if (!File.Exists(p))
                //{
                //    File.WriteAllText(p, $"{UserId};{PostId}");
                //}
                //else 
                //{
                //    string wrez = "";
                //    foreach(var i in cachingSystem.Keys)
                //        wrez += $"{i};{cachingSystem[i]}\n";
                //    File.WriteAllText(p, wrez.Trim());
                //}
                //return rez;
            }
            catch (Exception)
            {

            }

            return "";
        }
        public static string LoadUserNameOnlyFromCashe(string id)
        {
            if (cachingSystem.ContainsKey(id))
            {
                return cachingSystem[id];
            }
            return "";
        }
    }
}
