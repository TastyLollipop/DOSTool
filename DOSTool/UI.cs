using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DOS
{
    public static class UI
    {
        public static Button pingButton;
        public static Button cancelButton;
        public static TextBox hostTextBox;
        public static TextBox instancesTextBox;
        public static TextBox packetSizeTextBox;
        public static TextBox timeoutTextBox;

        public static Label invokeTarget;

        public enum InvokeMode { StartPing, CancelPing }

        public static void ToggleElementModification()
        {
            pingButton.IsEnabled = !pingButton.IsEnabled;
            hostTextBox.IsEnabled = !hostTextBox.IsEnabled;
            timeoutTextBox.IsEnabled = !timeoutTextBox.IsEnabled;
            instancesTextBox.IsEnabled = !instancesTextBox.IsEnabled;
            packetSizeTextBox.IsEnabled = !packetSizeTextBox.IsEnabled;
        }

        public static void ChangeAppTitle(string title)
        {
            MainWindow.mainWindow.Title = $"DOSTool - {title}";
        }

        public static void InvokeFunction(InvokeMode mode)
        {
            switch (mode)
            {
                case InvokeMode.CancelPing:
                    Action cancelAction = delegate { cancelButton.IsEnabled = true; };
                    pingButton.Dispatcher.Invoke(cancelAction);

                    cancelAction = delegate { ToggleElementModification(); };
                    pingButton.Dispatcher.Invoke(cancelAction);

                    cancelAction = delegate { ChangeAppTitle("Idle"); };
                    pingButton.Dispatcher.Invoke(cancelAction);
                    break;
            }
        }

        public static void ShowMessageBox(int mode)
        {
            string messageTitle = "";
            string messageText = "";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;

            switch (mode)
            {
                case 0:
                    messageTitle = "Missing Details";
                    messageText = "You are missing details. Please check and try again";
                    break;

                case 1:
                    messageTitle = "Invalid Details";
                    messageText = "The details you are using are invalid. Please check and try again";
                    break;

                case 2:
                    messageTitle = "Packet Size Error";
                    messageText = "Your packet size cannot go above 65500. Please check and try again";
                    break;
            }

            MessageBox.Show(messageText, messageTitle, button, icon);
        }
    }
}
