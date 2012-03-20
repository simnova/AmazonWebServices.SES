using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents the raw data of the message.
    /// </summary>
    public class RawMessage
    {
        /// <summary>
        /// The raw data of the message. The client must ensure that the message format complies with Internet email standards regarding email header fields, MIME types, MIME encoding, and base64 encoding (if necessary).
        /// </summary>
        public string Data { get; set; }
    }
}
