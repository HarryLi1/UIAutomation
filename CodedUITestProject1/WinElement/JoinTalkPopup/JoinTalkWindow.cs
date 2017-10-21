using CodedUITestProject1.WinElement.JoinTalkPopup;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement
{
    public class JoinTalkWindow : WinWindow
    {
        public JoinTalkWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "TXMenuWindow";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            #endregion
        }

        public JoinTalkMenu JoinTalkMenu
        {
            get
            {
                if (this.joinTalkMenu == null)
                {
                    this.joinTalkMenu = new JoinTalkMenu(this);
                }

                return this.joinTalkMenu;
            }
        }
        private JoinTalkMenu joinTalkMenu;
    }
}
