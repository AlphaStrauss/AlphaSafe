using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace AlphaSafe.SystemSpecific.Utils
{
    public class ClipboardService
    {
        public static void CopyToClipboard(string text)
        {
            DataPackage content = new DataPackage();
            content.SetText(text);

            Clipboard.SetContent(content);
        }
    }
}
