using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using bpac;
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

        public static void print(sticker Sticker)
        {
            string templatePath = @"%userprofile%\Documents\Stickr\" + Sticker.File;
            bpac.Document doc = new bpac.Document();
            if (doc.Open(templatePath) != false)
            {
                foreach (fieldItem item in Sticker.fields)
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
            else
            {
            }
        }
        //public static List<sticker> stickerList;
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

            //Long Text
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Text",
                text = "Text1",
            });
            stickers.Add(new sticker()
            {
                File = @"BigText.lbx",
                Name = "Long-Ass Sticker",
                fields = new ObservableCollection<fieldItem>(tempFields),

            });


            //Small Text
            //=========================================
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Text",
                text = "Text1",
            });
            stickers.Add(new sticker()
            {
                File = @"Normal.lbx",
                Name = "Small Sticker",
                fields = new ObservableCollection<fieldItem>(tempFields),

            });


            //QR Code
            //=========================================
            tempFields.Clear();
            tempFields.Add(new fieldItem()
            {
                name = "Header",
                text = "Text1",
            });
            tempFields.Add(new fieldItem()
            {
                name = "QRCode",
                text = "Text2",
            });
            stickers.Add(new sticker()
            {
                File = @"QR_Code.lbx",
                Name = "QR Code",
                fields = new ObservableCollection<fieldItem>(tempFields)

            });
        }
    }

}
