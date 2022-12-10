using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        targetRot = transform.eulerAngles.z;
    }
    Vector3 input;
    Vector3 cam_Forward;
    Vector3 diff;
    [SerializeField] float maxSpeed;
    [SerializeField] Camera playerCam;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speedMultiplier;
    [SerializeField] float currSpeed;
    [SerializeField] float sensitivity = 1f;
    float desiredRotation;
    Vector3 lastPos = Vector3.zero;
    Vector3 newRotation;
    float cineMachinePitch;
    float cineMachineYaw;
    float targetRot;
    public GameObject CinemachineTarget;

    [SerializeField] Animator animator;

    private void FixedUpdate()
    {

      
        Move();
      
      




    }
    private void Update()
    {
       // RotateWCamera();
        Jump();
       
    }
    private void LateUpdate()
    {

        RotateWCamera();
        CameraRotation();



    }
    void Move()
    {
        input.x = Input.GetAxis("Horizontal");
        input.z = Input.GetAxis("Vertical");
        currSpeed = (transform.position - lastPos).magnitude;
        lastPos = transform.position;
        if (currSpeed < maxSpeed)
        {

            rb.AddRelativeForce(input * speedMultiplier * Time.deltaTime);
           
        }
        if (currSpeed < 0.1)
        {
            animator.SetFloat("animSpeedCap", 0f);
        }
        else
        {
            animator.SetFloat("animSpeedCap", 1f);
        }
        
    }
    void Jump()
    {
        //TODO Add jumping

    }
    void RotateWCamera()
    {
        Vector3 newRotation = new Vector3(transform.eulerAngles.x, playerCam.gameObject.transform.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(newRotation);
    }
    void CameraRotation()
    {
        //IF DEVICE IS ON MOBILE REMEMBER TO MULTIPLY BY TIME.DELTATIME
        cineMachineYaw += Input.GetAxis("Mouse X")*sensitivity; 
        cineMachinePitch += (-1)*Input.GetAxis("Mouse Y")*sensitivity ;
       


        cineMachineYaw = ClampAngle(cineMachineYaw, float.MinValue, float.MaxValue);
        cineMachinePitch = ClampAngle(cineMachinePitch, -30, 70);

        CinemachineTarget.transform.rotation = Quaternion.Euler(cineMachinePitch, cineMachineYaw, 0.0f);

    }
    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
