using PornTokF.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PornTokF.Services
{
    public class Tag
    {
        public string label { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public override string ToString()
        {
            return value;
        }
    }
    public class TagsServices
    {
        public static async Task<List<Tag>> GetTagsAsync(string query="") 
        {
            //https://ac.rule34.xxx/autocomplete.php?q=fu
            HttpClient client = new HttpClient();


            string u = "https://ac.rule34.xxx/autocomplete.php?q=" + query;
            HttpResponseMessage response = await client.GetAsync(u);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Tag>>(responseBody);
            
            //var l = new string[11];
            //int i = 0;
            //foreach (string s in Finder.TagsList)
            //{
            //    if (s.Contains(query))
            //        l[i++] = s;
            //    if (i >= 10)
            //        break;
            //}
            //return l;   
        }
    }
}
