using System;

namespace PrototypeGame_1
{
    class Pollution
    {
        public int playerAmount;
        private int defaultPlayerAmount;

        public Pollution(int pollutionPlayerAmount)
        {
            playerAmount = defaultPlayerAmount = pollutionPlayerAmount;
        }

        static public void resetReputation(Pollution pollution)
        {
            pollution.playerAmount = pollution.defaultPlayerAmount;
        }
    }
}
