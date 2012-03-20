using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents the body of the message. You can specify text, HTML, or both. If you use both, then the message should display correctly in the widest variety of email clients.
    /// </summary>
    public class Body
    {
        /// <summary>
        /// The content of the message, in HTML format. Use this for email clients that can process HTML. You can include clickable links, formatted text, and much more in an HTML message.
        /// </summary>
        public Content Html { get; set; }

        /// <summary>
        /// The content of the message, in text format. Use this for text-based email clients, or clients on high-latency networks (such as mobile devices).
        /// </summary>
        public Content Text { get; set; }
    }
}
