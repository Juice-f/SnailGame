using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollScript : MonoBehaviour
{
    public Rigidbody rb;

    public float maxGroundedDistance = 3f, timeSinceSound = 0f;

    private AudioSource aud;

    public AudioClip[] bounceSounds;

    private AudioClip bounceSound;

    public SphereCollider rollCollider;
    public CapsuleCollider slimeCollider;

    public Vector3 downForce;

   
    void Start()
    {

        aud = gameObject.GetComponent<AudioSource>();
        rb = gameObject.GetComponent<Rigidbody>();
        Vector3 downForce = new Vector3(0f, -10f, 0f);

    }
    
    void Update()
    {
        timeSinceSound += Time.deltaTime;
    
        if (Physics.Raycast(transform.position, Vector3.up *-1f, maxGroundedDistance))
        {
            Debug.Log("hit");
            rb.AddForce(downForce);
            rb.velocity += rb.velocity * 0.3f * Time.deltaTime;
        }
        else
        {
            Debug.Log("fly");
            rb.AddForce(downForce*-122f);
        }

        if (Input.GetButtonUp("Roll"))
        {

            this.gameObject.GetComponent<SnailCrawlMovement>().enabled = true;
            this.gameObject.GetComponent<RollScript>().enabled = false;
            rollCollider.enabled = false;
            slimeCollider.enabled = true;
        }
     
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (timeSinceSound > 0.1f)
        {
            timeSinceSound = 0f;
            int index = Random.Range(0, bounceSounds.Length);
            bounceSound = bounceSounds[index];
            aud.clip = bounceSound;
            aud.Play();
        }

    }


}
