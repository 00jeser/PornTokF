using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PornTokF.Services
{
    public static class Subscriber
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "subscribe");
        public static List<string> SubList = Init();

        public static List<string> Init() 
        {
            try
            {
                return File.ReadAllLines(path).ToList();
            }
            catch (Exception)
            {
                using (var writer = new StreamWriter(File.Create(path)))
                {
                    // do work here.
                }
            }
            return new List<string>();
        }

        public static bool Contains(string name) 
        {
            foreach (var s in SubList)
                if (s == name)
                    return true;
            return false;

        }
        public static void Add(string name)
        {
            SubList.Add(name);
            File.WriteAllLines(path, SubList);
        }
        public static void Remove(string name)
        {
            SubList.Remove(name);
            File.WriteAllLines(path, SubList);
        }
    }
}
