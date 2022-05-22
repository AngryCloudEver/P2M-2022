using System;

namespace PrototypeGame_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int moneyAmount = 3000;
            int industryAmount = 10;
            int reputationAmount = 100;
            int pollutionAmount = 0;
            int powerAmount = 0;
            int tempPowerAmount = 0;
            int policyRng = 0;
            int turn = 1;

            int maxPowerAmount = 15;

            bool gameOver = false;
            bool powerProduced = false;
            bool foodProduced = false;

            // Policy Lists
            Policy[] policies = new Policy[]
            {
                new Policy(
                    "RobbinFood",
                    "A series of thefts have been reported recently. Curiously, the stole goods consists exclusively of food. Police investigations report thejob was not particularly professional, but enough to stump them. This bill will authorize the hiring of additional personel to aid the theft investigations.",
                    500,
                    60,
                    -1000,
                    0,
                    0,
                    0,
                    5,
                    0,
                    -3,
                    0,
                    0,
                    5
                ),
                new Policy(
                    "PetroBigBro",
                    "The fossil fuel industry is noticing the shift in attitude towards cleaner energy in the populace and is making anticipatory moves. Several of the largest oil companies are requesting an increase of oil price caps, potentially causing oil prices to soar to record high prices as they try to lose their stock while transitioning to the new markets.This bill will increase oil price caps to support companies while they transition.",
                    2000,
                    50,
                    5000,
                    0,
                    -3,
                    0,
                    8,
                    0,
                    0,
                    0,
                    -3,
                    5
                ),
                new Policy(
                    "RedGreenBurn",
                    "The sun shines in the bright blue sky. Unfortunately, it has spawned a bright red flame among the bright green leaves of the forest, causing massive damage to the environment. The central government is willing to aid, but a major part of the bill will still need to be footed by the local government. This bill will allow the formation of a joint firefighting force to deal with the forest fire.",
                    2000,
                    60,
                    -5000,
                    0,
                    10,
                    0,
                    5,
                    0,
                    0,
                    30,
                    0,
                    -10
                ),
                new Policy(
                    "PlantGrant",
                    "Your city has been invited to participate in an international tree planting competition in a bid to promote the dire need of regeneration of forests around the world. A lot of people are excited about the competition. This bill will formally declare the city's participation in the competition.",
                    2000,
                    50,
                    -2000,
                    0,
                    -8,
                    0,
                    3,
                    0,
                    0,
                    -5,
                    3,
                    -8
                ),
                new Policy(
                    "TreeToFood",
                    "A major player in the food industry is eager to expand operations and is requesting a forest-clearing permit, concluding that the forest was too small to be productive for logging operations despite massive protests by various environmental impact analysis experts. They claim the extra space would allow them to produce food for less money and more efficiently and are threatening to take their business elsewhere if not allowed to grow. This bill will grant the company a forest-clearing permit.",
                    500,
                    30,
                    5000,
                    0,
                    8,
                    10,
                    -10,
                    -2000,
                    -5,
                    0,
                    -10,
                    10
                ),
                new Policy(
                    "NoWayHome",
                    "A neighborhood complained that the roads in their neighborhood have fallen into disrepair. A few people have taken the issue to social media and pressing the city's Public Infrastructures Department to take action, however the department advised that the repairs will require a section of a nearby river to be reclaimed, potentially destroying the habitat of various local fishes and reducing the amount of water that flows downstream. This bill will authorize road repairs for the neighborhood.",
                    500,
                    50,
                    -1000,
                    0,
                    3,
                    0,
                    5,
                    0,
                    0,
                    0,
                    -3,
                    3
                ),
            };

            Power[] powers = new Power[]
            {
                new Power("Oil", 400, 2, 8),
                new Power("Tidal", 700, 1, 2),
            };

            Food food = new Food(2, 800, 10, 3);

            Random random = new Random();

            // Turn Loop
            do
            {
                // Reseting Variables
                policyRng = 0;

                // Count Power
                foreach (var power in powers)
                {
                    tempPowerAmount += power.playerAmount;
                }

                powerAmount = tempPowerAmount;
                tempPowerAmount = 0;

                // Produce Power
                while (powerAmount < maxPowerAmount)
                {
                    var chosenPower = Power.selectPowerToUse(powers, moneyAmount);
                    
                    if(chosenPower == null)
                    {
                        break;
                    }

                    powerAmount++;
                    chosenPower.playerAmount++;
                    moneyAmount -= chosenPower.cost;
                    pollutionAmount += chosenPower.pollution;
                }

                // Produce Food
                if(powerAmount >= food.powerCost && moneyAmount >= food.moneyCost)
                {
                    food.playerAmount += food.foodProduced;
                    moneyAmount -= food.moneyCost;

                    for(int i = 0; i < food.powerCost; i++)
                    {
                        bool powerReduced = false;

                        while(powerReduced == false)
                        {
                            var powerToUse = random.Next(0, powers.Length);

                            if(powers[powerToUse].playerAmount > 0)
                            {
                                powers[powerToUse].playerAmount -= 1;
                                powerReduced = true;
                            }
                        }
                    }

                    powerAmount -= food.powerCost;
                    moneyAmount += random.Next(500, 1000);
                }
                pollutionAmount += random.Next(1, 4);

                // Adjusting Policy RNG
                if(powerAmount >= 2 && food.playerAmount >= 3)
                {
                    policyRng += 5; // 2 from power and 3 from food
                }
                else if(powerAmount >= 2 && food.playerAmount < 3)
                {
                    policyRng -= 1; // 2 from power and -3 from food
                }
                else if(powerAmount < 2 && food.playerAmount >= 3)
                {
                    policyRng += 1; // -2 from power and 3 from food
                }
                else
                {
                    policyRng -= 5; // -2 from power and -3 from food
                }

                // Display Current Resources
                Console.WriteLine($"Power: {powerAmount}");
                Console.WriteLine($"Oil: {powers[0].playerAmount}");
                Console.WriteLine($"Tidal: {powers[1].playerAmount}");
                Console.WriteLine($"Food: {food.playerAmount}");
                Console.WriteLine($"Money: {moneyAmount}");
                Console.WriteLine($"Industry: {industryAmount}");
                Console.WriteLine($"Reputation: {reputationAmount}");
                Console.WriteLine($"Pollution: {pollutionAmount}");
                Console.WriteLine($"Policy RNG: {policyRng}");

                Console.ReadLine();

                turn++;
            } while (gameOver == false);
        }
    }
}
