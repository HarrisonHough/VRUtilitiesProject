using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayer : MonoBehaviour {

    private VRPointer pointer;
    private VRTeleport teleporter;

    private float playerHeight = 1.913f;
	// Use this for initialization
	void Start () {
        InitializeVariables();
	}

    void InitializeVariables()
    {
        pointer = GetComponent<VRPointer>();
        pointer.TurnPointerOn();

        teleporter = GetComponent<VRTeleport>();
        
    }
	
	// Update is called once per frame
	void Update () {
        pointer.CheckRayCast();
        CheckInput();

    }


    void CheckInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            teleporter.MoveToPosition(playerHeight);
        }
    }
}
