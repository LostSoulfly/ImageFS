using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.FileSystem
{
    public class ImageFSDirectory
    {

        public ImageFSDirectory(string dirName)
        {
            this.directoryName = dirName;
            subDirectories = new List<ImageFSDirectory>();
            directoryFiles = new List<ImageFSFile>();
        }

        public string directoryName;
        public List<ImageFSDirectory> subDirectories;
        public List<ImageFSFile> directoryFiles;

        public ImageFSDirectory CreateSubDirectory(string dirName)
        {
            ImageFSDirectory newDirectory = new ImageFSDirectory(dirName);
            subDirectories.Add(newDirectory);
            return newDirectory;
        }
    }
}
