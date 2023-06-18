using DK_Hardware.Entity;
using DK_Hardware.Services;
using Newtonsoft.Json;
using NLog;
using System.Xml.Serialization;

namespace DK_Hardware.DAL
{
    public class ProductDetails
    {
        public void GetProductDetails(ILogger _logger)
        {
            string token;
            try
            {

                DKServices bApi = new DKServices();

                #region Generating Token
                token = bApi.getToken(_logger);
                Console.WriteLine("Toke Generated " + token);
                _logger.Info("...Toke Generated..." + token);
                if (token == null || token == string.Empty)
                {
                    Console.WriteLine("Unable to process data due to token is not generating.. ");


                };

                #endregion end

                #region Fetching Banner XML API Data
                var xmlResponse = bApi.getXmlData(token, _logger);
                if (xmlResponse == null || xmlResponse == string.Empty)
                {
                    Console.WriteLine("Unable to process data due to get XML API response is blank.. ");


                };
                Console.WriteLine("XML API Response " + xmlResponse);
                _logger.Info("...XML API Response..." + xmlResponse);


                XmlSerializer serializer = new XmlSerializer(typeof(Entity_Banner));
                Entity_Banner xmlResult;
                using (StringReader reader = new StringReader(xmlResponse))
                {
                    xmlResult = (Entity_Banner)serializer.Deserialize(reader);
                }

                #endregion end XML


                #region Fetching RSHughes Json API Data
                var jsonResponse = bApi.getJsonData(token, _logger);
                if (jsonResponse == null || jsonResponse.ToString() == string.Empty)
                {
                    Console.WriteLine("Unable to process data due to Json API response is blank.. ");
                }

                Console.WriteLine("Json API Response " + jsonResponse);
                _logger.Info("...Json API Response..." + jsonResponse);

                Entity_RSHughes myDeserializedClass = JsonConvert.DeserializeObject<Entity_RSHughes>(jsonResponse.ToString());

                #endregion End Json 

                //Convert data in to List for matching records
                List<Entity_RSHughes> list1 = new List<Entity_RSHughes> { myDeserializedClass };
                List<Entity_Banner> list2 = new List<Entity_Banner> { xmlResult };

                //var matchedlist = list2.Where(list2_item => !list1.Any(dbItem => list2_item.Product[0].Upc == dbItem.products[0].upc && list2_item.Product[0].Upc != 0)).ToList();

                var matchedlist = list2[0].Product.Where(list2_item => list1[0].products.Any(dbItem => list2_item.Upc == dbItem.upc && list2_item.Upc != 0)).ToList();

                // var matchedlist = list2.Where(list2_item => !list1.Any(dbItem => list2_item.Product[0].Upc == dbItem.products[0].upc && list2_item.Product[0].Upc != 0)).ToList();


                // Print Matched records data
                
                Console.WriteLine(".......Product Matching Data.......");

                foreach (var calculation in matchedlist)
                {
                    Console.WriteLine("ItemCode: {0} UPC: {1}", calculation.ItemCode, calculation.Upc);
                }

                Console.WriteLine(".......End Product Matching Data.......");
            }
            catch (Exception e)
            {
                _logger.Fatal(e, "An unexpected exception has occured");
                Console.WriteLine("An unexpected exception has occured...");

            }
            finally
            {
                _logger.Info("Application terminated. Press <enter> to exit...");
                Console.WriteLine("Application terminated. Press <enter> to exit...");
                Console.ReadLine();
            }


        }
    }
}

