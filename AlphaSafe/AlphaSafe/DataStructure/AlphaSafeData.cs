using System;
using System.Collections.Generic;
using System.Text;

using AlphaSafe.SystemSpecific.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlphaSafe.UI.DataStructure
{
    public class AlphaSafeData
    {
        public static bool isMobile = false;

        public static List<Profile> profileList = new List<Profile>();

        #region helper functions

        private static string profilesFile = "profiles";
        public async static Task ReadProfileList()
        {
            Debug.WriteLine("AlphaSafeData > ReadProfileList");

            try
            {
                string jsonProfiles = await FileReaderWriter.ReadFile(profilesFile);

                if (jsonProfiles != "")
                    profileList = JsonConvert.DeserializeObject<List<Profile>>(jsonProfiles);

                Debug.WriteLine("AlphaSafeData > ReadProfileList > succeed\n"+jsonProfiles);
            }
            catch(Exception e)
            {
                Debug.WriteLine("!!! Exception !!!\nAlphaSafeData > ReadProfileList\n"+e.Message);
            }
        }

        public async static Task WriteProfileList()
        {
            Debug.WriteLine("AlphaSafeData > WriteProfileList");

            string jsonProfiles = JsonConvert.SerializeObject(profileList);

            await FileReaderWriter.WriteFile(profilesFile, jsonProfiles);
        }

        #endregion
    }
}
