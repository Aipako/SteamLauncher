using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace SteamLauncher.Model
{
    internal class SteamProcess
    {
        public static void CloseSteam()
        {
            Process[] processes = Process.GetProcessesByName("Steam");
            foreach (Process process in processes)
            {
                if (process.MainModule.FileName == Constants.STEAM_PATH + "\\Steam.exe")
                {
                    process.Kill();
                    process.WaitForExit();
                    break;
                }
            }
        }

        public static void StartSteam()
        {
            Process.Start(Constants.STEAM_PATH + "\\Steam.exe");
        }

        public static void CopyAccountToSteam(SteamAccount account)
        {
            LogoffFromSteam();
            string accountLoginFile = Constants.USER_DATA_PATH + "\\" + account.AccountName + "\\loginusers.vdf";
            string accountConfigFile = Constants.USER_DATA_PATH + "\\" + account.AccountName + "\\config.vdf";
            
            Directory.CreateDirectory(Constants.STEAM_PATH + "\\config");

            File.Copy(accountConfigFile, Constants.STEAM_PATH + "\\config\\config.vdf");
            File.Copy(accountLoginFile, Constants.STEAM_PATH + "\\config\\loginusers.vdf");

            foreach (string filepath in Directory.GetFiles(Constants.USER_DATA_PATH + "\\" + account.AccountName, Constants.STEAM_GUARD_FILE_PREFIX))
            {
                File.Copy(filepath, Constants.STEAM_PATH + "\\" + Path.GetFileName(filepath));
            }

            SetRegistryAccountName(account.AccountName);
            
            StartSteam();

        }
        
        public static void LogoffFromSteam()
        {
            CloseSteam();
            RemoveAccountFromSteam();
        }

        public static void RemoveAccountFromSteam()
        {
            if (Directory.Exists(Constants.STEAM_PATH + "\\config"))
            {
                Directory.Delete(Constants.STEAM_PATH + "\\config", true);
            }

            if (Directory.Exists(Constants.STEAM_PATH + "\\appcache"))
            {
                Directory.Delete(Constants.STEAM_PATH + "\\appcache", true);
            }

            if (Directory.Exists(Constants.STEAM_PATH + "\\userdata"))
            {
                Directory.Delete(Constants.STEAM_PATH + "\\userdata", true);
            }

            foreach (string filepath in Directory.GetFiles(Constants.STEAM_PATH, Constants.STEAM_GUARD_FILE_PREFIX))
            {
                File.Delete(filepath);
            }
        }

        public static void InitSteamDirectory()
        {
            try
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Constants.STEAM_PATH = (string)Registry.GetValue(Constants.STEAM_REGISTRY_PATH_64, "InstallPath", null);
                }
                else
                {
                    Constants.STEAM_PATH = (string)Registry.GetValue(Constants.STEAM_REGISTRY_PATH_32, "InstallPath", null);
                }
                if (Constants.STEAM_PATH == null)
                {
                    throw new Exception("No Steam installed");
                }
            }
            catch (Exception ex)
            {
               if (MessageBox.Show(ex.Message, "Failed to find Steam directory", MessageBoxButton.OK, MessageBoxImage.Error) == MessageBoxResult.OK)
               {
                    Environment.Exit(0);
               }
               
            }
        }

        public static string GetGameDirectoryById()
        {

        }

        private static void SetRegistryAccountName(string accountName)
        {
            Registry.SetValue(Constants.STEAM_REGISTRY_USER_PATH, "AutoLoginUser", accountName);
        }

        private
    }
}
