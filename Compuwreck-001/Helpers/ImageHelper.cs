using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Compuwreck_001.Models;
using System.Drawing;
using System.Drawing.Imaging;

namespace Compuwreck_001.Helpers {
    public class ImageHelper : Controller {

        public static int UploadImage(HttpPostedFileBase file, Photo photo) {
            int uploadStatus = 0;
            try
            {
                if (file != null)
                {
                    //set directory path
                    string dirPath = Path.Combine(HostingEnvironment.MapPath("~/shipwreckImages/" + photo.Shipwreck_id));

                    //create folders if they dont exist
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                        Directory.CreateDirectory(dirPath + "/" + "thumbs");
                    }

                    //Save the image
                    using (var image = Image.FromStream(file.InputStream, true, true)) {
                        string URL = "";
                        using (var encParams = new EncoderParameters(1)) {
                            var changedFile = Path.ChangeExtension(photo.FileName, ".jpg");
                            URL = Path.Combine(HostingEnvironment.MapPath("~/shipwreckImages/" + photo.Shipwreck_id + "/"), changedFile);
                            const long quality = 100;
                            encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                            image.Save(URL, ImageFormat.Jpeg); //, jpgInfo, encParams
                        }
                    }

                    //Call create thumbnail method
                    CreateThumbNail(file, photo);

                    //Create stream for database.
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        uploadStatus = 1;
                    }
                    return uploadStatus = 1;
                }
            }
            catch (System.IO.IOException e)
            {
                return uploadStatus;
            }
            
            return uploadStatus;
        }

        public static void CreateThumbNail(HttpPostedFileBase file, Photo photo) {

            using (var image = Image.FromStream(file.InputStream, true, true))
            {
                using (var thumb = image.GetThumbnailImage(
                        100, /* width*/
                        70, /* height*/
                        () => false,
                        IntPtr.Zero)) {
                            //var jpgInfo = ImageCodecInfo.GetImageEncoders().Where(codecInfo => codecInfo.MimeType == "image/png").First();
                            using (var encParams = new EncoderParameters(1))
                            {
                                var changedFile = Path.ChangeExtension(photo.FileName, ".jpg");
                                string outputPath = Path.Combine(HostingEnvironment.MapPath("~/shipwreckImages/" + photo.Shipwreck_id + "/thumbs"), changedFile);
                                const long quality = 100;
                                encParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                                thumb.Save(outputPath, ImageFormat.Jpeg); //, jpgInfo, encParams
                            }
                        }
            }
        }
    }
}