using Amazon.Lambda.Core;
using System;
using System.IO;
using System.Collections.Generic;

using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Lambda.ApplicationLoadBalancerEvents;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Namespace
{
    public class S3
    {

      private static AmazonS3Client client = new AmazonS3Client();
      public ApplicationLoadBalancerResponse FunctionHandler(JObject input)
      {
        LambdaLogger.Log(input.ToString());

        try {
          char[] toTrim = new[] {'/'};

          GetObjectRequest request = new GetObjectRequest() {
            BucketName = Environment.GetEnvironmentVariable("assetbucket"),
            Key = input.SelectToken("path").ToString().TrimStart(toTrim)
          };

          GetObjectResponse response = client.GetObjectAsync(request).Result;

          byte[] data;
          using (StreamReader sr = new StreamReader(response.ResponseStream))
          {
            using (MemoryStream ms = new MemoryStream())
            {
              sr.BaseStream.CopyTo(ms);
              data = ms.ToArray();

              string responseBody = Convert.ToBase64String(data);
              Dictionary<string, string> headers = new Dictionary<string, string>();
              headers.Add("Content-Type", response.Headers["Content-Type"]);

              ApplicationLoadBalancerResponse albResponse = new ApplicationLoadBalancerResponse() {
                IsBase64Encoded = true,
                StatusCode = 200,
                StatusDescription = "200 OK",
                Headers = headers,
                Body = responseBody
              };

              return albResponse;
            }
          }
        } catch (Exception ex) {
            LambdaLogger.Log(ex.Message);

            return null;
        }
      }
    }
}