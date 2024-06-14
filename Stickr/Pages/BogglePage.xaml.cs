using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Stickr.Drivers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Stickr.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BogglePage : Page
    {
        public BogglePage()
        {
            this.InitializeComponent();
        }

        public AutoSettings SettingsBox = new AutoSettings();
        public TimeSettings timerSettings = new TimeSettings();

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            JsonFormat.Text = SettingsBox.Export();
        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            SettingsBox = AutoSettings.Import(JsonFormat.Text);
            //SettingsViewer.ItemsSource = null;
            //SettingsViewer.ItemsSource = SettingsBox.GetSettings;
            SettingsOne.DataContext = null;
            SettingsOne.DataContext = SettingsBox;
        }
    }


    public class BoggleSelector : DataTemplateSelector
    {
        public DataTemplate boolTemplate { get; set; }
        public DataTemplate intTemplate { get; set; }
        public DataTemplate optionTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is intSet)
                return intTemplate;
            if (item is boolSet)
                return boolTemplate;
            if (item is optionSet)
                return optionTemplate;

            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }

    }
}
