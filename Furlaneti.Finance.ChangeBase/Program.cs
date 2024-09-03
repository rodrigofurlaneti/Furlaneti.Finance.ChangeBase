using Furlaneti.Finance.ChangeBase.Action;
using Furlaneti.Finance.ChangeBase.Data;

namespace HttpClientExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Consulta no banco o nome dos ativos
            var listCodeActive = ActiveData.GetAllCodeActive();

            foreach (var code in listCodeActive)
            {
                //Consulta na api os dados do ativo
                var active = await ActionSecond.GetActiveApiAsync(code.NameActive);

                //Inserir na tabela Ativo
                if (active.Name != null)
                    if (!active.Name.Equals(string.Empty))
                        ActiveData.PostActive(active);
            }

            //Consulta na api os dados do ativos Get High B3
            var listActiveGetHighB3 = await ActionThree.GetActiveApiGetHighB3Async();

            //Inserir na tabela Ativo Get High B3
            ActiveData.PostActiveGetHighB3(listActiveGetHighB3);

            //Consulta na api os dados do ativos Get Low B3
            var listActiveGetLowB3 = await ActionFour.GetActiveApiGetLowB3Async();

            //Inserir na tabela Ativo Get High B3
            ActiveData.PostActiveGetLowB3(listActiveGetLowB3);

            //Consulta os dados do site "status invest"
            foreach (var code in listCodeActive)
            {
                //Consulta na api os dados do ativo
                await ActionFive.ExtractWebsiteDataAsync(code.NameActive);
            }

        }
    }
}