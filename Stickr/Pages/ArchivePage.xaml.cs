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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

using Stickr.Pages;
using Stickr.Drivers;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Stickr.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ArchivePage : Page
    {
        ObservableCollection<fieldItem> ActiveFields { get; set; }
        public ArchivePage()
        {
            this.InitializeComponent();
            ActiveFields = new ObservableCollection<fieldItem>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ActiveFields.Add(new fieldItem() { name = "asdf", text = "cooool text" });
        }
    }
}
