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
        public int cashEffectAccept, foodEffectAccept, pollutionEffectAccept, industryEffectAccept, reputationEffectAccept;
        public int cashEffectReject, foodEffectReject, pollutionEffectReject, industryEffectReject, reputationEffectReject;

        // Constructor
        public Policy(string title, string description, int cashCost, int popularity, int cashEffectAccept, int foodEffectAccept, int pollutionEffectAccept, int industryEffectAccept, int reputationEffectAccept, int cashEffectReject, int foodEffectReject, int pollutionEffectReject, int industryEffectReject, int reputationEffectReject)
        {
            this.title = title;
            this.description = description;
            this.cashCost = cashCost;
            this.popularity = popularity;

            this.cashEffectAccept = cashEffectAccept;
            this.foodEffectAccept = foodEffectAccept;
            this.pollutionEffectAccept = pollutionEffectAccept;
            this.industryEffectAccept = industryEffectAccept;
            this.reputationEffectAccept = reputationEffectAccept;

            this.cashEffectReject = cashEffectReject;
            this.foodEffectReject = foodEffectReject;
            this.pollutionEffectReject = pollutionEffectReject;
            this.industryEffectReject = industryEffectReject;
            this.reputationEffectReject = reputationEffectReject;
        }

    }
}