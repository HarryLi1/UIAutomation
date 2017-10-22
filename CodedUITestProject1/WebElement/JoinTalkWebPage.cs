using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class JoinTalkWebPage : BrowserWindow
    {
        public JoinTalkWebPage()
        {
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
        }

        public void LaunchUrl(System.Uri url)
        {
            this.CopyFrom(BrowserWindow.Launch(url));
        }

        public JoinTalkWebDocument JoinTalkDoc
        {
            get
            {
                if (this.joinTalkDoc == null)
                {
                    this.joinTalkDoc = new JoinTalkWebDocument(this);
                }

                return this.joinTalkDoc;
            }
        }
        private JoinTalkWebDocument joinTalkDoc;
    }
}
