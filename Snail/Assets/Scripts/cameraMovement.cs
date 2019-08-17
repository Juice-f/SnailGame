using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public GameObject snail;
    private Vector2 cameraPosition, playerPos;

    public float camStrayDistance, lerpMultiplier = 15f, distanceToPlayer = 0f;
   
    void Start()
    {
        snail = GameObject.Find("Snail");


    }

    void FixedUpdate()
    {
        cameraPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        playerPos = new Vector2(snail.transform.position.x, snail.transform.position.y + 4f);

        distanceToPlayer = Mathf.Abs(Mathf.Abs(playerPos.magnitude) - Mathf.Abs(cameraPosition.magnitude));

        Debug.Log("Mathf.Abs(Mathf.Abs(playerPos.magnitude) - Mathf.Abs(cameraPosition.magnitude))");

       
        
        transform.position = new Vector3 (Mathf.Lerp(cameraPosition.x, playerPos.x, lerpMultiplier * Time.deltaTime * distanceToPlayer), Mathf.Lerp(cameraPosition.y, playerPos.y, lerpMultiplier * Time.deltaTime), transform.position.z);

            
        


    }
}
