using LoanCalculator.Core;
using Newtonsoft.Json;
using System;

namespace Host.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("Loan Calculator");
            System.Console.WriteLine("Please Enter loan information");
            System.Console.WriteLine("amount, interest, downpayment, and term");
            System.Console.WriteLine("==========================================");

            bool amountSet = false, interestSet = false, downPaymentSet = false, termSet = false;
            double amount = 0;
            double interest = 0;
            double downpayment = 0;
            int term = 0;

            try
            {
                // since its a demo we can just do a quick in place parsing
                // but there are so many good cmd libraries out there can do the job in cleaner way
                string inputLine = string.Empty;
                while (!string.IsNullOrEmpty(inputLine = System.Console.ReadLine()) 
                    || (amountSet && interestSet && downPaymentSet && termSet) == false)
                {
                    inputLine = inputLine.ToLower();
                    var values = inputLine.Split(':');
                    if (values.Length != 2) System.Console.WriteLine("Enter valid format <key>:<value>");

                    if (values[0].Equals("amount"))
                    {
                        if (!double.TryParse(values[1], out amount))
                            System.Console.WriteLine("Enter valid <amount>");
                        else amountSet = true;
                    }
                    else if (values[0].Equals("interest"))
                    {
                        string val = values[1].TrimEnd('%');
                        if (!double.TryParse(val, out interest))
                            System.Console.WriteLine("Enter valid <interest>");
                        else interestSet = true;
                    }
                    else if (values[0].Equals("downpayment"))
                    {
                        if (!double.TryParse(values[1], out downpayment))
                            System.Console.WriteLine("Enter valid <downpayment>");
                        else downPaymentSet = true;
                    }
                    else if (values[0].Equals("term"))
                    {
                        if (!int.TryParse(values[1], out term))
                            System.Console.WriteLine("Enter valid <term>");
                        else termSet = true;
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid value, please enter valid loan value or blank line to calculate");
                    }
                }

                // loan calcualtion
                var log = new InMemoryLog();
                var strategy = new CompoundInterestCalculationStrategy();
                var engine = new LoanCalculationEngine(strategy, log);

                var loanInfo = engine.GetLoanInfo(amount, downpayment, interest, term);

                if(loanInfo == null)
                {
                    System.Console.WriteLine("Failed to calcualte loan info");
                    foreach(var message in log)
                        System.Console.WriteLine($"Error: {message}");
                }
                else
                {
                    var loanJson = JsonConvert.SerializeObject(loanInfo);
                    System.Console.WriteLine("Loan Info:");
                    System.Console.WriteLine(loanJson);
                }

            }
            catch(Exception ex)
            {
                System.Console.WriteLine($"Error occured: {ex}");
            }

            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadLine();
        }
    }

}
