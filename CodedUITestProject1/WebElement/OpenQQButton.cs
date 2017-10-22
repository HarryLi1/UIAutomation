using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.WebElement
{
    public class OpenQQButton:WinButton
    {
        public OpenQQButton(UITestControl container)
            : base(container)
        {
            this.SearchProperties[WinButton.PropertyNames.Name] = "打开 腾讯QQ";
        }

        public WinClient OpenQQClient
        {
            get
            {
                if(this.openQQClient==null){
                    this.openQQClient = new WinClient(this);
                    this.openQQClient.SearchProperties[WinButton.PropertyNames.Name] = "打开 腾讯QQ";
                }

                return this.openQQClient;
            }
        }

        private WinClient openQQClient;
    }
}
