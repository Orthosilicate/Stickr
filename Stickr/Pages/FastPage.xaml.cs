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
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Stickr.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FastPage : Page
    {
        public sticker CurrentSticker { get; set; } 
        public sticker StaticSticker 
        {
            get { return CurrentSticker; }   // get method
            set { CurrentSticker = value; }  // set method
        }
        List<sticker> stickerOptions;

        public FastPage()
        {
            this.InitializeComponent();
            StickerTypes.Make();
            StaticSticker = StickerTypes.stickers[0];

            stickerOptions = new List<sticker>();   

            //StaticSticker = StickerTypes.stickers[0];
            addtypes();
        }

        private void addtypes()
        {
            foreach(sticker sti in StickerTypes.stickers)
            {
                stickerOptions.Add(sti);
            }
        }



        private void printClick(object sender, RoutedEventArgs e)
        {


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
            //var index = sender.SelectedItem;
            int t = 2;
        }
    }
}
