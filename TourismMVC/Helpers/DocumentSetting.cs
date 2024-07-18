namespace TourismMVC.Helpers
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {

            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName);

            string filename = file.FileName;


            string filePath = Path.Combine(folderpath, filename);


            using var filestream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(filestream);

            return filename;

        }

        public static void DeleteFile(string foldername, string filename)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", foldername, filename);
            if (File.Exists(filepath))
                File.Delete(filepath);
        }
    }
}
