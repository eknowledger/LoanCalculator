using System;

namespace LoanCalculator.Core
{
    public class CompoundInterestCalculationStrategy : ILoanCalculationStrategy
    {
        public double Calculate(double amount, double downPayment, double rate, int termsInYears)
        {
            int months = termsInYears * 12;
            double monthlyRate = rate / (12 * 100);
            double monthlyPayment = (monthlyRate + (monthlyRate / ((Math.Pow(1 + monthlyRate, months) - 1)))) * (amount - (downPayment));

            return monthlyPayment;
        }
    }
}
