using Newtonsoft.Json;
using System;

namespace LoanCalculator.Core
{
    public sealed class LoanInfo
    {
        private double _payment;
        public LoanInfo(double amount, double downPayment, double rate, int termsInYears, double payment)
        {
            Amount = amount;
            DownPayment = downPayment;
            AnnualInterestRate = rate;
            Terms = termsInYears;
            _payment = payment;
        }

        [JsonIgnore]
        public double Amount { get; private set; }

        [JsonIgnore]
        public double DownPayment { get; private set; }

        [JsonIgnore]
        public double Principle { get { return Amount - DownPayment; } }

        [JsonIgnore]
        public double AnnualInterestRate { get; private set; }

        [JsonIgnore]
        public int Terms { get; private set; }

        [JsonProperty(propertyName: "monthly payment")]
        public double MonthlyPayment { get { return Math.Round(_payment, 2); } }

        [JsonProperty(propertyName: "total payment")]
        public double TotalPayment { get { return Math.Round(MonthlyPayment * Terms * 12, 2); } }

        [JsonProperty(propertyName: "total interest")]
        public double TotalInterest { get { return Math.Round(TotalPayment - Principle, 2); } }
    }

}
