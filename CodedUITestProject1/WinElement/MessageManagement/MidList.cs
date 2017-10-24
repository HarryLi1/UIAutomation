using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement.MessageManagement
{
    public class MidList : WinList
    {
        public MidList(UITestControl container)
            : base(container)
        {
            this.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);
        }

        public WinListItem Next()
        {
            return (WinListItem)this.GetChildren()[currentIndex++];
        }

        public bool HasNext()
        {
            return currentIndex < this.GetChildren().Count;
        }

        private int currentIndex = 0;
    }
}
