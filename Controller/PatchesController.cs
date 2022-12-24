using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamLauncher.Model;
using SteamLauncher.View;
using System.Windows;
using System.Windows.Controls;
namespace SteamLauncher.Controller
{
    internal class PatchesController
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
            PatchManifest ptchManifest = new PatchManifest();
            ptchManifest.Closed += OnPatchesWindowClosed;
            return ptchManifest;
        }

        public void OnPatchesWindowClosed(object sender, EventArgs e)
        {
            PatchesWindowInstance = null;
        }
    }
}
