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
                foreach (field item in Sticker.fields)
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


    public class field
    {
        public string name;
        public string text;
    }
    public class sticker
    {
        public string File;
        public string Name;
        public ObservableCollection<field> fields;

        public override string ToString()
        {
            return Name;
        }
    }
    public class StickerTypes
    {
        public static List<sticker> stickers = new List<sticker>();
        public static void Make()
        {
            ObservableCollection<field> tempFields = new ObservableCollection<field>();
            stickers.Clear();

            //Long Text
            tempFields.Clear();
            tempFields.Add(new field()
            {
                name = "Text",
                text = "Text1",
            });
            stickers.Add(new sticker()
            {
                File = @"BigText.lbx",
                Name = "Long-Ass Sticker",
                fields = new ObservableCollection<field>(tempFields),

            });


            //Small Text
            //=========================================
            tempFields.Clear();
            tempFields.Add(new field()
            {
                name = "Text",
                text = "Text1",
            });
            stickers.Add(new sticker()
            {
                File = @"Normal.lbx",
                Name = "Small Sticker",
                fields = new ObservableCollection<field>(tempFields),

            });


            //QR Code
            //=========================================
            tempFields.Clear();
            tempFields.Add(new field()
            {
                name = "Header",
                text = "Text1",
            });
            tempFields.Add(new field()
            {
                name = "QRCode",
                text = "Text2",
            });
            stickers.Add(new sticker()
            {
                File = @"QR_Code.lbx",
                Name = "QR Code",
                fields = new ObservableCollection<field>(tempFields)

            });
        }
    }

}
