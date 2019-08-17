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

    [SerializeField] float righySpeed = 5f;
    [SerializeField] float movementSpeed;
    [SerializeField] float groundCheckRange;
    [SerializeField] Vector3 groundCheckDirection;
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
    RaycastHit GroundHitRayHit
    {
        get
        {
            RaycastHit hit;
            Physics.Raycast(GroundCheckPosition, GroundCheckDirection, out hit, groundCheckRange, groundMask);
            return hit;
        }
    }

    bool IsTouchingGround
    {
        get
        {
            RaycastHit hit;
            if (Physics.Raycast(GroundCheckPosition, GroundCheckDirection, out hit, groundCheckRange, groundMask))
            {
                Debug.Log(hit.collider.name);
                return true;
            }
            else return false;
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
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().useGravity = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GroundHitRayHit.normal), righySpeed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(transform.position + transform.right * movementSpeed * xAxis * Time.deltaTime);

        }


    }


    private void OnDrawGizmosSelected()
    {
        foreach (var item in snailThingies)
        {
            Gizmos.DrawSphere(transform.rotation * item.position + transform.position, .2f);
            Gizmos.DrawLine(transform.position + transform.rotation * item.position, transform.position + transform.rotation * item.position +transform.rotation * item.direction* item.range);
        }

        //Gizmos.DrawSphere(transform.position + transform.rotation * groundCheckOrigin, .4f);
        //Gizmos.DrawLine(transform.position + transform.rotation * groundCheckOrigin, transform.position + transform.rotation * groundCheckOrigin + GroundCheckDirection * groundCheckRange);
    }


}
