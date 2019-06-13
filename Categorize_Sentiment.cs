using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hollan.Function
{
    public static class Categorize_Sentiment
    {
        [FunctionName("Categorize_Sentiment")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req,
            ILogger log)
        {
            string category;
            log.LogInformation("C# HTTP trigger function processed a request.");
            using (var sr = new StreamReader(req.Body)) {

                var requestBody = await sr.ReadToEndAsync();
                double score = double.Parse(requestBody);

                if(score <= .3) {
                    category = "RED";
                } else if (score <= .6) {
                    category = "YELLOW";
                } else {
                    category = "GREEN";
                }

                return new OkObjectResult(category);
            }
        }
    }
}
