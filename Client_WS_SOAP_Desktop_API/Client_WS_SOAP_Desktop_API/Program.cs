using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_WS_SOAP_Desktop_API
{
    internal class Program
    { 
        static void Main(string[] args)
        {
            /* Client_WS_SOAP_HelloWorld.ServiceReference_HelloWorld.WS_HelloWorldSoapClient client = 
             new Client_WS_SOAP_HelloWorld.ServiceReference_HelloWorld.WS_HelloWorldSoapClient();
             String result = client.HelloWorld("Mahyou");
             String result1 = client.HelloWorldEnglish("Mahyou");
             Console.WriteLine(result);
             Console.WriteLine(result1);*/
            var client = new Client_WS_SOAP_HelloWorld.ServiceReference_Financial_of_Production.WS_financial_of_productionSoapClient();
        
            Console.WriteLine("Please enter the start date (format: yyyy-MM-ddT00:00:00):");
            string startInput = Console.ReadLine();
         
            Console.WriteLine("Please enter the end date (format: yyyy-MM-ddT00:00:00):");
            string endInput = Console.ReadLine();
           
            Console.WriteLine($"Part with Greatest Loss: {client.GetPartWithGreatestLoss()}");
          
            DateTime startDate = DateTime.Parse(startInput);
            DateTime endDate = DateTime.Parse(endInput);
            Console.WriteLine($"Total Production Costs: {client.GetTotalProductionCosts(startDate, endDate)}");
            Console.WriteLine($"Total Profit: {client.GetTotalProfit(startDate, endDate)}");
            var totalLossByPart = client.GetTotalLossByPart(startDate, endDate);
            Console.WriteLine("Total Loss by Part:");
            foreach (var item in totalLossByPart)
            {
                Console.WriteLine($"Part: {item.PartCode}, Total Loss: {item.TotalLoss}");
            }

        }
    }
}
