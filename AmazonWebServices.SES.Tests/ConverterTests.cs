using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AmazonWebServices.SES.DataTypes;
using NUnit.Framework;

namespace AmazonWebServices.SES.Tests
{
    [TestFixture]
    public class ConverterTests
    {

        [Test]
        public void ParseRequestIdJson()
        {
            const string sampleResponse = "{\"VerifyEmailAddressResponse\":{\"ResponseMetadata\":{\"RequestId\":\"b6368119-71ff-11e1-8c9b-c31653d270c2\"}}}";
            var expected = "b6368119-71ff-11e1-8c9b-c31653d270c2";
            var results = Converter.GetRequestIdJson(sampleResponse);
            Assert.That(results, Is.EqualTo(expected));
        }

        [Test]
        public void ParseRequestId()
        {
            const string sampleResponse = "<DeleteVerifiedEmailAddressResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                                          "  <ResponseMetadata>" +
                                          "    <RequestId>5634af08-c865-11e0-8986-3f99a698f914</RequestId>" +
                                          "  </ResponseMetadata>" +
                                          "</DeleteVerifiedEmailAddressResponse>";
            var expected = "5634af08-c865-11e0-8986-3f99a698f914";
            var results = Converter.GetRequestId(sampleResponse);
            Assert.That(results, Is.EqualTo(expected));
        }

        [Test]
        public void ParseSendQuotaResponseJson()
        {
            const string sampleResponse = "{\"GetSendQuotaResponse\":{\"GetSendQuotaResult\":{\"Max24HourSend\":200.0,\"MaxSendRate\":1.0,\"SentLast24Hours\":3.0},\"ResponseMetadata\":{\"RequestId\":\"18bca67d-71f8-11e1-81ae-e38fc85d3210\"}}}";
            var expected = new DataTypes.GetSendQuotaResult()
            {
                SentLast24Hours = 3,
                Max24HourSend = 200,
                MaxSendRate = 1
            };
            var results = Converter.ParseGetSendQuotaResponseJson(sampleResponse);
            Assert.That(results.SentLast24Hours, Is.EqualTo(expected.SentLast24Hours));
            Assert.That(results.Max24HourSend, Is.EqualTo(expected.Max24HourSend));
            Assert.That(results.MaxSendRate, Is.EqualTo(expected.MaxSendRate));
        }

        [Test]
        public void ParseSendQuotaResponse()
        {
            const string sampleResponse = "<GetSendQuotaResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                                          "  <GetSendQuotaResult>" +
                                          "    <SentLast24Hours>127.0</SentLast24Hours>" +
                                          "    <Max24HourSend>200.0</Max24HourSend>" +
                                          "    <MaxSendRate>1.0</MaxSendRate>" +
                                          "  </GetSendQuotaResult>" +
                                          "  <ResponseMetadata>" +
                                          "    <RequestId>273021c6-c866-11e0-b926-699e21c3af9e</RequestId>" +
                                          "  </ResponseMetadata>" +
                                          "</GetSendQuotaResponse>";
            var expected = new DataTypes.GetSendQuotaResult()
                               {
                                   SentLast24Hours = 127,
                                   Max24HourSend = 200,
                                   MaxSendRate = 1
                               };
            var results = Converter.ParseGetSendQuotaResponse(sampleResponse);
            Assert.That(results.SentLast24Hours, Is.EqualTo(expected.SentLast24Hours));
            Assert.That(results.Max24HourSend, Is.EqualTo(expected.Max24HourSend));
            Assert.That(results.MaxSendRate, Is.EqualTo(expected.MaxSendRate));
        }

        [Test]
        public void ParseGetSendStatisticsJson()
        {
            const string sampleResponse =
                "{\"GetSendStatisticsResponse\":{\"GetSendStatisticsResult\":{\"SendDataPoints\":[{\"Bounces\":0,\"Complaints\":0,\"DeliveryAttempts\":2,\"Rejects\":0,\"Timestamp\":1.33225914E9}]},\"ResponseMetadata\":{\"RequestId\":\"cb31d1a3-72a7-11e1-87ad-0b0e460fd318\"}}}";


            var expected = new DataTypes.GetSendStatisticsResult()
            {
                SendDataPoints = new List<DataTypes.SendDataPoint>()
                              {
                                  new DataTypes.SendDataPoint()
                                      {
                                          DeliveryAttempts = 2,
                                          Timestamp = DateTime.Parse("Tue, 20 Mar 2012 15:59:00 GMT"),
                                          Rejects = 0,
                                          Bounces = 0,
                                          Complaints = 0
                                      },
                              }
            };

            var results = Converter.ParseGetSendStatisticsResultJson(sampleResponse);

            var result1 = results.SendDataPoints.First();
            var expected1 = expected.SendDataPoints.First();

            Assert.That(result1.DeliveryAttempts, Is.EqualTo(expected1.DeliveryAttempts));
            Assert.That(result1.Timestamp, Is.EqualTo(expected1.Timestamp));
            Assert.That(result1.Rejects, Is.EqualTo(expected1.Rejects));
            Assert.That(result1.Bounces, Is.EqualTo(expected1.Bounces));
            Assert.That(result1.Complaints, Is.EqualTo(expected1.Complaints));
        }

        [Test]
        public void ParseGetSendStatistics()
        {
            const string sampleResponse =
                "<GetSendStatisticsResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                "  <GetSendStatisticsResult>" +
                "    <SendDataPoints>" +
                "      <member>" +
                "        <DeliveryAttempts>8</DeliveryAttempts>" +
                "        <Timestamp>2011-08-03T19:23:00Z</Timestamp>" +
                "        <Rejects>0</Rejects>" +
                "        <Bounces>0</Bounces>" +
                "        <Complaints>0</Complaints>" +
                "      </member>" +
                "      <member>" +
                "        <DeliveryAttempts>7</DeliveryAttempts>" +
                "        <Timestamp>2011-08-03T06:53:00Z</Timestamp>" +
                "        <Rejects>0</Rejects>" +
                "        <Bounces>0</Bounces>" +
                "        <Complaints>0</Complaints>" +
                "      </member>" +
                "    </SendDataPoints>" +
                "  </GetSendStatisticsResult>" +
                "  <ResponseMetadata>" +
                "    <RequestId>c2b66ee5-c866-11e0-b17f-cddb0ab334db</RequestId>" +
                "  </ResponseMetadata>" +
                "</GetSendStatisticsResponse>";
            
            var expected = new DataTypes.GetSendStatisticsResult()
            {
                SendDataPoints = new List<DataTypes.SendDataPoint>()
                              {
                                  new DataTypes.SendDataPoint()
                                      {
                                          DeliveryAttempts = 8,
                                          Timestamp = DateTime.Parse("2011-08-03T19:23:00Z"),
                                          Rejects = 0,
                                          Bounces = 0,
                                          Complaints = 0
                                      },
                                  new DataTypes.SendDataPoint()
                                      {
                                          DeliveryAttempts = 7,
                                          Timestamp = DateTime.Parse("2011-08-03T06:53:00Z"),
                                          Rejects = 0,
                                          Bounces = 0,
                                          Complaints = 0
                                      },
                              }
            };

            var results = Converter.ParseGetSendStatisticsResult(sampleResponse);

            var result1 =results.SendDataPoints.Where(x => x.DeliveryAttempts == 8).First();
            var expected1 = expected.SendDataPoints.Where(x => x.DeliveryAttempts == 8).First();

            var result2 = results.SendDataPoints.Where(x => x.DeliveryAttempts == 7).First();
            var expected2 = expected.SendDataPoints.Where(x => x.DeliveryAttempts == 7).First();

            Assert.That(result1.DeliveryAttempts, Is.EqualTo(expected1.DeliveryAttempts));
            Assert.That(result1.Timestamp, Is.EqualTo(expected1.Timestamp));
            Assert.That(result1.Rejects, Is.EqualTo(expected1.Rejects));
            Assert.That(result1.Bounces, Is.EqualTo(expected1.Bounces));
            Assert.That(result1.Complaints, Is.EqualTo(expected1.Complaints));

            Assert.That(result2.DeliveryAttempts, Is.EqualTo(expected2.DeliveryAttempts));
            Assert.That(result2.Timestamp, Is.EqualTo(expected2.Timestamp));
            Assert.That(result2.Rejects, Is.EqualTo(expected2.Rejects));
            Assert.That(result2.Bounces, Is.EqualTo(expected2.Bounces));
            Assert.That(result2.Complaints, Is.EqualTo(expected2.Complaints));

        }

        [Test]
        public void ParseListVerifiedEmailAddressesJson()
        {
            const string sampleResponse =
                "{\"ListVerifiedEmailAddressesResponse\":{\"ListVerifiedEmailAddressesResult\":{\"VerifiedEmailAddresses\":[\"example@amazon.com\"]},\"ResponseMetadata\":{\"RequestId\":\"b0337b00-7296-11e1-b42a-6576da542b40\"}}}";

            var expected = new DataTypes.ListVerifiedEmailAddressesResult() { VerifiedEmailAddresses = new List<string>() { "example@amazon.com" } };
            var results = Converter.ListVerifiedEmailAddressesResultJson(sampleResponse);
            Assert.That(results.VerifiedEmailAddresses.First(), Is.EqualTo(expected.VerifiedEmailAddresses.First()));
        }

        [Test]
        public void ParseListVerifiedEmailAddresses()
        {
            const string sampleResponse = 
                "<ListVerifiedEmailAddressesResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                "  <ListVerifiedEmailAddressesResult>" +
                "    <VerifiedEmailAddresses>" +
                "      <member>example@amazon.com</member>" +
                "    </VerifiedEmailAddresses>" +
                "  </ListVerifiedEmailAddressesResult>" +
                "  <ResponseMetadata>" +
                "    <RequestId>3dd50e97-c865-11e0-b235-099eb63d928d</RequestId>" +
                "  </ResponseMetadata>" +
                "</ListVerifiedEmailAddressesResponse>";
            var expected = new DataTypes.ListVerifiedEmailAddressesResult(){VerifiedEmailAddresses = new List<string>() {"example@amazon.com"}};
            var results = Converter.ListVerifiedEmailAddressesResult(sampleResponse);
            Assert.That(results.VerifiedEmailAddresses.First(), Is.EqualTo(expected.VerifiedEmailAddresses.First()));
        }

        [Test]
        public void ParseSendEmailResultJson()
        {
            const string sampleResponse =
                "{\"SendEmailResponse\":{\"ResponseMetadata\":{\"RequestId\":\"7f385046-72a6-11e1-bade-cfc4dbd0c819\"},\"SendEmailResult\":{\"MessageId\":\"0000013630db06c5-1a49bc14-4f9d-4d72-a067-9dac2796ceb0-000000\"}}}";

            var expected = new DataTypes.SendEmailResult() { MessageId = "0000013630db06c5-1a49bc14-4f9d-4d72-a067-9dac2796ceb0-000000" };
            var results = Converter.ParseSendEmailResultJson(sampleResponse);
            Assert.That(results.MessageId, Is.EqualTo(expected.MessageId));
        }

        [Test]
        public void ParseSendEmailResult()
        {
            const string sampleResponse =
                "<SendEmailResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                "  <SendEmailResult>" +
                "    <MessageId>00000131d51d2292-159ad6eb-077c-46e6-ad09-ae7c05925ed4-000000</MessageId>" +
                "  </SendEmailResult>" +
                "  <ResponseMetadata>" +
                "    <RequestId>d5964849-c866-11e0-9beb-01a62d68c57f</RequestId>" +
                "  </ResponseMetadata>" +
                "</SendEmailResponse>";
            var expected = new DataTypes.SendEmailResult() { MessageId = "00000131d51d2292-159ad6eb-077c-46e6-ad09-ae7c05925ed4-000000" };
            var results = Converter.ParseSendEmailResult(sampleResponse);
            Assert.That(results.MessageId, Is.EqualTo(expected.MessageId));
        }

        [Test]
        public void ParseSendRawEmailResult()
        {
            const string sampleResponse =
                "<SendRawEmailResponse xmlns=\"http://ses.amazonaws.com/doc/2010-12-01/\">" +
                "  <SendRawEmailResult>" +
                "    <MessageId>00000131d51d6b36-1d4f9293-0aee-4503-b573-9ae4e70e9e38-000000</MessageId>" +
                "  </SendRawEmailResult>" +
                "  <ResponseMetadata>" +
                "    <RequestId>e0abcdfa-c866-11e0-b6d0-273d09173b49</RequestId>" +
                "  </ResponseMetadata>" +
                "</SendRawEmailResponse>";
            var expected = new DataTypes.SendRawEmailResult() { MessageId = "00000131d51d6b36-1d4f9293-0aee-4503-b573-9ae4e70e9e38-000000" };
            var results = Converter.ParseSendRawEmailResult(sampleResponse);
            Assert.That(results.MessageId, Is.EqualTo(expected.MessageId));
        }
        
    }
}
