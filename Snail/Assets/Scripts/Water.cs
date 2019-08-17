using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [System.Serializable]
    class WaterData
    {
        public Vector3 center;
        public Vector3 size;

    }
    [SerializeField]
    List<WaterData> waterDatas = new List<WaterData>();
    public Vector3 rollForce = new Vector3(0, 20, 0);
    public Vector3 crawlForce = new Vector3(0, 80, 0);

    private void FixedUpdate()
    {
        foreach (var item in waterDatas)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position + item.center, item.size / 2);
            if (colliders.Length > 0)
            {
                bool appliedForce = false;
                foreach (var col in colliders)
                {
                    if (!appliedForce && col.GetComponent<PlayerCollider>() != null)
                    {
                        // bool isRoll = col.GetComponent<PlayerCollider>().isRoll;
                        switch (col.GetComponent<PlayerCollider>().isRoll)
                        {
                            case (true):
                                col.GetComponent<PlayerCollider>().rigidBody.AddForce(transform.rotation * rollForce);
                                appliedForce = true;
                                Debug.Log("applied roll force");
                                break;
                            case (false):
                                col.GetComponent<PlayerCollider>().rigidBody.AddForce(transform.rotation * crawlForce);
                                appliedForce = true;
                                Debug.Log("Applied crawl force");
                                break;
                        }
                    }
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(new Ray(transform.position,transform.rotation *crawlForce.normalized));
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(transform.position + transform.rotation * new Vector3(1, 0, 0), transform.rotation * crawlForce.normalized));
        foreach (var item in waterDatas)
        {

            Gizmos.color = new Color(0, 0, 1, .5f);

            Gizmos.DrawCube(item.center + transform.position, item.size);
        }
    }

}
