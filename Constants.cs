using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;

namespace SteamLauncher
{
    internal class Constants
    {
        public static readonly string USER_DATA_PATH = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SteamLauncher";
        public static readonly string PROGRAM_CONFIG_PATH = USER_DATA_PATH + "\\config.txt";
        public static readonly string STEAM_REGISTRY_PATH_32 = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Valve\\Steam";
        public static readonly string STEAM_REGISTRY_PATH_64 = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam";
        public static readonly string STEAM_REGISTRY_USER_PATH = "HKEY_CURRENT_USER\\SOFTWARE\\Valve\\Steam";
        public static readonly string STEAM_GUARD_FILE_PREFIX = "ssfn*";

        //Not constant at all, but very want to be
        public static string STEAM_PATH;

    }
}
