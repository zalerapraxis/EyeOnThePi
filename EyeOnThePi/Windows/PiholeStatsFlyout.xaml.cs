using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using EyeOnThePi.Models;

namespace EyeOnThePi
{
    /// <summary>
	/// Interaction logic for PiholeStatsFlyout.xaml
	/// </summary>
	public partial class PiholeStatsFlyout : Window
    {
        private App GlobalApp = ((App) Application.Current);

        public PiholeStatsFlyout()
        {
            DataContext = GlobalApp.piholeApiStatsViewModel;

            InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            AcrylicHelper.EnableBlur(this);

			Left = SystemParameters.WorkArea.Width - Width;
			Top = SystemParameters.WorkArea.Height - Height;

			// come back to this later and see if things work properly with it
            Activate();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
			Close();
        }
    }
}
