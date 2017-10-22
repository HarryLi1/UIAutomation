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
using CodedUITestProject1.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium;
using CodedUITestProject1.WebElement;
using CodedUITestProject1.WebElement2;//引用Selenium

namespace CodedUITestProject1
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    //[TestClass]
    public class CodedUITest1
    {
        public CodedUITest1()
        {
        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void CodedUITestMethod1()
        {
            for (int i = 0; i < 10; i++)
            {
                //使用默认浏览器（IE）打开指定地址
                MyBrowserWindow myBW = new MyBrowserWindow();
                myBW.LaunchUrl(new System.Uri("http://url.cn/51X5yCO"));
                //点击“加入多人聊天”按钮
                myBW.TalkDocument.BtnJoinTalk.WaitForControlExist();
                Mouse.Click(myBW.TalkDocument.BtnJoinTalk);
                //关闭浏览器
                myBW.Close();
                myBW.WaitForControlNotExist();

                //获取聊天对话框
                QQTalkWindow window = new QQTalkWindow();
                //获取工具条按钮
                WinSplitButton btnJoinTalk = window.TalkToolBar.BtnJoinTalk;
                //点击“邀请加入多人聊天”
                Mouse.Click(btnJoinTalk);

                //获取弹出窗口
                JoinTalkWindow jtWin = new JoinTalkWindow();
                //等待弹出窗口显示完毕
                jtWin.JoinTalkMenu.JoinTalkItem.WaitForControlExist();
                //点击“复制邀请链接”
                Mouse.Click(jtWin.JoinTalkMenu.JoinTalkItem);
                Thread.Sleep(1000);

                //从黏贴板中读取新邀请链接
                string url;
                string linkContent = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(linkContent) && linkContent.StartsWith("点击链接加入多人聊天"))
                {
                    linkContent = Clipboard.GetText().Split("\r\n".ToCharArray())[1];
                }
                //保存新Url,http://url.cn/5rUmF4D

                //关闭讨论组标签
                Keyboard.SendKeys(window, KeyboardKeys.ESC);
            }

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
