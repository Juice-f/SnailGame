using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailCrawlMovement : MonoBehaviour
{
    [System.Serializable]
    class SnailThingy
    {
        public float range;
        public Vector3 position;
        public Vector3 direction;

        //public float
    }

    public void StartWall(Vector3 vector3)
    {

    }

    [SerializeField] SnailThingy[] snailThingies = new SnailThingy[] { new SnailThingy() };
    [SerializeField] Collider rollCollider;
    [SerializeField] Collider slimeCollider;
    [SerializeField] float righySpeed = 5f;
    [SerializeField] float movementSpeed;
    [SerializeField] float groundCheckRange;
    [SerializeField] Vector3 groundCheckDirection;

    [SerializeField] int leftIndex, rightIndex, middleIndex;

    Vector3 GroundCheckDirection
    {
        get => transform.rotation * groundCheckDirection;
    }
    [SerializeField] Vector3 groundCheckOrigin;
    [SerializeField] LayerMask groundMask;
    Vector3 GroundCheckPosition
    {
        get => transform.position + transform.rotation * groundCheckOrigin;
    }


    RaycastHit GetGroundRay(int index)
    {
        RaycastHit hit;

        Physics.Raycast(transform.position + transform.rotation * snailThingies[index].position, transform.rotation * snailThingies[index].direction, out hit, snailThingies[index].range, groundMask);
        return hit;
    }

    RaycastHit GroundHitRayHit
    {
        get
        {
            //RaycastHit hit;
            //Physics.Raycast(GroundCheckPosition, GroundCheckDirection, out hit, groundCheckRange, groundMask);
            //return hit;

            RaycastHit hit;
            RaycastHit truHit;
            Physics.Raycast(transform.position + transform.rotation * snailThingies[0].position, transform.rotation * snailThingies[0].direction, out hit, snailThingies[0].range, groundMask);
            foreach (var item in snailThingies)
            {
                if (Physics.Raycast(transform.position + transform.rotation * item.position, transform.rotation * item.direction, out hit, item.range, groundMask))
                {
                    truHit = hit;
                    return truHit;
                }

            }
            return hit;
        }
    }

    bool IsTouchingGround
    {
        get
        {
            foreach (var item in snailThingies)
            {
                RaycastHit hit;
                Debug.DrawRay(transform.position + transform.rotation * item.position, transform.rotation * item.direction);
                if (Physics.Raycast(transform.position + transform.rotation * item.position, transform.rotation * item.direction, out hit, item.range, groundMask))
                {
                    Debug.Log(hit.collider.name);
                    return true;
                }
            }


            //if (Physics.Raycast(GroundCheckPosition, GroundCheckDirection, out hit, groundCheckRange, groundMask))
            //{
            //    Debug.Log(hit.collider.name);
            //    return true;
            //}
            return false;
        }
    }
    public float xAxis { get => Input.GetAxis("Horizontal"); }
    public float yAxis { get => Input.GetAxis("Vertical"); }
    public float xAxisRaw { get => Input.GetAxisRaw("Horizontal"); }
    public float yAxisRaw { get => Input.GetAxisRaw("Vertical"); }

    private void Update()
    {
        // transform.Translate(new Vector3(xAxis * movementSpeed, 0, 0), Space.Self);
        if (!IsTouchingGround)
        {
            GetComponent<Rigidbody>().useGravity = true;

        }
        else
        {
            GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(movementSpeed * xAxis, 0, 0);
            GetComponent<Rigidbody>().useGravity = false;

            if (xAxis < 0)
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(leftIndex).normal), righySpeed * Time.deltaTime));
            }
            if (xAxis > 0)
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(rightIndex).normal), righySpeed * Time.deltaTime));
            }
            else
            {
                GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(middleIndex).normal), righySpeed * Time.deltaTime));
            }

            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GroundHitRayHit.normal), righySpeed * Time.deltaTime));
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GroundHitRayHit.normal), righySpeed * Time.deltaTime);
            //   GetComponent<Rigidbody>().MovePosition(transform.position + transform.right * movementSpeed * xAxis * Time.deltaTime);

        }

        if (Input.GetButtonDown("Roll"))
        {
            GetComponent<Rigidbody>().useGravity = true;
            rollCollider.enabled = true;
            slimeCollider.enabled = false;
            GetComponent<RollScript>().enabled = true;
            this.enabled = false;
        }


    }


    private void OnDrawGizmosSelected()
    {
        foreach (var item in snailThingies)
        {
            Gizmos.DrawSphere(transform.rotation * item.position + transform.position, .2f);
            Gizmos.DrawLine(transform.position + transform.rotation * item.position, transform.position + transform.rotation * item.position + transform.rotation * item.direction * item.range);
        }

        //Gizmos.DrawSphere(transform.position + transform.rotation * groundCheckOrigin, .4f);
        //Gizmos.DrawLine(transform.position + transform.rotation * groundCheckOrigin, transform.position + transform.rotation * groundCheckOrigin + GroundCheckDirection * groundCheckRange);
    }


}
