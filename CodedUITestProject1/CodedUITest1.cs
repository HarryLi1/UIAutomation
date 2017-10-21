using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
using MouseButtons = System.Windows.Forms.MouseButtons;
using StudyUITtest.WinElement;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System.Threading;
using CodedUITestProject1.WinElement;


namespace CodedUITestProject1
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            QQTalkWindow window = new QQTalkWindow();
            WinSplitButton btn = window.TalkToolBar.BtnJoinTalk;
            Mouse.Click(btn);

            JoinTalkWindow jtWin = new JoinTalkWindow();
            jtWin.JoinTalkMenu.JoinTalkItem.WaitForControlExist();
            Mouse.Click(jtWin.JoinTalkMenu.JoinTalkItem);
            Thread.Sleep(1000);
            string url;

            string linkContent = Clipboard.GetText();
            if (string.IsNullOrWhiteSpace(linkContent) && linkContent.StartsWith("点击链接加入多人聊天"))
            {
                linkContent = Clipboard.GetText().Split("\r\n".ToCharArray())[1];
            }
            //http://url.cn/5rUmF4D

        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
