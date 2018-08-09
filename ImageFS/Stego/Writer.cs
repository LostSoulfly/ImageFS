using PNGMask_Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageFS.Stego
{
    class Writer
    {
        private SteganographyProvider provider;

        public bool HideData(string imagePath, string hideFilePath, string password = "", int fileSlot = 0)
        {

            StegoProvider prov;

            if (fileSlot == 0)
                prov = Providers.XOREOF;
            else
                prov = Providers.XORIDAT;

            provider = (SteganographyProvider)Activator.CreateInstance(prov.ProviderType, imagePath, false);
            provider.SetPassword(password, false);

            byte[] data = File.ReadAllBytes(hideFilePath);
            if (Imprint(DataType.Binary, data))
            {
                using (FileStream fs = File.Open(imagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    provider.WriteToStream(fs);

                return true;
            }

            return false;

        }

        public bool HideFSS(string imagePath, string fileSystemData, string password = "", int fileSlot = 0)
        {

            StegoProvider prov;

            if (fileSlot == 0)
                prov = Providers.XOREOF;
            else
                prov = Providers.XORIDAT;

            provider = (SteganographyProvider)Activator.CreateInstance(prov.ProviderType, imagePath, false);
            provider.SetPassword(password, false);

            if (Imprint(DataType.FileSystem, fileSystemData))
            {
                using (FileStream fs = File.Open(imagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    provider.WriteToStream(fs);

                return true;
            }

            return false;

        }

        public bool HideImage(string imagePath, string hideFilePath, string password = "", int fileSlot = 0)
        {

            StegoProvider prov;

            if (fileSlot == 0)
                prov = Providers.XOREOF;
            else
                prov = Providers.XORIDAT;

            provider = (SteganographyProvider)Activator.CreateInstance(prov.ProviderType, imagePath, false);
            provider.SetPassword(password, false);

            byte[] data = File.ReadAllBytes(hideFilePath);
            if (Imprint(DataType.ImageBytes, data))
            {
                using (FileStream fs = File.Open(imagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    provider.WriteToStream(fs);

                return true;
            }

            return false;

        }

        public bool HideText(string imagePath, string hideText, string password = "", int fileSlot = 0)
        {

            StegoProvider prov;

            if (fileSlot == 0)
                prov = Providers.XOREOF;
            else
                prov = Providers.XORIDAT;

            provider = (SteganographyProvider)Activator.CreateInstance(prov.ProviderType, imagePath, false);
            provider.SetPassword(password, false);

            if (Imprint(DataType.Text, hideText))
            {
                using (FileStream fs = File.Open(imagePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                    provider.WriteToStream(fs);

                return true;
            }

            return false;
        }

        bool Imprint(DataType type, object data)
        {
            try
            {
                provider.Imprint(type, data);
                return true;
            }
            catch (NotEnoughSpaceException ex) { Console.WriteLine("Out of Space: " + ex.Message); }

            return false;
        }
    }
}
