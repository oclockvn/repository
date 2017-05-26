using System;

namespace oclockvn.Extensions
{
    public static class MoneyExtensions
    {
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

        public static int ToVnd(this decimal money)
        {
            return money.ToRoundMoney(500, 200);
        }
    }
}
