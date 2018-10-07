using System;
using System.Collections.Generic;
using System.Text;
using ImageFS.FileSystem;
using static ImageFS.FileSystem.ImageFSHelpers;

namespace ImageFS.FileSystem
{
    public class ImageFSFileSlot
    {
        public StorageMethod storageMethod;
        public ulong storedFileId;
        public ulong storedFileDirectoryId;
        public long storedFileSize;
        public bool fileSlotInUse;

    }
}
