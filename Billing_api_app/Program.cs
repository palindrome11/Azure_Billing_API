using CodeHollow.AzureBillingApi;
using System;
using System.Linq;
 
namespace BillingApiSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeHollow.AzureBillingApi.Client c = new CodeHollow.AzureBillingApi.Client(
                "rvbdtech.onmicrosoft.com", "31cfb965-8f6c-4a59-a7e8-4c535915cb71", "05ef5fb3-737f-4623-a119-51ff5e4923df", "http://localhost/billingapi");

//            c.GetResourceCostsForPeriod

//            var costs = c.GetResourceCostsForPeriod("MS-AZR-0017G", "EUR", "en-US", "AT", 2021, 6);

//            c.GetResourceCostsForPeriod()

/*            Console.WriteLine("Total costs: " + costs.TotalCosts);
            PrintMeters(costs);
*/
            Console.WriteLine("Press key to exit!");
            Console.ReadKey();
        }

        private static void PrintMeters(ResourceCostData resourceCosts)
        {
            var meterIds = resourceCosts.GetUsedMeterIds();

            foreach (var x in meterIds)
            {
                var currates = resourceCosts.Costs.GetCostsByMeterId(x);
                string metername = resourceCosts.GetMeterById(x).MeterName;
                var curcosts = currates.Sum(y => y.CalculatedCosts);
                var billable = currates.Sum(y => y.BillableUnits);
                var usage = currates.Sum(y => y.UsageValue.Properties.Quantity);

                var curusagevalue = currates.First().UsageValue;

                Console.WriteLine($"{metername.PadRight(72)} : {usage.ToString("0.################")} ({billable.ToString("0.################")}) - {curcosts.ToString("0.################")}");
            }
        }
    }
}
