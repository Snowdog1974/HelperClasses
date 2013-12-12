using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HelperClasses
{
    public class ImageUtilities
    {
        public static BitmapImage byteArrayToImage(byte[] byteArrayIn)
        {

            MemoryStream ms = new MemoryStream(byteArrayIn);

            try
            {
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();


                // Rewind the stream... 

                ms.Seek(0,

                SeekOrigin.Begin);


                // Tell the WPF image to use this stream... 

                bi3.StreamSource = ms;

                bi3.EndInit();


                // And your WPF image is ready to go! (good idea to close that stream once your image is loaded...)

                return bi3;

            }

            finally
            {
                //ms.Close();
            }



        }

        /// <summary>
        /// Function to get byte array from a object
        /// </summary>
        /// <param name="_Object">object to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] ObjectToByteArray(object _Object)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream _MemoryStream = (MemoryStream)_Object;

                // create new BinaryFormatter
                //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                //            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //// Serializes an object, or graph of connected objects, to the given stream.
                //_BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array and return
                return _MemoryStream.ToArray();
            }
            catch (Exception _Exception)
            {
                // Error
                // Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }

        /// <summary>
        /// Function to get byte array from a object
        /// </summary>
        /// <param name="_Object">object to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] MemStreamToByteArray(MemoryStream _Object)
        {
            try
            {
                //// create new memory stream
                //System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();

                //// create new BinaryFormatter
                //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                //            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //// Serializes an object, or graph of connected objects, to the given stream.
                //_BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array and return
                return _Object.ToArray();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }

        public static byte[] ObjectToByteArray2(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }


        public static BitmapImage memstreamToImage(MemoryStream ms)
        {



            try
            {
                BitmapImage bi3 = new BitmapImage();

                bi3.BeginInit();


                // Rewind the stream... 

                ms.Seek(0,

                SeekOrigin.Begin);


                // Tell the WPF image to use this stream... 

                bi3.StreamSource = ms;

                bi3.EndInit();


                // And your WPF image is ready to go! (good idea to close that stream once your image is loaded...)

                return bi3;

            }

            finally
            {
                //ms.Close();
            }



        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }




        public static byte[] ByteArrayFromImage(BitmapImage imageSource)
        {
            Stream stream = imageSource.StreamSource;
            byte[] buffer = null;

            if (stream != null && stream.Length > 0)
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    buffer = br.ReadBytes((Int32)stream.Length);
                }
            }

            return buffer;
        }

        /// <summary>

        /// This method will converts image in byte array format and returns to its caller.

        /// use System.IO namespace regarding streaming concept.

        /// </summary>

        /// <param name="imageLocation"></param>

        /// <returns></returns>

        public static byte[] ReadImageFile(string imageLocation)
        {

            byte[] imageData = null;

            FileInfo fileInfo = new FileInfo(imageLocation);

            long imageFileLength = fileInfo.Length;



            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imageData = br.ReadBytes((int)imageFileLength);

            return imageData;

        }




    }
}
