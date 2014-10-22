using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Amazon.S3;
using Amazon.S3.Model;

namespace SMN.Data.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private IAmazonS3 _amazonS3Client;

        public ImageRepository(IAmazonS3 amazonS3Client)
        {
            _amazonS3Client = amazonS3Client;
        }

        public void UploadImage(HttpPostedFileBase image, string id)
        {
            PutObjectRequest request = new PutObjectRequest();
            request.BucketName = ConfigurationManager.AppSettings["AWS:ImagesBucketName"];
            request.ContentType = image.ContentType;
            request.InputStream = image.InputStream;
            request.Key = "images/" + id;
            request.CannedACL = S3CannedACL.PublicRead;
            var response = _amazonS3Client.PutObject(request);
        }

        public void DeleteImage(string original)
        {
            _amazonS3Client.DeleteObjectAsync(
                new Amazon.S3.Model.DeleteObjectRequest 
                {
                    BucketName = ConfigurationManager.AppSettings["AWS:ImagesBucketName"],
                    Key = original
                }
            );
        }
    }
}
