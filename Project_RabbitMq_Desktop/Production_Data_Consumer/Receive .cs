using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Data_Consumer
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private const string ApiBaseUrl = "https://localhost:7031/api/Product";

        public Form1()
        {
            InitializeComponent();
            _httpClient.BaseAddress = new Uri(ApiBaseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            StartListening();
        }

        private async void StartListening()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.ExchangeDeclareAsync("production_data_exchange", ExchangeType.Topic, durable: true);

            var queueDeclareResult = await channel.QueueDeclareAsync(
                queue: "production_data_ex",
                durable: true,
                exclusive: false,
                autoDelete: false
            );
            var queueName = queueDeclareResult.QueueName;

            await channel.QueueBindAsync(queue: queueName, exchange: "production_data_exchange", routingKey: "test.failed");
            await channel.QueueBindAsync(queue: queueName, exchange: "production_data_exchange", routingKey: "test.passed");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var routingKey = ea.RoutingKey;

                Console.WriteLine($"Received message with routing key: {routingKey} - Message: {message}");

                if (routingKey == "test.failed")
                {
                    await ProcessFailedMessage(message);
                }
                else if (routingKey == "test.passed")
                {
                    await ProcessPassedMessage(message);
                }
            };
            await channel.BasicConsumeAsync(queue: queueName, autoAck: true, consumer: consumer);
        }

        private async Task ProcessFailedMessage(string message)
        {
            var data = message.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 5)
            {
                MessageBox.Show("Received message format is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string date = data[0].Trim();
            string time = data[1].Trim();
            string partCode = data[2].Trim();
            string productionTime = data[3].Trim();
            string testResult = data[4].Trim();

            string displayMessage = $"Date: {date}, Time: {time}, Part Code: {partCode}, Production Time: {productionTime}, Test Result: {testResult}";
            MessageBox.Show($"Received failed message: {displayMessage}");
            listBoxMessages.Items.Add($"Failed: {displayMessage}");
        }

        private async Task ProcessPassedMessage(string message)
        {
            var data = message.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (data.Length < 5)
            {
                MessageBox.Show("Received message format is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string date = data[0].Trim();
            string time = data[1].Trim();
            string partCode = data[2].Trim();
            string productionTime = data[3].Trim();
            string testResult = data[4].Trim();

            // Send data to the API regardless of the testResult
            bool isSuccess = await SendDataToWebAPI(partCode, date, time, productionTime, testResult);
            if (isSuccess)
            {
                Console.WriteLine($"Data sent to API successfully for part code: {partCode}");
            }
        }

        private async Task<bool> SendDataToWebAPI(string partCode, string date, string time, string productionTime, string testResult)
        {
            try
            {
                var productData = new
                {
                    Codigo_Peca = partCode,
                    Data_Producao = date,
                    Hora_Producao = time,
                    Tempo_Producao = productionTime,
                    TestResult = testResult
                };
                var json = JsonSerializer.Serialize(productData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync(ApiBaseUrl, content);

                Console.WriteLine($"API Response Status: {response.StatusCode}");
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show($"API Error: {response.StatusCode} - {responseContent}",
                        "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Network Error: {httpEx.Message}",
                    "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
