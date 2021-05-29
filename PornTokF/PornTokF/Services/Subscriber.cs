using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PornTokF.Services
{
    public static class Subscriber
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "subscribe");
        public static List<string> SubList;

        public static void Init() 
        {
            SubList = File.ReadAllLines(path).ToList();
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
