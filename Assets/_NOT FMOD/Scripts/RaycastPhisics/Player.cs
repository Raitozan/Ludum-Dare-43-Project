using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
	[Header("Physics Settings")]
	public float jumpHeight;
	public float timeToJumpApex;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public float moveSpeed;
	public float crouchModificator;

	bool isMoving = false;
	bool isCrouching;

	float gravity;
	float jumpVelocity;

	Vector3 velocity;
	float velocityXSmoothing;
	float sfxVelocity = 0f;

	Controller2D controller;

	FMOD.Studio.EventInstance robotMovementSFX;
	FMOD.Studio.EventInstance crouchingSFX;

	delegate void Ability();
	List<Ability> abilities = new List<Ability>();

	[HideInInspector] public int ability1;
	[HideInInspector] public int ability2;

	//DASH
	[HideInInspector] public bool canDash;
	[HideInInspector] public bool isDashing;
	[Header("Abilities")]
	public float dashmoveSpeed;
	public float dashDuration;
	public float dashAcceleration;
	[HideInInspector]
	public bool isRight = true;
	public GameObject burningPrefab;
	public float climbDuration;
	public bool canDoubleJump;

	void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		abilities.Add(Dash);
		abilities.Add(Burn);
		abilities.Add(Climb);
		abilities.Add(DoubleJump);
		ability1 = -1;
		ability2 = -1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!GameManager.instance.gamePaused)
		{
			if (controller.collisions.above || controller.collisions.below)
				velocity.y = 0;

			Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			if (Input.GetButtonDown("Jump") && controller.collisions.below)
			{
				velocity.y = jumpVelocity;
				canDoubleJump = true;
				FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_JUMP, GetComponent<Transform>().position);  // jump sound      
			}

			sfxVelocity = velocity.y;

			if (Input.GetButtonDown("Crouch"))
			{
				isCrouching = true;
				controller.Crouch();
				StartCrouchingSFX();
			}
			if (!Input.GetButton("Crouch") && isCrouching)
			{
				controller.Uncrouch(ref isCrouching);
				StopCrouchingSFX();
			}

			if (Input.GetButtonDown("Ability1") && ability1 != -1 && !isCrouching)
				abilities[ability1]();
			if (Input.GetButtonDown("Ability2") && ability2 != -1 && !isCrouching)
				abilities[ability2]();

			float targetVelocityX = input.x * moveSpeed;
			if (isCrouching)
				targetVelocityX *= crouchModificator;
			velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);

			if (controller.collisions.below)
			{
				canDash = true;
				canDoubleJump = false;
			}

            if (targetVelocityX != 0 && (!Input.GetButtonDown("Jump"))) // checking if player moving, then executing MovementSFX
            {
                if (!isMoving)
                {
                    MovementSFX();
                    isMoving = true;
					if (targetVelocityX > 0)
						isRight = true;
					else if (targetVelocityX < 0)
						isRight = false;
				}
            }

            if (targetVelocityX == 0 || Input.GetButtonDown("Jump") || sfxVelocity != 0)
            {
                MovementSFXStop();
                isMoving = false;
            }
        }
	}

    #region Abilities

    public void Dash()
	{
		if(canDash)
			StartCoroutine(dashCoroutine());
	}
	IEnumerator dashCoroutine()
	{
		float normalGravity = gravity;

		canDash = false;
		moveSpeed += dashmoveSpeed;
		accelerationTimeAirborne -= dashAcceleration;
		accelerationTimeGrounded -= dashAcceleration;
		gravity = 0;
		isDashing = true;
		yield return new WaitForSeconds(dashDuration);
		moveSpeed -= dashmoveSpeed;
		accelerationTimeAirborne += dashAcceleration;
		accelerationTimeGrounded += dashAcceleration;
		gravity = normalGravity;
		isDashing = false;
	}

	public void Burn()
	{
		float xPos = (isRight) ? 1 : -1;
		Instantiate(burningPrefab, new Vector3(transform.position.x + xPos, transform.position.y, transform.position.z), Quaternion.identity, transform);
	}

	public void Climb()
	{
		StartCoroutine(climbCoroutine());
	}
	IEnumerator climbCoroutine()
	{
		float normalAngle = controller.maxClimbAngle;
		controller.maxClimbAngle = 90;
		yield return new WaitForSeconds(climbDuration);
		controller.maxClimbAngle = normalAngle;
	}

	public void DoubleJump()
	{
		if(canDoubleJump)

	}

	public void changeAbility(int ind)
	{
		ability1 = ind;
	}

    # endregion

    #region SFX methods

    void MovementSFX() // starts movement Loop
    {
        robotMovementSFX = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.ROBOT_MOVE);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(robotMovementSFX, this.transform, GetComponent<Rigidbody>());
        robotMovementSFX.start();
    }

    void MovementSFXStop()
    {
        robotMovementSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stopping movement sound when jumping
        robotMovementSFX.release();
    }

    void StartCrouchingSFX()
    {
        crouchingSFX = FMODUnity.RuntimeManager.CreateInstance(FMODPaths.CROUCH);
        crouchingSFX.start();
    }

    void StopCrouchingSFX()
    {
        crouchingSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        crouchingSFX.release();
    }

    #endregion



}
