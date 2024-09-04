using Furlaneti.Finance.ChangeBase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Furlaneti.Finance.ChangeBase.Action
{
    public class ActionThree
    {
        public static async Task<List<Active>> GetActiveApiGetHighB3Async()
         {
            string symbol = "get-high";

            Console.WriteLine("Name symbol get high B3:" + symbol);

            List<Active> activeList = new List<Active>();

            string key = "6c0850e0";

            string route = "https://api.hgbrasil.com/finance/stock_price?key=" + key + "&symbol=" + symbol;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(route);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                        foreach (JsonProperty property in resultsElement.EnumerateObject())
                        {
                            // Aqui você pode acessar o valor de cada símbolo dinamicamente
                            JsonElement activeElement = property.Value;

                            var active = new Active
                            {
                                Symbol = property.Name,
                                Kind = activeElement.GetProperty("kind").GetString(),
                                Name = activeElement.GetProperty("name").GetString(),
                                CompanyName = activeElement.GetProperty("company_name").GetString(),
                                Document = activeElement.GetProperty("document").GetString(),
                                Description = activeElement.GetProperty("description").GetString(),
                                Website = activeElement.GetProperty("website").GetString(),
                                Sector = activeElement.GetProperty("sector").GetString(),
                                Currency = activeElement.GetProperty("currency").GetString(),
                                MarketCap = activeElement.GetProperty("market_cap").GetDouble(),
                                Price = activeElement.GetProperty("price").GetDouble(),
                                ChangePercent = activeElement.GetProperty("change_percent").GetDouble(),
                                ChangePrice = activeElement.GetProperty("change_price").GetDouble(),
                                UpdatedAt = activeElement.GetProperty("updated_at").GetString(),
                                Financials = new Financials
                                {
                                    Quota_count = activeElement.GetProperty("financials").GetProperty("quota_count").GetInt64(),
                                    Dividends = new Dividends
                                    {
                                        Yield_12m = activeElement.GetProperty("financials").GetProperty("dividends").GetProperty("yield_12m").GetDouble(),
                                        Yield_12m_sum = activeElement.GetProperty("financials").GetProperty("dividends").GetProperty("yield_12m_sum").GetDouble()
                                    }
                                },
                                MarketTime = new MarketTime
                                {
                                    Open = activeElement.GetProperty("market_time").GetProperty("open").GetString(),
                                    Close = activeElement.GetProperty("market_time").GetProperty("close").GetString(),
                                    Timezone = activeElement.GetProperty("market_time").GetProperty("timezone").GetInt32()
                                }
                            };

                            // Verificar se a propriedade "logo" existe e se não é nula
                            if (activeElement.TryGetProperty("logo", out JsonElement logoElement) && logoElement.ValueKind != JsonValueKind.Null)
                            {
                                active.Logo = new Logo
                                {
                                    Small = logoElement.GetProperty("small").GetString(),
                                    Big = logoElement.GetProperty("big").GetString()
                                };
                            }

                            activeList.Add(active);
                        }
                    }
                }
                else
                {
                    // Tratar erro aqui, se necessário
                    Console.WriteLine("Error fetching data.");
                }
            }

            client.Dispose();

            return activeList;
        }
    }
}
