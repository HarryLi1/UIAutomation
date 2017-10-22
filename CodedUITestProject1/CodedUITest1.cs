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
using CodedUITestProject1.DAO;
using CodedUITestProject1.Entity;
using System.Diagnostics;//引用Selenium

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

        [ClassInitialize()]
        public static void init(TestContext context)
        {
            //判断QQ是否登录
            var processes = Process.GetProcessesByName("QQ");
            if (processes == null || processes.Length == 0)
            {
                Assert.Fail("未启动QQ，请先启动QQ");
            }
        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void CodedUITestMethod1()
        {
            DiscussGroupLinkService service = new DiscussGroupLinkService();
            List<DiscussGroupLink> list = service.getUnprocessedData();
            for (int i = 0; i < list.Count; i++)
            {
                string oldLink = list[i].OldLink;
                //使用默认浏览器（IE）打开指定地址
                JoinTalkWebPage jtPage = new JoinTalkWebPage();
                jtPage.LaunchUrl(new System.Uri(oldLink));
                //点击“加入多人聊天”按钮
                jtPage.JoinTalkDoc.BtnJoinTalk.WaitForControlExist();
                Mouse.Click(jtPage.JoinTalkDoc.BtnJoinTalk);
                //关闭浏览器
                jtPage.Close();
                jtPage.WaitForControlNotExist();

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
                string newLink = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(newLink) && newLink.StartsWith("点击链接加入多人聊天"))
                {
                    newLink = Clipboard.GetText().Split("\r\n".ToCharArray())[1];
                }

                //关闭讨论组标签
                Keyboard.SendKeys(window, KeyboardKeys.ESC);

                //保存新Url,http://url.cn/5rUmF4D
                service.UpdateData(list[i].ID, newLink);
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
