using System.IO;

namespace DatasetManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"F:\Downloads\Machine Learning personal dataset\Machine Learning personal dataset";
            DirectoryInfo[] directories = new DirectoryInfo(path).GetDirectories();
            foreach(DirectoryInfo dir in directories)
            {
                if(dir.GetFiles().Length >= 5)
                {
                    Images images = new Images();
                    images.path = dir.FullName + @"\";
                    images.rotate();
                    images.flip();
                    images.mirror();
                    images.adjustQualityLevel(60L);
                    images.adjustQualityLevel(20L);
                }
                else
                {
                    dir.Delete(true);
                }
            }
        }
    }
}
