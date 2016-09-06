using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.DataStructure
{
    public class SafeNodePassword : SafeNode
    {
        public string encryptedPassword;
        
        public SafeNodePassword() : base()
        {
            type = SafeNodeType.Password;
        }

        public void SetPassword(string masterpassword, string password)
        {
            // @todo: encrypt password
            encryptedPassword = password;
        }
    }
}
