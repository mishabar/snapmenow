using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN.Data.Repositories
{
    public interface IImageRepository
    {
        void UploadImage(System.Web.HttpPostedFileBase image, string id);

        void DeleteImage(string original);
    }
}
