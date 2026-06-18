using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class Sequencer : MonoBehaviour 
{
    private readonly List<Sequence> m_active = new();

    public static Sequencer I { get; private set; }

    private SequenceContext m_context;

    private readonly Dictionary<System.Type, SequenceDefinition> m_definitions = new();

    private void Awake()
    {
        if (I != null) 
        {
            Destroy(gameObject);
            return;
        }   
        I = this;

        foreach (var def in FindObjectsByType<SequenceDefinition>())
        {
            m_definitions[def.GetType()] = def;
        }
    }
    private void OnDestroy()
    {
        if (I == this) 
        {
            I = null;
        }
    }
    private void Start()
    {
        CinemachineBrain brain = FindAnyObjectByType<CinemachineBrain>();
        if (brain == null)
        {
            Debug.Log("Brain NULL");
            return;
        }
        CinemachineCamera camera = brain.ActiveVirtualCamera as CinemachineCamera;
        BasicFPCC player = FindAnyObjectByType<BasicFPCC>();
        if (player == null)
        {
            Debug.Log("Player NULL");
            return;            
        }

        m_context = new SequenceContext()
        {
            Player = player,
            Camera = camera.transform,
            CameraRoot = camera.transform.parent,
            EffectController = EffectController.I
        };

    }
    public void Play<T>() where T : SequenceDefinition
    {
        if (!m_definitions.TryGetValue(typeof(T), out var definition))
        {
            Debug.LogWarning($"No SequenceDefinition of type {typeof(T).Name} found.");
            return;
        }

        Play(definition.Create(m_context));
    }
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