using System;

namespace PrototypeGame_1
{
    class Food
    {
        public int powerCost;
        public int moneyCost;
        public int playerAmount;
        public int foodProduced;

        public Food(int foodPowerCost, int foodMoneyCost, int foodPlayerAmount, int foodProduce)
        {
            powerCost = foodPowerCost;
            moneyCost = foodMoneyCost;
            playerAmount = foodPlayerAmount;
            foodProduced = foodProduce;
        }

        
    }
}
