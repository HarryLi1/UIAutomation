using CodedUITestProject1.WinElement;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyUITtest.WinElement
{
    public class QQTalkWindow : WinWindow
    {
        public QQTalkWindow()
        {
            #region Search Criteria
            //this.SearchProperties[WinWindow.PropertyNames.Name] = "";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            #endregion
        }

        public QQTalkToolBar TalkToolBar
        {
            get
            {
                if (this.talkToolBar == null)
                {
                    this.talkToolBar = new QQTalkToolBar(this);
                }

                return this.talkToolBar;
            }
        }

        public QQInput Input
        {
            get
            {
                if (this.input == null)
                {
                    this.input = new QQInput(this);
                }

                return this.input;
            }
        }

        private QQTalkToolBar talkToolBar;
        private QQInput input;

    }
}
