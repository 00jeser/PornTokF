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
        private class Tag
        {
            public string count;
            public string value;
            public string types;
            public Tag(string[] vals)
            {
                count = vals[0];
                value = vals[1];
                types = vals[2];
            }
        }

        private static string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "likes");
        public static List<string> likes = File.Exists(p) ? File.ReadAllText(p).Split('\n').ToList() : new List<string>();
        static List<Tag> tags = new StreamReader(new Posts().GetType().Assembly.GetManifestResourceStream("PornTokF.tags.txt")).ReadToEnd().Split('\n').Select(x => new Tag(x.Split('~'))).ToList();
        //static List<string> generalTags = tags.Where(x => x.types.Contains("general")).Select(x => x.value).ToList();
        //static string[][] generalTags = Hashing(tags.Where(x => x.types.Contains("general")).Select(x => x.value).ToList());
        static string[][] generalTags;
        static string[][] ambiguousTags;
        static string[][] metadataTags;
        static string[][] copyrightTags;
        static string[][] artistTags;
        static string[][] characterTags;


        public static void Init() 
        {
            generalTags = Hashing(tags.Where(x => x.types.Contains("general")).Select(x => x.value).ToList());
            ambiguousTags = Hashing(tags.Where(x => x.types.Contains("ambiguous")).Select(x => x.value).ToList());
            metadataTags = Hashing(tags.Where(x => x.types.Contains("metadata")).Select(x => x.value).ToList());
            copyrightTags = Hashing(tags.Where(x => x.types.Contains("copyright")).Select(x => x.value).ToList());
            artistTags = Hashing(tags.Where(x => x.types.Contains("artist")).Select(x => x.value).ToList());
            characterTags = Hashing(tags.Where(x => x.types.Contains("character")).Select(x => x.value).ToList());
        }



        private static string[][] Hashing(List<string> ls)
        {
            List<List<string>> rezs = new List<List<string>>();


            for(int i = 0; i <= 1000; i++) 
            {
                rezs.Add(new List<string>());
            }
            foreach (var s in ls) 
            {
                var c = GetStringHash(s);
                var cc = s.ToList().Select(x => (int)(x));
                rezs[c].Add(s);
            }


            return rezs.Select(x => x.ToArray()).ToArray();
        }

        private static int GetStringHash(string s) => s.ToList().Select(x => (int)(x)).Sum() % 1000;



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
            try
            {
                var y = likes?.Where(x => x.StartsWith(id)).ToList().Count >= 1;
                return y;
            }
            catch (Exception)
            {
                return false;
            }
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
            try
            {
                if (likes?.Count == 0)
                    return "";
                string r = "";
                var Rand = new Random();
                var needType = Rand.Next(3);
                switch (needType)
                {
                    case 0:
                        var likesCharecter = new List<string>();
                        foreach (var s in likes)
                            foreach (var ss in s.Split('~')[1].Split(' '))
                                if (characterTags[GetStringHash(ss)].Contains(ss))
                                    likesCharecter.Add(ss);
                        if (likesCharecter.Count == 0)
                            return "";
                        return likesCharecter[Rand.Next(likesCharecter.Count)];
                        break;
                    case 1:
                        var likesGeneral = new List<string>();
                        foreach (var s in likes)
                            foreach (var ss in s.Split('~')[1].Split(' '))
                                if (generalTags[GetStringHash(ss)].Contains(ss))
                                    likesGeneral.Add(ss);
                        if (likesGeneral.Count == 0)
                            return "";
                        r += likesGeneral[Rand.Next(likesGeneral.Count)];
                        r += "+";
                        r += likesGeneral[Rand.Next(likesGeneral.Count)];
                        break;
                    case 2:
                        var likesArtist = new List<string>();
                        foreach (var s in likes)
                            foreach (var ss in s.Split('~')[1].Split(' '))
                                if (artistTags[GetStringHash(ss)].Contains(ss))
                                    likesArtist.Add(ss);
                        if (likesArtist.Count == 0)
                            return "";
                        return likesArtist[Rand.Next(likesArtist.Count)];
                        break;
                }
                r += "+-fur";
                return r;
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
