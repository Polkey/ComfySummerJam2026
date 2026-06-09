using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance { get; private set; }

    [field: Header("Pickup SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Pickup SFX")]
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
