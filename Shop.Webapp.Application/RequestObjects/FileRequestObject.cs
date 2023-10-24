using Microsoft.AspNetCore.Http;

namespace Shop.Webapp.Application.RequestObjects
{
    public class RemoveFileRequestObject
    {
        public string[] Endpoints { get; set; } = new string[0];
    }

    public class EJ2RequestObject
    {
        public string? Action { get; set; }
        public string? Path { get; set; }
        public bool ShowHiddenItems { get; set; }
        public object? Data { get; set; }

        public string? Filename { get; set; }
        public IFormFile? UploadFiles { get; set; }
    }
}
