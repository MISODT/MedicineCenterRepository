using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MedicineCenterAutomatedProgram.Models.Management
{
    public class ByteImageValuesManager
    {
        public static ImageSource GetImageFromBytes(string bytesValue )
        {
            if(bytesValue != null)
            {
                byte[] splitedBytes = bytesValue.Split(';').Select(x => byte.Parse(x)).ToArray();

                MemoryStream memoryStream = new MemoryStream(splitedBytes);

                Image image = new Image();

                image.Source = BitmapFrame.Create(memoryStream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                return image.Source;
            }

            else
            {
                return null;
            }
        }

        public static string GetImageByteStringBuilder(BitmapImage bitmapImage)
        {
            if (bitmapImage != null)
            {
                PngBitmapEncoder pngEncoder = new PngBitmapEncoder();

                MemoryStream memoryStream = new MemoryStream();

                StringBuilder stringBuilder = new StringBuilder();

                pngEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));

                pngEncoder.Save(memoryStream);

                byte[] byteImage = memoryStream.ToArray();

                foreach (var b in byteImage)
                {
                    stringBuilder.Append(b).Append(';');
                }

                stringBuilder.Remove(stringBuilder.Length - 1, 1);

                //GetIMGDB(stringBuilder.ToString());

                return stringBuilder.ToString();
            }

            else
            {
                return null;
            }
        }
    }
}