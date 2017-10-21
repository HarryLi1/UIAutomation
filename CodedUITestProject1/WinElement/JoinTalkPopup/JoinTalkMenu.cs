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
            #region Search Criteria
            this.SearchProperties[WinMenu.PropertyNames.Name] = "TXMenuWindow";
            #endregion
        }

        public WinMenuItem JoinTalkItem
        {
            get
            {
                if (this.joinTalkItem == null)
                {
                    this.joinTalkItem = new WinMenuItem(this);

                    #region Search Criteria
                    this.joinTalkItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "复制邀请链接";
                    #endregion
                }

                return this.joinTalkItem;
            }


        }
        private WinMenuItem joinTalkItem;
    }
}
