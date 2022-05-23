using System;

namespace PrototypeGame_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // For Balancing
            Power[] powers = new Power[]
            {
                new Power("Oil", 250, 2, 8),
                new Power("Tidal", 500, 1, 3),
            };
            Money money = new Money(3000);
            Industry industry = new Industry(10);
            Food food = new Food(2, 600, 10, 3);
            Reputation reputation = new Reputation(100);
            Pollution pollution = new Pollution(0);
            int maxPowerAmount = 15;
            int policyCooldownIfNotChosen = 2;
            int minMoneyGainAfterProducingFood = 500;
            int maxMoneyGainAfterProducingFood = 1000;

            // Don't Change!
            int powerAmount = 0;
            int tempPowerAmount = 0;
            int policyRng = 0;
            int turn = 1;
            int numberOfPolicyThisTurn = 0;
            int popularityRng = 0;
            int policyAccept = 0;
            int policyChosen = 0;
            int restartGame = 0;
            bool gameOver = false;
            bool powerProduced = false;
            bool foodProduced = false;
            bool policyValid = false;

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
                    0,
                    5,
                    0,
                    -3,
                    0,
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
                    0,
                    -3,
                    0,
                    8,
                    0,
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
                    0,
                    10,
                    0,
                    5,
                    0,
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
                    0,
                    -8,
                    0,
                    3,
                    0,
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
                    0,
                    8,
                    10,
                    -10,
                    -2000,
                    -5,
                    0,
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
                    0,
                    3,
                    0,
                    5,
                    0,
                    0,
                    0,
                    0,
                    -3,
                    3
                ),
            };

            Policy[] availablePolicies = new Policy[policies.Length];



            Random random = new Random();

            // Turn Loop
            do
            {
                // Reseting Variables
                policyRng = 0;

                // Reduce Policy Cooldown
                Policy.reduceTurnCooldown(policies);

                // Count Power
                foreach (var power in powers)
                {
                    tempPowerAmount += power.playerAmount;
                }

                // Check Money Cap
                if(money.playerAmount < 0)
                {
                    money.playerAmount = 0;
                }

                powerAmount = tempPowerAmount;
                tempPowerAmount = 0;

                // Produce Power
                while (powerAmount < maxPowerAmount)
                {
                    var chosenPower = Power.selectPowerToUse(powers, money.playerAmount);
                    
                    if(chosenPower == null)
                    {
                        break;
                    }

                    powerAmount++;
                    chosenPower.playerAmount++;
                    money.playerAmount -= chosenPower.cost;
                    pollution.playerAmount += chosenPower.pollution;
                }

                // Produce Food
                if(powerAmount >= food.powerCost && money.playerAmount >= food.moneyCost)
                {
                    food.playerAmount += food.foodProduced;
                    money.playerAmount -= food.moneyCost;

                    Power.AddPower(powers, food.powerCost * -1);

                    powerAmount -= food.powerCost;
                    money.playerAmount += random.Next(minMoneyGainAfterProducingFood, maxMoneyGainAfterProducingFood);
                }
                pollution.playerAmount += random.Next(1, 4);

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

                // Get Random Policy
                availablePolicies = Policy.getAvailablePolicies(policies);

                if(availablePolicies.Length >= 3)
                {
                    availablePolicies = Policy.getRandomPolicies(availablePolicies, 3);
                    numberOfPolicyThisTurn = 3;
                }
                else
                {
                    availablePolicies = Policy.getRandomPolicies(availablePolicies, availablePolicies.Length);
                    numberOfPolicyThisTurn = availablePolicies.Length;
                }


                // Display Current Resources
                Console.WriteLine($"\nMonth {turn}");
                Console.WriteLine("Resources Amount:");
                Console.WriteLine($"Power: {powerAmount}");
                Console.WriteLine($"Food: {food.playerAmount}");
                Console.WriteLine($"Money: {money.playerAmount}");
                Console.WriteLine($"Industry: {industry.playerAmount}");
                Console.WriteLine($"Reputation: {reputation.playerAmount}");
                Console.WriteLine($"Pollution: {pollution.playerAmount}");
                Console.WriteLine($"Policy RNG: {policyRng}");

                do
                {
                    // Display Policies
                    Console.WriteLine("\nChoose Policy:");
                    for (int i = 0; i < numberOfPolicyThisTurn; i++)
                    {
                        Console.WriteLine($"Policy #{i + 1}: {availablePolicies[i].title} | Cost: {availablePolicies[i].cashCost}");
                    }
                    Console.WriteLine($"{numberOfPolicyThisTurn + 1}: Skip Turn");

                    // Player Choose Policy
                    do
                    {
                        Console.WriteLine("\nPlease Select Policy you want to implement:");
                        try
                        {
                            policyChosen = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine($"Please enter only 1 to {numberOfPolicyThisTurn+1}!");
                            policyChosen = 0;
                        }
                    } while (policyChosen < 1 || policyChosen > numberOfPolicyThisTurn + 1);

                    if(policyChosen != numberOfPolicyThisTurn + 1 && money.playerAmount < availablePolicies[policyChosen - 1].cashCost)
                    {
                        Console.WriteLine("Not Enough Money to Implement This Policy!");
                        policyValid = false;
                    }
                    else
                    {
                        policyValid = true;
                    }

                } while (policyValid == false);

                // Policy Cooldown Adjust
                for (int i = 0; i < numberOfPolicyThisTurn; i++)
                {
                    if(i+1 == Convert.ToInt32(policyChosen))
                    {
                        availablePolicies[i].cooldown = 3;
                    }
                    else
                    {
                        availablePolicies[i].cooldown = 1;
                    }
                }

                if(policyChosen != numberOfPolicyThisTurn + 1)
                {
                    // Accept / Reject Policy
                    do
                    {
                        Console.WriteLine($"\nAccept {availablePolicies[policyChosen - 1].title}?");
                        Console.WriteLine("1 = Accept | 0 = Reject:");
                        try
                        {
                            policyAccept = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine($"Please enter only 1 to {numberOfPolicyThisTurn + 1}!");
                            policyAccept = -1;
                        }
                    } while (policyAccept < 0 || policyAccept > 1);

                    // Policy Takes Effect
                    if(policyAccept == 1)
                    {
                        money.playerAmount += availablePolicies[policyChosen - 1].cashEffectAccept;
                        food.playerAmount += availablePolicies[policyChosen - 1].foodEffectAccept;
                        industry.playerAmount += availablePolicies[policyChosen - 1].industryEffectAccept;
                        pollution.playerAmount += availablePolicies[policyChosen - 1].pollutionEffectAccept;
                        Power.AddPower(powers, availablePolicies[policyChosen - 1].powerEffectAccept);
                        reputation.playerAmount += availablePolicies[policyChosen - 1].reputationEffectAccept;
                    }
                    else if(policyAccept == 0)
                    {
                        money.playerAmount += availablePolicies[policyChosen - 1].cashEffectReject;
                        food.playerAmount += availablePolicies[policyChosen - 1].foodEffectReject;
                        industry.playerAmount += availablePolicies[policyChosen - 1].industryEffectReject;
                        pollution.playerAmount += availablePolicies[policyChosen - 1].pollutionEffectReject;
                        Power.AddPower(powers, availablePolicies[policyChosen - 1].powerEffectReject);
                        reputation.playerAmount += availablePolicies[policyChosen - 1].reputationEffectReject;
                    }

                    money.playerAmount -= availablePolicies[policyChosen - 1].cashCost;

                    // Reputation From Popularity
                    if(availablePolicies[policyChosen - 1].popularity >= 50 && policyAccept == 1)
                    {
                        popularityRng = random.Next(5, 15);
                    }
                    else if (availablePolicies[policyChosen - 1].popularity >= 50 && policyAccept == 0)
                    {
                        popularityRng = random.Next(-15, -5);
                    }
                    else if(availablePolicies[policyChosen - 1].popularity < 50 && policyAccept == 1)
                    {
                        popularityRng = random.Next(-15, -5);
                    }
                    else if (availablePolicies[policyChosen - 1].popularity < 50 && policyAccept == 0)
                    {
                        popularityRng = random.Next(5, 15);
                    }

                    reputation.playerAmount += popularityRng;
                }

                turn++;

                // Restart Game
                do
                {
                    Console.WriteLine($"\nRestart Game?");
                    Console.WriteLine("1 = Restart Game | 0 = End Turn");
                    try
                    {
                        restartGame = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine($"Please enter only 0 or 1!");
                        restartGame = -1;
                    }
                } while (restartGame < 0 || restartGame > 1);

                if(restartGame == 1)
                {
                    // For Balancing
                    pollution.playerAmount = 0;
                    Money.resetMoney(money);
                    Power.resetPower(powers);
                    Food.resetFood(food);
                    Industry.resetIndustry(industry);
                    Reputation.resetReputation(reputation);

                    // Don't Change!
                    turn = 1;
                }

            } while (gameOver == false);
        }
    }
}
