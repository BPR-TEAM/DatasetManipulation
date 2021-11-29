using System;
using System.IO;
using System.Linq;
using System.Drawing.Imaging;
using System.Drawing;

namespace DatasetManipulation
{
    /// <summary>
    /// Class for representing image collections
    /// </summary>
    class Images
    {
        /// <summary>
        /// The path to the collection of images
        /// </summary>
        /// <value></value>
        public string path{get;set;}

        /// <summary>
        /// Rotates the image by 90 degrees
        /// </summary>
        public void rotate()
        {
            var files = new DirectoryInfo(path).GetFiles().Where(f => f.IsImage());

            foreach (var file in files)
            {
                using (var image = Image.FromFile(file.FullName))
                {
                    try
                    {
                        var newImageName = String.Format("{0}{1}{2}{3}",path,file.Name,"_rotated_by_90",file.Extension);
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        image.Save(newImageName, ImageFormat.Jpeg);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// Flips the image by the X axiss
        /// </summary>
        public void flip()
        {
            var files = new DirectoryInfo(path).GetFiles().Where(f => f.IsImage());

            foreach (var file in files)
            {
                using (var image = Image.FromFile(file.FullName))
                {
                    try
                    {
                        var newImageName = String.Format("{0}{1}{2}{3}",path,file.Name,"_flipped",file.Extension);
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        image.Save(newImageName, ImageFormat.Jpeg);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// Mirrors the image by flipping on the Y axis
        /// </summary>
        public void mirror()
        {
            var files = new DirectoryInfo(path).GetFiles().Where(f => f.IsImage());

            foreach (var file in files)
            {
                using (var image = Image.FromFile(file.FullName))
                {
                    try
                    {
                        var newImageName = String.Format("{0}{1}{2}{3}",path,file.Name,"_mirrored",file.Extension);
                        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        image.Save(newImageName, ImageFormat.Jpeg);
                    }
                    catch
                    {
                        continue;
                    } 
                }
            }
        }
        /// <summary>
        /// Adjusts the quality level of the image
        /// </summary>
        /// <param name="QL">The quality level to make the image - "90L" is default but original is "100L"</param>
        public void adjustQualityLevel(long QL)
        {
            var files = new DirectoryInfo(path).GetFiles().Where(f => f.IsImage());

            foreach (var file in files)
            {
                using (var bitmap = new Bitmap(file.FullName))
                {
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                    System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, QL);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    try
                    {
                        var newImageName = String.Format("{0}{1}{2}{3}{4}",path,file.Name,"_QL_", QL,".jpg");
                        bitmap.Save(newImageName, jpgEncoder, myEncoderParameters);
                    }
                    catch
                    {
                        continue;
                    } 
                }
            }
        }
        /// <summary>
        /// Gets the encoder for the specified imageformat
        /// </summary>
        /// <param name="format">The imge format to fetch the encoder for</param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

    }

    
    public static class Extensions
    {
        /// <summary>
        /// Extension method for checking if the given file is an image by
        /// the file extension - .jpg/png/gif/jpeg
        /// </summary>
        /// <param name="file">The FileInfo of the file to be checked</param>
        /// <returns>Whether or not the file is an image</returns>
        public static bool IsImage(this FileInfo file)
        {
            var allowedExtensions = new[] {".jpg", ".png", ".gif", ".jpeg"};
            return allowedExtensions.Contains(file.Extension.ToLower());
        }
    }
}
