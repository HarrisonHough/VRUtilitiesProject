using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 2018
* VERSION: 1.0
* SCRIPT: VR Teleport Class
*/

[RequireComponent(typeof(VRPointer))]
public class VRTeleport : MonoBehaviour {

    private VRPointer pointer;

    //COOLDOWN FUNCTIONALITY
    [SerializeField]
    private float _movementCooldown = 2f;
    private bool _isCoolingdown;
    private WaitForSeconds _waitForSeconds;

    public bool canMove = true;

    // Use this for initialization
    void Start () {

        InitializeVariables();
	}

    void InitializeVariables()
    {
        pointer = GetComponent<VRPointer>();
        _waitForSeconds = new WaitForSeconds(_movementCooldown);
    }

    /// <summary>
    /// 
    /// </summary>
    public void MoveToPosition(float playerHeight)
    {
        //if cooldown still active then skip
        if (_isCoolingdown)
        {
            Debug.Log("Not Ready To Teleport again");
            Debug.Log("Cool down still in effect");
            return;
        }

        //set new position and rotation
        transform.position = pointer.TargetPosition + (Vector3.up * playerHeight);
        transform.eulerAngles = pointer.TargetRotation;

        //Start cooldown timer.
        StartCoroutine(MovementCooldown());

    }

    /// <summary>
    /// Coroutine which toggles player movement cooldown, will re-enable movement once x-seconds have passed.
    /// </summary>
    /// <returns></returns>
    IEnumerator MovementCooldown()
    {
        _isCoolingdown = true;
        yield return _waitForSeconds;
        _isCoolingdown = false;
    }
}
