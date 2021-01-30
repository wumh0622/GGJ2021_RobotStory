using System;
using System.Collections.Generic;

public static class MathUtility
{
    public static int RoundToInt(float f)
    {
        int floorValue = (int)f;
        if (f >= 0f)
        {
            if (f - floorValue >= 0.5f)
            {
                return floorValue + 1;
            }
            else
            {
                return floorValue;
            }
        }
        else
        {
            if (floorValue - f >= 0.5f)
            {
                return floorValue - 1;
            }
            else
            {
                return floorValue;
            }
        }
    }

    public static int RandomWithWeights(Random random, params int[] weights)
    {
        int total = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            total += weights[i];
        }

        int randomNumber = random.Next(1, total + 1);
        int resultIndex = -1;
        for (int i = weights.Length - 1; i >= 0; i--)
        {
            total -= weights[i];
            if (randomNumber > total)
            {
                resultIndex = i;
                break;
            }
        }

        // Impossible to reach here
        return resultIndex;
    }

    public static int RandomWithWeights(Random random, List<int> weights)
    {
        int total = 0;
        for (int i = 0; i < weights.Count; i++)
        {
            total += weights[i];
        }

        int randomNumber = random.Next(1, total + 1);
        int resultIndex = -1;
        for (int i = weights.Count - 1; i >= 0; i--)
        {
            total -= weights[i];
            if (randomNumber > total)
            {
                resultIndex = i;
                break;
            }
        }

        // Impossible to reach here
        return resultIndex;
    }
}
