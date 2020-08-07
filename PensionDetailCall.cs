using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProcessPension
{
    public class PensionDetailCall
    {
        /// <summary>
        /// Dependency Injection
        /// </summary>
        private IConfiguration configuration;
        public PensionDetailCall(IConfiguration iConfig)
        {
            configuration = iConfig;
        }
        /// <summary>
        /// Calling the Pension Detail Microservice
        /// </summary>
        /// <param name="aadhar"></param>
        /// <returns>value for calculations and client input</returns>
        public HttpResponseMessage CallPensionDetail(string aadhar)
        {
            PensionDetailCall banktype = new PensionDetailCall(configuration);
            ClientInput res = new ClientInput();
            HttpResponseMessage response = new HttpResponseMessage();
            string uriConn = configuration.GetValue<string>("MyUriLink:UriLink");
            using (var client = new HttpClient())
            {
                client.BaseAddress =new Uri (uriConn);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    response = client.GetAsync("api/PensionerDetail/" + aadhar).Result;
                }
                catch(Exception e)
                { response = null; }
            }
            return response;
        }



        public ClientInput GetClientInfo(string aadhar)
        {
            ClientInput res = new ClientInput();
            HttpResponseMessage response = CallPensionDetail(aadhar);
            if (response == null)
            {
                res = null;
                return res;
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<ClientInput>(responseValue);

            return res;
        }


        public ValueforCalCulation GetCalculationValues(string aadhar)
        {
            ValueforCalCulation res = new ValueforCalCulation();
            HttpResponseMessage response = CallPensionDetail(aadhar);
            if (response == null)
            {
                res = null;
                return res;
            }
            string responseValue = response.Content.ReadAsStringAsync().Result;
            res = JsonConvert.DeserializeObject<ValueforCalCulation>(responseValue);
            return res;
        }

    }
}
