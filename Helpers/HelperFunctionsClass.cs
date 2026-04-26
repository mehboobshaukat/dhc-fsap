namespace DHC_FSAP.Helpers
{
    public static class HelperFunctionsClass
    {
        public static async Task<string> SaveImageAsync(IFormFile file)
        {
            long MaxLength = 1024 * 1024;

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/profile/images");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folder, fileName);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extensions = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extensions))
                throw new Exception("Invalid file type");

            if (file.Length > MaxLength)
                throw new Exception("Image size must not exceed than 1 MB");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/profile/images/" + fileName;
        }

        public static string GenerateSlug(string title)
        {
            return title.ToLower()
                .Replace(" ", "-")
                .Replace("_", "-")
                .Replace("-", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace("?", "")
                .Replace("!", "")
                .Replace("/", "-")
                .Replace("\\", "-");
        }

        public static async Task<string> GenerateUniqueSlugAsync(
            string title,
            Func<string, Task<bool>> slugExists)
        {
            string baseSlug = GenerateSlug(title);
            string slug = baseSlug;
            int counter = 1;

            while (await slugExists(slug))
            {
                slug = $"{baseSlug}-{counter}";
                counter++;
            }
            return slug;
        }
    }
}
