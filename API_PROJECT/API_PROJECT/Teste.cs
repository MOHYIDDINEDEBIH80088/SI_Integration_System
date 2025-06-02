using Microsoft.Data.SqlClient;

namespace API_PROJECT
{
    public class Teste
    {
        //public int ID_Teste { get; set; }
        public int ID_Produto { get; set; }
        public string? Codigo_Resultado { get; set; }
        public DateTime Data_Teste { get; set; }

        public void WriteItem(SqlCommand cmd,
        int ID_Produto,
       string Codigo_Resultado, DateTime Data_Teste)
        {
           
            cmd.Parameters.Add("@ID_Produto", System.Data.SqlDbType.Int).Value = ID_Produto;
            cmd.Parameters.Add("@Codigo_Resultado", System.Data.SqlDbType.VarChar).Value = Codigo_Resultado;
            cmd.Parameters.Add("@Data_Teste", System.Data.SqlDbType.Date).Value = Data_Teste;
        
        }
    }
}
