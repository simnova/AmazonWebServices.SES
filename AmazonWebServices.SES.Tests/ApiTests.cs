using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using AmazonWebServices.SES.DataTypes;
using NUnit.Framework;

namespace AmazonWebServices.SES.Tests
{
    [TestFixture]
    public class ApiTests
    {
        public string AwsSecretAccessKey;
        public string AwsAccessKeyId;
        public string VerifiedEmailAddress;
        public string SecondaryVerifiedEmailAddress;

        //  http://docs.amazonwebservices.com/ses/latest/DeveloperGuide/SMTP.Credentials.html
        public string SmtpUsername;
        public string SmtpPassword;
        public string SmtpServerName;
        public int SmtpPort; // 25,465,587

        public CommonQueryParameters QueryParameters;

        [SetUp]
        public void Init()
        {
            AwsSecretAccessKey = ConfigurationManager.AppSettings["AwsSecretAccessKey"];
            AwsAccessKeyId = ConfigurationManager.AppSettings["AwsAccessKeyId"];
            VerifiedEmailAddress = ConfigurationManager.AppSettings["VerifiedEmailAddress"];
            SecondaryVerifiedEmailAddress = ConfigurationManager.AppSettings["SecondaryVerifiedEmailAddress"];

        //  http://docs.amazonwebservices.com/ses/latest/DeveloperGuide/SMTP.Credentials.html
            SmtpUsername = ConfigurationManager.AppSettings["SmtpUsername"];
            SmtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            SmtpServerName = ConfigurationManager.AppSettings["SmtpServerName"];
            SmtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]); // 25,465,587

            QueryParameters = new CommonQueryParameters(
                awsSecretAccessKey: AwsSecretAccessKey,
                awsAccessKeyId: AwsAccessKeyId,
                signatureMethod: CommonQueryParameters.SignatureMethodTypes.HmacSHA256);
        }

        [Test]
        public void DeleteVerifiedEmailAddresses()
        {
            var results = Api.DeleteVerifiedEmailAddress(VerifiedEmailAddress, QueryParameters);
            Assert.That(results, Is.Not.Null);
        }


        [Test]
        public void GetSendQuota()
        {
            var results = Api.GetSendQuota(QueryParameters);
            Assert.That(results.Max24HourSend, Is.GreaterThan(0));
        }

        [Test]
        public void GetSendStatistics()
        {
            var results = Api.GetSendStatistics(QueryParameters);
            Assert.That(results.SendDataPoints, Is.Not.Null);
        }

        [Test]
        public void ListVerifiedEmailAddresses()
        {
            var results = Api.ListVerifiedEmailAddresses(QueryParameters);
            Assert.That(results, Is.Not.Null);
        }

        /// <summary>
        /// Note that you will want to update the ToAddress to be a verified email address if you are in the sandbox.
        /// You must add a SECOND Verified Email Address (can't send email to yourself)
        /// </summary>
        [Test]
        public void SendEmail()
        {
            var results = Api.SendEmail(
                destination: new DataTypes.Destination() { ToAddresses = new List<string>() { SecondaryVerifiedEmailAddress } },
                message: new DataTypes.Message() { Body = new DataTypes.Body() { Text = new Content() { Charset = "UTF-8", Data = "This is a body" } }, Subject = new Content() { Charset = "UTF-8", Data = "This is a subject" } },
                source: VerifiedEmailAddress,
                commonQueryParameters: QueryParameters,
                replyToAddresses: new List<string>() { VerifiedEmailAddress }
                );

            Assert.That(results, Is.Not.Null);
        }

        [Test]
        public void VerifyEmailAddress()
        {
            var results = Api.VerifyEmailAddress(VerifiedEmailAddress, QueryParameters);
            Assert.That(results, Is.Not.Null);
        }

        [Test]
        public void SendEmailViaSmtp()
        {
            var mail = new MailMessage();
            mail.To.Add(SecondaryVerifiedEmailAddress);
            mail.From = new MailAddress(VerifiedEmailAddress);
            mail.Subject = "Test Email Subject SMTP";
            mail.Body = "This is a Body";
            mail.IsBodyHtml = false;
            var smtp = new SmtpClient
                           {
                               Host = SmtpServerName,
                               Port = SmtpPort,
                               Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword),
                               EnableSsl = true
                           };
            smtp.Send(mail);
        }

    }
}
