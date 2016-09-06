using AlphaStrauss.AlphaCloudApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.UI.DataStructure
{
    public class Profile
    {
        public string profileName;
        public string password { get; set; }

        public CloudType cloud;

        public Profile(string name, string password, CloudType cloud)
        {
            this.profileName = name;
            this.password = password;
            this.cloud = cloud;
        }
    }
}
