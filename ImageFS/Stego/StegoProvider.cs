using PNGMask_Core.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFS.Stego
{
    public static class Providers
    {
        public static StegoProvider
        XOREOF = new StegoProvider("XOR (EOF)", typeof(XOREOF)),
        XORTXT = new StegoProvider("XOR (tEXt)", typeof(XORTEXT)),
        XORIDAT = new StegoProvider("XOR (IDAT)", typeof(XORIDAT));
    }

    public class StegoProvider
    {
        public string Name;
        public Type ProviderType;

        public StegoProvider(string Name, Type ProviderType)
        {
            this.Name = Name;
            this.ProviderType = ProviderType;
        }
    }
}
