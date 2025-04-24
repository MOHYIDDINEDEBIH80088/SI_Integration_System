using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        string sqlConnectionString = "Data Source=localhost\\SQLEXPRESS;" +
            "Initial Catalog=Producao;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False";

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                Console.Write("GET Request");
                List<Product> products = new List<Product>();

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetProdutos", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        con.Open();
                        Console.WriteLine("✅ Connected successfully!");
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Product item = new Product();
                           // item.ID_Produto = Convert.ToInt32(reader["id_produto"]);
                            item.Codigo_Peca = Convert.ToString(reader["codigo_peca"]);
                            item.Data_Producao = Convert.ToDateTime(reader["data_producao"]);
                            item.Hora_Producao = (TimeSpan)reader["hora_producao"];
                            item.Tempo_Producao = Convert.ToInt32(reader["tempo_producao"]);
                            products.Add(item);
                        }
                        con.Close();
                        return Ok(products);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        // GET api/<ProductController>/5
        /*[HttpGet("{ID_Produto}")]
        public string Get(int ID_Produto)
        {
            return "value";
        }
        */
        // POST api/<ProductController>
        [HttpPost]
        public ActionResult Post([FromBody] Product body)
        {
            try
            {
                Console.Write("POST Request");
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_InsertProduto", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                        Product product = new Product();

                        product.WriteItem(cmd, body.Codigo_Peca,
                            body.Data_Producao, body.Hora_Producao,body.Tempo_Producao);

                        cmd.ExecuteNonQuery();
                        con.Close();

                        return Ok("Registration successful!");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString()); return BadRequest();
            }

        }


        // PUT api/<ProductController>/5
        [HttpPut("{ID_Produto}")]
        public ActionResult Put(int ID_Produto, [FromBody] Product body)
        {
            try
            {
                Console.Write("PUT Request");
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateProductByID", con))
                    {
                        //CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@ID_Produto", body.ID_Produto);
                        cmd.Parameters.AddWithValue("@Codigo_Peca", body.Codigo_Peca);
                        cmd.Parameters.AddWithValue("@Data_Producao", body.Data_Producao);
                        cmd.Parameters.AddWithValue("@Hora_Producao", body.Hora_Producao);
                        cmd.Parameters.AddWithValue("@Tempo_Producao", body.Tempo_Producao);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        return Ok("Update successful!");

                    }
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString()); 
                return BadRequest();
            }
        }


                // DELETE api/<ProductController>/5
                [HttpDelete("{ID_Produto}")]
                public ActionResult Delete(int ID_Produto)
                {
                    try
                    {
                        Console.Write("DELETE Request");

                        using (SqlConnection con = new SqlConnection(sqlConnectionString))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.CommandText = "SP_DeleteProdutoByID"; 
                                cmd.Connection = con;
                                cmd.CommandType = System.Data.CommandType.StoredProcedure; 
                                cmd.Parameters.AddWithValue("@ID_Produto", ID_Produto);

                                con.Open(); 
                                cmd.ExecuteNonQuery(); 
                                con.Close();

                                return Ok("Product successfully eliminated");
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString()); 
                        return BadRequest();
                    }
                }
    }

}
    
