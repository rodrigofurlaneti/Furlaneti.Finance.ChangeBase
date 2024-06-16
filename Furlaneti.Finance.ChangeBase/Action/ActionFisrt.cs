using OfficeOpenXml;
using Furlaneti.Finance.ChangeBase.Data;
using Furlaneti.Finance.ChangeBase.Model;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Furlaneti.Finance.ChangeBase.Action
{
    public static class ActionFisrt
    {
        public static void PopulateExchangeAssetCodeTable()
        {
            // Caminho para o arquivo Excel
            string filePath = "C:\\Users\\adm\\source\\repos\\Finance\\Furlaneti.Finance.ChangeBase\\Furlaneti.Finance.ChangeBase\\Archive\\acoes.xlsx";

            // Verifica se o arquivo existe
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Arquivo não encontrado.");
                return;
            }

            // Carrega o arquivo Excel
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Verifica se há planilhas no arquivo
                if (package.Workbook.Worksheets.Count == 0)
                {
                    Console.WriteLine("O arquivo Excel não contém planilhas.");
                    return;
                }

                var workbook = package.Workbook;

                var worksheets = package.Workbook.Worksheets;

                //Obtém o número total de planilhas
                int totalWorksheets = package.Workbook.Worksheets.Count;

                Console.WriteLine($"Número de planilhas no arquivo: {totalWorksheets}");

                if (worksheets.Count == 0)
                {
                    Console.WriteLine("O arquivo Excel não contém planilhas.");
                    return;
                }

                int desiredIndex = 1; // Example index (1-based)

                if (desiredIndex > 0 && desiredIndex <= totalWorksheets)
                {
                    //Acessa a planilha por índice
                    var worksheetIndex = package.Workbook.Worksheets[desiredIndex];

                    //Executa operações na planilha
                    Console.WriteLine($"Worksheet Name: {worksheetIndex.Name}");

                }
                else
                {
                    Console.WriteLine("Worksheet index out of range.");
                }

                var worksheet = package.Workbook.Worksheets[desiredIndex];


                // Verifica se a planilha não é nula
                if (worksheet == null)
                {
                    Console.WriteLine("A planilha não foi encontrada.");
                    return;
                }

                Console.WriteLine($"Lendo a planilha: {worksheet.Name}");

                // Obtém o número total de linhas
                int totalRows = worksheet.Dimension?.Rows ?? 0;

                if (totalRows == 0)
                {
                    Console.WriteLine("A planilha está vazia ou não pôde ser lida.");
                    return;
                }

                // Itera sobre cada linha da primeira coluna
                for (int row = 1; row <= totalRows; row++)
                {
                    var cellValue = worksheet.Cells[row, 1].Text; // Primeira coluna (index 1)

                    Console.WriteLine($"Linha {row}: {cellValue}");

                    ActiveData.PostCodeActive(cellValue);
                }
            }
        }
        public async static void RequestForHgBrasilApi()
        {
            //Consulta no banco o nome dos ativos
            var listCodeActive = ActiveData.GetAllCodeActive();

            foreach (var code in listCodeActive)
            {
                Console.WriteLine("Name active:"+code.NameActive);
                //Consulta na api os dados do ativo
                var active = await ActionSecond.GetActiveApiAsync(code.NameActive);

                //Inserir na tabela Ativo
                ActiveData.PostActive(active);
            }
        }


    }
}
