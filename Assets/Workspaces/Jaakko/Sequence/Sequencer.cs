using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sequencer : MonoBehaviour 
{
    private readonly List<Sequence> m_active = new();
    public void Play(Sequence sequence) 
    {
        var type = sequence.GetType();

        if (!sequence.IsStackable) 
        {
            bool alreadyRunning = m_active.Exists(s => s.GetType() == type);
            if (alreadyRunning) 
            {
                return;
            }
        }
        Debug.Log($"Start: {sequence}");
        m_active.Add(sequence);
        sequence._Start();
    } 
    private void SequenceFinished(Sequence sequence) 
    {
        if (!m_active.Contains(sequence)) 
        {
            Debug.LogWarning($"Sequencer.Finish(): active does not contain {sequence}");
            return;
        }
        Debug.Log($"Stop: {sequence}");
        m_active.Remove(sequence);
    }
    private void Update()
    {
        for (int i = 0; i < m_active.Count; i++) 
        {
            if (m_active[i].IsFinished) 
            {
                SequenceFinished(m_active[i]);
                continue;
            }
            m_active[i]._Tick();
        }
    }
}