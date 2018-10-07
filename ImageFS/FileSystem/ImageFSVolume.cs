using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSVolume
    {

        public ImageFSVolume()
        {
            volumeData = new List<ImageFSFile>();
            volumeDirectories = new List<ImageFSDirectory>();
            maxFileCount = 100;
            maxFileSize = 1024 * 10;
            currentFileCount = 0;
        }

        public List<ImageFSFile> volumeData;   // complete list of all images and their data
        public List<ImageFSDirectory> volumeDirectories;
        public long maxFileSize;
        public long maxFileCount;
        public long currentFileCount;

    }
}
