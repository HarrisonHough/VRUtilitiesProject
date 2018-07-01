using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: VRPointer Class
*/


[RequireComponent(typeof(VRTeleport))]
public class VRPointer : MonoBehaviour
{
    public LayerMask moveLayer;
    public LayerMask blockLayer;
    [SerializeField]
    private GameObject pointer;
    [SerializeField]
    private GameObject playerCam;
    [SerializeField]
    private float maxDistance = 10;
    private bool pointerOn; //0 = none 1 = move mode
    public Vector3 TargetPosition { get { return pointer.transform.position; } }
    public Vector3 TargetRotation { get { return pointer.transform.rotation.eulerAngles; } }

    private Vector3 targetPosition;
    private Vector3 targetRotation;
    [SerializeField]
    private GameObject aimer;
    private GameObject joyAimer;

    

    // Use this for initialization
    void Start()
    {
        InitializeVariables();
    }

    void InitializeVariables()
    {
        pointer.SetActive(false);
        joyAimer = new GameObject();
        joyAimer.transform.parent = pointer.transform;

        aimer = new GameObject();
        aimer.transform.parent = playerCam.transform;
        aimer.name = "Aimer";
        aimer.transform.localPosition = new Vector3(0, 0, maxDistance);
        if (!playerCam)
            playerCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    /// <summary>
    /// 
    /// </summary>
    public void TurnPointerOn()
    {

        //show pointer
        pointer.SetActive(true);
        //enable moveMode
        pointerOn = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void TurnPointerOff()
    {
        //disable move mode
        pointerOn = false;
        //hide pointer
        pointer.SetActive(false);


    }

    /// <summary>
    /// 
    /// </summary>
    public void CheckRayCast()
    {
        if (!pointerOn)
            return;

        RaycastHit hit;

        if (true)
        {
            Vector3 relativePos = pointer.transform.position - playerCam.transform.position;
            relativePos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            
            pointer.transform.rotation = rotation; 
        }

        if ((Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxDistance + 2f, blockLayer)))
        {
            //Debug.Log("Cant move way is blocked");
            //change colour
            pointer.SetActive(false);
            return;
        }
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, maxDistance, moveLayer))
        {
            pointer.SetActive(true);
            pointer.transform.position = hit.point;
            //print("Found an object - distance: " + hit.distance);
        }
        else if (Physics.Raycast(aimer.transform.position, Vector3.down, out hit, maxDistance, moveLayer))
        {
            pointer.SetActive(true);
            pointer.transform.position = hit.point;
            //print("Found an object - distance: " + hit.distance);
        }
        
    }

    public void SetRotation()
    {

    }

    public void SetRotationFromInput(float x, float y)
    {
        pointer.transform.eulerAngles = new Vector3(pointer.transform.localEulerAngles.x, Mathf.Atan2(x, -y) * Mathf.Rad2Deg + playerCam.transform.eulerAngles.y, pointer.transform.localEulerAngles.z);
    }
}
