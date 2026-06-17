using System.Collections;
using UnityEngine;

public class Datacenter : MonoBehaviour
{
    public float time = 1f;
    public void Destroy()
    {
        StartCoroutine(DestroyCo());       
    }
    IEnumerator DestroyCo() 
    {
        Vector3 startScale = transform.localScale;
        float elapsed = 0f;

        while (elapsed < time) 
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;

            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            yield return null;
        }
        Destroy(gameObject);
    }
}
