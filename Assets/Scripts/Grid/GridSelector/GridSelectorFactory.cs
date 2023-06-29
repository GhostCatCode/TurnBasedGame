using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSelectorFactory
{
    private static Dictionary<string, IGridSelector> cache = new Dictionary<string, IGridSelector>();

    public static IGridSelector GetGridSelector(E_GridSelector_Type gridSelector)
    {
        string gridSelectorName = "GridSelector" + gridSelector.ToString();
        if (!cache.ContainsKey(gridSelectorName))
        {
            Type type = Type.GetType(gridSelectorName);
            cache.Add(gridSelectorName, Activator.CreateInstance(type) as IGridSelector);
        }
        return cache[gridSelectorName];
    }

    public static void Clear()
    {
        cache.Clear();
    }
}
