using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class NavigatorBar : WinGroup
    {
        public NavigatorBar(UITestControl container)
            : base(container)
        {

        }

        public WinEdit UrlEdit
        {
            get
            {
                if (this.urlEdit == null)
                {
                    this.urlEdit = new WinEdit(this);

                    this.urlEdit.SearchProperties[PropertyNames.Name] = "地址和搜索栏";
                }

                return this.urlEdit;
            }
        }
        private WinEdit urlEdit;
    }
}
