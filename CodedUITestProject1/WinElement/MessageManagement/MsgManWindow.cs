using Microsoft.VisualStudio.TestTools.UITest.Extension;
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

        public void display(UITestControl control, int level, int index)
        {
            int spaceCount = level * 4;
            if (level >= 10) return;
            Console.WriteLine(new string(' ', spaceCount) + index + ":" + control.GetType().FullName);
            Console.WriteLine(new string(' ', spaceCount) + index + ":" + "Name:" + control.Name + "; ClassName:" + control.ClassName + "; ControlType:" + control.ControlType + "; WindowTitles:" + control.WindowTitles);
            Console.WriteLine(new string(' ', spaceCount) + index + ":" + "Has child:" + control.GetChildren().Count);

            int i = 0;
            foreach (var child in control.GetChildren())
            {
                display(child, level + 1, ++i);
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

        public WinList MidList
        {
            get
            {
                if (this.midList == null)
                {

                    this.midList = (WinList)this.GetChildren()[3].GetChildren()[1].GetChildren()[0].GetChildren()[1].GetChildren()[0].GetChildren()[1].GetChildren()[0];
                }

                return this.midList;
            }
        }

        public bool HasNext(WinList list, int index)
        {
            return index < list.GetChildren().Count - 1;
        }

        public WinListItem Next(WinList list, int index)
        {
            return (WinListItem)list.GetChildren()[index];
        }

        public void ScrolToTop(WinList list)
        {
            WinScrollBar bar = new WinScrollBar(list);
            Mouse.MoveScrollWheel(bar, 3);
        }

        private MsgManTabs msgManTabs;
        private LeftList leftList;
        private WinList midList;
    }
}
