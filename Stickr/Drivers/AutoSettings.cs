using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Windows.Markup;
using System.Reflection;
using Windows.ApplicationModel.ConversationalAgent;

namespace Stickr.Drivers
{
    public static class ObjectToListConverter
    {
        public static ObservableCollection<boggleSetting> ConvertObjectToList(object obj)
        {
            ObservableCollection<boggleSetting> fieldValues = new ObservableCollection<boggleSetting>();

            // Get the type of the object
            Type objectType = obj.GetType();

            // Use reflection to get all the fields
            FieldInfo[] fields = objectType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (FieldInfo field in fields)
            {
                // Get the value of each field from the object
                if(field.FieldType == typeof(string)) { continue; }

                object fieldValue = field.GetValue(obj);
                boggleSetting b = fieldValue as boggleSetting;
                fieldValues.Add(b);
            }

            return fieldValues;
        }
    }

    public class BoggleBase
    {
        public string Export()
        {
            return JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        public static AutoSettings Import(string data)
        {
            AutoSettings importedSettings = new();
            if (string.IsNullOrEmpty(data)) return new AutoSettings();
            try
            {
                importedSettings = JsonConvert.DeserializeObject<AutoSettings>(data);
            }
            catch
            {
                
            }
            return importedSettings;
            
        }
        [JsonIgnore]
        private ObservableCollection<boggleSetting> Listo;
        [JsonIgnore]
        public ObservableCollection<boggleSetting> GetSettings
        {
            get { Listo = ObjectToListConverter.ConvertObjectToList(this); return Listo; }
            set { Listo = value; }
        }
    
    }
    public class AutoSettings : BoggleBase
    {
        public string Name { get; set; } = "Thermal Settings";

        public boolSet Power = new boolSet { Name = "Power", Units = "", Value = false };
        public intSet Temperature = new intSet { Name = "Temperature", Units = "F", Value = 0 };
        public optionSet Mode = new optionSet { Name = "Mode", Value = "Manual", Options = { "Remote", "Automatic", "Manual" } };
        public optionSet Cats = new optionSet { Name = "Cat Name", Value = "Toodles", Options = new List<string> { "Toodles", "Jeff", "Stripey" } };

    }

    public class TimeSettings : BoggleBase
    {
        public string Name { get; private set; } = "Time Settings";


        public optionSet Time = new optionSet { Name = "Time", Value = "Now", Options = new List<string> { "Now", "Later", "Much Later" } };
        public optionSet Head = new optionSet { Name = "Head", Value = "Batman", Options = new List<string> { "Batman", "Darth Vader", "Knight" } };

    }

    public interface boggleSetting
    {
        public string Name { get; set; }
        public string Units { get; set; }

    }


    public class boolSet : boggleSetting
    {
        [JsonIgnore]
        public string Name { get; set; }

        [JsonIgnore]
        public string Units { get; set; }
        public bool Value { get; set; }
    }

    public class intSet : boggleSetting
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public string Units { get; set; }
        
        public int Value { get; set; }
    }

    public class stringSet : boggleSetting
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonIgnore]
        public string Units { get; set; }
        public string Value { get; set; }
    }

    public class optionSet : boggleSetting
    {
        [JsonIgnore]
        public string Name { get; set; } = "";
        [JsonIgnore]
        public string Units { get; set; } = "";
        public string Value { get; set; } = "";
        [JsonIgnore]
        public List<string> Options = new List<string>();
    }
}
