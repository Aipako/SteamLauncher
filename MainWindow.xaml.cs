using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SteamLauncher.Controller;
using SteamLauncher.Model;
using Microsoft.Win32;
using Indieteur.VDFAPI;
using System.IO;

namespace SteamLauncher
{
   
    public partial class MainWindow : Window
    {
        public List<Button> AccountButtons = new List<Button>();

        private AccountSelector MyAccountSelector;
        private PatchesController MyPatchesController;
        public MainWindow()
        {
            InitializeComponent();
            MyAccountSelector = InitializeAccountSelector();
            MyPatchesController = InitializePatchesController();

            UserData.InitializeUserData();
            SteamProcess.InitSteamDirectory();
            Accounts.ItemsSource = AccountButtons;
            InitializeAccountButtons();
            

        }

        private void Button_Add_Account_Click(object sender, RoutedEventArgs e)
        {
            SteamAccount newAccount = GetActiveSteamAccount();
            if (UserData.UsersList.Contains(newAccount))
            {
                MessageBox.Show("This account was already added", "Adding account", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //creating folder for Account files
            UserData.CreateAccountFolder(newAccount.AccountName);
            UserData.CopyAccountFiles(newAccount.AccountName);
            UserData.UpdateUserListAdd(newAccount);
            AddAccountButton(newAccount);
            
        }

        private void InitializeAccountButtons()
        {
            foreach (SteamAccount account in UserData.UsersList)
            {
                AddAccountButton(account);
            }
        }

        public void AddAccountButton(SteamAccount account)
        {
            Button accountButton = new Button();
            //accountButton.Name = account.PersonalName;
            accountButton.Content = account.AccountName;
            accountButton.Click += MyAccountSelector.OnAccountButtonClick;
            AccountButtons.Add(accountButton);
            Accounts.Items.Refresh();
        }

        public void RemoveAccountButton(SteamAccount account)
        {
            AccountButtons.Remove(AccountButtons.Find(x => (string)x.Content == account.AccountName));
            Accounts.Items.Refresh();
        }

        public void ReEnableAccountButttons()
        {
            MyAccountSelector.ClickedButton = null;
            foreach (Button accountButton in AccountButtons)
            {
                accountButton.IsEnabled = true;
            }
            
        }

        private void Button_Remove_Account_Click(object sender, RoutedEventArgs e)
        {
            SteamAccount accountToRemove = MyAccountSelector.GetSelectedAccount();
            if (accountToRemove == null)
            {
                return;
            }
            RemoveAccountButton(accountToRemove);
            UserData.UpdateUserListRemove(accountToRemove);
            UserData.DeleteAccountFolder(accountToRemove.AccountName);
        }

        private void Button_Login_Account_Click(object sender, RoutedEventArgs e)
        {
            SteamAccount accountToRemove = MyAccountSelector.GetSelectedAccount();
            if (accountToRemove == null)
            {
                return;
            }
            SteamProcess.CopyAccountToSteam(accountToRemove);
        }
        
        private AccountSelector InitializeAccountSelector()
        {
            return new AccountSelector(this);
        }

        private PatchesController InitializePatchesController()
        {
            PatchesController ptchController = new PatchesController(this);
            PathesBtn.Click += ptchController.OnPatchesButtonClick;
            return ptchController; 
        }

        private SteamAccount GetActiveSteamAccount()
        {

            string accountLoginFile = Constants.STEAM_PATH + "\\config\\loginusers.vdf";
            //string accountConfigFile = steamPath + "\\config\\config.vdf";
            //string steamAppData = steamPath + "\\config\\SteamAppData.vd";

            VDFData loginFile = new VDFData(accountLoginFile);
            List<VDFKey> vdfKeys = loginFile.Nodes.FirstOrDefault().Nodes.FirstOrDefault().Keys;
            string accountName = vdfKeys.Find(x => x.Name == "AccountName").Value;
            string personalName = vdfKeys.Find(x => x.Name == "PersonaName").Value;
            SteamAccount activeAccount = new SteamAccount(accountName, personalName);
            
            return activeAccount;
        }

        private void Button_Logoff_Account_Click(object sender, RoutedEventArgs e)
        {
            ReEnableAccountButttons();
            SteamProcess.LogoffFromSteam();
            SteamProcess.StartSteam();
        }
    }
}
