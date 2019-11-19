using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class D_CircularList<T>
{
    private List<T> list;
    private int targetSize;
    private int location = 0;
    
    public D_CircularList(int size)
    {
        list = new List<T>(size);
        targetSize = size;
    }


    public void add(T item)
    {
        if (list.Count < targetSize)
        {
            list.Add(item);
        }
        else
        {
            list[location] = item;
        }

        location++;
        if (location >= targetSize)
            location = 0;
    }

    public T get(int i)
    {
        int temp = getLocation(i);
        if (temp > 0 && temp < list.Count)
        {
            return list[temp];
        }
        else
        {
            Debug.Log("Circular List result out of bounds");
            return list[0];
        }
    }

    public List<T> getRange(int a, int b)
    {
        int tempSize = b - a;
        if (tempSize > list.Count)
        {
            return list;
        }
        List<T> tempList = new List<T>();
        for (int i = a; i < b; i++)
        {
            tempList.Add(list[getLocation(i)]);
        }
        return tempList;
    }

    public int getLocation(int i)
    {
        int temp = i;
        temp = temp % list.Count;
        temp += location;
        if (temp < 0)
        {
            temp += list.Count;
        }
        temp = temp % list.Count;
        return temp;
    }
    
    
    
    
    
}
