using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement.MessageManagement
{
    public class LeftList : WinList

    {
        public LeftList(UITestControl container)
            : base(container)
        {
          
        }

        public WinListItem MyMultipleTalk
        {
            get
            {
                if (this.myMultipleTalk == null)
                {
                    this.myMultipleTalk =(WinListItem)this.GetChildren()[0];
                }

                return this.myMultipleTalk;
            }
        }

        public WinListItem CancelledTalk
        {
            get
            {
                if (this.cancelledTalk == null)
                {
                    this.cancelledTalk = (WinListItem)this.GetChildren()[1];
                }

                return this.cancelledTalk;
            }
        }

        private WinListItem myMultipleTalk;
        private WinListItem cancelledTalk;
    }
}
