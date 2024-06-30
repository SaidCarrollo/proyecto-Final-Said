using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectionOrden
{
    public static void selectionSortEnhanced(int[] numbers)
    {
        int tmp;
        int minId;
        for (int i = 0; i < numbers.Length - 1; ++i)
        {
            minId = i;
            for (int j = i + 1; j < numbers.Length; ++j)
            {
                if (numbers[minId] > numbers[j])
                {
                    minId = j;
                }
            }
            if (minId != i)
            {
                tmp = numbers[i];
                numbers[i] = numbers[minId];
                numbers[minId] = tmp;
            }
        }
        printArray(numbers);
    }

    private static void printArray(int[] arr)
    {
        string arrayString = "Sorted Times: ";
        foreach (int num in arr)
        {
            arrayString += num.ToString() + ", ";
        }
        Debug.Log(arrayString);
    }
}