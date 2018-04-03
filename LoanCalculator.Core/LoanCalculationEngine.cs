using System;

namespace LoanCalculator.Core
{

    public class LoanCalculationEngine
    {
        private ILoanCalculationStrategy _strategy;
        private ILog _log;

        // Inject depndencies
        // DI framework can be used to achieve this task for sake of simplicity we will just inject interfaces in ctor
        public LoanCalculationEngine(ILoanCalculationStrategy loanCalculationStrategy, ILog log)
        {
            _strategy = loanCalculationStrategy;
            _log = log;
        }

        public LoanInfo GetLoanInfo(double amount, double downPayment, double rate, int termsInYears)
        {
            try
            {
                var monthlyPayment = _strategy.Calculate(amount, downPayment, rate, termsInYears);
                LoanInfo info = new LoanInfo(amount, downPayment, rate, termsInYears, monthlyPayment);
                return info;
            }
            catch (Exception ex)
            {
                _log.Error($"Error occured:{ex.Message}");
            }

            return null;
        }
    }

}
