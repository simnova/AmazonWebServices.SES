using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents the message to be sent, composed of a subject and a body.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// The message body.
        /// </summary>
        public Body Body { get; set; }

        /// <summary>
        /// The subject of the message: A short summary of the content, which will appear in the recipient's inbox.
        /// </summary>
        public Content Subject { get; set; }
    }
}
