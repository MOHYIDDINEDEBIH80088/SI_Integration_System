using Microsoft.Data.SqlClient;

namespace API_PROJECT
{
    public class Custos_Peca
    {
        // public int ID_Custo { get; set; }
        public int ID_Produto { get; set; }
        public string? Codigo_Peca { get; set; }
        public int Tempo_Producao { get; set; }
        public int Custo_Producao { get; set; }
        public float Lucro { get; set; }
        public float Prejuizo { get; set; }

        public DateTime Data_Producao { get; set; }

        public void WriteItem(SqlCommand cmd,int ID_Produto,
         string Codigo_Peca, int Tempo_Producao, int Custo_Producao, float Lucro,float Prejuizo, DateTime Data_Producao)
        {
            cmd.Parameters.Add("@ID_Produto", System.Data.SqlDbType.Int).Value = ID_Produto;
            cmd.Parameters.Add("@Codigo_Peca", System.Data.SqlDbType.VarChar).Value = Codigo_Peca;
            cmd.Parameters.Add("@Tempo_Producao", System.Data.SqlDbType.Int).Value = Tempo_Producao;
            cmd.Parameters.Add("@Custo_Producao", System.Data.SqlDbType.Int).Value = Custo_Producao;
            cmd.Parameters.Add("@Lucro", System.Data.SqlDbType.Float).Value = Lucro;
            cmd.Parameters.Add("@Prejuizo", System.Data.SqlDbType.Float).Value = Prejuizo;
            cmd.Parameters.Add("@Data_Producao", System.Data.SqlDbType.Date).Value = Data_Producao;

        }
    }
}
