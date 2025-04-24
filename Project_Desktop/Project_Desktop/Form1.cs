using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Project_Desktop
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string partCode = GeneratePartCode();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("HH:mm:ss");
            int productionTime = GenerateProductionTime();
            string testResult = GenerateTestResult();

            txtPartCode.Text = partCode;
            txtDate.Text = date;
            txtTime.Text = time;
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
            string partCode = txtPartCode.Text;
            string date = txtDate.Text;
            string time = txtTime.Text;
            int productionTime = int.Parse(txtProductionTime.Text);
            string testResult = txtTestResult.Text;


            SaveDataToFile(partCode, date, time, productionTime, testResult);
            MessageBox.Show("Data saved to file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SaveDataToFile(string partCode, string date, string time, int productionTime, string testResult)
        {
            string data = $"{partCode},{date},{time},{productionTime},{testResult}";
            System.IO.File.AppendAllText("production_data.csv", data + Environment.NewLine);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            // Get data from textboxes
            string partCode = txtPartCode.Text;
            string date = txtDate.Text;
            string time = txtTime.Text;          
            int productionTime = int.Parse(txtProductionTime.Text);
            string testResult = txtTestResult.Text;

            bool success = await SendDataToWebAPI(partCode,date, time, productionTime, testResult);
            if (success)
            {
                MessageBox.Show("Data sent to web API successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to send data to web API.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private async Task<bool> SendDataToWebAPI(string date, string time, string partCode, int productionTime, string testResult)
        {
            using (HttpClient client = new HttpClient())
            {
                var json = $"{{ \"partCode\": \"{partCode}\",\"date\": \"{date}\", \"time\": \"{time}\", \"productionTime\": {productionTime}, \"testResult\": \"{testResult}\"}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7031/api/Product", content);
                    response.EnsureSuccessStatusCode(); 
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}
