using Newtonsoft.Json;
using static Banhcafe.Microservices.CableColor.Infraestructure.Model.conectionModel;

namespace Banhcafe.Microservices.CableColor.Infraestructure
{
    public class conexionBaseDatos
    {
        private static HttpClient _httpClient = new HttpClient();
        public static string consumir(request request)
        {
            string url = "http://192.168.151.37:49157/petition/request";
            string returnValue = "";

            using (var content = new StringContent(JsonConvert.SerializeObject(request), System.Text.Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage result = _httpClient.PostAsync(url, content).Result;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    returnValue = result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    returnValue = result.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
            }

            return returnValue;
        }
    }
}