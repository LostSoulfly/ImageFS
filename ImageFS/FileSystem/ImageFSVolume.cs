using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSVolume
    {
        List<ImageFSFile> volumeData;   // complete list of all images and their data
        List<ImageFSDirectory> volumeDirectories;
        long maxFileSize;
        long maxFileCount;
        long currentFileCount;
    }
}
