using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CircularArray<T>
{

    private T[] array;
    private int position = 0;
    private int len = 0;

    public D_CircularArray(int length)
    {
        array  = new T[length];
        len = length;
    }
    

    public void Add(T input)
    {
        position++;
        if (position > len-1)
            position = 0;
        array[position] = input;
    }

    public T Get(int i)
    {
        return array[convert(i)];
    }

    public bool testValid(int i)
    {
        return array[convert(i)] != null;
        
    }

    private int convert(int i)
    {
        int offset = (i%len) + position;
        if (offset < 0)
        {
            return len + offset;
        }
        else if (i > len-1)
        {
            return offset % len;
        }
        return offset;
    }

    public int length()
    {
        return len;
    }
}
