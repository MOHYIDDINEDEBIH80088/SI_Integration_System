using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Windows.Forms;
using RabbitMQ.Stream.Client;
using RabbitMQ.Stream.Client.Reliable;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Net;
namespace Production_Data_Consumer
{
    public partial class Form1 : Form
    {
        private int totalPartsProduced = 0;
        private int totalPartsWithoutDefects = 0;
        private int totalPartsMarkedOK = 0;
        private List<double> productionTimes = new List<double>();
        private StreamSystem streamSystem;
        private Consumer consumer;
        public Form1()
        {
            InitializeComponent();
            StartListening();
        }

        private async void StartListening()
        {
            try
            {
                // Create a connection to the RabbitMQ server
                streamSystem = await StreamSystem.Create(new StreamSystemConfig
                {
                    UserName = "guest",
                    Password = "guest",
                    Endpoints = new List<EndPoint>
                    {
                        new IPEndPoint(IPAddress.Loopback, 5552) // Use the stream port
                    }
                });
                consumer = await Consumer.Create(new ConsumerConfig(streamSystem, "production_data_stream")
                {
                    OffsetSpec = new OffsetTypeFirst(),
                    MessageHandler = async (stream, _, _, message) =>
                    {
                        string msg = Encoding.UTF8.GetString(message.Data.Contents);
                        
                        Invoke(new Action(() => ProcessMessage(msg)));
                        await Task.CompletedTask;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting consumer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void ProcessMessage(string message)
        {
            
            var data = message.Split(',');
           
            if (data.Length < 5)
            {
                MessageBox.Show("Message format is incorrect.");
                return;
            }
            string date = data[0];
            string time = data[1];
            string partCode = data[2];
            double productionTime;
            if (!double.TryParse(data[3], out productionTime))
            {
                MessageBox.Show("Invalid production time format.");
                return;
            }
            string testResult = data[4];
            totalPartsProduced++;
            productionTimes.Add(productionTime);
            switch (testResult)
            {
                case "01": // Ok
                    totalPartsMarkedOK++;
                    break;
                case "02": // Failed visual inspection
                case "03": // Failed resistance inspection
                case "04": // Failed dimension inspection
                case "05": // Failed tightness inspection
                           // Do not increment totalPartsWithoutDefects
                    break;
                case "06": // Unknown
                           // Handle unknown case if needed
                    break;
                default:
                    MessageBox.Show("Unknown test result code.");
                    return;
            }
            // Increment total parts without defects if the part is not defective
            if (testResult == "01")
            {
                totalPartsWithoutDefects++;
            }
            double averageProductionTime = productionTimes.Average();
            UpdateMetricsDisplay();
            string displayMessage = $"Date: {date},   Time: {time} ,   Part Code: {partCode} ,   Production Time: {productionTime} ,   Test Result: {testResult} ";
            listBoxMessages.Items.Add(displayMessage);
        }
        private void UpdateMetricsDisplay()
        {
            lblTotalPartsProduced.Text = $"Total Parts Produced: {totalPartsProduced}";
            lblTotalPartsWithoutDefects.Text = $"Total Without Defects: {totalPartsWithoutDefects}";
            lblTotalPartsOK.Text = $"Total OK: {totalPartsMarkedOK}";
            lblAverageProductionTime.Text = $"Average Production Time: {productionTimes.Average():F2}";
        }
    }
}
