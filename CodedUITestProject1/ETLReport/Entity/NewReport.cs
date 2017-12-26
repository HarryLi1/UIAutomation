using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.ETLReport.Entity
{
    public class NewReport
    {
        public long Id;
        public long VendorId;
        public int Environment;
        public decimal TotalProgress;
        public string InterfaceProcessInfo;
        public string CaseInfo;
        public string CreateUser;
        public string ChangeUser;
        public DateTime CreateTime;
        public DateTime ChangeTime;
    }
}
