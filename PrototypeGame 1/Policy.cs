using System;

namespace PrototypeGame_1
{
    class Policy
    {
        // Must Have
        public string title;
        public string description;
        public int cashCost;
        public int popularity;

        // Stats Affected
        public int cashEffectAccept, foodEffectAccept, powerEffectAccept, pollutionEffectAccept, industryEffectAccept, reputationEffectAccept;
        public int cashEffectReject, foodEffectReject, powerEffectReject, pollutionEffectReject, industryEffectReject, reputationEffectReject;

        public int cooldown;

        // Constructor
        public Policy(string title, string description, int cashCost, int popularity, int cashEffectAccept, int foodEffectAccept, int powerEffectAccept, int pollutionEffectAccept, int industryEffectAccept, int reputationEffectAccept, int cashEffectReject, int foodEffectReject, int powerEffectReject, int pollutionEffectReject, int industryEffectReject, int reputationEffectReject)
        {
            this.title = title;
            this.description = description;
            this.cashCost = cashCost;
            this.popularity = popularity;

            this.cashEffectAccept = cashEffectAccept;
            this.foodEffectAccept = foodEffectAccept;
            this.powerEffectAccept = powerEffectAccept;
            this.pollutionEffectAccept = pollutionEffectAccept;
            this.industryEffectAccept = industryEffectAccept;
            this.reputationEffectAccept = reputationEffectAccept;

            this.cashEffectReject = cashEffectReject;
            this.foodEffectReject = foodEffectReject;
            this.powerEffectReject = powerEffectReject;
            this.pollutionEffectReject = pollutionEffectReject;
            this.industryEffectReject = industryEffectReject;
            this.reputationEffectReject = reputationEffectReject;

            this.cooldown = 0;
        }

        static public Policy[] getAvailablePolicies(Policy[] policies)
        {
            Policy[] availablePolicies = new Policy[policies.Length];
            int index = 0;

            foreach(var policy in policies)
            {
                if(policy.cooldown == 0)
                {
                    availablePolicies[index++] = policy;
                }
            }

            Policy[] policiesToUse = new Policy[index];

            for(int i = 0; i < index; i++)
            {
                policiesToUse[i] = availablePolicies[i];
            }

            return policiesToUse;
        }

        static public Policy[] getRandomPolicies(Policy[] policies, int numberOfPolicies)
        {
            Policy[] chosenPolicies = new Policy[numberOfPolicies];
            int number = 0;

            Random rd = new Random();

            for (int i = 0; i < numberOfPolicies; i++)
            {
                bool policyChosen = false;

                while(policyChosen == false)
                {
                    number = rd.Next(0, policies.Length);

                    if(policies[number].cooldown == 0)
                    {
                        chosenPolicies[i] = policies[number];
                        policyChosen = true;
                        policies[number].cooldown = 2;
                    }
                }
            }

            return chosenPolicies;
        }

        static public void reduceTurnCooldown(Policy[] policies)
        {
            foreach(var policy in policies)
            {
                if(policy.cooldown > 0)
                {
                    policy.cooldown--;
                }
            }
        }
    }
}