using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AmazonWebServices.SES
{
    /// <summary>
    /// The request parameters that all actions use
    /// </summary>
    public class CommonQueryParameters
    {
        public enum SignatureMethodTypes
        {
            HmacSHA256,
            HmacSHA1
        }

        private string _action;
        private string _awsSecretAccessKey;
        private string _awsAccessKeyId;
        private string _signature;
        private SignatureMethodTypes _signatureMethod;
        private string _signatureVersion;
        private string _version;

        /// <summary>
        /// Constructor with required parameters
        /// </summary>
        /// <param name="awsSecretAccessKey">Secret Access Key.</param>
        /// <param name="awsAccessKeyId">The Access Key ID corresponding to the AWS Secret Access Key you used to sign the request.</param>
        /// <param name="signatureMethod">The hash algorithm you used to create the request signature.</param>
        public CommonQueryParameters(string awsSecretAccessKey, string awsAccessKeyId, SignatureMethodTypes signatureMethod)
        {
            _awsSecretAccessKey = awsSecretAccessKey;
            _awsAccessKeyId = awsAccessKeyId;
            _signatureMethod = signatureMethod;
        }

        /// <summary>
        /// The action to perform. 
        /// </summary>
        public string Action { get { return _action; } set { _action = value; } }

        /// <summary>
        /// The parameters required to authenticate a query request.
        /// Contains: AWSAccessKeyID, SignatureVersion,Timestamp, Signature
        /// </summary>
        public string AuthParams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AWSSecretAccessKey { get { return _awsSecretAccessKey; } set { _awsSecretAccessKey = value; } }

        /// <summary>
        /// The Access Key ID corresponding to the AWS Secret Access Key you used to sign the request.
        /// </summary>
        public string AWSAccessKeyId { get { return _awsAccessKeyId; } set { _awsAccessKeyId = value; } }

        /// <summary>
        /// The date and time at which the request signature expires, in the format YYYY-MM-DDThh:mm:ssZ, as specified in the ISO 8601 standard.
        /// Condition: Requests must include either Timestamp or Expires, but not both.
        /// </summary>
        public string Expires { get; set; }

        /// <summary>
        /// The temporary security token obtained through a call to AWS Security Token Service. Only available for actions in the following AWS services: Amazon EC2, Amazon Simple Notification Service, Amazon SQS, and AWS SimpleDB.
        /// </summary>
        public string SecurityToken { get; set; }

        /// <summary>
        /// The digital signature you created for the request. Refer to the service's developer documentation for information about how to generate the signature.
        /// </summary>
        public string Signature { get { return _signature; } set { _signature = value; } }

        /// <summary>
        /// The hash algorithm you used to create the request signature.
        /// </summary>
        public SignatureMethodTypes SignatureMethod { get { return _signatureMethod; } set { _signatureMethod = value; } }

        /// <summary>
        /// The signature version you use to sign the request. Set this to the value recommended in your product-specific documentation on security.
        /// </summary>
        public string SignatureVersion { get { return _signatureVersion; } set { _signatureVersion = value; } }

        /// <summary>
        /// The date and time the request was signed, in the format YYYY-MM-DDThh:mm:ssZ, as specified in the ISO 8601 standard.
        /// Condition: Requests must include either Timestamp or Expires, but not both.
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// The API version to use, in the format YYYY-MM-DD.
        /// </summary>
        public string Version { get { return _version; } set { _version = value; } }

    }
}
