using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class TabPage : WinTabPage
    {
        public TabPage(UITestControl container)
            :base(container)
        {
            this.SearchProperties[WinTabPage.PropertyNames.Name] = "加入多人聊天";
        }

        public WinButton BtnClose
        {
            get
            {
                if (this.btnClose == null)
                {
                    this.btnClose = new WinButton(this);

                    this.btnClose.SearchProperties[WinButton.PropertyNames.Name] = "关闭";
                }

                return this.btnClose;
            }
        }

        private WinButton btnClose;
    }
}
