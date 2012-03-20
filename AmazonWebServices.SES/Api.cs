using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RestSharp;

namespace AmazonWebServices.SES
{
    public static class Api
    {
        /// <summary>
        /// Deletes the specified email address from the list of verified addresses.
        /// </summary>
        /// <param name="emailAddress">An email address to be removed from the list of verified addreses.</param>
        /// <param name="commonQueryParameters"></param>
        /// <returns>RequestId</returns>
        public static string DeleteVerifiedEmailAddress(string emailAddress, CommonQueryParameters commonQueryParameters)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );

            restRequest.AddParameter("Action", "DeleteVerifiedEmailAddress");
            restRequest.AddParameter("EmailAddress", emailAddress);

            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);

            var parsedResponse = Converter.GetRequestIdJson(response);
            return parsedResponse;
        }

        /// <summary>
        /// Returns the user's current sending limits.
        /// </summary>
        /// <param name="commonQueryParameters"></param>
        /// <returns></returns>
        public static DataTypes.GetSendQuotaResult GetSendQuota(CommonQueryParameters commonQueryParameters)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );
            
            restRequest.AddParameter("Action", "GetSendQuota");
            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);

            var parsedResponse = Converter.ParseGetSendQuotaResponseJson(response);

            return parsedResponse;
        }

        /// <summary>
        /// Returns the user's sending statistics. The result is a list of data points, representing the last two weeks of sending activity.
        /// Each data point in the list contains statistics for a 15-minute interval.
        /// </summary>
        /// <param name="commonQueryParameters"></param>
        /// <returns></returns>
        public static DataTypes.GetSendStatisticsResult GetSendStatistics(CommonQueryParameters commonQueryParameters)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );

            restRequest.AddParameter("Action", "GetSendStatistics");
            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);

            var parsedResponse = Converter.ParseGetSendStatisticsResultJson(response);

            return parsedResponse;
        }

        /// <summary>
        /// Returns a list containing all of the email addresses that have been verified.
        /// </summary>
        /// <param name="commonQueryParameters"></param>
        /// <returns></returns>
        public static DataTypes.ListVerifiedEmailAddressesResult ListVerifiedEmailAddresses(CommonQueryParameters commonQueryParameters)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );

            restRequest.AddParameter("Action", "ListVerifiedEmailAddresses");

            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);
            
            var parsedResponse = Converter.ListVerifiedEmailAddressesResultJson(response);

            return parsedResponse;
        }

        /// <summary>
        /// Composes an email message based on input data, and then immediately queues the message for sending.
        /// The total size of the message cannot exceed 10 MB.
        /// Amazon SES has a limit on the total number of recipients per message: The combined number of To:, CC: and BCC: email addresses cannot exceed 50. If you need to send an email message to a larger audience, you can divide your recipient list into groups of 50 or fewer, and then call Amazon SES repeatedly to send the message to each group.
        /// For every message that you send, the total number of recipients (To:, CC: and BCC:) is counted against your sending quota - the maximum number of emails you can send in a 24-hour period. For information about your sending quota, go to the "Managing Your Sending Activity" section of the Amazon SES Developer Guide.
        /// </summary>
        /// <param name="destination">The destination for this email, composed of To:, CC:, and BCC: fields.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="source">The sender's email address.</param>
        /// <param name="commonQueryParameters"></param>
        /// <param name="replyToAddresses">(optional) The reply-to email address(es) for the message. If the recipient replies to the message, each reply-to address will receive the reply.</param>
        /// <param name="returnPath">(optional) The email address to which bounce notifications are to be forwarded. If the message cannot be delivered to the recipient, then an error message will be returned from the recipient's ISP; this message will then be forwarded to the email address specified by the ReturnPath parameter.</param>
        /// <returns></returns>
        public static DataTypes.SendEmailResult SendEmail(DataTypes.Destination destination, DataTypes.Message message, string source, CommonQueryParameters commonQueryParameters, IList<string> replyToAddresses =null, string returnPath = null)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );

            restRequest.AddParameter("Action", "SendEmail");
            var i = 0;

            if (destination.ToAddresses != null){
                foreach (var toAddress in destination.ToAddresses)
                {
                    i++;
                    restRequest.AddParameter("Destination.ToAddresses.member." + i, toAddress);
                }
            }

            if (destination.CcAddresses != null){
                foreach (var ccAddress in destination.CcAddresses)
                {
                    i++;
                    restRequest.AddParameter("Destination.CcAddresses.member." + i, ccAddress);
                }
            }

            if (destination.BccAddresses != null){
                foreach (var bccAddress in destination.BccAddresses)
                {
                    i++;
                    restRequest.AddParameter("Destination.BccAddresses.member." + i, bccAddress);
                }
            }

            if (message.Subject != null)
            {
                if ((message.Subject.Data) != null) restRequest.AddParameter("Message.Subject.Data", message.Subject.Data);
                if ((message.Subject.Charset) != null) restRequest.AddParameter("Message.Subject.Charset", message.Subject.Charset);
            }

            if(message.Body.Html != null)
            {
                if((message.Body.Html.Data) != null) restRequest.AddParameter("Message.Body.Html.Data", message.Body.Html.Data);
                if ((message.Body.Html.Charset) != null) restRequest.AddParameter("Message.Body.Html.Charset", message.Body.Html.Charset);
            }

            if (message.Body.Text != null)
            {
                if ((message.Body.Text.Data) != null) restRequest.AddParameter("Message.Body.Text.Data", message.Body.Text.Data);
                if ((message.Body.Text.Charset) != null) restRequest.AddParameter("Message.Body.Text.Charset", message.Body.Text.Charset);
            }

            if (returnPath != null) restRequest.AddParameter("ReturnPath", returnPath);
            if (source != null) restRequest.AddParameter("Source", source);
            

            if(replyToAddresses != null)
            {
                i = 0;
                foreach (var replyToAddress in replyToAddresses)
                {
                    i++;
                    restRequest.AddParameter("ReplyToAddresses.member." + i, replyToAddress);
                }
            }

            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);

            var parsedResponse = Converter.ParseSendEmailResultJson(response);

            return parsedResponse;
        }

        /// <summary>
        /// Sends an email message, with header and content specified by the client. The SendRawEmail action is useful for sending multipart MIME emails. The raw text of the message must comply with Internet email standards; otherwise, the message cannot be sent.
        /// The total size of the message cannot exceed 10 MB. This includes any attachments that are part of the message.
        /// Amazon SES has a limit on the total number of recipients per message: The combined number of To:, CC: and BCC: email addresses cannot exceed 50. If you need to send an email message to a larger audience, you can divide your recipient list into groups of 50 or fewer, and then call Amazon SES repeatedly to send the message to each group.
        /// For every message that you send, the total number of recipients (To:, CC: and BCC:) is counted against your sending quota - the maximum number of emails you can send in a 24-hour period. For information about your sending quota, go to the "Managing Your Sending Activity" section of the Amazon SES Developer Guide.
        /// </summary>
        /// <param name="rawMessage">The raw text of the message. The client is responsible for ensuring the following:</param>
        /// <param name="destinations">A list of destinations for the message.</param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTypes.SendRawEmailResult SendRawEmail(DataTypes.RawMessage rawMessage, IList<string> destinations = null, string source = null)
        {
            return null;
        }

        /// <summary>
        /// Verifies an email address. This action causes a confirmation email message to be sent to the specified address.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="commonQueryParameters"></param>
        /// <returns>Request ID</returns>
        public static string VerifyEmailAddress(string emailAddress, CommonQueryParameters commonQueryParameters)
        {
            RestSharp.RestClient restClient;
            RestSharp.RestRequest restRequest;
            AwsService.PrepareServiceCall(
                commonQueryParameters.SignatureMethod,
                commonQueryParameters.AWSAccessKeyId,
                commonQueryParameters.AWSSecretAccessKey,
                out restClient,
                out restRequest
                );

            restRequest.AddParameter("Action", "VerifyEmailAddress");
            restRequest.AddParameter("EmailAddress", emailAddress);

            var response = restClient.Execute(restRequest).Content;
            Trace.Write(response);

            var parsedResponse = Converter.GetRequestIdJson(response);
            return parsedResponse;
        }

    
    }
}
