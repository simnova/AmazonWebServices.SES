using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents a list of all the email addresses verified for the current user.
    /// </summary>
    public class ListVerifiedEmailAddressesResult
    {
        public IEnumerable<string> VerifiedEmailAddresses { get; set; }
    }
}
