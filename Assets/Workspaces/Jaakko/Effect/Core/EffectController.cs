using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.Cinemachine;
using System.Linq;

public class EffectController : MonoBehaviour 
{
    public static EffectController I { get; private set; }
    private Volume GlobalVolume;
    private CinemachineBrain CinemachineBrain;

    private readonly List<IEffectInstance> m_activeEffects = new();

    private Dictionary<string, EffectDefinition> m_effectMap;
    private List<EffectDefinition> m_effectDefinitions;
    private void Awake()
    {
        if (I != null) 
        {
            Debug.LogWarning($"EffectController: Duplicate instance found!");
            Destroy(gameObject);
            return;                
        }
        I = this;
        DontDestroyOnLoad(gameObject);

        if (!RefreshVolume()) 
        {
            Debug.LogError($"EffectController: No global volume found in the scene! Effects will not work.");
            return;
        }
        CinemachineBrain = FindAnyObjectByType<CinemachineBrain>();
        if (CinemachineBrain == null)
        {
            Debug.LogError($"EffectController: No CinemachineBrain found in the scene! Effects will not work.");
            return;
        }

        m_effectDefinitions = new List<EffectDefinition>(Resources.LoadAll<EffectDefinition>("Effects"));
        m_effectMap = new Dictionary<string, EffectDefinition>();
        foreach (var def in m_effectDefinitions)
        {
            Debug.Log($"EffectController: Loaded effect definition: {def.name}");

            m_effectMap[def.name] = def;
        }
    }
    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = m_activeEffects.Count - 1; i >= 0; i--) 
        {
            var e = m_activeEffects[i];
            e.Tick(dt);

            if (e.IsFinished) 
            {
                m_activeEffects[i].OnExit();
                m_activeEffects.RemoveAt(i);                
            }                
        }
    }
    public EffectDefinition Get(string id)
    {
        if (m_effectMap.TryGetValue(id, out var def))
            return def;

        Debug.LogWarning($"EffectController: Effect not found: {id}");
        return null;
    }
    public void PlayEffect(EffectDefinition def) 
    {
        if (def == null)
        {
            Debug.LogWarning("EffectController: Cannot play null effect.");
            return;
        }
        if (GlobalVolume == null) 
        {
            Debug.LogWarning($"EffectController: GlobalVolume == null");
            return;
        }

        CinemachineCamera camera = CinemachineBrain.ActiveVirtualCamera as CinemachineCamera;

        var context = new EffectContext(GlobalVolume, camera, camera.transform);
        IEffectInstance instance = def.Create(context);
        instance.OnEnter();
        m_activeEffects.Add(instance);
    }
    // idk if this is needed but i was thinking that if the global volume gets changed
    // ie. Lighting_ object is changed then this can be called to update the reference
    public bool RefreshVolume() 
    {
        GlobalVolume = GameObject.FindObjectsByType<Volume>().FirstOrDefault(v => v.isGlobal);
        return GlobalVolume != null;
    }
    
}