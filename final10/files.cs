using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Shop_manager
{
    internal static class FileWorks
    {
        public static void Serialize<T>(T data, string fileName)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(desktop + "\\" + fileName + ".json", json);
        }

        public static T Deserialize<T>(string fileName)
        {

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string json = File.ReadAllText(desktop + "\\" + fileName + ".json");
            T data = JsonConvert.DeserializeObject<T>(json);
            return data;

        }
    }
}