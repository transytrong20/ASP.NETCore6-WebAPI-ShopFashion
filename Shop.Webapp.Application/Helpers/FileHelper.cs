using Microsoft.AspNetCore.Http;
using Shop.Webapp.Application.Dto;
using Shop.Webapp.Application.RequestObjects;
using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Helpers
{
    public static class FileHelper
    {
        private static readonly string rootFolder = "wwwroot";
        public static readonly string[] URI_CHARACTERS_IGNORE = { ";", "/", "?", ":", "@", "&", "=", "+", "$", ",", "#" };

        public static string UploadFile(IFormFile file, string folderName, string fileName)
        {
            if (file.OpenReadStream().Length == 0)
                throw new CustomerException(MessageError.FileEmpty);

            foreach (var item in URI_CHARACTERS_IGNORE)
                fileName = fileName.Replace(item, string.Empty);
            fileName = fileName.Replace(" ", "_");

            var path = Path.Combine(rootFolder, "files", folderName, fileName);
            var directory = Path.GetDirectoryName(path.Replace("\\", "/"));
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                file.OpenReadStream().CopyTo(stream);
            }

            return path.Replace("\\", "/").Replace(rootFolder, string.Empty);
        }

        public static void RemoveFile(string filePath)
        {
            var path = Path.Combine(rootFolder, filePath);
            if (File.Exists(path))
                File.Delete(path);
        }

        public static EJ2FileDto GetEj2File(EJ2RequestObject request)
        {
            var listItem = new List<FileItem>();
            var path = Path.Combine(rootFolder, request.Path.TrimStart('/'));
            if (!Directory.Exists(path))
                return new EJ2FileDto { Error = MessageError.DirectoryEmpty, Files = new FileItem[0] };

            foreach (var item in Directory.GetDirectories(path))
            {
                var folderItem = FileItem.CreateFolderItem(Path.GetFileName(item));

                foreach (var child in Directory.GetDirectories(item))
                    folderItem.AddItem(FileItem.CreateFolderItem(Path.GetFileName(child)));

                foreach (var child in Directory.GetFiles(item))
                    folderItem.AddItem(FileItem.CreateFileItem(Path.GetFileName(child)));

                listItem.Add(folderItem);
            }

            foreach (var item in Directory.GetFiles(path))
                listItem.Add(FileItem.CreateFileItem(Path.GetFileName(item)));
            return new EJ2FileDto { Files = listItem.ToArray() };
        }
    }
}
