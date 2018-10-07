using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSDirectory
    {

        public ImageFSDirectory(string direcotryName)
        {
            this.directoryName = directoryName;
            subDirectories = new List<ImageFSDirectory>();
            directoryFiles = new List<ImageFSFile>();
        }

        string directoryName;
        List<ImageFSDirectory> subDirectories;
        List<ImageFSFile> directoryFiles;
    }
}
