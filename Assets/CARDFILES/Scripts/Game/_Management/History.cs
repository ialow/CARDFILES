using Game.Data;

namespace Game.Management
{
    public class History
    {
        private Company company;

        private readonly HistorySO historyDataSO;

        public History(HistorySO historyDataSO)
        {
            this.historyDataSO = historyDataSO;
        }

        public void StartHistory()
        {
            var company = new Company(historyDataSO.Company);
        }

        public void LaunchingEra()
        {

        }

        public void EndEra()
        {

        }

        private void EndHistory()
        {

        }
    }
}
