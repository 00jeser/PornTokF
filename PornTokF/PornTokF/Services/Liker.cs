using PornTokF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PornTokF.Services
{
    static class Liker
    {
        public static string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "likes");
        static List<string> likes = File.Exists(p) ? File.ReadAllText(p).Split('\n').ToList() : new List<string>();
        
        
        public static void LikeIt(string tags, string id)
        {
            try
            {
                likes.Add($"{id}~{tags}");
                File.WriteAllText(p, string.Join("\n", likes));
            }
            catch (FileNotFoundException)
            {
                likes = new List<string>() { $"{id}~{tags}" };
                File.WriteAllText(p, $"{id}~{tags}");
            }
        }
        public static void LikeIt(Post p)
        {
            LikeIt(p.Tags, p.Id);
        }
        public static void LikeIt(PhotoViewModel p)
        {
            LikeIt(p.Photo.Tags, p.Photo.Id);
        }


        public static void DeleteIt(string id)
        {
            likes = likes.Where(x => !x.StartsWith(id)).ToList();
            File.WriteAllText(p, string.Join("\n", likes));
        }
        public static void DeleteIt(Post p)
        {
            DeleteIt(p.Id);
        }
        public static void DeleteIt(PhotoViewModel p)
        {
            DeleteIt(p.Photo.Id);
        }


        public static bool FindIt(string id)
        {
            var y = likes.Where(x => x.StartsWith(id)).ToList().Count >= 1;
            return y;
        }
        public static bool FindIt(Post p)
        {
            return FindIt(p.Id);
        }
        public static bool FindIt(PhotoViewModel p)
        {
            return FindIt(p.Photo.Id);
        }


        public static string GenetateTags() 
        {
            if (likes.Count == 0)
                return "";
            string r = "";
            var ls = likes.Select(x => x.Split('~')[1].Split(' ')).ToList();
            var Rand = new Random();
            int sp = Rand.Next(ls.Count);
            r += ls[sp][Rand.Next(ls[sp].Length)];
            r += '+';
            r += ls[sp][Rand.Next(ls[sp].Length)];
            r += "+-fur";
            return r;
        }
    }
}
