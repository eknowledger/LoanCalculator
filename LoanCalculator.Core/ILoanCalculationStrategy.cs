namespace LoanCalculator.Core
{
    /// <summary>
    ///  Interface to implement different loan interest calcualtion strategies
    /// </summary>
    public interface ILoanCalculationStrategy
    {
        double Calculate(double amount, double downPayment, double rate, int termsInYears);
    }
}
