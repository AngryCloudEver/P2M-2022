using System;
using System.Text;
using System.IO;

using System.Collections.Generic;
using System.Collections;

namespace PrototypeGame_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Access text file
            const string powerFile = "power.txt";
            const string foodFile = "food.txt";
            const string statFile = "stats.txt";
            const string policyFile = "policy.txt";
            const string policyDescFile = "policyDescription.txt";
            StreamReader powerReader = new StreamReader(powerFile);
            StreamReader foodReader = new StreamReader(foodFile);
            StreamReader statReader = new StreamReader(statFile);
            StreamReader policyReader = new StreamReader(policyFile);
            StreamReader descriptionReader = new StreamReader(policyDescFile);
            // Create dictionary to store key value data from text file
            Dictionary<string, int[]> powerDictionary = new Dictionary<string, int[]>();
            Dictionary <string,int> statDictionary = new Dictionary<string, int>();
            List<int> foodArray = new List<int>();
            Dictionary<string, List<int>> policyDictionary = new Dictionary<string, List<int>>();
            Dictionary<string, string> descriptionDictionary = new Dictionary<string, string>();
            // Store value from text file to created dictionaries and list
            string line;
            while((line = powerReader.ReadLine())!= null)
            {
                string[] srArray = line.Split(',');
                int[] powerInt = {int.Parse(srArray[1]), int.Parse(srArray[2]), int.Parse(srArray[3]) };
                powerDictionary.Add(srArray[0], powerInt);
            }
            while ((line = foodReader.ReadLine()) != null)
            {
                string[] srArray = line.Split(',');
                foodArray.Add(int.Parse(srArray[0]));
                foodArray.Add(int.Parse(srArray[1]));
                foodArray.Add(int.Parse(srArray[2]));
                foodArray.Add(int.Parse(srArray[3]));
            }
            while ((line=statReader.ReadLine()) != null)
            {
                string[] srArray = line.Split(',');
                statDictionary.Add(srArray[0],int.Parse(srArray[1]));
            }
            while ((line = policyReader.ReadLine()) != null)
            {
                string[] srArray = line.Split(',');
                List<int> srList = new List<int>();
                int i = 0;
                foreach(string s in srArray)
                {
                    if (i != 0)
                    {
                        srList.Add(int.Parse(s));
                    }
                    i++;
                }
                policyDictionary.Add(srArray[0], srList);
            }
            while ((line = descriptionReader.ReadLine()) != null)
            {
                string[] srArray = line.Split(',');
                descriptionDictionary.Add(srArray[0], srArray[1]);
            }
            // Declare stat values based on  value from dictionary and list

            Power[] powers = new Power[]
            {

                new Power("Oil", 250, 2, 8),
                new Power("Tidal", 500, 1, 3),

                new Power("Oil",powerDictionary["Oil"][0],powerDictionary["Oil"][1], powerDictionary["Oil"][2]),
                new Power("Tidal",powerDictionary["Tidal"][0],powerDictionary["Tidal"][1],powerDictionary["Tidal"][2])

            };
            Food food = new Food(foodArray[0], foodArray[1], foodArray[2], foodArray[3]);
            Money money = new Money(statDictionary["money"]);
            Industry industry = new Industry(statDictionary["industry"]);
            Reputation reputation = new Reputation(statDictionary["reputation"]);
            Pollution pollution = new Pollution(statDictionary["pollution"]);
            int maxPowerAmount = statDictionary["maxPowerAmount"];
            int policyCooldownIfNotChosen = statDictionary["policyCooldownIfNotChosen"];
            int minMoneyGainAfterProducingFood = statDictionary["minMoneyGainAfterProducingFood"];
            int maxMoneyGainAfterProducingFood = statDictionary["maxMoneyGainAfterProducingFood"];

            powerReader.Close();
            foodReader.Close();
            statReader.Close();
            policyReader.Close();
            descriptionReader.Close();
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
                
                new Policy("RobbinFood",
                    descriptionDictionary["RobbinFood"],
                    policyDictionary["RobbinFood"][0],
                    policyDictionary["RobbinFood"][1],
                    policyDictionary["RobbinFood"][2],
                    policyDictionary["RobbinFood"][3],
                    policyDictionary["RobbinFood"][4],
                    policyDictionary["RobbinFood"][5],
                    policyDictionary["RobbinFood"][6],
                    policyDictionary["RobbinFood"][7],
                    policyDictionary["RobbinFood"][8],
                    policyDictionary["RobbinFood"][9],
                    policyDictionary["RobbinFood"][10],
                    policyDictionary["RobbinFood"][11],
                    policyDictionary["RobbinFood"][12],
                    policyDictionary["RobbinFood"][13]

                ),
                new Policy(
                    "PetroBigBro",
                    descriptionDictionary["PetroBigBro"],
                    policyDictionary["PetroBigBro"][0],
                    policyDictionary["PetroBigBro"][1],
                    policyDictionary["PetroBigBro"][2],
                    policyDictionary["PetroBigBro"][3],
                    policyDictionary["PetroBigBro"][4],
                    policyDictionary["PetroBigBro"][5],
                    policyDictionary["PetroBigBro"][6],
                    policyDictionary["PetroBigBro"][7],
                    policyDictionary["PetroBigBro"][8],
                    policyDictionary["PetroBigBro"][9],
                    policyDictionary["PetroBigBro"][10],
                    policyDictionary["PetroBigBro"][11],
                    policyDictionary["PetroBigBro"][12],
                    policyDictionary["PetroBigBro"][13]
                ),
                new Policy(
                    "RedGreenBurn",
                    descriptionDictionary["RedGreenBurn"],
                    policyDictionary["RedGreenBurn"][0],
                    policyDictionary["RedGreenBurn"][1],
                    policyDictionary["RedGreenBurn"][2],
                    policyDictionary["RedGreenBurn"][3],
                    policyDictionary["RedGreenBurn"][4],
                    policyDictionary["RedGreenBurn"][5],
                    policyDictionary["RedGreenBurn"][6],
                    policyDictionary["RedGreenBurn"][7],
                    policyDictionary["RedGreenBurn"][8],
                    policyDictionary["RedGreenBurn"][9],
                    policyDictionary["RedGreenBurn"][10],
                    policyDictionary["RedGreenBurn"][11],
                    policyDictionary["RedGreenBurn"][12],
                    policyDictionary["RedGreenBurn"][13]
                ),
                new Policy(
                    "PlantGrant",
                    descriptionDictionary["PlantGrant"],
                    policyDictionary["PlantGrant"][0],
                    policyDictionary["PlantGrant"][1],
                    policyDictionary["PlantGrant"][2],
                    policyDictionary["PlantGrant"][3],
                    policyDictionary["PlantGrant"][4],
                    policyDictionary["PlantGrant"][5],
                    policyDictionary["PlantGrant"][6],
                    policyDictionary["PlantGrant"][7],
                    policyDictionary["PlantGrant"][8],
                    policyDictionary["PlantGrant"][9],
                    policyDictionary["PlantGrant"][10],
                    policyDictionary["PlantGrant"][11],
                    policyDictionary["PlantGrant"][12],
                    policyDictionary["PlantGrant"][13]
                ),
                new Policy(
                    "TreeToFood",
                    descriptionDictionary["TreeToFood"],
                    policyDictionary["TreeToFood"][0],
                    policyDictionary["TreeToFood"][1],
                    policyDictionary["TreeToFood"][2],
                    policyDictionary["TreeToFood"][3],
                    policyDictionary["TreeToFood"][4],
                    policyDictionary["TreeToFood"][5],
                    policyDictionary["TreeToFood"][6],
                    policyDictionary["TreeToFood"][7],
                    policyDictionary["TreeToFood"][8],
                    policyDictionary["TreeToFood"][9],
                    policyDictionary["TreeToFood"][10],
                    policyDictionary["TreeToFood"][11],
                    policyDictionary["TreeToFood"][12],
                    policyDictionary["TreeToFood"][13]
                ),
                new Policy(
                    "NoWayHome",
                    descriptionDictionary["NoWayHome"],
                    policyDictionary["NoWayHome"][0],
                    policyDictionary["NoWayHome"][1],
                    policyDictionary["NoWayHome"][2],
                    policyDictionary["NoWayHome"][3],
                    policyDictionary["NoWayHome"][4],
                    policyDictionary["NoWayHome"][5],
                    policyDictionary["NoWayHome"][6],
                    policyDictionary["NoWayHome"][7],
                    policyDictionary["NoWayHome"][8],
                    policyDictionary["NoWayHome"][9],
                    policyDictionary["NoWayHome"][10],
                    policyDictionary["NoWayHome"][11],
                    policyDictionary["NoWayHome"][12],
                    policyDictionary["NoWayHome"][13]
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
