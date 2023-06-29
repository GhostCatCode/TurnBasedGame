using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillImpactFactory
{
    private static Dictionary<string, ISkillImpact> cache = new Dictionary<string, ISkillImpact>();

    public static ISkillImpact GetSkillImpact(string impact)
    {
        string impactName = "SkillImpact" + impact;
        if (!cache.ContainsKey(impactName))
        {
            Type type = Type.GetType(impactName);
            cache.Add(impactName, Activator.CreateInstance(type) as ISkillImpact);
        }
        return cache[impactName];
    }

    public static void Clear()
    {
        cache.Clear();
    }
}
