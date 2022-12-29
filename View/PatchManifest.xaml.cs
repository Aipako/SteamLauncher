using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SteamLauncher.Controller;
using SteamLauncher.Model;
namespace SteamLauncher.View
{
    public partial class PatchManifest : Window
    {

        private PatchesController ControllerInstance { get; set; } 

        public PatchManifest(PatchesController controllerInstance)
        {
            ControllerInstance = controllerInstance;
            InitializeComponent();
            InitializeHandlers();
        }

        private void InitializeHandlers()
        {
            FileDialogBtn.Click += ControllerInstance.OnFileDialogButtonClick;
            PatchBtn.Click += ControllerInstance.OnPatchButtonClick;
            RestoreBtn.Click += ControllerInstance.OnRestoreButtonClick;
        }
    }
}
