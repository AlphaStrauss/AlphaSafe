using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AlphaSafe.SystemSpecific.Utils
{
    public class FileReaderWriter
    {
        public static async Task WriteFile(string file, string content)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile localfile = await localfolder.CreateFileAsync(file, CreationCollisionOption.OpenIfExists);

            await FileIO.WriteTextAsync(localfile, content);
        }

        public static async Task<string> ReadFile(string file)
        {
            string output = "";

            try
            {
                StorageFolder localfolder = ApplicationData.Current.LocalFolder;
                StorageFile localfile = await localfolder.GetFileAsync(file);

                output = await FileIO.ReadTextAsync(localfile);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception in AlphaSafe.UWP.Utils.FileReaderWriter/FileReaderWriter.ReadFile\nException-Message: " + e.Message);
            }

            return output;
        }
    }
}
