using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement
{
    public class QQInput : WinEdit
    {
        public QQInput(UITestControl container)
            : base(container)
        {

        }

        public WinEdit BtnJoinTalk
        {
            get
            {
                if ((this.btnJoinTalk == null))
                {
                    this.btnJoinTalk = new WinEdit(this);
                    this.SearchProperties[WinWindow.PropertyNames.Name] = "输入";
                    #region Search Criteria
                    #endregion
                }
                return this.btnJoinTalk;
            }
        }
        private WinEdit btnJoinTalk;
    }
}
