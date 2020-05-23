using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EyeOnThePi.Models
{
    class PiholeStatsJsonModel
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("domains_being_blocked")]
        [Display(Name = "Domains in blocklist")]
        public int DomainsInBlocklist { get; set; }

        [JsonPropertyName("dns_queries_today")]
        [Display(Name = "DNS Queries today")]
        public int DnsQueriesToday { get; set; }

        [JsonPropertyName("ads_blocked_today")]
        [Display(Name = "Queries Blocked today")]
        public int QueriesBlockedToday { get; set; }

        [JsonPropertyName("ads_percentage_today")]
        [Display(Name = "% Blocked today")]
        public decimal PercentBlockedToday { get; set; }

        [JsonPropertyName("queries_forwarded")]
        [Display(Name = "Queries Forwarded")]
        public int QueriesForwarded { get; set; }

        [JsonPropertyName("queries_cached")]
        [Display(Name = "Queries Cached")]
        public int QueriesCached { get; set; }

        [JsonPropertyName("unique_clients")]
        [Display(Name = "Unique Clients")]
        public int UniqueClients { get; set; }

        [JsonPropertyName("dns_queries_all_types")]
        [Display(Name = "Total queries")]
        public int TotalQueries { get; set; }

    }
}
