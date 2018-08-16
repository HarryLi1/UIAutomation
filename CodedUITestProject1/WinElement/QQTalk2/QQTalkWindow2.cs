using CodedUITestProject1.WinElement;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyUITtest.WinElement
{
    public class QQTalkWindow2 : WinWindow
    {
        public QQTalkWindow2()
        {
            #region Search Criteria
            //this.SearchProperties[WinWindow.PropertyNames.Name] = "";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            #endregion
        }

        public UIItemPane TalkToolBar
        {
            get
            {
                if (this.talkToolBar == null)
                {
                    this.talkToolBar = new UIItemPane(this);
                }

                return this.talkToolBar;
            }
        }


        private UIItemPane talkToolBar;

    }

    public class UIItemPane : WinPane
    {
        public UIItemPane(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            //this.WindowTitles.Add("携程-长隆对接等5个会话");
            #endregion
        }

        #region Properties
        public WinButton MoreInfoButton
        {
            get
            {
                if ((this.moreInfoButton == null))
                {
                    this.moreInfoButton = new WinButton(this);
                    #region Search Criteria
                    this.moreInfoButton.SearchProperties[WinButton.PropertyNames.Name] = "邀请加入多人聊天";
                    //this.mUI邀请加入多人聊天Button.WindowTitles.Add("携程-长隆对接等5个会话");
                    #endregion
                }
                return this.moreInfoButton;
            }
        }
        #endregion

        private WinButton moreInfoButton;

    }

    public class UIMenuWindow : WinWindow
    {
        public UIMenuWindow()
        {
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "TXMenuWindow";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            this.WindowTitles.Add("TXMenuWindow");
            #endregion
        }

        #region Properties
        public UIMenu UIMenu
        {
            get
            {
                if ((this.uiMenu == null))
                {
                    this.uiMenu = new UIMenu(this);
                }
                return this.uiMenu;
            }
        }
        #endregion

        #region Fields
        private UIMenu uiMenu;
        #endregion
    }

    public class UIMenu : WinMenu
    {
        public UIMenu(UITestControl searchLimitContainer) :
            base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WinMenu.PropertyNames.Name] = "TXMenuWindow";
            this.WindowTitles.Add("TXMenuWindow");
            #endregion
        }

        #region Properties
        public WinMenuItem CopyLinkMenuItem
        {
            get
            {
                if ((this.copyLinkMenuItem == null))
                {
                    this.copyLinkMenuItem = new WinMenuItem(this);
                    #region Search Criteria
                    this.copyLinkMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "复制邀请链接";
                    this.copyLinkMenuItem.WindowTitles.Add("TXMenuWindow");
                    #endregion
                }
                return this.copyLinkMenuItem;
            }
        }
        #endregion

        #region Fields
        private WinMenuItem copyLinkMenuItem;
        #endregion
    }
}
