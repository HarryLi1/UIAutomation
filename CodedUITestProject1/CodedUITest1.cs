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
using System.Diagnostics;
using CodedUITestProject1.Util;
using System.Web;
using CodedUITestProject1.WinElement.MessageManagement;
using System.IO;//引用Selenium

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
        [DeploymentItem(".\\files\\ShowNotify.exe", ".\\files")]
        public void TC02_RetrieveNewLink()
        {
            DiscussGroupLinkService linkService = new DiscussGroupLinkService();
            List<DiscussGroupLink> list = linkService.getByStatus(EntityStatus.Waiting);
            Console.WriteLine("获得{0}条未处理的DiscussGroupLink", list.Count);

            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                DiscussGroupLink link = list[i];
                Console.WriteLine("处理DiscussGroupLink, ID={0}", link.ID);

                string root = (new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)).Directory.FullName;
                string exeFile = root + "\\files\\ShowNotify.exe";
                Process.Start(exeFile, "1000 " + string.Format("正在处理：{0}/{1}", i, count));

                try
                {
                    linkService.UpdateStatus(link.ID, EntityStatus.Processing, "处理中");
                    string newLink = "";

                    //QQ讨论组
                    if (link.LinkType == "Z")
                    {
                        string oldLink = link.OldLink;

                        //使用默认浏览器（IE）打开指定地址
                        Console.WriteLine("[{0}] 打开IE浏览器", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        JoinTalkWebPage jtPage = new JoinTalkWebPage();
                        jtPage.LaunchUrl(new System.Uri(oldLink));
                        //点击“加入多人聊天”按钮
                        Console.WriteLine("[{0}] 点击“加入多人聊天”按钮", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        jtPage.JoinTalkDoc.BtnJoinTalk.WaitForControlExist();
                        Mouse.Click(jtPage.JoinTalkDoc.BtnJoinTalk);
                        //关闭浏览器
                        Console.WriteLine("[{0}] 关闭浏览器", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        jtPage.Close();
                        jtPage.WaitForControlNotExist();

                        //获取聊天对话框
                        Console.WriteLine("[{0}] 获取聊天对话框", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        QQTalkWindow window = new QQTalkWindow();
                        //获取工具条按钮
                        Console.WriteLine("[{0}] 获取工具条按钮", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        WinSplitButton btnJoinTalk = window.TalkToolBar.BtnJoinTalk;
                        //点击“邀请加入多人聊天”
                        Console.WriteLine("[{0}] 点击“邀请加入多人聊天”", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Mouse.Click(btnJoinTalk);

                        //获取弹出窗口
                        Console.WriteLine("[{0}] 获取弹出窗口", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        JoinTalkWindow jtWin = new JoinTalkWindow();
                        //等待弹出窗口显示完毕
                        Console.WriteLine("[{0}] 等待弹出窗口显示完毕", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        jtWin.JoinTalkMenu.JoinTalkItem.WaitForControlExist();
                        //点击“复制邀请链接”
                        Console.WriteLine("[{0}] 点击“复制邀请链接”", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Mouse.Click(jtWin.JoinTalkMenu.JoinTalkItem);
                        Console.WriteLine("[{0}] 休眠1秒钟", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Thread.Sleep(1000);

                        //从黏贴板中读取新邀请链接
                        Console.WriteLine("[{0}] 从黏贴板中读取新邀请链接", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        newLink = Clipboard.GetText();
                        if (!string.IsNullOrWhiteSpace(newLink) && newLink.StartsWith("点击链接加入多人聊天"))
                        {
                            newLink = Clipboard.GetText().Split("\r\n".ToCharArray())[1];
                        }

                        //Keyboard.SendKeys(window.Input, ":) just for holding this group. sorry to bother.");
                        //Keyboard.SendKeys(window.Input, KeyboardKeys.ENTER);

                        //关闭讨论组标签
                        Console.WriteLine("[{0}] 关闭讨论组标签", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        Keyboard.SendKeys(window, KeyboardKeys.ESC);
                    }
                    else
                    {
                        //QQ群
                    }

                    //保存新Url,http://url.cn/5rUmF4D
                    if (!string.IsNullOrWhiteSpace(newLink))
                    {
                        linkService.UpdateNewLink(link.ID, newLink);
                    }
                    else
                    {
                        linkService.UpdateStatus(link.ID, EntityStatus.Fail, "获取新链接失败");
                    }

                }
                catch (Exception ex)
                {
                    linkService.UpdateStatus(link.ID, EntityStatus.Fail, HttpUtility.UrlEncode(ex.Message));
                }

                //break;
            }

        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void TC01_BreakDownLinks()
        {
            ContactInfoService contactInfoService = new ContactInfoService();
            DiscussGroupLinkService linkService = new DiscussGroupLinkService();

            //清空DiscussGroupLink表
            linkService.DeleleAll();
            //获取待处理ContactInfo记录
            List<ContactInfo> list = contactInfoService.getByStatus(EntityStatus.Waiting);
            Console.WriteLine("获得{0}条未处理的ContactInfo", list.Count);

            foreach (var info in list)
            {
                Console.WriteLine("处理ContactInfo, ID={0}", info.ID);

                //更新:处理中
                contactInfoService.UpdateStatus(info.ID, EntityStatus.Processing, "处理中");

                List<DiscussGroupLink> links = new List<DiscussGroupLink>();

                //获取原字符串中的讨论组
                List<string> links1 = RegexUtil.getMatchedStrings(info.OldValue, RegexUtil.QQTaoLunZuPattern);
                links1.ForEach(x =>
                {
                    if (string.IsNullOrWhiteSpace(x)) return;

                    DiscussGroupLink link = new DiscussGroupLink()
                    {
                        Key = info.Key,
                        OldLink = x,
                        LinkType = "Z",
                        Status = EntityStatus.Waiting,
                        Message = "待处理",
                        NewLink = ""
                    };

                    links.Add(link);
                });

                ////获取原字符串中的QQ群
                List<string> links2 = RegexUtil.getMatchedStrings(info.OldValue, RegexUtil.QQQunPattern);
                links2.ForEach(x =>
                {
                    if (string.IsNullOrWhiteSpace(x)) return;
                    DiscussGroupLink link = new DiscussGroupLink()
                    {
                        Key = info.Key,
                        OldLink = x,
                        LinkType = "Q",
                        Status = EntityStatus.Waiting,
                        Message = "待处理",
                        NewLink = ""
                    };
                    links.Add(link);
                });

                //插入数据库
                linkService.BulkInsert(links);
            }
        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void TC03_ReplaceWithNewLink()
        {
            ContactInfoService contactInfoService = new ContactInfoService();
            DiscussGroupLinkService linkService = new DiscussGroupLinkService();

            //检查是否所有链接都已替换成功
            Assert.IsTrue(linkService.checkAllProcessed(), "链接没有完全替换成功");

            List<ContactInfo> list = contactInfoService.getByStatus(EntityStatus.Processing);
            Console.WriteLine("获得{0}条处理中的ContactInfo", list.Count);

            foreach (var info in list)
            {
                Console.WriteLine("处理ContactInfo, ID={0}", info.ID);

                string newString = info.OldValue;
                List<DiscussGroupLink> links = linkService.getByKey(info.Key);
                foreach (var link in links)
                {
                    newString = newString.Replace(link.OldLink, link.NewLink);
                }

                contactInfoService.UpdateNewValue(info.ID, newString);
            }
        }

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void TC04_GetQQTaoLunZuList()
        {
            OrigionLinkService olService = new OrigionLinkService();

            MsgManWindow mmw = new MsgManWindow();

            Mouse.Click(mmw.MsgManTabs.MultipleTalksTabPage);
            Mouse.Click(mmw.LeftList.MyMultipleTalk);

            WinListItem last = null;
            int i = 0;
            while (mmw.HasNext(mmw.MidList, i))
            {

                if (last != null)
                {
                    Mouse.Click(last);
                    Keyboard.SendKeys(KeyboardKeys.DOWN);
                }

                WinListItem item = mmw.Next(mmw.MidList, i);
                Mouse.DoubleClick(item);

                #region
                //获取聊天对话框
                Console.WriteLine("[{0}] 获取聊天对话框", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                QQTalkWindow window = new QQTalkWindow();
                //获取工具条按钮
                Console.WriteLine("[{0}] 获取工具条按钮", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                WinSplitButton btnJoinTalk = window.TalkToolBar.BtnJoinTalk;
                //点击“邀请加入多人聊天”
                Console.WriteLine("[{0}] 点击“邀请加入多人聊天”", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Mouse.Click(btnJoinTalk);

                //获取弹出窗口
                Console.WriteLine("[{0}] 获取弹出窗口", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                JoinTalkWindow jtWin = new JoinTalkWindow();
                //等待弹出窗口显示完毕
                Console.WriteLine("[{0}] 等待弹出窗口显示完毕", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                jtWin.JoinTalkMenu.JoinTalkItem.WaitForControlExist();
                //点击“复制邀请链接”
                Console.WriteLine("[{0}] 点击“复制邀请链接”", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //Mouse.Click(jtWin.JoinTalkMenu.JoinTalkItem);
                Keyboard.SendKeys(KeyboardKeys.DOWN);
                Keyboard.SendKeys(KeyboardKeys.DOWN);
                Keyboard.SendKeys(KeyboardKeys.ENTER);
                Console.WriteLine("[{0}] 休眠1秒钟", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Thread.Sleep(1000);

                //从黏贴板中读取新邀请链接
                Console.WriteLine("[{0}] 从黏贴板中读取新邀请链接", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                string newLink = Clipboard.GetText();
                if (!string.IsNullOrWhiteSpace(newLink) && newLink.StartsWith("点击链接加入多人聊天"))
                {
                    newLink = Clipboard.GetText().Split("\r\n".ToCharArray())[1];
                }

                //关闭讨论组标签
                Console.WriteLine("[{0}] 关闭讨论组标签", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Keyboard.SendKeys(window, KeyboardKeys.ESC);
                #endregion

                OrigionLink link = new OrigionLink()
                {
                    QQOwner = "",
                    RetrieveDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                    Type = "Z",
                    Name = item.Name.Substring(0, 1000),
                    JoinLink = newLink
                };

                try
                {
                    olService.Insert(link);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(link.ToString());
                    Console.WriteLine(ex.Message);
                }

                last = item;
                //if (++i >= 595) break;
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
