using System;

namespace PrototypeGame_1
{
    class Power
    {
        public string name;
        public int cost;
        public int pollution;
        public int playerAmount;

        private int defaultPlayerAmount;

        public Power(string powerName, int powerCost, int powerPollution, int powerPlayerAmount)
        {
            name = powerName;
            cost = powerCost;
            pollution = powerPollution;
            playerAmount = defaultPlayerAmount = powerPlayerAmount;
        }

        static public Power selectPowerToUse(Power[] powers, int money)
        {
            Power chosenPower = null;
            int minPollution = -1;
            Random rd = new Random();
            int RNG = 0;

            foreach (var power in powers)
            {
                if(minPollution == -1)
                {
                    if(money >= power.cost)
                    {
                        minPollution = power.pollution;
                        chosenPower = power;
                    }
                }
                else
                {
                    if(minPollution > power.pollution && money >= power.cost)
                    {
                        minPollution = power.pollution;
                        chosenPower = power;
                    }
                }
            }

            RNG = rd.Next(0, powers.Length + 2);

            if(RNG < powers.Length && powers[RNG] != chosenPower)
            {
                return powers[RNG];
            }

            return chosenPower;
        }

        static public void AddPower(Power[] powers, int powerAmount)
        {
            Random random = new Random();

            if(powerAmount > 0)
            {
                for (int i = 0; i < powerAmount; i++)
                {
                    bool powerReduced = false;

                    while (powerReduced == false)
                    {
                        var powerToUse = random.Next(0, powers.Length);

                        if (powers[powerToUse].playerAmount > 0)
                        {
                            powers[powerToUse].playerAmount += 1;
                            powerReduced = true;
                        }
                    }
                }
            }
            else if(powerAmount < 0)
            {
                for (int i = 0; i > powerAmount; i--)
                {
                    bool powerReduced = false;

                    while (powerReduced == false)
                    {
                        var powerToUse = random.Next(0, powers.Length);

                        if (powers[powerToUse].playerAmount > 0)
                        {
                            powers[powerToUse].playerAmount -= 1;
                            powerReduced = true;
                        }
                    }
                }
            }
        }

        static public void resetPower(Power[] powers)
        {
            foreach(var power in powers)
            {
                power.playerAmount = power.defaultPlayerAmount;
            }
        }

        static public int getCostMin(Power[] powers)
        {
            int min = -1;

            foreach(var power in powers)
            {
                if(min == -1)
                {
                    min = power.cost;
                }
                else
                {
                    if(power.cost < min)
                    {
                        min = power.cost;
                    }
                }
            }

            return min;
        }
    }
}
