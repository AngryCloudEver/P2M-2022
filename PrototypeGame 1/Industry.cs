using System;

namespace PrototypeGame_1
{
    class Industry
    {
        public int playerAmount;
        private int defaultPlayerAmount;

        public Industry(int industryPlayerAmount)
        {
            playerAmount = defaultPlayerAmount = industryPlayerAmount;
        }

        static public void resetIndustry(Industry industry)
        {
            industry.playerAmount = industry.defaultPlayerAmount;
        }
    }
}
