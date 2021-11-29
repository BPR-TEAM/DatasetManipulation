using System.IO;

namespace DatasetManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"path_to_image_directory_goes_here";
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            foreach(DirectoryInfo dir in directories)
            {
                Images images = new Images();
                images.path = dir.FullName + @"\";
                images.rotate();
                images.flip();
                images.mirror();
                images.adjustQualityLevel(60L);
                images.adjustQualityLevel(20L);
            }
        }
    }
}
