using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;

using Amazon.Lambda.ApplicationLoadBalancerEvents;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Namespace
{
    public class Page
    {
      public ApplicationLoadBalancerResponse FunctionHandler(JObject input)
      {
        string responseString = @"<html>  
                                    <head>
                                      <title>Hi there</title>
                                    </head>
                                    <body>
                                      <h1>Hi there</h1>
                                      <p>Welcome to this basic page, powered by Lambda</p>
                                      <p><img src='/assets/200.jpg' /></p>
                                    </body>
                                  </html>";

        Dictionary<string, string> Headers = new Dictionary<string, string>();
        Headers.Add("Content-Type", "text/html;");

        ApplicationLoadBalancerResponse response = new ApplicationLoadBalancerResponse() {
          IsBase64Encoded = false,
          StatusCode = 200,
          StatusDescription = "200 OK",
          Headers = Headers,
          Body = responseString
        };

        return response;
      }
    }
}