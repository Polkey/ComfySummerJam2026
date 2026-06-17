using System.Collections;
using UnityEngine;

public class Datacenter : MonoBehaviour
{
    public float speed = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Destroy();
    }
    public void Destroy()
    {
        StartCoroutine(DestroyCo());       
    }
    IEnumerator DestroyCo() 
    {
        while (true) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, speed * Time.deltaTime);
            if (transform.localScale.magnitude <= Vector3.zero.magnitude) 
            {
                Destroy(gameObject);
                yield break;
            }                
            yield return null;
        }
    }
}
