using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace AmazonWebServices.SES
{
    public static class Converter
    {

        private static readonly XNamespace SesNs = "http://ses.amazonaws.com/doc/2010-12-01/";


        public static string GetRequestId(string xmlResponse)
        {
            var root = XElement.Parse(xmlResponse);
            var requestId = root.Descendants(SesNs + "RequestId").FirstOrDefault();
            return requestId != null ? requestId.Value : string.Empty;
        }

        public static string GetRequestIdJson(string jsonResponse)
        {
            var root = JObject.Parse(jsonResponse);
            var requestIdParent = root.Descendants().Where(x => x.SelectToken("RequestId",false) != null).FirstOrDefault();
            var requestId = requestIdParent != null ? requestIdParent["RequestId"].Value<string>() : string.Empty;
            return requestId;
        }

        public static DataTypes.GetSendQuotaResult ParseGetSendQuotaResponseJson(string jsonResponse)
        {
            var result = new DataTypes.GetSendQuotaResult();

            var root = JObject.Parse(jsonResponse);
            var response = root.SelectToken("GetSendQuotaResponse.GetSendQuotaResult");
            if(response != null)
            {
                result.SentLast24Hours = response["SentLast24Hours"].Value<double>();
                result.Max24HourSend = response["Max24HourSend"].Value<double>();
                result.MaxSendRate = response["MaxSendRate"].Value<double>();
            }
            return result;
        }

        public static DataTypes.GetSendQuotaResult ParseGetSendQuotaResponse(string xmlResponse)
        {
            var result = new DataTypes.GetSendQuotaResult();

            var root = XElement.Parse(xmlResponse);
            var response = root.Descendants(SesNs + "GetSendQuotaResult").FirstOrDefault();
            if(response != null)
            {
                result.SentLast24Hours = double.Parse(response.Descendants(SesNs + "SentLast24Hours").First().Value);
                result.Max24HourSend = double.Parse(response.Descendants(SesNs + "Max24HourSend").First().Value);
                result.MaxSendRate = double.Parse(response.Descendants(SesNs + "MaxSendRate").First().Value);
            }
            return result;
        }

        public static DataTypes.GetSendStatisticsResult ParseGetSendStatisticsResultJson(string jsonResponse)
        {
            var result = new DataTypes.GetSendStatisticsResult();

            var root = JObject.Parse(jsonResponse);
            var response = root.SelectToken("GetSendStatisticsResponse.GetSendStatisticsResult.SendDataPoints");
            if (response != null && response.Children().Count() > 0)
            {
                result.SendDataPoints = (from member in response.Children()
                                         select new DataTypes.SendDataPoint
                                         {

                                             Bounces = member["Complaints"].Value<long>(),
                                             Complaints = member["Bounces"].Value<long>(),
                                             DeliveryAttempts = member["DeliveryAttempts"].Value<long>(),
                                             Rejects = member["Rejects"].Value<long>(),
                                             Timestamp = UnixTimeStampToDateTime(member["Timestamp"].Value<double>())
                                         });
            }
            return result;
        }

        public static DataTypes.GetSendStatisticsResult ParseGetSendStatisticsResult(string xmlResponse)
        {
            var result = new DataTypes.GetSendStatisticsResult();

            var root = XElement.Parse(xmlResponse);
            var response = root.Descendants(SesNs + "GetSendStatisticsResult").FirstOrDefault();
            if (response != null)
            {
                result.SendDataPoints = (from member in response.Descendants(SesNs + "member")
                    select new DataTypes.SendDataPoint
                    {
                        
                        Bounces = long.Parse(member.Descendants(SesNs + "Bounces").First().Value),
                        Complaints = long.Parse(member.Descendants(SesNs + "Complaints").First().Value),
                        DeliveryAttempts = long.Parse(member.Descendants(SesNs + "DeliveryAttempts").First().Value),
                        Rejects = long.Parse(member.Descendants(SesNs + "Rejects").First().Value),
                        Timestamp = DateTime.Parse(member.Descendants(SesNs + "Timestamp").First().Value)
                    });
            }
            return result;
        }

        public static DataTypes.ListVerifiedEmailAddressesResult ListVerifiedEmailAddressesResultJson(string jsonResponse)
        {
            var result = new DataTypes.ListVerifiedEmailAddressesResult();

            var root = JObject.Parse(jsonResponse);
            var response = root.SelectToken("ListVerifiedEmailAddressesResponse.ListVerifiedEmailAddressesResult.VerifiedEmailAddresses");
            if (response != null && response.Children().Count() > 0)
            {
                result.VerifiedEmailAddresses = (from emailAddress in response.Children()
                                                 select emailAddress.Value<string>());
            }
            return result;
        }

        public static DataTypes.ListVerifiedEmailAddressesResult ListVerifiedEmailAddressesResult(string xmlResponse)
        {
            var result = new DataTypes.ListVerifiedEmailAddressesResult();

            var root = XElement.Parse(xmlResponse);
            var response = root.Descendants(SesNs + "ListVerifiedEmailAddressesResult").FirstOrDefault();
            if (response != null)
            {
                result.VerifiedEmailAddresses = from member in response.Descendants(SesNs + "member")
                                         select member.Value;
            }
            return result;
        }

        public static DataTypes.SendEmailResult ParseSendEmailResult(string xmlResponse)
        {
            var root = XElement.Parse(xmlResponse);
            var messageId = root.Descendants(SesNs + "MessageId").FirstOrDefault();
            var results = new DataTypes.SendEmailResult() {MessageId = messageId != null ? messageId.Value : string.Empty};
            return results;
        }

        public static DataTypes.SendEmailResult ParseSendEmailResultJson(string jsonResponse)
        {
            var root = JObject.Parse(jsonResponse);
            var messageIdParent = root.Descendants().Where(x => x.SelectToken("MessageId", false) != null).FirstOrDefault();
            var messageId = messageIdParent != null ? messageIdParent["MessageId"].Value<string>() : string.Empty;
            var results = new DataTypes.SendEmailResult() { MessageId = messageId };
            return results;
        }

        public static DataTypes.SendRawEmailResult ParseSendRawEmailResult(string xmlResponse)
        {
            var root = XElement.Parse(xmlResponse);
            var messageId = root.Descendants(SesNs + "MessageId").FirstOrDefault();
            var results = new DataTypes.SendRawEmailResult() { MessageId = messageId != null ? messageId.Value : string.Empty };
            return results;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
