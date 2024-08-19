using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Data;

namespace Game.View
{
    public class PartView : MonoBehaviour
    {
        private Company company;

        [SerializeField] private TextMeshProUGUI numberPart;

        [Space, SerializeField] private TextMeshProUGUI countMoney;
        [SerializeField] private List<TextMeshProUGUI> assetsName;
        //

        [Space, SerializeField] private Slider timer;
        [SerializeField] private TextMeshProUGUI personnelInfo;
        //

        [Space, SerializeField] private TextMeshProUGUI expensesMoney;
        [SerializeField] private TextMeshProUGUI incomeMoney;

        public void Init(Company company)
        {
            this.company = company;

            company.MoneyChangedEvent += CountMoneyView;
            company.Personnel.DistributionEmployeesEvent += PersonnelView;
            // ивенты о даннх компании и инветы хода таймера
        }

        public void StartInfoView()
        {
            CountMoneyView();
            AssetsView(company.Assets[0].Name, company.Assets[1].Name);
            PersonnelView();

            ExpensesMoneyView();
            IncomeMoneyView();
        }

        private void CountMoneyView()
        {
            countMoney.text = $"{company.Money}";

            //ExpensesMoneyView();
            //IncomeMoneyView();
        }

        private void PersonnelView()
        {
            personnelInfo.text = $"{company.Personnel.NumberEmployedEmployees}" +
                $"/{company.Personnel.NumberAvailableEmployees + company.Personnel.NumberEmployedEmployees}";
        }

        private void ExpensesMoneyView()
        {
            expensesMoney.text = $"{company.ExpensesForPart}";
        }

        private void IncomeMoneyView()
        {
            incomeMoney.text = $"{company.IncomeForPart}";
        }



        private void AssetsView(params string[] assetsName)
        {
            // 

            this.assetsName[0].text = assetsName[0];
            this.assetsName[1].text = assetsName[1];
        }



        public void NumberPartView(int numberPart)
        {
            this.numberPart.text = $"0{numberPart}";
        }

        public void TimerView(float timerNormolized)
        {
            timer.value = timerNormolized;
        }
    }
}
