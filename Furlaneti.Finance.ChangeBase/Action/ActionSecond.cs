using Furlaneti.Finance.ChangeBase.Model;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Furlaneti.Finance.ChangeBase.Action
{
    public static class ActionSecond
    {
        public static async Task<Active> GetActiveApiAsync(string active)
        {
            Console.WriteLine("Name active:" + active);

            Active activeObj = new Active();

            string key = "6c0850e0";

            string route = "https://api.hgbrasil.com/finance/stock_price?key=" + key + "&symbol=" + active;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(route);

            client.DefaultRequestHeaders.Accept.Add(

            new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                var dataObjects = await response.Content.ReadAsStringAsync();

                if (!dataObjects.Contains("\"error\":true"))
                {
                    // Desserializar apenas o atributo results
                    using (JsonDocument document = JsonDocument.Parse(dataObjects))
                    {
                        JsonElement root = document.RootElement;

                        JsonElement resultsElement = root.GetProperty("results");

                        JsonElement resultsElementActive = resultsElement.GetProperty(active);

                        activeObj = JsonSerializer.Deserialize<Active>(resultsElementActive.GetRawText());

                        Console.WriteLine("Code active B3:" + active);

                        Console.WriteLine("Name active api:" + activeObj.Name);

                        Console.WriteLine("Price active api:" + activeObj.Price);
                    }

                    client.Dispose();

                }
                else
                {
                    Console.WriteLine("Error to get Stock for #"+active+": Erro 852 - Símbolo não encontrado, por favor entre em contato conosco em console.hgbrasil.com.");
                    
                }

            }
                

            return activeObj;
        }
    }
}
