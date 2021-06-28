using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticLibrary
{
    public static void Shuffle<T>(List<T> array)
    {
        System.Random rng = new System.Random();
        int n = array.Count;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
