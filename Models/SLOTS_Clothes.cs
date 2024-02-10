using System;

namespace NoIntegrity.Models
{
    [Serializable]

    public class SLOTS_Clothes
    {
        public SLOTS_Clothing hat;
        public SLOTS_Clothing glasses;
        public SLOTS_Clothing mask;
        public SLOTS_Clothing shirt;
        public SLOTS_Clothing vest;
        public SLOTS_Clothing backpack;
        public SLOTS_Clothing pants;

        public SLOTS_Clothes(SLOTS_Clothing hat, SLOTS_Clothing glasses, SLOTS_Clothing mask, SLOTS_Clothing shirt, SLOTS_Clothing vest, SLOTS_Clothing backpack, SLOTS_Clothing pants)
        {
            this.hat = hat;
            this.glasses = glasses;
            this.mask = mask;
            this.shirt = shirt;
            this.vest = vest;
            this.backpack = backpack;
            this.pants = pants;
        }
    }
}
