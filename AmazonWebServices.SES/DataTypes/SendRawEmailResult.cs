using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents a unique message ID returned from a successful SendRawEmail request.
    /// </summary>
    public class SendRawEmailResult
    {
        /// <summary>
        /// The unique message identifier returned from the SendRawEmail action.
        /// </summary>
        public string MessageId { get; set; }
    }
}
