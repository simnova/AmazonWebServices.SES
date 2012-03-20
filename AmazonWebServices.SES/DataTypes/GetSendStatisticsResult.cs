using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES.DataTypes
{
    /// <summary>
    /// Represents a list of SendDataPoint items returned from a successful GetSendStatistics request. This list contains aggregated data from the previous two weeks of sending activity.
    /// </summary>
    public class GetSendStatisticsResult
    {
        public IEnumerable<SendDataPoint> SendDataPoints { get; set; }
    }
}
