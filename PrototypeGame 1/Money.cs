using System;

namespace PrototypeGame_1
{
    class Money
    {
        public int playerAmount;
        private int defaultPlayerAmount;

        public Money(int moneyPlayerAmount)
        {
            playerAmount = defaultPlayerAmount = moneyPlayerAmount;
        }

        static public void resetMoney(Money money)
        {
            money.playerAmount = money.defaultPlayerAmount;
        }
    }
}
