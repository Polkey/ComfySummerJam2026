using System.Collections.Generic;
using UnityEngine;

public static class ObjectiveManager 
{
    private static readonly HashSet<string> m_completed = new();

    public static void CompleteObjective(string tag) 
    {
        if (m_completed.Contains(tag)) return;

        Debug.Log(tag);
        m_completed.Add(tag);
    }
    public static bool HasObjective(string objectiveTag)
    {
        return m_completed.Contains(objectiveTag);
    }
}