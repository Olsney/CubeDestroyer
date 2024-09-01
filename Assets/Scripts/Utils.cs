﻿using UnityEngine;

namespace DefaultNamespace
{
    public class Utils
    {
        public static bool RollChance(int chance)
        {
            int maxChance = 100;
            int minChance = 0;

            return chance > Random.Range(minChance, maxChance);
        }
    }
}