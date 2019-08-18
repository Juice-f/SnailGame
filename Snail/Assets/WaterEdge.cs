using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEdge : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject tinter;
    public bool useME = false;
    public float range;
    // Start is called before the first frame update
    private void Update()
    {
        if (Vector3.Distance(transform.position, cam.transform.position) <= range)
        {
            useME = true;
            tinter.GetComponent<WaterCamera>().waterLevel = transform.position.y;
        }
        if (useME && Vector3.Distance(transform.position, cam.transform.position) >= range) tinter.gameObject.SetActive(false);


    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    Debug.Log("Player entered");
        //    tinter.SetActive(!isActive);
        //    isActive = !isActive;
        //}
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, range);
    }

}
