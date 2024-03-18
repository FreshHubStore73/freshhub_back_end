namespace FreshHub_BE.Helpers
{
    public static class ImagePathCreator
    {
        private const string imagesPath = "Images";
        public static string CreatePath(string image)
        {
            return string.IsNullOrEmpty(image) ? null : Path.Combine(imagesPath, image);
        }
    }
}
