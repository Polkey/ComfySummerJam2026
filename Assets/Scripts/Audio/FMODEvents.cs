using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Item SFX")]
    [field: SerializeField] public EventReference pickupSFX { get; private set; }
    [field: SerializeField] public EventReference pickupIdle { get; private set; }

    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one FMODEvents instances in the scene");
        }
        instance = this;
    }

}
