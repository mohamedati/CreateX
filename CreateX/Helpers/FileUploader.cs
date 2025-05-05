namespace API.Helpers
{
    public static class FileUploader
    {

        public static async Task<string> UploadFile(IFormFile file, string folderName, string baseurl)
        {
            try
            {
                // نحدد المسار بناءً على Current Directory (مكان تشغيل التطبيق)
                // نخزن داخل مجلد "Files" مباشرة داخل المشروع
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", folderName);

                // إذا المجلد مش موجود، ننشئه
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // اسم الملف مع امتداده
                string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string finalPath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(finalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                string fileUrl = $"{baseurl}/Files/{folderName}/{fileName}";

                return fileUrl;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static void delete(string foldername, string filename)
        {
            try
            {
                var path = Directory.GetCurrentDirectory() + "/Files/" + foldername + filename;
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

            }
            catch (Exception e)
            {

            }
        }
    }
}