namespace Shop.WebApp.Web.Error.Models
{
    public class ErrorModel
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public List<string> Error { get; set; }
    }
}
