using System;
using System.Collections.Generic;

namespace Game.Data
{
    public class Company
    {
        public readonly string Name;
        public readonly string Description;

        public int Money { get; private set; }
        public List<Asset> Assets { get; private set; } = new List<Asset>();
        public Personnel Personnel { get; private set; }

        public Loan Loan { get; private set; }

        public int IncomeForPart { get; private set; }
        public int ExpensesForPart { get; private set; }

        public void CalculationIncomeForPart()
        {
            var result = 0;

            var countAssets = Assets.Count;
            for (var i = 0; i < countAssets; i++)
                result += (int)Math.Round(Assets[i].IncomeBasic * Assets[i].PromotionNValue * Assets[i].SupportNValue);

            IncomeForPart = result;
        }

        public void CalculationExpensesForPart()
        {
            var result = 0;

            result += Personnel.SalaryTotalForPart;

            var countAssets = Assets.Count;
            for (var i = 0; i < countAssets; i++)
                result += Assets[i].CostSecurity;

            if (Loan != null)
                result += Loan.Payment;

            ExpensesForPart = result;
        }

        public Company(CompanyData data)
        {
            Name = data.Name;
            Description = data.Description;

            Money = data.Money;

            var countAssets = data.AssetsData.Count;
            for (var i = 0; i < countAssets; i++)
            {
                var asset = new Asset(data.AssetsData[i]);

                asset.ChangeParameters += CalculationIncomeForPart;
                asset.ChangeParameters += CalculationExpensesForPart;

                Assets.Add(asset);
            }
            Personnel = new Personnel(data.PersonalData);

            CalculationIncomeForPart();
            CalculationExpensesForPart();
        }

        public event Action MoneyChangedEvent;

        public void MoneyChanged(List<float> addingMoney)
        {
            Money += (int)Math.Round(addingMoney[0]);

            //CalculationIncomeForPart();
            //CalculationExpensesForPart();

            MoneyChangedEvent?.Invoke();
        }

        public void TakeLoan(int loanAmount, int countParts, float loanRate)
        {
            Loan = new Loan(loanAmount, countParts, loanRate);
            Loan.LoanClosed += CloseLoan;

            CalculationExpensesForPart();
        }

        public void CloseLoan()
        {
            Loan.LoanClosed -= CloseLoan;
            Loan = null;
        }

        public void ReportFinancial()
        {
            //Money += IncomeForPart;
            //Money -= ExpensesForPart;

            if (Loan != null)
                Loan.MakePayment();

            CalculationIncomeForPart();
            CalculationExpensesForPart();
        }
    }
}