// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT;
using Stickr.Drivers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Stickr
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public static AppWindow MyAppWindow;
        public string WindowTitle;

        WindowsSystemDispatcherQueueHelper m_wsdqHelper; // See separate sample below for implementation
        Microsoft.UI.Composition.SystemBackdrops.MicaController m_micaController;
        Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController m_acrylicController;
        Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration m_configurationSource;

        public Dictionary<string, Type> _pages = new Dictionary<string, Type>();
        public MainWindow()
        {
            this.InitializeComponent();

            NavPane.IsPaneOpen = false;
            NavPane.IsPaneOpen = true;

            MyAppWindow = AppWindowExtensions.GetAppWindow(this);

            MyAppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            MyAppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;

            _pages.Add("archive", typeof(Pages.ArchivePage));
            _pages.Add("fast", typeof(Pages.FastPage));
            TrySetAcrylicBackdrop();

        }

        private void NavPane_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(_pages["fast"]);
        }

        private void NavPane_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected == true)
            {
                ContentFrame.Navigate(typeof(Pages.ArchivePage), null, new DrillInNavigationTransitionInfo());
            }
            else
            {
                string navItemTag = args.SelectedItemContainer.Tag.ToString();
                ContentFrame.Navigate(_pages[navItemTag], null, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
            }
        }


        bool TrySetAcrylicBackdrop()
        {
            if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
            {
                m_wsdqHelper = new WindowsSystemDispatcherQueueHelper();
                m_wsdqHelper.EnsureWindowsSystemDispatcherQueueController();

                // Hooking up the policy object
                m_configurationSource = new Microsoft.UI.Composition.SystemBackdrops.SystemBackdropConfiguration();
                this.Activated += Window_Activated;
                this.Closed += Window_Closed;
                ((FrameworkElement)this.Content).ActualThemeChanged += Window_ThemeChanged;

                // Initial configuration state.
                m_configurationSource.IsInputActive = true;
                SetConfigurationSourceTheme();

                m_acrylicController = new Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController();

                // Enable the system backdrop.
                // Note: Be sure to have "using WinRT;" to support the Window.As<...>() call.
                m_acrylicController.AddSystemBackdropTarget(this.As<Microsoft.UI.Composition.ICompositionSupportsSystemBackdrop>());
                m_acrylicController.SetSystemBackdropConfiguration(m_configurationSource);
                return true; // succeeded
            }

            return false; // Acrylic is not supported on this system
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            m_configurationSource.IsInputActive = args.WindowActivationState != WindowActivationState.Deactivated;
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            // Make sure any Mica/Acrylic controller is disposed so it doesn't try to
            // use this closed window.
            if (m_acrylicController != null)
            {
                m_acrylicController.Dispose();
                m_acrylicController = null;
            }
            this.Activated -= Window_Activated;
            m_configurationSource = null;
        }

        private void Window_ThemeChanged(FrameworkElement sender, object args)
        {
            if (m_configurationSource != null)
            {
                SetConfigurationSourceTheme();
            }
        }

        private void SetConfigurationSourceTheme()
        {
            switch (((FrameworkElement)this.Content).ActualTheme)
            {
                case ElementTheme.Dark: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Light: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
                case ElementTheme.Default: m_configurationSource.Theme = Microsoft.UI.Composition.SystemBackdrops.SystemBackdropTheme.Dark; break;
            }
        }
    }



    public static class AppWindowExtensions
    {
        public static AppWindow GetAppWindow(this Microsoft.UI.Xaml.Window window)
        {
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
            return GetAppWindowFromWindowHandle(windowHandle);
        }

        private static AppWindow GetAppWindowFromWindowHandle(IntPtr windowHandle)
        {
            WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            return AppWindow.GetFromWindowId(windowId);
        }
    }


}
