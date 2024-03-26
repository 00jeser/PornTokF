using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace PornTokF.Services
{
    public static class SafeModeService
    {
        private static string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "safe");
        public static string SafeString
        {
            get
            {
                if (!File.Exists(p))
                {
                    File.WriteAllText(p, "huge* pregnant anthro*");
                }
                return File.ReadAllText(p);
            }
            set
            {
                File.WriteAllText(p, value);
            }
        }
        public static string SafeTags
        {
            get => (" " + SafeString).Replace(" ", " -");
        }
    }
}
