namespace Shop.WebApp.Web.Error.ErrorModel
{
    public class ErrorModel
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public List<string> Error { get; set; }
    }
}
