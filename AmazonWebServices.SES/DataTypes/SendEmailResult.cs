using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents a unique message ID returned from a successful SendEmail request.
    /// </summary>
    public class SendEmailResult
    {
        /// <summary>
        /// The unique message identifier returned from the SendEmail action.
        /// </summary>
        public string MessageId { get; set; }
    }
}
