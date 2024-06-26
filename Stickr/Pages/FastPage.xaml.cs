// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Stickr.Drivers;
using Windows.System;
using System.Xml.Linq;
using System.Collections.ObjectModel;
 // To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Stickr.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FastPage : Page
    {

        public sticker ActiveSticker { get; set; }
        public ObservableCollection<fieldItem> ActiveFields { get; set; }
        public sticker ActiveStickerBind {
            get { return ActiveSticker; }
            set { value =  ActiveSticker; }
        }


        public FastPage()
        {
            StickerBox.Make();
            this.InitializeComponent();
            ActiveSticker = new sticker();
            ActiveFields = new ObservableCollection<fieldItem>();


            StickerSelect.SelectedIndex = 0;
        }

        private async void printClick(object sender, RoutedEventArgs e)
        {

                ContentDialog noWifiDialog = new ContentDialog()
                {
                    Title = "Fields",
                    Content = "Printint Stuff",
                    CloseButtonText = "Ok",
                    XamlRoot = this.XamlRoot
                };
                await noWifiDialog.ShowAsync();

            Printo.print(ActiveSticker,ActiveFields);
        }


        private void jsonPrint(object sender, RoutedEventArgs e)
        {

            Printo.print(ActiveSticker, ActiveFields);
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var y = FontCombo.SelectedItem as TextBlock;
            if (y != null)
            {
                FontCombo.FontFamily = y.FontFamily;
            }
        }

        private void StickerSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StickerSelect.SelectedItem == null) return;
            sticker selected = StickerSelect.SelectedItem as sticker;
            ActiveSticker = selected;
            ActiveFields.Clear();
            foreach (fieldItem f in ActiveSticker.fields)
            {
                ActiveFields.Add(f);
            }
        }

        private void Serialize_Click(object sender, RoutedEventArgs e)
        {
            JsonFormat.Text = Printo.export(ActiveSticker);
        }
    }
}
