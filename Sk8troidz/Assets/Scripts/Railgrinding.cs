using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;
using Photon.Pun;
public class Railgrinding : MonoBehaviour
{
    bool jump;
    Vector3 input;

    public bool onRail;
    [SerializeField] float speed;
    [SerializeField] float min_speed;

    [SerializeField] float height_offset;
    float time_for_spline;
    float elapsed_time;
    [SerializeField] float lerp_speed = 10f;
    public float progress;

    [SerializeField] Rail curr_rail; //script for rail
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerMovement pm;
    [SerializeField] Trick_System ts;
    [SerializeField] HoldButton taunt_btn;
    [SerializeField] Animator animator;
    [SerializeField] GameObject crosshair;
    [SerializeField] GameObject sparks;
    [SerializeField] PhotonView pv;
    [SerializeField] Player_Health ph;

    private KeyCode jumpKey;
    private KeyCode trickKey;

    bool dir;
    public void JumpOffRail()
    {
        if (onRail)
        {
            crosshair.SetActive(true);
            ThrowOffRail();
            pm.canJump = true;
            pm.Jump();
            onRail = false;
            pm.onRail = false;
        }
    }
   

    void FixedUpdate()
    {
        if(onRail == true)
        {
            MoveAlongRail();           
        }
    }
    private void Update()
    {
        if (onRail == true)
        {
            if (Input.GetKeyDown(jumpKey))
            {
                JumpOffRail();
 
            }
        }
    }

    void MoveAlongRail()
    {
        
        if (curr_rail != null && onRail == true && ph.current_health > 0)
        {
            animator.SetFloat("animSpeedCap", 0);
            if (Input.GetKeyDown(jumpKey))
            {
                JumpOffRail();
                return;
            }
            if (taunt_btn.isDown || Input.GetKey(trickKey))
            {
                ts.Start_Rail_Trick_System();
            }
            pm.onRail = true;
            progress = elapsed_time / time_for_spline;
            if(progress < 0 || progress > 1)
            {
                ThrowOffRail();
                return;
            }

            float deltaTime = Time.deltaTime;

            if (Input.GetKey(trickKey))
            {
                deltaTime *= 2f;
            }

            float next_time_normalized;
            if(dir == true)
            {
                next_time_normalized = (elapsed_time + deltaTime) / time_for_spline;
            }
            else
            {
                next_time_normalized = (elapsed_time - deltaTime) / time_for_spline;
            }
            float3 pos, tangent, up;
            float3 next_pos_float, next_tan, next_up;
            SplineUtility.Evaluate(curr_rail.rail_spline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(curr_rail.rail_spline.Spline, next_time_normalized, out next_pos_float, out next_tan, out next_up);

            Vector3 world_pos = curr_rail.LocalToWorld(pos);
            Vector3 next_pos = curr_rail.LocalToWorld(next_pos_float);

            transform.position = world_pos + (transform.up * height_offset);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(next_pos - world_pos), lerp_speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, lerp_speed * Time.deltaTime);

            if (dir == true)
            {
                elapsed_time += deltaTime;
            }
            else
            {
                elapsed_time -= deltaTime;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rail" && !onRail && pv.IsMine)
        {
            animator.SetLayerWeight(3, 1f);
            animator.SetFloat("animSpeedCap", 0);
            onRail = true;
            curr_rail = collision.gameObject.GetComponent<Rail>();
            SetRailPosition();
            pm.onRail = true;
            rb.useGravity = false;
            speed = min_speed + Mathf.Abs(rb.linearVelocity.x + rb.linearVelocity.z);
            crosshair.SetActive(false);
            sparks.SetActive(true);
        }
    }



    void SetRailPosition()
    {
        time_for_spline = curr_rail.length / speed;
        
        Vector3 spline_point;
        float normalized_time = curr_rail.CalcTargetRailPoint(transform.position, out spline_point);
        elapsed_time = time_for_spline * normalized_time;
        float3 pos, forward, up;
        SplineUtility.Evaluate(curr_rail.rail_spline.Spline, normalized_time, out pos, out forward, out up);
        dir = curr_rail.CalcDirection(forward, transform.forward);
        transform.position = spline_point + (transform.up * height_offset);

    }
    public void ThrowOffRail()
    {
        crosshair.SetActive(true);
        sparks.SetActive(false);
        animator.SetLayerWeight(3, 0f);
        pm.onRail = false;
        onRail = false;
        curr_rail = null;
        transform.position += transform.forward * 1;
        rb.useGravity = true;
    }
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        if (pv.IsMine)
        {
            jumpKey = (KeyCode)PlayerPrefs.GetInt("JumpKey", (int)KeyCode.Space);
            trickKey = (KeyCode)PlayerPrefs.GetInt("TrickKey", (int)KeyCode.T);
        }
    }
}
