namespace NatroDomainManager.MVC.Models
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }  // Data alanı
        public int ResultStatus { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
