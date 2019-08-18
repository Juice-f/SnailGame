using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    public float waterLevel = 438;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (cam.transform.position.y < waterLevel)
        {
            transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
            GetComponent<MeshRenderer>().enabled = true;
        }
        else GetComponent<MeshRenderer>().enabled = false;
    }
}
