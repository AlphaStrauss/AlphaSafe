using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaSafe.Core.DataStructure
{
    public class SafeNodeNotice : SafeNode
    {
        public string encryptedNotice;
        public SafeNodeNotice() : base()
        {
            type = SafeNodeType.Notice;
        }

        public void SetNotice(string masterpassword, string notice)
        {
            // @todo: encrypt notice
            encryptedNotice = notice;
        }
    }
}
