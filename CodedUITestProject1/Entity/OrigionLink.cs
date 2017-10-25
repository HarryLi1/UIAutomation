using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodedUITestProject1.Entity
{
    public class OrigionLink
    {
        public long ID;
        public string QQOwner;
        public DateTime RetrieveDate;
        public string Type;
        public string Name;
        public string JoinLink;
        public DateTime CreateTime;
        public DateTime ChangeTime;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ID:").Append(ID).Append(";");
            sb.Append("QQOwner:").Append(QQOwner).Append(";");
            sb.Append("RetrieveDate:").Append(RetrieveDate.ToString("yyyy-MM-dd")).Append(";");
            sb.Append("Type:").Append(Type).Append(";");
            sb.Append("Name:").Append(Name).Append(";");
            sb.Append("JoinLink:").Append(JoinLink).Append(";");

            return sb.ToString();
        }
    }
}
