using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;


public class DistanceToWater : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;

    [Range(0, 100)]
    [SerializeField] float parameterDistance;
    private float parameterValue;

    [Header("Player")]
    [SerializeField] private Transform player;


    [Header("Spline")]
    [SerializeField] private SplineContainer splineContainer;
    private Spline spline;

    void Awake()
    {
        spline = splineContainer.Spline;

        if(player == null)
        {
            player = FindAnyObjectByType<CharacterController>().transform;
        }
    }

    void Update()
    {

        Vector3 localSplinePoint = 
            splineContainer.transform.InverseTransformPoint(player.position);

        SplineUtility.GetNearestPoint(
            spline, 
            localSplinePoint, 
            out float3 nearestPoint, 
            out _);
            
        Vector3 nearestWorldPosition = splineContainer.transform.TransformPoint(nearestPoint);

        transform.position = nearestWorldPosition;

        var distanceToWater = Mathf.Abs(Vector3.Distance(player.position, transform.position));

        parameterValue = Mathf.InverseLerp(40, 0, distanceToWater);

        AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, parameterDistance);
    }
}
