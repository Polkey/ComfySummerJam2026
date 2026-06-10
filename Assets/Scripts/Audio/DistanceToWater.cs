using UnityEngine;


public class DistanceToWater : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;

    [Range(0, 50)]
    [SerializeField] float parameterDistance;
    private float parameterValue;


    [Header("Player")]
    [SerializeField] private Transform playerPos;

    void Update()
    {
        transform.position = new Vector3(playerPos.position.x, transform.position.y, transform.position.z);
        var distanceToWater = Mathf.Abs(transform.position.z - playerPos.position.z);

        parameterValue = Mathf.InverseLerp(40, 0, distanceToWater);
        // Debug.Log(parameterValue);

        AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, parameterDistance);
    }
}
