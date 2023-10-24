using Shop.Webapp.Shared.ConstsDatas;
using Shop.Webapp.Shared.Exceptions;

namespace Shop.Webapp.Application.Dto
{
    public class EJ2FileDto
    {
        public FileItem[] Files { get; set; } = new FileItem[0];
        public string? Error { get; set; }
    }

    public class FileItem
    {
        public static FileItem CreateFileItem(string fileName)
        {
            return new FileItem { Name = fileName, Type = "file" };
        }

        public static FileItem CreateFolderItem(string directoryName)
        {
            return new FileItem { Name = directoryName, Type = "folder", Files = new FileItem[0] };
        }

        private FileItem() { }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public FileItem[] Files { get; private set; }

        public void AddItem(FileItem item)
        {
            if (this.Type == "file" || this.Files == null)
                throw new CustomerException(MessageError.FileCannotAddItem);
            if (this.Files.Any(x => x.Name == item.Name))
                throw new CustomerException(MessageError.FileAvailabel);
            var newFiles = this.Files.ToList();
            newFiles.Add(item);
            this.Files = newFiles.ToArray();
        }
    }
}
