using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;

namespace PornTokF.Services
{
    public static class Subscriber
    {
        private static string path = Path.Combine(FileSystem.AppDataDirectory, "subscribe");
        public static List<string> SubList = Init();

        public static List<string> Init() 
        {
            try
            {
                return File.ReadAllLines(path).ToList();
            }
            catch (Exception)
            {
                FileSaver.Default.SaveAsync(path, new MemoryStream(new byte[0]));
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
