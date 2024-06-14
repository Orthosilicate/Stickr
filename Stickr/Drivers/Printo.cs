using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bpac;
using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;

namespace Stickr.Drivers
{
    class Printo
    {
        public static void print(String Text,String Field)
        {
            string templatePath = @"C:\Users\Sam\Desktop\Printer Maker\BigText.lbx";
            bpac.Document doc = new bpac.Document();
            if (doc.Open(templatePath) != false)
            {
                doc.GetObject(Field).Text = Text;
                //doc.GetObject(Field).SetFontName

                // doc.SetMediaById(doc.Printer.GetMediaId(), true);
                doc.StartPrint("", PrintOptionConstants.bpoDefault);
                doc.PrintOut(1, PrintOptionConstants.bpoDefault);
                doc.EndPrint();
                doc.Close();
            }
            else
            {
            }
        }

        public static sticker import(String Text)
        {
            return  new sticker();
        }

        public static string export(sticker Sticker)
        {
            return JsonConvert.SerializeObject(Sticker, Newtonsoft.Json.Formatting.Indented);
        }


        public static  void print(sticker Sticker,ObservableCollection<fieldItem> Fields)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string templatePath = path + @"\Stickr\" + Sticker.File;
            bpac.Document doc = new bpac.Document();
            if (doc.Open(templatePath) != false)
            {
                try
                {

                    foreach (fieldItem item in Fields)
                    {
                        doc.GetObject(item.name).Text = item.text;
                    }
                    //doc.GetObject(Field).SetFontName

                    // doc.SetMediaById(doc.Printer.GetMediaId(), true);
                    doc.StartPrint("", PrintOptionConstants.bpoDefault);
                    doc.PrintOut(1, PrintOptionConstants.bpoDefault);
                    doc.EndPrint();
                    doc.Close();

                }
                catch
                {

                }

            }
            else
            {
            }
        }
    }


    public class fieldItem
    {
        public string name;
        public string text;
    }
    public class sticker
    {
        public string File;
        public string Name;
        public ObservableCollection<fieldItem> fields;

        public override string ToString()
        {
            return Name;
        }
    }
    public static class StickerBox
    {
        public static List<sticker> stickers;
        public static void Make()
        {
            ObservableCollection<fieldItem> tempFields = new ObservableCollection<fieldItem>();
            stickers = new List<sticker>();

            //Normal Text
            //=========================================
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Text",
                text = "",
            });
            stickers.Add(new sticker()
            {
                File = @"NormalLabel.lbx",
                Name = "Normal Sticker",
                fields = new ObservableCollection<fieldItem>(tempFields),

            });

            //Long Text
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Text",
                text = "",
            });
            stickers.Add(new sticker()
            {
                File = @"BigText.lbx",
                Name = "Big-Ass Sticker",
                fields = new ObservableCollection<fieldItem>(tempFields),

            });


            //QR Code
            //=========================================
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Header",
                text = "",
            });
            tempFields.Add(new fieldItem()
            {
                name = "QR Code",
                text = "",
            });
            stickers.Add(new sticker()
            {
                File = @"QRCode.lbx",
                Name = "QR Code",
                fields = new ObservableCollection<fieldItem>(tempFields)

            });

            //Barcode
            //=========================================
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Barcode Data",
                text = "",
            });
            stickers.Add(new sticker()
            {
                File = @"Barcode.lbx",
                Name = "Barcode Text",
                fields = new ObservableCollection<fieldItem>(tempFields)

            });

            //Normal Text
            //=========================================
            tempFields.Clear();

            tempFields.Add(new fieldItem()
            {
                name = "Item",
                text = "",
            }); tempFields.Add(new fieldItem()
            {
                name = "Number",
                text = "",
            });
   
            tempFields.Add(new fieldItem()
            {
                name = "Owner",
                text = "",
            });
            tempFields.Add(new fieldItem()
            {
                name = "Details",
                text = "",
            });
            stickers.Add(new sticker()
            {
                File = @"BoxLabel.lbx",
                Name = "Asset Tag",
                fields = new ObservableCollection<fieldItem>(tempFields),

            });

        }
        public static void Import()
        {

        }
    }

}
