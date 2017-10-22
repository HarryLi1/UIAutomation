using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class JoinTalkWebDocument : HtmlDocument
    {
        public JoinTalkWebDocument(UITestControl container)
            : base(container)
        {
            this.FilterProperties[HtmlDocument.PropertyNames.AbsolutePath] = "/cgi-bin/dc/ft";
        }

        public HtmlDiv BtnJoinTalk
        {
            get
            {
                if (this.btnJoinTalk == null)
                {
                    this.btnJoinTalk = new HtmlDiv(this);

                    this.btnJoinTalk.SearchProperties[HtmlDiv.PropertyNames.Id] = "enterButton";
                    this.btnJoinTalk.FilterProperties[HtmlDiv.PropertyNames.InnerText] = "加入多人聊天";
                    this.btnJoinTalk.FilterProperties[HtmlDiv.PropertyNames.Class] = "button";
                }

                return this.btnJoinTalk;
            }
        }

        private HtmlDiv btnJoinTalk;
    }
}
