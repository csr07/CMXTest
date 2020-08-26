using UnityEngine;
using System.Collections;
using System;

public class TP_Animator : MonoBehaviour
{
	//Animation names Optimization 17/08/2020  CSR
	public String character_name;
	public GameObject character_Ragdoll;
	//


    public enum Direction
    { 
        Stationary, Forward, Backward, Left, Right,
        LeftForward, RightForward, LeftBackward, RightBackward
    }

    public enum CharacterState
    { 
        Idle, Running, WalkBackwards, StrafingLeft, StrafingRight, Jumping,
        Falling, Landing, Climbing, Sliding, Using, Dead, ActionLocked
    }

    public static TP_Animator Instance;
	
	private CharacterState lastState;
	private Transform climbPoint;
	
	public Vector3 ClimbOffset = Vector3.zero;
	public Vector3 PostClimbOffset = Vector3.zero;
	public float ClimbJumpStartTime = 0.4333f;
	public float ClimbAnchorTime = 0.6f;
	
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////		
	public Transform pelvis;	//Character pelvis joint transform, set in Inspector.
	

	//transforms de los meshes poligonales
	private Transform armaduraa;	
	
	
	private Vector3 initialPosition = Vector3.zero;
	private Quaternion initialRotation = Quaternion.identity;
	private GameObject ragdoll;
	
	private float climbTargetOffset = 0f;
	private float ClimbInitialTargetHeight = 0f;
	
    public Direction MoveDirection { get; set; }
    public CharacterState State { get; set; }
	public bool IsDead{get;set;}


    void Awake()
    {
        Instance = this;
		//pelvis = transform.Find ("grp_BONES01/bn_spA01") as Transform; // already set from inspector
		initialPosition = transform.position;
		initialRotation = transform.rotation;	
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////		
		//inicializar los meshes
		//armaduraa = transform.FindChild("armaduraa0") as Transform;
		
	
    }

    void Update()
    {
        DetermineCurrentState();
        ProcessCurrentState();

        //Debug.Log("Current Character State: " + State.ToString());
    }

    public void DetermineCurrentMoveDirection()
    {
        var forward = false;
        var backward = false;
        var left = false;
        var right = false;

        if (TP_Motor.Instance.MoveVector.z > 0)
            forward = true;
        if (TP_Motor.Instance.MoveVector.z < 0)
            backward = true;
        if (TP_Motor.Instance.MoveVector.x > 0)
            right = true;
        if (TP_Motor.Instance.MoveVector.x < 0)
            left = true;

        if (forward)
        {
            if (left)
                MoveDirection = Direction.LeftForward;
            else if (right)
                MoveDirection = Direction.RightForward;
            else
                MoveDirection = Direction.Forward;            
        }
        else if (backward)
        {
            if (left)
                MoveDirection = Direction.LeftBackward;
            else if (right)
                MoveDirection = Direction.RightBackward;
            else
                MoveDirection = Direction.Backward;
        }
        else if (left)
            MoveDirection = Direction.Left;        
        else if (right)        
            MoveDirection = Direction.Right;       
        else        
            MoveDirection = Direction.Stationary;
    }

    void DetermineCurrentState()
    {
        if (State == CharacterState.Dead)
            return;

        if (!TP_Controller.CharacterController.isGrounded)
        {
            if (State != CharacterState.Falling &&
               State != CharacterState.Jumping &&
               State != CharacterState.Landing)
            { 
                // Deber�a estar cayendo.
				Fall ();
            }
        }

        if (State != CharacterState.Falling &&
            State != CharacterState.Jumping &&
            State != CharacterState.Landing &&
            State != CharacterState.Using &&
            State != CharacterState.Climbing &&
            State != CharacterState.Sliding)
        {
            switch (MoveDirection)
            { 
                case Direction.Stationary:
                    State = CharacterState.Idle;
                    break;
                case Direction.Forward:
                    State = CharacterState.Running;
                    break;
                case Direction.Backward:
                    State = CharacterState.WalkBackwards;
                    break;
                case Direction.Left:
                    State = CharacterState.StrafingLeft;
                    break;
                case Direction.Right:
                    State = CharacterState.StrafingRight;
                    break;
                case Direction.LeftForward:
                    State = CharacterState.Running;
                    break;
                case Direction.RightForward:
                    State = CharacterState.Running;
                    break;
                case Direction.LeftBackward:
                    State = CharacterState.WalkBackwards;
                    break;
                case Direction.RightBackward:
                    State = CharacterState.WalkBackwards;
                    break;                
            }
        }
    }

    void ProcessCurrentState()
    {
        switch (State)
        { 
            case CharacterState.Idle:
				Idle ();
                break;
            case CharacterState.Running:
				Running();
                break;
            case CharacterState.WalkBackwards:
				WalkBackwards();
                break;
            case CharacterState.StrafingLeft:
				StrafingLeft();
                break;
            case CharacterState.StrafingRight:
				StrafingRight();
                break;
            case CharacterState.Jumping:
				Jumping ();
                break;
            case CharacterState.Falling:
				Falling ();
                break;
            case CharacterState.Landing:
				Landing ();
                break;
            case CharacterState.Climbing:
				Climbing ();
                break;
            case CharacterState.Sliding:
				Sliding();
                break;
            case CharacterState.Using:
				Using();
                break;
            case CharacterState.Dead:
				Dead ();
                break;
            case CharacterState.ActionLocked:
                break;
        }
    }
	
	
	#region Character State Methods
	
	void Idle()
	{
		GetComponent<Animation>().CrossFade(character_name + "_Idle");		
	}
	
	void Running()
	{
		GetComponent<Animation>().CrossFade(character_name + "_Running");		
	}
	
	void WalkBackwards()
	{
		GetComponent<Animation>().CrossFade(character_name + "_WalkBackwards");		
	}
	
	void StrafingLeft()
	{
		GetComponent<Animation>().CrossFade(character_name + "_StrafingLeft");		
	}
	
	void StrafingRight()
	{
		GetComponent<Animation>().CrossFade(character_name + "_StrafingRight");		
	}
	
	void Using()
	{
		if(!GetComponent<Animation>().isPlaying)
		{
			State = CharacterState.Idle;
			GetComponent<Animation>().CrossFade(character_name + "_Idle");
		}
	}	
	
	void Jumping()
	{
		if((!GetComponent<Animation>().isPlaying && TP_Controller.CharacterController.isGrounded) ||
			TP_Controller.CharacterController.isGrounded)
		{
			if(lastState == CharacterState.Running)
				GetComponent<Animation>().CrossFade(character_name + "_RunLand");
			else			
				GetComponent<Animation>().CrossFade(character_name + "_JumpLand");
			State = CharacterState.Landing;
		}		
		else if(!GetComponent<Animation>().IsPlaying(character_name + "_Jump"))
		{
			State = CharacterState.Falling;
			GetComponent<Animation>().CrossFade(character_name + "_Falling");
			TP_Motor.Instance.IsFalling = true;
		}
		else
		{
			State = CharacterState.Jumping;
			//determinar si esta cayendo desde muy alto
		}
	}
	
	void Falling()
	{
		if(TP_Controller.CharacterController.isGrounded)
		{
			if(lastState == CharacterState.Running)
				GetComponent<Animation>().CrossFade(character_name + "_RunLand");
			else
				GetComponent<Animation>().CrossFade(character_name + "_JumpLand");
			State = CharacterState.Landing;
		}
	}
	
	void Landing()
	{
		if(lastState == CharacterState.Running)
		{
			if(!GetComponent<Animation>().IsPlaying(character_name + "_RunLand"))	
			{
				State = CharacterState.Running;
				GetComponent<Animation>().Play (character_name + "_Running");
			}
		}
		else
		{
			if(!GetComponent<Animation>().IsPlaying(character_name + "_JumpLand"))	
			{
				State = CharacterState.Idle;
				GetComponent<Animation>().Play (character_name + "_Idle");
			}
		}
		TP_Motor.Instance.IsFalling = false;
	}
	
	void Sliding()
	{
		if(!TP_Motor.Instance.IsSliding)
		{
			State = CharacterState.Idle;
			GetComponent<Animation>().CrossFade(character_name + "_Idle");
		}
	}
	
	void Climbing()
	{
		if(GetComponent<Animation>().isPlaying)
		{
			var time = GetComponent<Animation>()[character_name + "_Climb"].time;
			if(time > ClimbJumpStartTime && time < ClimbAnchorTime)
			{
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,
					Mathf.Lerp(transform.rotation.eulerAngles.y,climbPoint.rotation.eulerAngles.y,
					(time - ClimbJumpStartTime)/(ClimbAnchorTime-ClimbJumpStartTime)),
					transform.rotation.eulerAngles.z);
				
				var climbOffset = transform.TransformDirection(ClimbOffset);
				
				transform.position = Vector3.Lerp (transform.position,
					new Vector3(climbPoint.position.x,transform.position.y,climbPoint.position.z) + climbOffset,
					(time - ClimbJumpStartTime)/(ClimbAnchorTime-ClimbJumpStartTime));
				
			}
			TP_Camera.Instance.TargetLookAt.localPosition = new Vector3(TP_Camera.Instance.TargetLookAt.localPosition.x,
																		pelvis.localPosition.y + climbTargetOffset,
																		TP_Camera.Instance.TargetLookAt.localPosition.z);
		}
		else
		{
			State = CharacterState.Idle;
			GetComponent<Animation>().Play (character_name + "_Idle");
			var postClimbOffset = transform.TransformDirection(PostClimbOffset);
			transform.position = new Vector3(pelvis.position.x, 
											 climbPoint.position.y + climbPoint.localScale.y / 2,
											 pelvis.position.z) + postClimbOffset;
			
			TP_Camera.Instance.TargetLookAt.localPosition = new Vector3(TP_Camera.Instance.TargetLookAt.localPosition.x,
																		ClimbInitialTargetHeight,
																		TP_Camera.Instance.TargetLookAt.localPosition.z);
		}
	}
	
	void Dead()
	{		
		State = CharacterState.Dead;
	}
	
	#endregion	
	
	#region Start Action Method
	
	public void Use()
	{
		State = CharacterState.Using;
		GetComponent<Animation>().CrossFade(character_name + "_Using");
	}
	
	public void Jump()
	{
		if(!TP_Controller.CharacterController.isGrounded || IsDead || State == CharacterState.Jumping)
			return;
		
		lastState = State;
		State = CharacterState.Jumping;
		GetComponent<Animation>().CrossFade (character_name + "_Jump");
	}
	
	public void Fall()
	{
		if(TP_Motor.Instance.VerticalVelocity >-5 || IsDead)
			return;
		lastState = State;
		State = CharacterState.Falling;
		
		TP_Motor.Instance.IsFalling = true;
		
		GetComponent<Animation>().CrossFade(character_name + "_Falling");
	}
	
	public void Slide()
	{
		State = CharacterState.Sliding;
		GetComponent<Animation>().CrossFade(character_name + "_Falling");
	}
	
	public void Climb()
	{
		if(!TP_Controller.CharacterController.isGrounded || IsDead || climbPoint==null)
			return;
		
		if(Mathf.Abs (climbPoint.transform.rotation.eulerAngles.y - transform.rotation.eulerAngles.y) >60)
		{
			TP_Controller.Instance.Jump();
			return;
		}	
		
		State = CharacterState.Climbing;
		GetComponent<Animation>().CrossFade(character_name + "_Climbing");


		if (TP_Controller.Instance.config_UseTPCamera)
		{
			climbTargetOffset = TP_Camera.Instance.TargetLookAt.localPosition.y - pelvis.localPosition.y;
			ClimbInitialTargetHeight = TP_Camera.Instance.TargetLookAt.localPosition.y;
		}
	}
	
	public void Die()
	{
		//inicializar todo
		IsDead = true;

		if (TP_Controller.Instance.config_UseRagdoll)
		{
			SetupRagdoll();
		}

		Dead();

	}
	
	public void Reset()
	{
		// para volver a jugar
		IsDead = false;
		transform.position = initialPosition;
		transform.rotation = initialRotation;
		State = CharacterState.Idle;
		GetComponent<Animation>().Play (character_name + "_Idle");

		if (TP_Controller.Instance.config_UseRagdoll)
		{
			ClearRagdoll();
		}

		//Reset PlayerStats
		gameObject.GetComponent<PlayerInfo>().Start();
	}
	
	#endregion
	
	public void SetClimbPoint(Transform climbPoint)
	{
		this.climbPoint = climbPoint;
		TP_Controller.Instance.ClimbEnabled = true;
	}
	
	public void ClearClimbPoint(Transform climbPoint)
	{
		if(this.climbPoint == climbPoint)
		{
			this.climbPoint = null;
			TP_Controller.Instance.ClimbEnabled = false;
		}
	}
	
	void SetupRagdoll()
	{		
		if(ragdoll == null)
		{
			//Hide Character
			var comps = this.GetComponentsInChildren<SkinnedMeshRenderer>();
			foreach (var comp in comps)
			{
				comp.enabled = false;
			}


			//Disable Character Controller to avoid Collision
			this.GetComponent<CharacterController>().enabled = false;

			ragdoll = GameObject.Instantiate(character_Ragdoll,
											 transform.position,
											 transform.rotation) as GameObject;

			var ragdollPelvis = ragdoll.GetComponent<TP_Ragdoll>().pelvis;
			MatchChildrenTransform(pelvis, ragdollPelvis);

			if (TP_Controller.Instance.config_UseTPCamera)
			{
				TP_Camera.Instance.TargetLookAt = ragdollPelvis;
			}
		}				
	}
	
	void ClearRagdoll()
	{
		//Destroy ragdoll
		if(ragdoll != null)
		{
			GameObject.Destroy (ragdoll);
			ragdoll = null;

			var comps = this.GetComponentsInChildren<SkinnedMeshRenderer>();

			foreach (var comp in comps)
			{
				comp.enabled = true;
			}
			this.GetComponent<CharacterController>().enabled = true;

			if (TP_Controller.Instance.config_UseTPCamera)
			{
				TP_Camera.Instance.TargetLookAt = transform.Find("targetLookAt");
			}
		}
	}
	
	void MatchChildrenTransform(Transform source, Transform target)
	{
		//moverse atravez de la jerarquia para coincidir las rotaciones de los joints
		
		if(source.childCount > 0)
		{
			foreach(Transform sourceTransform in source.transform)
			{
				Transform targetTransform = target.Find (sourceTransform.name);
				
				if(targetTransform!=null)
				{
					MatchChildrenTransform(sourceTransform,targetTransform);
					Debug.Log("CSR - Matching Transforms!!!");
					targetTransform.localPosition = sourceTransform.localPosition;
					targetTransform.localRotation = sourceTransform.localRotation;
				}
			}
		}		
	}
}