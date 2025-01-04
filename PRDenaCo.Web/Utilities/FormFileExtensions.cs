using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PRDenaCo.Web.Utilities
{
    public static class FormFileExtensions
    {
        public static  byte[] GetBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public static IFormFile GetIFormFile(this byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var file = new FormFile(stream, 0, byteArray.Length, "", "")
                {
                    Headers = new HeaderDictionary()
                };

                System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = file.FileName
                };
                file.ContentDisposition = cd.ToString();
                return file;
            }
            
        }

    }
}
