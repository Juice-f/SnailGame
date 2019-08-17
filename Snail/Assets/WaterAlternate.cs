using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAlternate : MonoBehaviour
{
    public float WaitTime = 2f;
    public float timer = 0;
    public Vector3 rot1, rot2;
    bool rotated;
    private void Update()
    {
        if (timer < WaitTime)
        {
            timer += Time.deltaTime;


        }
        else
        {
            timer = 0;  
            if (!rotated)
            {
                transform.rotation = Quaternion.Euler(rot2);
                rotated = true;
            }
            else
            {
                rotated = false;
                transform.rotation = Quaternion.Euler(rot1);
            }
        }
    }

}
