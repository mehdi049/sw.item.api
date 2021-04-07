using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SW.Item.Data.Common.Helpers
{
    public static class ApiCall
    {

        public static Response ApiGetObject(string serviceUrl, string endPoint)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //GET Method  
                var response = client.GetAsync(endPoint);
                if (response.Result.IsSuccessStatusCode)
                {
                    Response r =
                        JsonConvert.DeserializeObject<Response>(response.Result.Content.ReadAsStringAsync().Result);

                    if (r.Status == HttpStatusCode.OK)
                        return new Response
                        {
                            Status = HttpStatusCode.OK,
                            Body = r.Body
                        };

                    return new Response()
                    {
                        Status = HttpStatusCode.BadRequest,
                        Message = r.Message
                    };
                }

                return new Response()
                {
                    Status = HttpStatusCode.BadRequest
                };
            }
        }
    }
}
