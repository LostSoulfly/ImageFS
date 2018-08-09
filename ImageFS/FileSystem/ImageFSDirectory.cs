using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSDirectory
    {
        List<ImageFSDirectory> subDirectories;
        List<ImageFSFile> directoryFiles;
    }
}
