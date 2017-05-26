using System;

namespace oclockvn.Extensions
{
    public static class MoneyExtensions
    {
        /// <summary>
        /// Round a decimal into integer
        /// </summary>
        /// <param name="money">The input value</param>
        /// <param name="unit">The unit of a money system. This is the minimum value in a money system.</param>
        /// <param name="min">
        /// The minimum value to compare. Default it is equal to unit
        /// </param>
        /// <returns>The rounded money</returns>
        public static int ToRoundMoney(this decimal money, int unit = 0, int min = 0)
        {
            if (money == 0)
                return 0;

            var negative = money < 0;

            money = Math.Abs(money);
            var val = (int)Math.Round(money, MidpointRounding.ToEven);
            var mod = val % Math.Max(unit, 1);

            if (mod >= min)
                val = val - mod + unit;
            else
                val = val - mod;

            return negative ? -val : val;
        }

        /// <summary>
        /// Round money in Vietnam money system
        /// </summary>
        /// <param name="money">The input value</param>
        /// <returns>The rounded money</returns>
        public static int ToVnd(this decimal money)
        {
            return money.ToRoundMoney(500, 200);
        }
    }
}
