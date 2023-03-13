using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace DOS
{
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;

        public MainWindow()
        {
            InitializeComponent();
            ConfigureVariables();
            UI.ChangeAppTitle("Idle");
        }

        private void ConfigureVariables()
        {
            mainWindow = this;

            UI.pingButton = PingButton;
            UI.hostTextBox = HostTextBox;
            UI.invokeTarget = InvokeTarget;
            UI.cancelButton = CancelButton;
            UI.timeoutTextBox = TimeoutTextBox;
            UI.instancesTextBox = InstancesTextBox;
            UI.packetSizeTextBox = PacketSizeTextBox;
        }

        private void PingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckParameters()) return;
            UI.ToggleElementModification();
            Threading.GenerateSpawnThread();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Threading.GenerateCancelThread();
        }

        private bool CheckParameters()
        {
            bool missingVariables = false;
            if (string.IsNullOrWhiteSpace(UI.hostTextBox.Text)) missingVariables = true;
            if (string.IsNullOrWhiteSpace(UI.instancesTextBox.Text)) missingVariables = true;
            if (string.IsNullOrWhiteSpace(UI.instancesTextBox.Text)) missingVariables = true;
            if (string.IsNullOrWhiteSpace(UI.packetSizeTextBox.Text)) missingVariables = true;
            if (missingVariables)
            {
                UI.ShowMessageBox(0);
                return false;
            }

            bool notDigit = false;
            if (!UI.instancesTextBox.Text.All(char.IsDigit)) notDigit = true;
            if (!UI.packetSizeTextBox.Text.All(char.IsDigit)) notDigit = true;
            if (notDigit)
            {
                UI.ShowMessageBox(1);
                return false;
            }

            bool bigPacket = false;
            if (int.Parse(UI.packetSizeTextBox.Text) > 65500) bigPacket = true;
            if (bigPacket)
            {
                UI.ShowMessageBox(2);
                return false;
            }

            Network.host = UI.hostTextBox.Text;
            Network.timeout = int.Parse(UI.timeoutTextBox.Text);
            Network.instances = int.Parse(UI.instancesTextBox.Text);
            Network.packetSize = new byte[int.Parse(UI.packetSizeTextBox.Text)];
            return true;
        }
    }
}
