using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSFile
    {
        public string imageFilePath;
        public bool fileExists;
        public string filePassword;
        public byte[] filePasswordIv;
        public List<ImageFSFileSlot> fileSlots; // two slots. Make it list for easier future expansion.
    }
}
