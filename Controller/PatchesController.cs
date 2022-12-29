using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamLauncher.Model;
using SteamLauncher.View;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;
namespace SteamLauncher.Controller
{
    public class PatchesController
    {
        private SteamLauncher.MainWindow MainInstance { get; set; }

        private PatchManifest PatchesWindowInstance { get; set; }

        public PatchesController(SteamLauncher.MainWindow mainInstance)
        {
            MainInstance = mainInstance;
        }

        public void OnPatchesButtonClick(object sender, RoutedEventArgs e)
        {
            if (PatchesWindowInstance == null)
                PatchesWindowInstance = GetPatchesWindow();
            PatchesWindowInstance.Show();
            PatchesWindowInstance.Focus();
        }

        private PatchManifest GetPatchesWindow()
        {
            PatchManifest ptchManifest = new PatchManifest(this);
            ptchManifest.Closed += OnPatchesWindowClosed;
            return ptchManifest;
        }

        public void OnPatchesWindowClosed(object sender, EventArgs e)
        {
            PatchesWindowInstance = null;
        }

        

        // Event handling

        public void OnFileDialogButtonClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                PatchesWindowInstance.FilePathBox.Text = openFileDialog.FileName;
        }

        public void OnPatchButtonClick(object sender, EventArgs e)
        {
            
        }

        public void OnRestoreButtonClick(object sender, EventArgs e)
        {

        }
        
        // Actions with View

        private int GetGameId()
        {
            
            return -1;
        }

        // Inner Methods

        private bool VerifyGameId()
        {

            return false;
        }
    }
}
