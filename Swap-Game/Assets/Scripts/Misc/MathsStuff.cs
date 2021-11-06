using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathsStuff
{
    public static int GenerateRandomIndex(int maxRange)
    {
        return Random.Range(0, maxRange);
    }

    public static bool CoinToss()
    {
        int coin = Random.Range(0, 1);

        if (coin == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
