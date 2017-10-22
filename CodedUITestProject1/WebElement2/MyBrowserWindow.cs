using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement2
{
    public class MyBrowserWindow : BrowserWindow
    {
        public MyBrowserWindow()
        {
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "IEFrame";
        }

        public void LaunchUrl(System.Uri url)
        {
            this.CopyFrom(BrowserWindow.Launch(url));
        }

        public TestDocument TalkDocument
        {
            get
            {
                if (this.talkDocument == null)
                {
                    this.talkDocument = new TestDocument(this);
                }

                return this.talkDocument;
            }
        }
        private TestDocument talkDocument;
    }
}
