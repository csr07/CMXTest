using UnityEngine;
using System;
using System.Collections;

public class TP_Controller : MonoBehaviour {
	
	public static CharacterController CharacterController;
    public static TP_Controller Instance;	
	public bool ClimbEnabled {get;set;}

	public bool config_ManualPlayer = true;
	public bool config_UseTPCamera = true;
	public bool config_UseRagdoll = true;
	public bool isInSafeArea;
	public float hInput;
	public float vInput;
	public float randomSide = 1.0f;
	
    // Awake se llama al crearse la clase
	void Awake()
    {
        CharacterController = GetComponent("CharacterController") as CharacterController;
        Instance = this;
		//nueva linea al crear la camara...

		if (config_UseTPCamera)
		{
			TP_Camera.UseExistingOrCreateNewMainCamera();
		}
    }
	
	// Update se llama una vez por frame
	void Update () {
		if (Camera.main == null)
            return;
		
		TP_Motor.Instance.ResetMotor();
		
		if(!TP_Animator.Instance.IsDead &&
			TP_Animator.Instance.State != TP_Animator.CharacterState.Using &&
			(TP_Animator.Instance.State != TP_Animator.CharacterState.Landing ||
			TP_Animator.Instance.GetComponent<Animation>().IsPlaying(TP_Animator.Instance.character_name + "_RunLand")) &&
			TP_Animator.Instance.State != TP_Animator.CharacterState.Climbing)
		{
			if (config_ManualPlayer)
			{
				GetAxisInput();
			}
			//else  //AIPlayer
			//{ 
			//	vInput = 1.0f; //go forward
			//}

			GetLocomotionInput();
			HandleActionInput();
		}
		else if(TP_Animator.Instance.IsDead)
		{
			if(Input.anyKeyDown)
				TP_Animator.Instance.Reset();
		}        

        TP_Motor.Instance.UpdateMotor();
	}

	void GetAxisInput()
	{
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");
	}
	
	void GetLocomotionInput()
    {		
        var deadZone = 0.1f;

        if (vInput > deadZone || vInput < -deadZone)
            TP_Motor.Instance.MoveVector += new Vector3(0, 0, vInput);

		if (hInput > deadZone || hInput < -deadZone)
		{
			if(config_UseTPCamera)
				TP_Motor.Instance.MoveVector += new Vector3(hInput, 0, 0);
			else
				TP_Motor.Instance.RotateVector = new Vector3(hInput, 0, 0);
		}
        TP_Animator.Instance.DetermineCurrentMoveDirection();
    }

    void HandleActionInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
			if (ClimbEnabled)
				Climb();
			else
			{
				Jump();				
			}
        }
		
		if (Input.GetKeyDown(KeyCode.E))
        {
            Use();
        }
		
		if (Input.GetKeyDown(KeyCode.F1))
        {
            Die();
        }
    }

    public void Jump()
    {		
        TP_Motor.Instance.Jump();
		TP_Animator.Instance.Jump();
    }
	
	public void Use()
	{
		TP_Animator.Instance.Use();
	}
	
	public void Climb()
	{
		TP_Animator.Instance.Climb();
	}
	
	public void Die()
	{
		TP_Animator.Instance.Die ();		
	}
}
