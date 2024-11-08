namespace NatroDomainManager.MVC.Models
{
    public class DomainInfoMVC
    {
        public int? DomainId { get; set; }
        public string DomainName { get; set; }
        public string QueryTime { get; set; }
        public string DomainRegistered { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string ExpiryDate { get; set; }
        public DateTime? LastCheckedDate { get; set; }
        public int? FavoriteId { get; set; } 
    }
}
