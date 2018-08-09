using PNGMask_Core;
using PNGMask_Core.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static ImageFS.FileSystem.ImageFSHelpers;

namespace ImageFS.Stego
{
    class Reader
    {
        PNG pngOriginal = null;
        SteganographyProvider provider = null;
        private string password = null;

        public Reader(string path, string password = "")
        {
            pngOriginal = new PNG(path);
            this.password = password;
        }

        public (DataType t, object data) ReadData(StorageMethod storageMethod)
        {

            DataType t = DataType.None;
            object data = null;

            using (MemoryStream stream = new MemoryStream())
            {
                pngOriginal.WriteToStream(stream, true, true);
                stream.Seek(0, SeekOrigin.Begin);
            }

            bool hasEOF = false;
            int IDATs = 0;
            foreach (PNGChunk chunk in pngOriginal.Chunks)
            {
                if (chunk.Name == "_EOF") hasEOF = true;
                if (chunk.Name == "IDAT") IDATs++;
            }
            
            StegoProvider pr = Providers.XOREOF;
            
            if (storageMethod == StorageMethod.IDAT)
                pr = Providers.XORIDAT;

            if (!hasEOF && IDATs == 1) { provider = null; Console.WriteLine($"There is no data in {storageMethod.ToString()}"); }
            else
            {
                try
                {
                    provider = (SteganographyProvider)Activator.CreateInstance(pr.ProviderType, pngOriginal, true);
                    provider.SetPassword(password);
                    t = provider.Extract(out data);
                    return (t, data);

                }
                catch (InvalidPasswordException)
                {
                    Console.WriteLine("The password was incorrect.");
                }
            }

            return (DataType.None, null);
        }
    }
}
