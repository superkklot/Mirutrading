using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirutrading.Application.Core.Models.Images
{
    public class HandledImageRequest
    {

        public ImageSize ImgSize { get; set; }
        public Stream ImageStream { get; set; }
        public string SrcName { get; set; }
        public int Quality { get; set; }
    }
}
