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
            Console.WriteLine("[{0}] JoinTalkWindow()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "TXMenuWindow";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            #endregion
        }

        public JoinTalkMenu JoinTalkMenu
        {
            get
            {
                Console.WriteLine("[{0}] JoinTalkMenu", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
