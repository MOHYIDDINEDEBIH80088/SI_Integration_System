using RabbitMQ.Client;
using RabbitMQ.Stream.Client;
using System;
using System.Text;
using System.Threading.Channels;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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
            string productType = productTypes[random.Next(0, productTypes.Length)];
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

        private void SaveDataToFile(string date, string time, string partCode, int productionTime, string testResult)
        {
            string data = $"{date},{time},{partCode},{productionTime},{testResult}";
            System.IO.File.AppendAllText("production_data.csv", data + Environment.NewLine);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            try
            {
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                
                await channel.ExchangeDeclareAsync(exchange: "production_data_exchange", type: ExchangeType.Topic, durable: true, autoDelete: false);
                string message = $"{txtDate.Text}," +
                                 $"{txtTime.Text}," +
                                 $"{txtPartCode.Text}," +
                                 $"{txtProductionTime.Text}," +
                                 $"{txtTestResult.Text}";
                var body = Encoding.UTF8.GetBytes(message);


                
                string routingKey = (txtTestResult.Text == "01") ? "test.passed" : "test.failed";
                MessageBox.Show($"Publishing message: {message} with routing key: {routingKey}");
                await channel.BasicPublishAsync(
                    exchange: "production_data_exchange",
                    routingKey: routingKey,
                    body: body);
                MessageBox.Show("Data published to RabbitMQ!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error publishing data to RabbitMQ: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    
}
}
