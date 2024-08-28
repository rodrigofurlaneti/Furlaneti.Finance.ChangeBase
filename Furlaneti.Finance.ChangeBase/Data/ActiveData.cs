using Furlaneti.Finance.ChangeBase.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Furlaneti.Finance.ChangeBase.Data
{
    public static class ActiveData
    {
        static string connectionString = "Server=rodrigofurlaneti3108_Finance.sqlserver.dbaas.com.br,1433;Database=rodrigofurlaneti3108_Finance;User Id=rodrigofurlaneti3108_Finance;Password=Digo310884@";

        public static void PostCodeActive(string code_active)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string storedProcedureName = "Finance_Procedure_Code_Active_Insert";

            SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.Add("@NameActive", SqlDbType.NVarChar, 10).Value = code_active;

            sqlConnection.Open();

            Thread.Sleep(500);

            sqlCommand.ExecuteNonQuery();

            Thread.Sleep(500);

            sqlConnection.Dispose();
        }

        public static void PostActive(Active active)
        {
            if (active != null)
            {
                if (active.Symbol != String.Empty)
                {
                    SqlConnection sqlConnection = new SqlConnection(connectionString);

                    string storedProcedureName = "Finance_Procedure_Active_Insert";

                    SqlCommand sqlCommand = new SqlCommand(storedProcedureName, sqlConnection);

                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add("@Kind", SqlDbType.VarChar, 10).Value = active.Kind != null ? active.Kind : string.Empty;

                    sqlCommand.Parameters.Add("@Symbol", SqlDbType.VarChar, 6).Value = active.Symbol != null ? active.Symbol : string.Empty;

                    sqlCommand.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = active.Name != null ? active.Name : string.Empty;

                    sqlCommand.Parameters.Add("@Company_name", SqlDbType.VarChar, 30).Value = active.CompanyName != null ? active.CompanyName : string.Empty;

                    sqlCommand.Parameters.Add("@Document", SqlDbType.VarChar, 50).Value = active.Document != null ? active.Document : string.Empty;

                    sqlCommand.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = active.Description != null ? active.Description : string.Empty;

                    sqlCommand.Parameters.Add("@Website", SqlDbType.VarChar, 50).Value = active.Website != null ? active.Website : string.Empty;

                    sqlCommand.Parameters.Add("@Sector", SqlDbType.VarChar, 50).Value = active.Sector != null ? active.Sector : string.Empty;

                    sqlCommand.Parameters.Add("@Region", SqlDbType.VarChar, 50).Value = active.Region != null ? active.Region : string.Empty;

                    sqlCommand.Parameters.Add("@Currency", SqlDbType.VarChar, 20).Value = active.Currency != null ? active.Currency : string.Empty;

                    sqlCommand.Parameters.Add("@Price", SqlDbType.Float).Value = active.Price != null ? active.Price : 0.00;

                    sqlCommand.Parameters.Add("@Change_price", SqlDbType.Float).Value = active.ChangePrice != null ? active.ChangePrice : 0.00;

                    sqlCommand.Parameters.Add("@Change_percent", SqlDbType.Float).Value = active.ChangePercent != null ? active.ChangePercent : 0.00;

                    sqlCommand.Parameters.Add("@MarketCap", SqlDbType.Float).Value = active.MarketCap != null ? active.MarketCap : 0.00;

                    if (active.MarketTime != null)
                    {
                        sqlCommand.Parameters.Add("@Equity", SqlDbType.BigInt).Value = active.Financials.Equity != null ? active.Financials.Equity : 0;

                        sqlCommand.Parameters.Add("@Equity_per_share", SqlDbType.Float).Value = active.Financials.Equity_per_share != null ? active.Financials.Equity_per_share : 0.00;

                        sqlCommand.Parameters.Add("@Price_to_book_ratio", SqlDbType.Float).Value = active.Financials.Price_to_book_ratio != null ? active.Financials.Price_to_book_ratio : 0.00;

                        sqlCommand.Parameters.Add("@Quota_count", SqlDbType.BigInt).Value = active.Financials.Quota_count != null ? active.Financials.Quota_count : 0;

                        sqlCommand.Parameters.Add("@Yield_12m", SqlDbType.Float).Value = active.Financials.Dividends.Yield_12m != null ? active.Financials.Dividends.Yield_12m : 0.00;

                        sqlCommand.Parameters.Add("@Yield_12m_sum", SqlDbType.Float).Value = active.Financials.Dividends.Yield_12m_sum != null ? active.Financials.Dividends.Yield_12m_sum : 0.00;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@Equity", SqlDbType.BigInt).Value = 0;

                        sqlCommand.Parameters.Add("@Equity_per_share", SqlDbType.Float).Value = 0.00;

                        sqlCommand.Parameters.Add("@Price_to_book_ratio", SqlDbType.Float).Value = 0.00;

                        sqlCommand.Parameters.Add("@Quota_count", SqlDbType.BigInt).Value = 0;

                        sqlCommand.Parameters.Add("@Yield_12m", SqlDbType.Float).Value = 0.00;

                        sqlCommand.Parameters.Add("@Yield_12m_sum", SqlDbType.Float).Value = 0.00;
                    }

                    if (active.MarketTime != null)
                    {
                        sqlCommand.Parameters.Add("@OpenTime", SqlDbType.VarChar, 5).Value = active.MarketTime.Open != null ? active.MarketTime.Open : "NULL";

                        sqlCommand.Parameters.Add("@CloseTime", SqlDbType.VarChar, 5).Value = active.MarketTime.Close != null ? active.MarketTime.Close : "NULL";

                        sqlCommand.Parameters.Add("@Timezone", SqlDbType.Int).Value = active.MarketTime.Timezone != null ? active.MarketTime.Timezone : 0;

                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@OpenTime", SqlDbType.VarChar, 5).Value = "00:00";

                        sqlCommand.Parameters.Add("@CloseTime", SqlDbType.VarChar, 5).Value = "00:00";

                        sqlCommand.Parameters.Add("@Timezone", SqlDbType.Int).Value = 0;

                    }

                    if (active.Logo != null)
                    {
                        sqlCommand.Parameters.Add("@Small", SqlDbType.VarChar, 250).Value = active.Logo.Small != null ? active.Logo.Small : string.Empty;

                        sqlCommand.Parameters.Add("@Big", SqlDbType.VarChar, 250).Value = active.Logo.Big != null ? active.Logo.Big : string.Empty;
                    }
                    else
                    {
                        sqlCommand.Parameters.Add("@Small", SqlDbType.VarChar, 250).Value = string.Empty;

                        sqlCommand.Parameters.Add("@Big", SqlDbType.VarChar, 250).Value = string.Empty;
                    }


                    sqlCommand.Parameters.Add("@Updated_at", SqlDbType.VarChar, 50).Value = active.UpdatedAt != null ? active.UpdatedAt : DateTime.Now;

                    SqlParameter outputIdActive = new SqlParameter("@IdActive", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    sqlCommand.Parameters.Add(outputIdActive);

                    SqlParameter outputReturn = new SqlParameter("@Return", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    sqlCommand.Parameters.Add(outputReturn);

                    sqlConnection.Open();

                    Thread.Sleep(250);

                    sqlCommand.ExecuteNonQuery();

                    Thread.Sleep(250);

                    sqlConnection.Dispose();

                    Console.WriteLine("Save BD:" + active.Name);
                }
                else
                {
                    Console.WriteLine("Active symbol empty");
                }

            }
            else
            {
                Console.WriteLine("Active null");
            }
            
        }

        public static List<CodeActive> GetAllCodeActive()
        {
            List<CodeActive> listCodeActive = new List<CodeActive>();

            string storedProcedureName = "Finance_Procedure_Code_Active_GetAll";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    connection.Open();

                    int count = 1;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            CodeActive codeActive = new CodeActive
                            {
                                IdCode = count,
                                NameActive = reader.GetString(reader.GetOrdinal("NameActive")),
                            };
                            listCodeActive.Add(codeActive);
                            count++;
                        }
                    }
                }
            }

            return listCodeActive;
        }

        
    }
}