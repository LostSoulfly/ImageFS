using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSFile
    {
        string imageFilePath;
        bool fileExists;
        string filePassword;
        byte[] filePasswordIv;
        List<ImageFSFileSlot> fileSlots; // two slots. Make it list for easier future expansion.
    }
}
