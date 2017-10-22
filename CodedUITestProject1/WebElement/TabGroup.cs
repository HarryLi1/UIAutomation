using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class TabGroup:WinTabList
        
    {
        public TabGroup(UITestControl container)
            : base(container)
        {

        }

        public WinButton BtnNewPage
        {
            get
            {
                if ((this.btnNewPage == null))
                {
                    this.btnNewPage = new WinButton(this);

                    this.btnNewPage.SearchProperties[WinButton.PropertyNames.Name] = "新标签页";
                }
                return this.btnNewPage;
            }

        }

        public TabPage TalkTabPage
        {
            get
            {
                if ((this.talkTabPage == null))
                {
                    this.talkTabPage = new TabPage(this);
                }
                return this.talkTabPage;
            }

        }

        private WinButton btnNewPage;
        private TabPage talkTabPage;
    }
}
