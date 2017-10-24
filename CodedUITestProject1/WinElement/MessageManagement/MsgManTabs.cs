using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement.MessageManagement
{
    public class MsgManTabs:WinTabList
    {
        public MsgManTabs(UITestControl container)
            :base(container)
        {
            Console.WriteLine("[{0}] MsgManTabs()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region Search Criteria
            #endregion
        }

        public WinTabPage MultipleTalksTabPage
        {
            get
            {
                if (this.multipleTalksTabPage == null)
                {
                    this.multipleTalksTabPage = new WinTabPage(this);
                    this.multipleTalksTabPage.SearchProperties[WinTabPage.PropertyNames.Name] = "多人聊天";
                }

                return this.multipleTalksTabPage;
            }
        }

        private WinTabPage multipleTalksTabPage;
    }
}
