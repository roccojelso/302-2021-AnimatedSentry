using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Camera cam;

    private CharacterController pawn;
    public float walkSpeed = 5;

    public Transform leg1;
    public Transform leg2;

    private Vector3 inputDirection = new Vector3();

    /// <summary>
    /// How fast the player is movering vertically (y-axis), in meters/second
    /// </summary>
    private float verticalVelocity = 0;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        pawn = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        WiggleLegs();

    }

    private void WiggleLegs()
    {
        float degrees = 45;
        float speed = 10;
        
        Vector3 inputDirLocal = transform.InverseTransformDirection(inputDirection);
        Vector3 axis = Vector3.Cross(inputDirLocal, Vector3.up);

        // check the alignment inputDirLocal against forward vector

        float allignment = Vector3.Dot(inputDirLocal, Vector3.forward);

        //if (alignment < 0) alignment *= -1;

        allignment = Mathf.Abs(allignment);

        degrees *= AnimMath.Lerp(0.25f, 1, allignment);

        float wave = Mathf.Sin(Time.time * speed) * degrees;

        leg1.localRotation = AnimMath.Slide(leg1.localRotation, Quaternion.AngleAxis(wave, axis), .01f);
        leg2.localRotation = AnimMath.Slide(leg2.localRotation, Quaternion.AngleAxis(-wave, axis), .01f);

    }

    private void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal"); // strafing or side-side
        float v = Input.GetAxis("Vertical"); // forward / backward

        bool isTryingToMove = (h != 0 || v != 0);
        if (isTryingToMove)
        {
            //turn to face the correct direction...
            float camYaw = cam.transform.eulerAngles.y;
            transform.rotation = AnimMath.Slide(transform.rotation, Quaternion.Euler(0, camYaw, 0), .02f);


        }

        inputDirection = transform.forward * v + transform.right * h;
        if (inputDirection.sqrMagnitude > 1) inputDirection.Normalize();

        verticalVelocity += 10 * Time.deltaTime;

        // adds later movement to vertical movement 
        Vector3 moveDelta = inputDirection * walkSpeed + verticalVelocity * Vector3.down;

        // move character
        CollisionFlags flags = pawn.Move(moveDelta * Time.deltaTime); 

        if(pawn.isGrounded)
        {
            verticalVelocity = 0;
        }
    }
}
