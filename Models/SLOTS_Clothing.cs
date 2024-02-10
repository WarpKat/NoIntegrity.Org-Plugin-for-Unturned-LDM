using System;

namespace NoIntegrity.Models
{
    [Serializable]

    public class SLOTS_Clothing
    {
        public ushort id;
        public byte quality;
        public byte[] state;

        public SLOTS_Clothing(ushort id, byte quality, byte[] state)
        {
            this.id = id;
            this.quality = quality;
            this.state = state;
        }
    }
}
