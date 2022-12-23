using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SteamLauncher.Controller;
using SteamLauncher.Model;
namespace SteamLauncher.Controller
{
    internal class AccountSelector
    {
        private Button clickedButton = null;

        public Button ClickedButton
        {
            get
            {
                return clickedButton;
            }
            set
            {
                clickedButton = value;
            }
        }

        private SteamLauncher.MainWindow MainInstance {get; set;}

        public AccountSelector(SteamLauncher.MainWindow mainInstance)
        {
            MainInstance = mainInstance;
        }

        public void OnAccountButtonClick(object sender, RoutedEventArgs e)
        {
            MainInstance.ReEnableAccountButttons();

            Button senderButton = sender as Button;
            senderButton.IsEnabled = false;
            clickedButton = senderButton;
            //string buttonName = senderButton.Name;

        }

        public SteamAccount GetSelectedAccount()
        {
            if (clickedButton == null)
                return null;
            return UserData.UsersList.Find(x => x.AccountName == (string)clickedButton.Content);
        }
            
    }
}
