using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEdge : MonoBehaviour
{
    [SerializeField] GameObject tinter;
    bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered");
            tinter.SetActive(!isActive);
            isActive = !isActive;
        }
    }
}
