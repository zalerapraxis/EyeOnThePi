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
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		[DllImport("user32.dll")]
		internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

		PiholeApiStatsViewModel piholeApiStatsViewModel = new PiholeApiStatsViewModel();

		public MainWindow()
        {
            DataContext = piholeApiStatsViewModel;

            piholeApiStatsViewModel.StartTimer();

			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
            EnableBlur();
			Left = SystemParameters.WorkArea.Width - Width;
			Top = SystemParameters.WorkArea.Height - Height;

			// come back to this later and see if things work properly with it
            Activate();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
			Close();
        }

        
		internal void EnableBlur()
		{
			var windowHelper = new WindowInteropHelper(this);

            uint _blurBackgroundColor = 0x009900;
			uint _blurOpacity = 0;

			var accent = new AccentPolicy();
			accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF);

			var accentStructSize = Marshal.SizeOf(accent);

			var accentPtr = Marshal.AllocHGlobal(accentStructSize);
			Marshal.StructureToPtr(accent, accentPtr, false);

			var data = new WindowCompositionAttributeData();
			data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
			data.SizeOfData = accentStructSize;
			data.Data = accentPtr;

			SetWindowCompositionAttribute(windowHelper.Handle, ref data);

			Marshal.FreeHGlobal(accentPtr);
		}

    }

    // ---- acrylic blur code ---- //
    // https://github.com/riverar/sample-win32-acrylicblur

    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
        ACCENT_INVALID_STATE = 5
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public uint AccentFlags;
        public uint GradientColor;
        public uint AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }
}
