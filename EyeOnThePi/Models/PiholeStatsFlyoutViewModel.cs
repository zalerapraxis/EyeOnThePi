using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace EyeOnThePi.Models
{
    public class PiholeStatsFlyoutViewModel : INotifyPropertyChanged
    {
        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int domainsInBlocklist;
        public int DomainsInBlocklist
        {
            get { return domainsInBlocklist; }
            set
            {
                if (domainsInBlocklist != value)
                {
                    domainsInBlocklist = value;
                    OnPropertyChanged("DomainsInBlocklist");
                }
            }
        }

        private int totalQueries;
        public int TotalQueries
        {
            get { return totalQueries; }
            set
            {
                if (totalQueries != value)
                {
                    totalQueries = value;
                    OnPropertyChanged("TotalQueries");
                }
            }
        }

        private int dnsQueriesToday;
        public int DnsQueriesToday
        {
            get { return dnsQueriesToday; }
            set
            {
                if (dnsQueriesToday != value)
                {
                    dnsQueriesToday = value;
                    OnPropertyChanged("DnsQueriesToday");
                }
            }
        }

        private int queriesBlockedToday;
        public int QueriesBlockedToday
        {
            get { return queriesBlockedToday; }
            set
            {
                if (queriesBlockedToday != value)
                {
                    queriesBlockedToday = value;
                    OnPropertyChanged("QueriesBlockedToday");
                }
            }
        }

        private decimal percentBlockedToday;
        public decimal PercentBlockedToday
        {
            get { return percentBlockedToday; }
            set
            {
                if (percentBlockedToday != value)
                {
                    percentBlockedToday = value;
                    OnPropertyChanged("PercentBlockedToday");
                }
            }
        }

        private int queriesForwarded;
        public int QueriesForwarded
        {
            get { return queriesForwarded; }
            set
            {
                if (queriesForwarded != value)
                {
                    queriesForwarded = value;
                    OnPropertyChanged("QueriesForwarded");
                }
            }
        }

        private int queriesCached;
        public int QueriesCached
        {
            get { return queriesCached; }
            set
            {
                if (queriesCached != value)
                {
                    queriesCached = value;
                    OnPropertyChanged("QueriesCached");
                }
            }
        }

        private int uniqueClients;
        public int UniqueClients
        {
            get { return uniqueClients; }
            set
            {
                if (uniqueClients != value)
                {
                    uniqueClients = value;
                    OnPropertyChanged("UniqueClients");
                }
            }
        }

        public void StartTimer()
        {
            DispatcherTimer piholeUpdateTimer = new DispatcherTimer();
            piholeUpdateTimer.Interval = TimeSpan.FromSeconds(0);
            piholeUpdateTimer.Tick += piholeUpdateTimer_Tick;
            piholeUpdateTimer.Start();
        }

        private async void piholeUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (((DispatcherTimer)sender).Interval == TimeSpan.FromSeconds(0))
                ((DispatcherTimer)sender).Interval = TimeSpan.FromSeconds(3);

            var stats = await PiholeApiHelper.GetStatsAsync();

            Status = stats.Status;
            DomainsInBlocklist = stats.DomainsInBlocklist;
            TotalQueries = stats.TotalQueries;
            DnsQueriesToday = stats.DnsQueriesToday;
            QueriesBlockedToday = stats.QueriesBlockedToday;
            PercentBlockedToday = stats.PercentBlockedToday;
            QueriesForwarded = stats.QueriesForwarded;
            QueriesCached = stats.QueriesCached;
            UniqueClients = stats.UniqueClients;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
