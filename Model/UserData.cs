using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using SteamLauncher.Controller;
using SteamLauncher;
namespace SteamLauncher.Model
{
    public static class UserData
    {
        [JsonInclude]
        public static List<SteamAccount> UsersList { get; private set; }
        public static void GetStoredAccountsForUser()
        {

        }
        public static void InitializeUserData()
        {
            if(!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SteamLauncher"))
            {
                CreateUserData();
            }
            try
            {
                FileStream UserFile = File.OpenRead(Constants.USER_DATA_PATH + "\\Users.json");
                UsersList = JsonSerializer.Deserialize<List<SteamAccount>>(UserFile);
                UserFile.Close();
            }
            catch
            {
                CreateUserData();
                UsersList = new List<SteamAccount>();
            }


        }

        public static void UpdateUserListAdd(SteamAccount newAccount)
        {
            UsersList.Add(newAccount);
            File.WriteAllText(Constants.USER_DATA_PATH + "\\Users.json", JsonSerializer.Serialize(UsersList));
        }

        public static void UpdateUserListRemove(SteamAccount newAccount)
        {
            UsersList.Remove(newAccount);
            File.WriteAllText(Constants.USER_DATA_PATH + "\\Users.json", JsonSerializer.Serialize(UsersList));
        }

        public static void CreateAccountFolder(string accountName)
        {
            Directory.CreateDirectory(Constants.USER_DATA_PATH + "\\" + accountName);
        }
        
        public static void DeleteAccountFolder(string accountName)
        {
            if(Directory.Exists(Constants.USER_DATA_PATH + "\\" + accountName))
            {
                Directory.Delete(Constants.USER_DATA_PATH + "\\" + accountName, true);
            }
        }

        public static void CopyAccountFiles(string targetAccount)
        {

            string accountLoginFile = Constants.STEAM_PATH + "\\config\\loginusers.vdf";
            string accountConfigFile = Constants.STEAM_PATH + "\\config\\config.vdf";
            string steamAppData = Constants.STEAM_PATH + "\\config\\SteamAppData.vdf";
            File.Copy(accountLoginFile, Constants.USER_DATA_PATH + "\\" + targetAccount + "\\loginusers.vdf");
            File.Copy(accountConfigFile, Constants.USER_DATA_PATH + "\\" + targetAccount + "\\config.vdf");
            //File.Copy(steamAppData, Constants.USER_DATA_PATH + "\\" + targetAccount + "\\SteamAppData.vdf");

            foreach (string filepath in Directory.GetFiles(Constants.STEAM_PATH, Constants.STEAM_GUARD_FILE_PREFIX))
            {
                File.Copy(filepath, Constants.USER_DATA_PATH + "\\" + targetAccount + "\\" + Path.GetFileName(filepath));
            }
        }

        private static void CreateUserData()
        {
            string path = Constants.USER_DATA_PATH;
            Directory.CreateDirectory(path);
            List<SteamAccount> emptyUserList = new List<SteamAccount>();
            File.WriteAllText(path + "\\Users.json", JsonSerializer.Serialize(emptyUserList));
        }

    }
}
