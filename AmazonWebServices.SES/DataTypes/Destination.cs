using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents the destination of the message, consisting of To:, CC:, and BCC: fields.
    /// </summary>
    public class Destination
    {
        /// <summary>
        /// The BCC: field(s) of the message.
        /// </summary>
        public IEnumerable<string> BccAddresses { get; set; }

        /// <summary>
        /// The CC: field(s) of the message.
        /// </summary>
        public IEnumerable<string> CcAddresses { get; set; }

        /// <summary>
        /// The To: field(s) of the message.
        /// </summary>
        public IEnumerable<string> ToAddresses { get; set; }
    }
}
