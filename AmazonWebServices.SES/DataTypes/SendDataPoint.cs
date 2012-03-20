using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents sending statistics data. Each SendDataPoint contains statistics for a 15-minute period of sending activity.
    /// </summary>
    public class SendDataPoint
    {
        /// <summary>
        /// Number of emails that have bounced.
        /// </summary>
        public long Bounces { get; set; }

        /// <summary>
        /// Number of unwanted emails that were rejected by recipients.
        /// </summary>
        public long Complaints { get; set; }

        /// <summary>
        /// Number of emails that have been enqueued for sending.
        /// </summary>
        public long DeliveryAttempts { get; set; }


        /// <summary>
        /// Number of emails rejected by Amazon SES.
        /// </summary>
        public long Rejects { get; set; }


        /// <summary>
        /// Time of the data point.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
