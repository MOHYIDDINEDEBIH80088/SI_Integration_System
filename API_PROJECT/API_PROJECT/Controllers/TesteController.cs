using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController : ControllerBase
    {
        string sqlConnectionString = "Data Source=localhost\\SQLEXPRESS;" +
           "Initial Catalog=Producao;" +
           "Integrated Security=True;" +
           "Connect Timeout=30;" +
           "Encrypt=False;" +
           "TrustServerCertificate=False;" +
           "ApplicationIntent=ReadWrite;" +
           "MultiSubnetFailover=False";
        /* // GET: api/<TesteController>
         [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }*/

        /* // GET api/<TesteController>/5
         [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }
        */
        // POST api/<TesteController>
        [HttpPost]
        public ActionResult Post([FromBody] Teste body)
        {
            try
            {
                Console.Write("POST Request");
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_InsertTestes", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        Teste test = new Teste();

                        test.WriteItem(cmd, body.ID_Produto, body.Codigo_Resultado, body.Data_Teste);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        return Ok("Registration successful!");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
        
       /*// PUT api/<TesteController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TesteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
