using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;

using Amazon.Lambda.ApplicationLoadBalancerEvents;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[assembly:LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Namespace
{
    public class API
    {
      public ApplicationLoadBalancerResponse FunctionHandler(JObject input)
      {
        JObject responseObj = new JObject();
        responseObj.Add("data", "hello 123");

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        ApplicationLoadBalancerResponse response = new ApplicationLoadBalancerResponse() {
          IsBase64Encoded = false,
          StatusCode = 200,
          StatusDescription = "200 OK",
          Headers = headers,
          Body = JsonConvert.SerializeObject(responseObj)
        };

        return response;
      }
    }
}