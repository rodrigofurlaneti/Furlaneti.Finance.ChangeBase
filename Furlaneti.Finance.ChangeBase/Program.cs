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
                if(active.Name != null)
                    if(!active.Name.Equals(string.Empty))
                        ActiveData.PostActive(active);
            }
        }
    }
}