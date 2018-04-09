using System;

namespace CarPricer
{
    public class PriceDeterminator
    {
        public decimal DetermineCarPrice(Car car)
        {
            // This is written in a "fine-grained" style, with logic aggressively separated out
            // into methods. I can also keep the logic inline if that's the team's preferred style.

            decimal value = car.PurchaseValue;

            value = DepreciateByAge(value, car.AgeInMonths);
            value = DepreciateByMileage(value, car.NumberOfMiles);
            value = DepreciateByPreviousOwners(value, car.NumberOfPreviousOwners);
            value = DepreciateByCollisions(value, car.NumberOfCollisions);
            value = AppreciateByPreviousOwners(value, car.NumberOfPreviousOwners);

            return value;
        }

        /// <summary>
        /// For each month of age, reduce the value by 0.5 percent, up to 10 years (120 months).
        /// </summary>
        private decimal DepreciateByAge(decimal value, int ageInMonths)
        {
            int effectiveAgeMonths = Math.Min(ageInMonths, 120);
            value -= value * 0.005m * effectiveAgeMonths;
            return value;
        }

        /// <summary>
        /// For each 1,000 miles, reduce the value by 0.2 percent, up to 150,000 miles.
        /// </summary>
        private decimal DepreciateByMileage(decimal value, int numberOfMiles)
        {
            // The instructions say: "Do not consider remaining miles." I am assuming that this
            // means that only whole increments of 1,000 miles are considered, and remaining miles
            // up to 999 are ignored.

            int thousandMileIncrements = Math.Min(numberOfMiles, 150000) / 1000;
            value -= value * 0.002m * thousandMileIncrements;
            return value;
        }

        /// <summary>
        /// If the car has had more than 2 previous owners, reduce its value by 25 percent.
        /// </summary>
        private decimal DepreciateByPreviousOwners(decimal value, int numberOfPreviousOwners)
        {
            if (numberOfPreviousOwners > 2)
            {
                value -= value * 0.25m;
            }

            return value;
        }

        /// <summary>
        /// For each collision, reduce the value by 2 percent, up to 5 collisions.
        /// </summary>
        private decimal DepreciateByCollisions(decimal value, int numberOfCollisions)
        {
            int effectiveCollisions = Math.Min(numberOfCollisions, 5);
            value -= value * 0.02m * effectiveCollisions;
            return value;
        }

        /// <summary>
        /// If the car has had no previous owners, add 10 percent to its value.
        /// </summary>
        private decimal AppreciateByPreviousOwners(decimal value, int numberOfPreviousOwners)
        {
            if (numberOfPreviousOwners == 0)
            {
                value += value * 0.1m;
            }

            return value;
        }
    }
}