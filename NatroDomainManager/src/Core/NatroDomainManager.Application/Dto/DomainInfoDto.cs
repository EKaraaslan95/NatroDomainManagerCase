using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NatroDomainManager.Application.Dto
{
   

    public class DomainInfoDto
    {
        public int? DomainId { get; set; }
        public string DomainName { get; set; }
        public string QueryTime { get; set; }
        public string DomainRegistered { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string ExpiryDate { get; set; }
        public DateTime? LastCheckedDate { get; set; }
        public string? LastCheckedDateStr { get; set; }
        public int? FavoriteId { get; internal set; }
    }

    public class Root
    {
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        [JsonPropertyName("domain_name")]
        public string DomainName { get; set; }

        [JsonPropertyName("query_time")]
        public string QueryTime { get; set; }



        [JsonPropertyName("domain_registered")]
        public string DomainRegistered { get; set; }

        [JsonPropertyName("registry_data")]
        public RegistryData RegistryData { get; set; }
    }


    public class RegistryData
    {
        [JsonPropertyName("domain_name")]
        public string DomainName { get; set; }

        [JsonPropertyName("query_time")]
        public string QueryTime { get; set; }


        [JsonPropertyName("create_date")]
        public string CreateDate { get; set; }

        [JsonPropertyName("update_date")]
        public string UpdateDate { get; set; }

        [JsonPropertyName("expiry_date")]
        public string ExpiryDate { get; set; }
    }


}
