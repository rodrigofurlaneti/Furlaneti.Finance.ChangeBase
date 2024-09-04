using System;
using System.Net.Http;
using System.Threading.Tasks;
using Furlaneti.Finance.ChangeBase.Data;
using Furlaneti.Finance.ChangeBase.Model;
using HtmlAgilityPack;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace Furlaneti.Finance.ChangeBase.Action
{
    public class ActionFive
    {
        public static async Task ExtractWebsiteDataAsync(string codeNameActive)
        {

                // Cria uma instância do HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Define a URL do endpoint
                    string url = "https://statusinvest.com.br/acoes/"+ codeNameActive;
                    
                    // Adiciona cabeçalhos necessários
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    client.DefaultRequestHeaders.Add("Accept-Language", "en,en-US;q=0.9,pt-BR;q=0.8,pt;q=0.7,es-MX;q=0.6,es;q=0.5");

                    try
                    {
                        // Faz a requisição GET para o endpoint
                        HttpResponseMessage response = await client.GetAsync(url);

                        // Verifica se a requisição foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            ValuationIndicators valuationIndicators = new ValuationIndicators();

                            valuationIndicators.Symbol = codeNameActive;

                            // Lê o conteúdo da resposta como uma string
                            string content = await response.Content.ReadAsStringAsync();

                            // Carrega o conteúdo HTML usando HtmlAgilityPack
                            HtmlDocument doc = new HtmlDocument();

                            doc.LoadHtml(content);

                            // Encontra o valor atual da ação que está dentro do <strong> após o texto "D.Y"
                            var valorAtualNode = doc.DocumentNode.SelectSingleNode("//div[@title='Valor atual do ativo']//strong[@class='value']");

                            if (valorAtualNode != null)
                            {
                                string valorAtualValue = valorAtualNode.InnerText.Trim();
                                Console.WriteLine($"Valor atual da ação: {valorAtualValue}");
                                valuationIndicators.Price = Convert.ToDecimal(valorAtualValue);
                            }
                            else
                            {
                                Console.WriteLine("Valor atual da ação não encontrado.");
                            }

                             // Encontra o valor do Dividend Yield que está dentro do <strong> após o texto "D.Y"
                            var dyNode = doc.DocumentNode.SelectSingleNode("//h3[text()='D.Y']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                            if (dyNode != null)
                            {
                                string dyValue = dyNode.InnerText.Trim();

                                if (dyValue.Equals("-%"))
                                {
                                    dyValue = "0.00";
                                    Console.WriteLine($"Valor D.Y: {dyValue}");
                                    valuationIndicators.DividendYield = Convert.ToDecimal(dyValue);
                                }
                                else if (dyValue.Contains("%"))
                                {
                                    Console.WriteLine($"Valor D.Y: {dyValue}");
                                    valuationIndicators.DividendYield = Convert.ToDecimal(dyValue.Replace("%",""));
                                }
                            }
                            else
                            {
                                Console.WriteLine("Valor P/L não encontrado.");
                            }

                        // Encontra o valor do Ev Ebitda que está dentro do <strong> após o texto "Ev Ebitda"                    value d-block lh-4 fs-4 fw-700
                        var evEbitdaNode = doc.DocumentNode.SelectSingleNode("//h3[text()='EV/EBITDA']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                        if (evEbitdaNode != null)
                        {
                            string evEbitdaValue = evEbitdaNode.InnerText.Trim();
                            if (evEbitdaValue.Equals("-"))
                            {
                                evEbitdaValue = "0.00";
                                Console.WriteLine($"Valor Ev Ebitda: {evEbitdaValue}");
                                valuationIndicators.EvEbitda = Convert.ToDecimal(evEbitdaValue);
                            }
                            else
                            {
                                Console.WriteLine($"Valor Ev Ebitda: {evEbitdaValue}");
                                valuationIndicators.EvEbitda = Convert.ToDecimal(evEbitdaValue);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Valor Ev Ebitda não encontrado.");
                        }

                        // Encontra o valor do Dív. líquida/EBITDA que está dentro do <strong> após o texto "Dív. líquida/EBITDA"
                        var dlEbitdaNode = doc.DocumentNode.SelectSingleNode("//h3[text()='D&#xED;v. l&#xED;quida/EBITDA']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                        if (dlEbitdaNode != null)
                        {
                            string dlEbitdaValue = dlEbitdaNode.InnerText.Trim();

                            if (dlEbitdaValue.Equals("-%"))
                            {
                                dlEbitdaValue = "0.00";
                                Console.WriteLine($"Valor Dív. líquida/EBITDA: {dlEbitdaValue}");
                                valuationIndicators.DlEbitda = Convert.ToDecimal(dlEbitdaValue);
                            }
                            else
                            {
                                Console.WriteLine($"Valor Dív. líquida/EBITDA: {dlEbitdaValue.Replace("%","")}");
                                valuationIndicators.DlEbitda = Convert.ToDecimal(dlEbitdaValue.Replace("%", ""));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Valor Dl Ebitda não encontrado.");
                        }

                        //CompoundAnnualGrowthRate
                        // Encontra o valor do Cagr lucro que está dentro do <strong> após o texto "CAGR Lucros 5 anos"
                        var growthRateNode = doc.DocumentNode.SelectSingleNode("//h3[text()='CAGR Lucros 5 anos']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                        if (growthRateNode != null)
                        {
                            string growthRateValue = growthRateNode.InnerText.Trim();

                            if (growthRateValue.Equals("-%"))
                            {
                                growthRateValue = "0.00";
                                Console.WriteLine($"Valor Cagr Lucro: {growthRateValue}");
                                valuationIndicators.CompoundAnnualGrowthRate = Convert.ToDecimal(growthRateValue);
                            }
                            else
                            {
                                Console.WriteLine($"Valor Cagr Lucro: {growthRateValue.Replace("%", "")}");
                                valuationIndicators.CompoundAnnualGrowthRate = Convert.ToDecimal(growthRateValue.Replace("%", ""));
                            }
                        }
                        else
                        {
                            Console.WriteLine("Valor Cagr Lucro não encontrado.");
                        }

                        // Encontra o valor do PriceProfit que está dentro do <strong> após o texto "P/L"
                        var plNode = doc.DocumentNode.SelectSingleNode("//h3[text()='P/L']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");
                            
                            if (plNode != null)
                            {
                                string plValue = plNode.InnerText.Trim();
                                Console.WriteLine($"Valor P/L: {plValue}");
                                valuationIndicators.PriceProfit = Convert.ToDecimal(plValue);
                            }
                            else
                            {
                                Console.WriteLine("Valor P/L não encontrado.");
                            }

                            // Encontra o valor do Price Over Asset Value que está dentro do <strong> após o texto "P/VP"
                            var pvpNode = doc.DocumentNode.SelectSingleNode("//h3[text()='P/VP']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                            if (pvpNode != null)
                            {
                                string pvpValue = pvpNode.InnerText.Trim();
                                Console.WriteLine($"Valor P/VP: {pvpValue}");
                                valuationIndicators.PriceOverAssetValue = Convert.ToDecimal(pvpValue);
                            }
                            else
                            {
                                Console.WriteLine("Valor P/VP não encontrado.");
                            }

                            // Encontra o valor Return On Equity que está dentro do <strong> após o texto "ROE"
                            var roeNode = doc.DocumentNode.SelectSingleNode("//h3[text()='ROE']/following::strong[@class='value d-block lh-4 fs-4 fw-700']");

                            if (roeNode != null)
                            {
                                string roeValue = roeNode.InnerText.Trim();

                                if (roeValue.Contains("%"))
                                {
                                    Console.WriteLine($"Valor ROE: {roeValue}");
                                    valuationIndicators.ReturnOnEquity = Convert.ToDecimal(roeValue.Replace("%",""));
                                }

                            }
                            else
                            {
                                Console.WriteLine("Valor ROE não encontrado.");
                            }

                            ActiveData.PostActiveValuationIndicators(valuationIndicators);

                        }
                        else
                        {
                            // Exibe o status de erro
                            Console.WriteLine($"Erro: {response.StatusCode}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Exibe a mensagem de exceção
                        Console.WriteLine($"Exceção: {ex.Message}");
                    }
                }
        }
    }
}
