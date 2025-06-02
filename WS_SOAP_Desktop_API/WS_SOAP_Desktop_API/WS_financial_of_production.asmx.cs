using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WS_SOAP_Desktop_API
{
    /// <summary>
    /// Description résumée de WS_financial_of_production
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_financial_of_production : System.Web.Services.WebService
    {
        string sqlConnectionString = "Data Source=localhost\\SQLEXPRESS;" +
             "Initial Catalog=Contabilidade;" +
             "Integrated Security=True;" +
             "Connect Timeout=30;" +
             "Encrypt=False;" +
             "TrustServerCertificate=False;" +
             "ApplicationIntent=ReadWrite;" +
             "MultiSubnetFailover=False";
        /*
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }*/
        // 1. Part with Greatest Loss
        [WebMethod]
        public string GetPartWithGreatestLoss()
        {
            string partCode = string.Empty;
            decimal maxLoss = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetPartWithGreatestLoss", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        partCode = Convert.ToString(reader["codigo_peca"]);
                        maxLoss = Convert.ToDecimal(reader["total_loss"]);
                    }
                }
            }
            return partCode;
        }
        // 2. Obtain Total Production Costs in a period
        [WebMethod]
        public decimal GetTotalProductionCosts(DateTime startDate, DateTime endDate)
        {
            decimal totalCost = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTotalProductionCosts", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    con.Open();
                   
                    object result = cmd.ExecuteScalar();
                   
                    if (result != DBNull.Value)
                    {
                        totalCost = Convert.ToDecimal(result);
                    }
                }
            }
            return totalCost;
        }
        // 3. Obtain total profit in a period
        [WebMethod]
        public decimal GetTotalProfit(DateTime startDate, DateTime endDate)
        {
            decimal totalProfit = 0;
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTotalProfit", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    con.Open();
                   
                    object result = cmd.ExecuteScalar();
                  
                    if (result != DBNull.Value)
                    {
                        totalProfit = Convert.ToDecimal(result);
                    }
                }
            }
            return totalProfit;
        }
        // 4. Obtain total loss of each of the parts in a period
        [WebMethod]
        public List<LossByPart> GetTotalLossByPart(DateTime startDate, DateTime endDate)
        {
            var totalLossByPart = new List<LossByPart>();
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTotalLossByPart", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var lossData = new LossByPart
                        {
                            PartCode = reader["PartCode"] != DBNull.Value ? reader["PartCode"].ToString() : string.Empty,
                            TotalLoss = reader["TotalLoss"] != DBNull.Value ? Convert.ToDecimal(reader["TotalLoss"]) : 0
                        };
                        totalLossByPart.Add(lossData);
                    }
                }
            }
            return totalLossByPart;
        }
        // 5. Get Detailed Financial Data by Part
        [WebMethod]
        public FinancialData GetDetailedFinancialData(string partCode)
        {
            FinancialData financialData = new FinancialData();
            using (SqlConnection con = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDetailedFinancialData", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PartCode", partCode);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        financialData.PartCode = Convert.ToString(reader["codigo_peca"]);
                        financialData.TotalCost = Convert.ToDecimal(reader["total_cost"]);
                        financialData.TotalProfit = Convert.ToDecimal(reader["total_profit"]);
                        financialData.TotalLoss = Convert.ToDecimal(reader["total_loss"]);
                    }
                }
            }
            return financialData;
        }
    }
    public class FinancialData
    {
        public string PartCode { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalLoss { get; set; }
    }
    public class LossByPart
    {
        public string PartCode { get; set; }
        public decimal TotalLoss { get; set; }
    }
}

