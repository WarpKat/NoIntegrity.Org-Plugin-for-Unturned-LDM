using System;

namespace NoIntegrity.Models
{
    [Serializable]

    public class SLOTS_Items
    {
        public Byte[] Meta;

        public ushort ID;

        private SLOTS_Items()
        {
        }

        public SLOTS_Items(ushort id, Byte[] meta)
        {
            Meta = meta;
            ID = id;
        }
    }
}
