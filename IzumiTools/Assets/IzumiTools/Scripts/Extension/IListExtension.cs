using System;
using System.Collections.Generic;
using UnityEngine;

public static class IListExtension
{
    public static T GetRandomElement<T>(this IList<T> list, bool removeFoundElement = false)
    {
        int dmpIndex;
        return GetRandomElement(list, out dmpIndex, removeFoundElement);
    }
    public static T RemoveRandomElement<T>(this IList<T> list)
    {
        int dmpIndex;
        return RemoveRandomElement(list, out dmpIndex);
    }
    public static T GetRandomElement<T>(this IList<T> list, out int index, bool removeFoundElement = false)
    {
        int count = list.Count;
        if(count > 0)
        {
            index = UnityEngine.Random.Range(0, count);
            T element = list[index];
            if (removeFoundElement)
                list.RemoveAt(index);
            return element;
        }
        else
        {
            index = -1;
            return default(T);
        }
    }
    public static T RemoveRandomElement<T>(this IList<T> list, out int index)
    {
        return GetRandomElement(list, out index, true);
    }
    public static List<T> GetUniqueRandomElements<T>(this IList<T> list, Func<T, List<T>, bool> continueCondition)
    {
        List<T> lestArray = new List<T>(list); 
        List<T> resultArray = new List<T>();
        while (lestArray.Count > 0)
        {
            int index;
            T pickedElement = GetRandomElement(lestArray, out index);
            lestArray.RemoveAt(index);
            resultArray.Add(pickedElement);
            if (!continueCondition.Invoke(pickedElement, resultArray))
                break;
        }
        return resultArray;
    }
    public static List<T> GetUniqueRandomElements<T>(this IList<T> list, int amount)
    {
        return amount <= 0 ? new List<T>() : GetUniqueRandomElements(list, (pickedElement, resultArray) => resultArray.Count < amount);
    }
    public static List<T> Shuffle_InsideOut<T>(this List<T> list)
    {
        List<T> tempList = new List<T>(list);
        int rand;
        T tempValue;
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            rand = UnityEngine.Random.Range(0, i + 1);
            tempValue = tempList[rand];
            tempList[rand] = tempList[i];
            tempList[i] = tempValue;
        }
        return tempList;
    }
    public static T RemoveAndGetAt<T>(this List<T> list, int index)
    {
        T element = list[index];
        list.RemoveAt(index);
        return element;
    }
    public static T RemoveFirst<T>(this List<T> list)
    {
        return list.RemoveAndGetAt(0);
    }
    public static T RemoveLast<T>(this List<T> list)
    {
        return list.RemoveAndGetAt(list.Count - 1);
    }
    public static T RemoveOne<T>(this List<T> list, Predicate<T> match)
    {
        foreach (T element in list)
        {
            if (match.Invoke(element))
            {
                list.Remove(element);
                return element;
            }
        }
        return default(T);
    }
    public static KeyValuePair<T, float> FindMostValuable<T>(this List<T> list, Func<T, float> valuer)
    {
        T candidate = default;
        float maxValue = 0;
        foreach(T element in list)
        {
            float value = valuer.Invoke(element);
            if (value > maxValue)
            {
                maxValue = value;
                candidate = element;
            }
        }
        return new KeyValuePair<T, float>(candidate, maxValue);
    }
}
