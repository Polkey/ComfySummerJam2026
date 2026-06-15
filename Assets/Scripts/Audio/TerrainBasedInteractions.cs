using System.Collections;
using UnityEngine;

public class TerrainBasedInteractions : MonoBehaviour
{
    BasicFPCC fpcc;
    [Range(0.01f, 1f)][SerializeField] private float footstepbuffer;
    private int currentTerrain;

    void Awake()
    {
        fpcc = GetComponent<BasicFPCC>();
    }

    private IEnumerator Footsteps()
    {
        while (true && fpcc.isGrounded)
        {
            AudioManager.instance.SetFootstepParameter("Terrain", currentTerrain);
            AudioManager.instance.PlayOneShot(FMODEvents.instance.playerFootsteps, transform.position);
        }
        return null;
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Grass":

                break;

            case "Rock":
                
                break;

            case "WetSand":
                
                break;

            default:
                // sand
                break;
        }
    }
}
