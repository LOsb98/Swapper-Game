using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SwapGame.StaticMethods
{
    public static class MathsStuff
    {
        public static int GenerateRandomIndex(int maxRange)
        {
            return Random.Range(0, maxRange);
        }

        /// <summary>
        /// Use when you want a 50/50
        /// </summary>
        /// <returns>Equal chance of true or false</returns>
        public static bool CoinToss()
        {
            int coin = Random.Range(0, 2);

            if (coin == 1)
            {
                Debug.Log("Coin toss heads");
                return true;
            }
            else
            {
                Debug.Log("Coin toss tails");
                return false;
            }
        }
    }
}
