using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LoanCalculator.Core.Tests
{
    [TestClass]
    public class LoanCalculationEngineTests
    {

        // Simple Unit test to establish the testing framework
        // more test cases can be added,  but eliminated due to time limits
        [TestMethod]
        public void GetLoanInfo_ValidInput_ShouldReturnCorrectInfo()
        {
            var log = new InMemoryLog();
            var strategy = new CompoundInterestCalculationStrategy();
            var engine = new LoanCalculationEngine(strategy, log);

            double amount = 100000;
            double interest = 5.5;
            double downpayment = 20000;
            int term = 30;


            var loanInfo = engine.GetLoanInfo(amount, downpayment, interest, term);

            Assert.IsNotNull(loanInfo);
            Assert.AreEqual(loanInfo.Amount, amount);
            Assert.AreEqual(loanInfo.AnnualInterestRate, interest);
            Assert.AreEqual(loanInfo.DownPayment, downpayment);
            Assert.AreEqual(loanInfo.Terms, term);

            Assert.AreEqual(loanInfo.MonthlyPayment, 454.23);
            Assert.AreEqual(loanInfo.Principle, 80000);
            Assert.AreEqual(loanInfo.TotalInterest, 83522.8);
            Assert.AreEqual(loanInfo.TotalPayment, 163522.8);
        }
    }
}
