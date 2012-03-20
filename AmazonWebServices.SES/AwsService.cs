using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using RestSharp;


namespace AmazonWebServices.SES
{
    public static class AwsService
    {
        public static void PrepareServiceCall(CommonQueryParameters.SignatureMethodTypes methodType,string awsAccessKeyId, string awsSecretAccessKey, out RestSharp.RestClient restClient, out RestSharp.RestRequest restRequest)
        {
            var signer = methodType == CommonQueryParameters.SignatureMethodTypes.HmacSHA1
                         ? (HMAC) new HMACSHA1()
                         : new HMACSHA256(System.Text.Encoding.UTF8.GetBytes(awsSecretAccessKey));

            var dateHeaderValue = DateTime.UtcNow.ToString("r");
            var stringToSign = signer.ComputeHash(Encoding.UTF8.GetBytes(dateHeaderValue));
            var requestSignature = Convert.ToBase64String(stringToSign);

            var awsAuthHeaderValue = String.Format(
                "AWS3-HTTPS AWSAccessKeyId={0}, Algorithm={1}, Signature={2}",
                awsAccessKeyId,
                methodType,
                requestSignature
                );

            restClient = new RestSharp.RestClient("https://email.us-east-1.amazonaws.com") { FollowRedirects = true };
            restRequest = new RestSharp.RestRequest(Method.POST);
            
            
            restRequest.AddHeader("X-Amzn-Authorization", awsAuthHeaderValue);
            restRequest.AddHeader("x-amz-date", dateHeaderValue);
        }
    }
}
