using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement
{
    public class QQTalkToolBar : WinToolBar
    {
        public QQTalkToolBar(UITestControl container)
            : base(container)
        {

        }

        public WinSplitButton BtnJoinTalk
        {
            get
            {
                if ((this.btnJoinTalk == null))
                {
                    this.btnJoinTalk = new WinSplitButton(this);

                    #region Search Criteria
                    #endregion
                }
                return this.btnJoinTalk;
            }
        }
        private WinSplitButton btnJoinTalk;
    }
}
