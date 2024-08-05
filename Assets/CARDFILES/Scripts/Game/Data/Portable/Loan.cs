using System;

namespace Game.Data
{
    public class Loan
    {
        public int CountRemainingPayments { get; private set; }
        public int Payment { get; private set; }

        public Loan(int loanAmount, int countParts, float loanRate) 
        {
            CountRemainingPayments = countParts;
            Payment = (int)Math.Round((loanAmount * Math.Pow(1 + (loanRate / 100), countParts / 4)) / countParts);
        }

        public event Action LoanClosed;

        public void MakePayment()
        {
            CountRemainingPayments -= 1;

            if (CountRemainingPayments == 0)
                LoanClosed?.Invoke();
        }
    }
}
