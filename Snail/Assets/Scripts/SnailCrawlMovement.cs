using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailCrawlMovement : MonoBehaviour
{
    bool wasRolling = true;
    [System.Serializable]
    class SnailThingy
    {
        public float range;
        public Vector3 position;
        public float degrees;
        public float avLenght;

        public Vector3 direction
        {
            get
            {
                float x = Mathf.Sin(degrees);
                float y = Mathf.Cos(degrees);
                return new Vector3(x, y, 0).normalized;
            }
        }
        //  public Vector3 direction;

        //public float
    }

    private void OnEnable()
    {
        snailSprite.SetActive(true);
        wasRolling = true;
        
    }
    private void OnDisable()
    {
        snailSprite.SetActive(false);
    }

    [SerializeField] GameObject snailSprite;
    [SerializeField] SnailThingy[] snailThingies = new SnailThingy[] { new SnailThingy() };
    [SerializeField] Collider rollCollider;
    [SerializeField] Collider slimeCollider;
    [SerializeField] float righySpeed = 5f;
    [SerializeField] float movementSpeed;
    //[SerializeField] float groundCheckRange;
    //[SerializeField] Vector3 groundCheckDirection;

    //[SerializeField] int leftIndex, rightIndex, middleIndex;

    //Vector3 GroundCheckDirection
    //{
    //    get => transform.rotation * groundCheckDirection;
    //}
    //[SerializeField] Vector3 groundCheckOrigin;
    [SerializeField] LayerMask groundMask;




    //Vector3 GroundCheckPosition
    //{
    //    get => transform.position + transform.rotation * groundCheckOrigin;
    //}

    public Vector3 GetAverageRayNormal()
    {
        List<Vector3> vector3s = new List<Vector3>();

        foreach (var item in snailThingies)
        {
            RaycastHit hit;
            // Debug.DrawLine(transform.position + transform.rotation * item.position, transform.position + transform.rotation * item.position + item.direction * item.range * 10);
            //Debug.DrawRay(transform.position + transform.rotation * item.position, transform.rotation * item.direction);
            if (Physics.Raycast(transform.position + transform.rotation * item.position, transform.rotation * item.direction, out hit, item.avLenght, groundMask))
            {
                vector3s.Add(hit.normal);
            }
        }
        Vector3 allVectors = new Vector3();
        foreach (var item in vector3s)
        {
            allVectors += item;
        }
        return allVectors / vector3s.Count;

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
                Debug.DrawRay(transform.position + transform.rotation * item.position, transform.rotation * item.direction * item.range);
                if (Physics.Raycast(transform.position + transform.rotation * item.position, transform.rotation * item.direction, out hit, item.range, groundMask))
                {
                    //     Debug.Log(hit.collider.name);
                    return true;
                }
            }
            return false;

            //if (Physics.Raycast(GroundCheckPosition, GroundCheckDirection, out hit, groundCheckRange, groundMask))
            //{
            //    Debug.Log(hit.collider.name);
            //    return true;
            //}

        }
    }
    public float xAxis { get => Input.GetAxis("Horizontal"); }
    public float yAxis { get => Input.GetAxis("Vertical"); }
    public float xAxisRaw { get => Input.GetAxisRaw("Horizontal"); }
    public float yAxisRaw { get => Input.GetAxisRaw("Vertical"); }
    bool facingLeft = false;

    private void Start()
    {
        rollCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Roll"))
        {
            GetComponent<Rigidbody>().useGravity = true;
            rollCollider.enabled = true;
            slimeCollider.enabled = false;
            GetComponent<RollScript>().enabled = true;
            this.enabled = false;
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        }
    }

    private void FixedUpdate()
    {


        // transform.Translate(new Vector3(xAxis * movementSpeed, 0, 0), Space.Self);
        if (!IsTouchingGround)
        {
            GetComponent<Rigidbody>().useGravity = true;

        }
        else if (this.enabled)
        {
            GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(movementSpeed * xAxis, -1f, 0);
            GetComponent<Rigidbody>().useGravity = false;

            if (xAxis < 0)
            {
                // snailSprite.GetComponent<SpriteRenderer>().flipX = true;
                snailSprite.transform.localRotation =Quaternion.Euler(0, 180, 0);
            }
            else if (xAxis > 0)
            {
            //    snailSprite.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                snailSprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
                // snailSprite.GetComponent<SpriteRenderer>().flipX = false;
            }

            //if (xAxis < 0 && GetGroundRay(leftIndex).collider != null)
            //{
            //    GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(leftIndex).normal), righySpeed * Time.deltaTime));
            //}
            //if (xAxis > 0 && GetGroundRay(rightIndex).collider != null)
            //{
            //    GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(rightIndex).normal), righySpeed * Time.deltaTime));
            //}
            //else
            //{
            //    GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetGroundRay(middleIndex).normal), righySpeed * Time.deltaTime));
            //}

            GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GetAverageRayNormal()), righySpeed * Time.deltaTime));
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector3.up, GroundHitRayHit.normal), righySpeed * Time.deltaTime);
            //   GetComponent<Rigidbody>().MovePosition(transform.position + transform.right * movementSpeed * xAxis * Time.deltaTime);

        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        if (wasRolling)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);
            GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(0, -1, 0);
            wasRolling = false;
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
