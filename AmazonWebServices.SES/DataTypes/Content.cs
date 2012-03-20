using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents textual data, plus an optional character set specification.
    /// By default, the text must be 7-bit ASCII, due to the constraints of the SMTP protocol. If the text must contain any other characters, then you must also specify a character set. Examples include UTF-8, ISO-8859-1, and Shift_JIS.
    /// </summary>
    public class Content
    {
        /// <summary>
        /// The character set of the content.
        /// </summary>
        public string Charset { get; set; }

        /// <summary>
        /// The textual data of the content.
        /// </summary>
        public string Data { get; set; }
    }
}
