using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Image crosshair;
    public CinemachineVirtualCamera cinemachineCamera; // Reference to the Cinemachine Virtual Camera
    public GameObject weapon_loc;
    public float maxDistance = 100f;
    public Camera camera;
    [SerializeField] GameObject Player;
    public float verticalAdjustmentFactor = 50f;  // Factor to adjust crosshair based on pitch
    public float verticalOffset = 50f;            // Offset to move the crosshair up from the center
    [SerializeField] float offsetChange = 20f;
    [SerializeField] float adjustmentChange = 1000f;

    public float baseOffset = 10f; // Example base value for verticalOffset
    public float offsetScaleFactor = 0.1f; // Example scale factor for adjusting offset based on maxDistance

    public float baseAdjustmentFactor = 5f; // Example base value for verticalAdjustmentFactor
    public float adjustmentScaleFactor = 0.05f; // Example scale factor for adjusting adjustment factor based on maxDistance


    private void Start()
    {
        // Initialize maxDistance based on the weapon's range
        maxDistance = Player.GetComponent<Weapon_Handler>().weapon.range;
        
    }


    void Update()
    {
        verticalOffset = baseOffset + (maxDistance * offsetScaleFactor);
        verticalAdjustmentFactor = baseAdjustmentFactor + (maxDistance * adjustmentScaleFactor);
        Ray ray = new Ray(weapon_loc.transform.position, weapon_loc.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            MoveCrosshairBasedOnPitch();
        }
    }

    void MoveCrosshairBasedOnPitch()
    {
        Camera mainCamera = camera;

        // Get the pitch from the camera's rotation
        float pitch = mainCamera.transform.eulerAngles.x;

        // Normalize pitch to the range of -90 to 90
        if (pitch > 180)
            pitch -= 360;

        // Adjust normalization for pitch to fit into the -0.3 to 0.7 range
        pitch = Mathf.Lerp(-0.3f, 0.7f, (pitch + 90f) / 180f);

        float adjustment = pitch * verticalAdjustmentFactor;

        // Adjust the y position of the reticle based on the pitch and the static offset
        crosshair.transform.position = new Vector3(crosshair.transform.position.x,
                                                   Screen.height / 2 + verticalOffset + adjustment,
                                                   crosshair.transform.position.z);
    }

}