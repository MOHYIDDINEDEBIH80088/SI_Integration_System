using System.Text;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using System.Net;
namespace Project_RabbitMq_Desktop
{
    public partial class Send : Form
    {
        public Send()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            string partCode = GeneratePartCode();
            int productionTime = GenerateProductionTime();
            string testResult = GenerateTestResult();

            txtDate.Text = date;
            txtTime.Text = time;
            txtPartCode.Text = partCode;
            txtProductionTime.Text = productionTime.ToString();
            txtTestResult.Text = testResult;
        }
        private string GeneratePartCode()
        {
            Random random = new Random();
            string[] productTypes = { "aa", "ab", "ba", "bb" };
            string productType = productTypes[random.Next(0, 4)];
            string uniqueId = random.Next(100000, 999999).ToString();
            return productType + uniqueId;
        }

        private int GenerateProductionTime()
        {
            Random random = new Random();
            return random.Next(10, 51);
        }

        private string GenerateTestResult()
        {
            Random random = new Random();
            return random.Next(1, 7).ToString("D2");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string date = txtDate.Text;
            string time = txtTime.Text;
            string partCode = txtPartCode.Text;
            int productionTime = int.Parse(txtProductionTime.Text);
            string testResult = txtTestResult.Text;


            SaveDataToFile(date, time, partCode, productionTime, testResult);
            MessageBox.Show("Data saved to file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SaveDataToFile(string partCode, string date, string time, int productionTime, string testResult)
        {
            string data = $"{date},{time},{partCode},{productionTime},{testResult}";
            System.IO.File.AppendAllText("production_data.csv", data + Environment.NewLine);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var streamSystem = await StreamSystem.Create(new StreamSystemConfig
                {
                    UserName = "guest",
                    Password = "guest",
                    Endpoints = new List<EndPoint>
                    {
                     new IPEndPoint(IPAddress.Loopback, 5552) 
                    }
                });

                var streamName = "production_data_stream";
                var streamExists = await streamSystem.StreamExists(streamName);


                if (!streamExists)
                {
                    await streamSystem.CreateStream(new StreamSpec(streamName)
                    {
                        MaxLengthBytes = 5_000_000_000 // Set max length for the stream
                    });
                }

                var producer = await Producer.Create(new ProducerConfig(streamSystem, "production_data_stream"));
               
                string message = $"{txtDate.Text}," +
                    $"{txtTime.Text}," +
                    $"{txtPartCode.Text}," +
                    $"{txtProductionTime.Text}," +
                    $"{txtTestResult.Text}";
                var body = new RabbitMQ.Stream.Client.Message(Encoding.UTF8.GetBytes(message));
              
                await producer.Send(body);
                MessageBox.Show("Data published to RabbitMQ stream!");
               
                await producer.Close();
                await streamSystem.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error publishing data to RabbitMQ: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    

