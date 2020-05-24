using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace EyeOnThePi
{
    /// <summary>
    /// Provides bindable properties and commands for the NotifyIcon. In this sample, the
    /// view model is assigned to the NotifyIcon in XAML. Alternatively, the startup routing
    /// in App.xaml.cs could have created this view model, and assigned it to the NotifyIcon.
    /// </summary>
    public class NotifyIconViewModel
    {
        private readonly string _autostartKey = "EyeOnThePi";

        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommandHelper()
                {
                    CanExecuteFunc = () => Application.Current.MainWindow == null,
                    CommandAction = () =>
                    {
                        Application.Current.MainWindow = new PiholeStatsFlyout();
                        Application.Current.MainWindow.Show();
                    }
                };
            }
        }

        public ICommand HideWindowCommand
        {
            get
            {
                return new DelegateCommandHelper
                {
                    CommandAction = () => Application.Current.MainWindow.Close(),
                    CanExecuteFunc = () => Application.Current.MainWindow != null
                };
            }
        }

        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommandHelper { CommandAction = () => Application.Current.Shutdown() };
            }
        }

        public ICommand ShowSettingsWindowCommand
        {
            get
            {
                return new DelegateCommandHelper
                {
                    CommandAction = () =>
                    {
                        var settingsWindow = new Settings();
                        settingsWindow.Show();
                    }
                };
            }
        }

        private bool autoStart;
        public bool AutoStart
        {
            get
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                return key.GetValue(_autostartKey) != null;
            }
            set
            {
                MessageBox.Show("yo");
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (value && key.GetValue(_autostartKey) == null)
                {
                    key.SetValue(_autostartKey, Path.ChangeExtension(AppDomain.CurrentDomain.BaseDirectory, "exe"));
                }
                else if (!value && key.GetValue(_autostartKey) != null)
                {
                    key.DeleteValue(_autostartKey);
                }
            }
        }
    }
}