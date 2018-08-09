using System;
using System.Collections.Generic;
using System.Text;
using ImageFS.FileSystem;
using static ImageFS.FileSystem.ImageFSHelpers;

namespace ImageFS.FileSystem
{
    public class ImageFSFileSlot
    {
        StorageMethod storageMethod;
        ulong storedFileId;
        ulong storedFileDirectoryId;
        long storedFileSize;
        bool fileSlotInUse;

    }
}
