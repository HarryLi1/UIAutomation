using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WinElement.MessageManagement
{
    public class MsgManWindow : WinWindow
    {
        public MsgManWindow()
        {
            Console.WriteLine("[{0}] MsgManWindow()", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            #region Search Criteria
            this.SearchProperties[WinWindow.PropertyNames.Name] = "消息管理器";
            this.SearchProperties[WinWindow.PropertyNames.ClassName] = "TXGuiFoundation";
            #endregion
        }

        public void display(UITestControl control, int level)
        {
            if (level >= 8) return;
            Console.WriteLine(new string(' ', level * 2) +  control.GetType().FullName);
            Console.WriteLine(new string(' ', level * 2) + "Has child:" + control.GetChildren().Count);

            foreach (var child in control.GetChildren())
            {
                display(child, level + 1);
            }

        }

        public MsgManTabs MsgManTabs
        {
            get
            {
                if (this.msgManTabs == null)
                {
                    this.msgManTabs = new MsgManTabs(this);
                }

                return this.msgManTabs;
            }
        }

        public LeftList LeftList
        {
            get
            {
                if (this.leftList == null)
                {
                    this.leftList = new LeftList(this);
                }

                return this.leftList;
            }
        }

        public MidList MidList
        {
            get
            {
                if (this.midList == null)
                {
                    this.midList = new MidList(this);
                }
                
                return this.midList;
            }
        }

        private MsgManTabs msgManTabs;
        private LeftList leftList;
        private MidList midList;
    }
}
