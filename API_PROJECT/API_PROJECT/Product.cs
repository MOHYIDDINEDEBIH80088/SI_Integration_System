using Microsoft.Data.SqlClient;

namespace API_PROJECT
{
    public class Product
    {
       // public int ID_Produto { get; set; }
        public string? Codigo_Peca { get; set; }
        public DateTime Data_Producao { get; set; }
        public TimeSpan Hora_Producao { get; set; }
        public int Tempo_Producao { get; set; }

        public string? TestResult { get; set; }


        public void WriteItem(SqlCommand cmd,
         string Codigo_Peca,
        DateTime Data_Producao, TimeSpan Hora_Producao, int Tempo_Producao, string TestResult)
        {
           // cmd.Parameters.Add("@ID_Produto", System.Data.SqlDbType.Int).Value = ID_Produto;
            cmd.Parameters.Add("@Codigo_Peca", System.Data.SqlDbType.VarChar).Value = Codigo_Peca;
            cmd.Parameters.Add("@Data_Producao", System.Data.SqlDbType.Date).Value = Data_Producao;
            cmd.Parameters.Add("@Hora_Producao", System.Data.SqlDbType.Time).Value = Hora_Producao;
            cmd.Parameters.Add("@Tempo_Producao", System.Data.SqlDbType.Int).Value = Tempo_Producao;
           cmd.Parameters.Add("@TestResult", System.Data.SqlDbType.VarChar).Value = TestResult;


        }
    }
}
