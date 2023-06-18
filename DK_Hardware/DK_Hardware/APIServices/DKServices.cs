using Newtonsoft.Json.Linq;
using NLog;
using RestSharp;
using System.Configuration;

namespace DK_Hardware.Services
{
    public class DKServices
    {
        #region Function for Get Token from Client Server
        public string getToken(ILogger _logger)
        {
            string GenToken = string.Empty;
            try
            {
                _logger.Info("........Call Token API.......");

                var DKTokenUrl = new RestClient(Convert.ToString(ConfigurationManager.AppSettings["tokenUrl"]));
                var DK_TokenRequest = new RestRequest();
                DK_TokenRequest.Method = Method.Post;
                DK_TokenRequest.Timeout = -1;

                DK_TokenRequest.AddHeader("content-type", ConfigurationManager.AppSettings["content_type"]);


                DK_TokenRequest.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"]);
                DK_TokenRequest.AddParameter("client_secret", ConfigurationManager.AppSettings["client_secret"]);
                DK_TokenRequest.AddParameter("grant_type", ConfigurationManager.AppSettings["grant_type"]);
                DK_TokenRequest.AddParameter("scope", ConfigurationManager.AppSettings["scope"]);

                RestResponse DK_TokenRespose = DKTokenUrl.Execute(DK_TokenRequest);

                dynamic TokenResponse = JObject.Parse(DK_TokenRespose.Content);
                if (Convert.ToString(DK_TokenRespose.StatusCode) == "OK")
                {
                    _logger.Info("Message for Finone Token generation DateTime " + DateTime.Now + "Token Deatils" + Convert.ToString(DK_TokenRespose.Content));
                    GenToken = TokenResponse.access_token;
                }
                else
                {

                    _logger.Info("Unable to Generate Token from API Server. DateTime " + DateTime.Now + "Error Deatils" + Convert.ToString(DK_TokenRespose.Content));

                }

                _logger.Info("........Generated Token......." + GenToken);
                return GenToken;

            }
            catch (Exception e)
            {
                _logger.Fatal(e, "An unexpected exception occured while generating token...");
                Console.WriteLine("An unexpected exception occured while generating token...");
            }
            return GenToken;

        }
        #endregion end


        #region Get XML API Data from Banner
        internal string getXmlData(string Token, ILogger _logger)
        {
            dynamic dataresult = string.Empty;
            var xmlReq = new RestClient(ConfigurationManager.AppSettings["Banner_xmlUrl"]);
            string xmlRes = string.Empty;

            try
            {
                var request = new RestRequest();
                request.Method = Method.Get;
                request.Timeout = -1;

                request.AddHeader("Authorization", "Bearer " + Token);
                RestResponse response = xmlReq.Execute(request);
                if (Convert.ToString(response.StatusCode) == "OK")
                {
                    dataresult = response.Content.ToString();
                }
                else { _logger.Info("........No Response from Banner ......."); }

                xmlRes = dataresult.ToString();
                return xmlRes;
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "An unexpected exception occured while generating token...", e.ToString());
                Console.WriteLine("An unexpected exception occured while generating token...", e.ToString());
            }
            return xmlRes;
        }
        #endregion end

        #region Get Json API Data from RSHughes
        internal JObject getJsonData(string Token, ILogger _logger)
        {
            dynamic dataresult = string.Empty;
            var jsonReq = new RestClient(ConfigurationManager.AppSettings["RSHughes_jsonUrl"]);

            try
            {
                var request = new RestRequest();
                request.Method = Method.Get;
                request.Timeout = -1;

                request.AddHeader("Authorization", "Bearer " + Token);
                request.AddHeader("cache-control", "no-cache");
                RestResponse response = jsonReq.Execute(request);
                if (Convert.ToString(response.StatusCode) == "OK")
                {
                    dataresult = JObject.Parse(response.Content);
                }
                else { _logger.Info("........Not Response RSHughes URL......."); }

            }
            catch (Exception e)
            {
                _logger.Fatal(e, "An unexpected exception occured while generating Json Data from RSHughes...", e.ToString());
                Console.WriteLine("An unexpected exception occured while generating Json Data from RSHughes...", e.ToString());
            }
            return dataresult;
        }

        #endregion end


    }
}
