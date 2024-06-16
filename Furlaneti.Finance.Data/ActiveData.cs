using Furlaneti.Finance.ChangeBase.Model;
using System.Data;
using System.Data.SqlClient;

namespace Furlaneti.Finance.Data
{
    public static class ActiveData
    {
        static string connectionString = "Server=rodrigofurlaneti3108_Finance.sqlserver.dbaas.com.br,1433;Database=rodrigofurlaneti3108_Finance;User Id=rodrigofurlaneti3108_Finance;Password=Digo310884@";

        public static void GetCodeActive(string code_active)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string storedProcedureName = "Finance_Procedure_Code_Active_Insert";

            SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@NameActive", SqlDbType.NVarChar, 10).Value = code_active;

            sqlConnection.Open();

            Thread.Sleep(1000);

            sqlCommand.ExecuteNonQuery();

            Thread.Sleep(1000);

            sqlConnection.Dispose();
        }

        public static List<CodeActive> GetAllCodeActive()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string storedProcedureName = "Finance_Procedure_Code_Active_Insert";

            SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@NameActive", SqlDbType.NVarChar, 10).Value = code_active;

            sqlConnection.Open();

            Thread.Sleep(1000);

            sqlCommand.ExecuteNonQuery();

            Thread.Sleep(1000);

            sqlConnection.Dispose();
        }

        public void PostActive(ActiveModel active)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string storedProcedureName = "Finance_Procedure_Active_Insert";

            SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@Document", SqlDbType.Int).Value = active.Document;

            sqlCommand.Parameters.Add("@Change_percent", SqlDbType.NVarChar, 50).Value = active.Change_percent;

            sqlCommand.Parameters.Add("@Change_price", SqlDbType.NVarChar, 30).Value = active.Change_price;

            sqlCommand.Parameters.Add("@Currency", SqlDbType.NVarChar, 20).Value = active.Currency;

            sqlCommand.Parameters.Add("@Company_name", SqlDbType.NVarChar, 50).Value = active.Company_name;

            sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 50).Value = active.Description;

            sqlCommand.Parameters.Add("@Financials", SqlDbType.NVarChar, 50).Value = active.Financials;

            sqlCommand.Parameters.Add("@Kind", SqlDbType.NVarChar, 50).Value = active.Kind;

            sqlCommand.Parameters.Add("@Price", SqlDbType.Float, 50).Value = active.Price;

            sqlCommand.Parameters.Add("@Logo", SqlDbType.NVarChar, 50).Value = active.Logo;

            sqlCommand.Parameters.Add("@Market_cap", SqlDbType.NVarChar, 50).Value = active.Market_cap;

            sqlCommand.Parameters.Add("@Market_time", SqlDbType.NVarChar, 50).Value = active.Market_time;

            sqlCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = active.Name;

            sqlCommand.Parameters.Add("@Price", SqlDbType.NVarChar, 50).Value = active.Price;

            sqlCommand.Parameters.Add("@Region", SqlDbType.NVarChar, 50).Value = active.Region;

            sqlCommand.Parameters.Add("@Sector", SqlDbType.NVarChar, 50).Value = active.Sector;

            sqlCommand.Parameters.Add("@Symbol", SqlDbType.NVarChar, 50).Value = active.Symbol;

            sqlCommand.Parameters.Add("@Updated_at", SqlDbType.NVarChar, 50).Value = active.Updated_at;

            sqlCommand.Parameters.Add("@Website", SqlDbType.NVarChar, 50).Value = active.Website;

            sqlConnection.Open();

            sqlCommand.ExecuteNonQuery();
        }
    }
}