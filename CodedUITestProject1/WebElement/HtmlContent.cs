using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class HtmlContent : BrowserWindow
    {
        public HtmlContent()
        {
            #region Search Criteria
            this.SearchProperties[UITestControl.PropertyNames.ClassName] = "TabWindowClass";
            #endregion
        }

        public HtmlDiv EnterButton
        {
            get
            {
                if(this.enterButton == null)
                {
                    this.enterButton = new HtmlDiv(this);
                    this.enterButton.SearchProperties[HtmlButton.PropertyNames.Id] = "enterButton";
                }

                return this.enterButton;
            }
        }

        private HtmlDiv enterButton;
    }
}
