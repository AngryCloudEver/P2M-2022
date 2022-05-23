using System;

namespace PrototypeGame_1
{
    class Reputation
    {
        public int playerAmount;
        private int defaultPlayerAmount;

        public Reputation(int reputationPlayerAmount)
        {
            playerAmount = defaultPlayerAmount = reputationPlayerAmount;
        }

        static public void resetReputation(Reputation reputation)
        {
            reputation.playerAmount = reputation.defaultPlayerAmount;
        }
    }
}
