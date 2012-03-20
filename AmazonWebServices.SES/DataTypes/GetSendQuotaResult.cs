using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents the user's current activity limits returned from a successful GetSendQuota request.
    /// </summary>
    public class GetSendQuotaResult
    {
        /// <summary>
        /// The maximum number of emails the user is allowed to send in a 24-hour interval.
        /// </summary>
        public double Max24HourSend { get; set; }

        /// <summary>
        /// The maximum number of emails the user is allowed to send per second.
        /// </summary>
        public double MaxSendRate { get; set; }

        /// <summary>
        /// The number of emails sent during the previous 24 hours.
        /// </summary>
        public double SentLast24Hours { get; set; }
    }
}
