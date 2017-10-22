using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class ChromeWindow : WinWindow
    {
        public ChromeWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "IEFrame";// "Chrome_WidgetWin_1";
            #endregion
        }

        public NavigatorBar UrlBar
        {
            get
            {
                if (this.urlBar == null)
                {
                    this.urlBar = new NavigatorBar(this);
                }

                return this.urlBar;
            }
        }
        public TabGroup Tabs
        {
            get
            {
                if (this.tabs == null)
                {
                    this.tabs = new TabGroup(this);
                }

                return this.tabs;
            }
        }

        private NavigatorBar urlBar;
        private TabGroup tabs;
    }
}
