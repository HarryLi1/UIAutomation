using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement.JoinTalkPopup
{
    public class JoinTalkMenu : WinMenu
    {
        public JoinTalkMenu(UITestControl container)
            : base(container)
        {
            Console.WriteLine("[{0}] JoinTalkMenu()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region Search Criteria
            this.SearchProperties[WinMenu.PropertyNames.Name] = "TXMenuWindow";
            #endregion
        }

        public WinMenuItem JoinTalkItem
        {
            get
            {
                Console.WriteLine("[{0}] JoinTalkItem start", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (this.joinTalkItem == null)
                {
                    this.joinTalkItem = new WinMenuItem(this);

                    #region Search Criteria
                    this.joinTalkItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "复制邀请链接";
                    #endregion
                }
                Console.WriteLine("[{0}] JoinTalkItem end", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                return this.joinTalkItem;
            }
        }
        private WinMenuItem joinTalkItem;
    }
}
