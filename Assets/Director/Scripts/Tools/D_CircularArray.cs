using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_CircularArray<T>
{

    private T[] array;
    private int position = 0;
    private int len = 0;
    private bool init = false;

    public D_CircularArray(int length, T initializeWith)
    {
        array  = new T[length];
        len = length;
        initialize(initializeWith);
        init = true;
    }
    
    public D_CircularArray(int length)
    {
        array  = new T[length];
        len = length;
    }

    private void initialize(T initializeWith)
    {
        for (int i = 0; i < 0; i++)
        {
            array[i] = initializeWith;
        }
    }
    

    public void Add(T input)
    {
        if (!init)
        {
            initialize(input);
            init = true;
        }
        array[position] = input;
        position++;
        if (position > len-1)
            position = 0;

    }

    public T Get(int i)
    {
        return array[convert(i)];
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
    
    
    
    
    
    /*
      public bool testValid(int i)
    {
        return array[convert(i)] != null;
        
    }

    public bool testRange(int lb, int ub)
    {
        bool test = true;
        for (int i = lb; i < ub; i++)
        {
            test = test && testValid(i);
        }
        return test;
    }
    public bool testRange(int i)
    {
        if (i > 0)
            return testRange(0, i);
        return testRange(i, 0);
    }
     */
    
    
}
