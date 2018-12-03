using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	bool isJumping;

	float gravity;
	float jumpVelocity;

	[HideInInspector]
	public Vector3 velocity;
	float velocityXSmoothing;
	float sfxVelocity = 0f;

	Controller2D controller;

	FMOD.Studio.EventInstance robotMovementSFX;
	FMOD.Studio.EventInstance crouchingSFX;

	delegate void Ability();
	List<Ability> abilities = new List<Ability>();

	[Header("Abilities")]
	public int ability1 = -1;
	public int ability2 = -1;
	public float dashmoveSpeed;
	public float dashDuration;
	public float dashAcceleration;
	[HideInInspector]
	public bool canDash;
	[HideInInspector]
	public bool isDashing;
	[HideInInspector]
	public bool isRight = true;
	public GameObject burningPrefab;
	[HideInInspector]
	public bool isBurning;
	public float climbDuration;
	[HideInInspector]
	public bool canDoubleJump;
	public Animator animator;

	public int energy;

	void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

		abilities.Add(Dash);
		abilities.Add(Burn);
		abilities.Add(Climb);
		abilities.Add(DoubleJump);
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
				isJumping = true;
			//	FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_JUMP, GetComponent<Transform>().position);  // jump sound      
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
			if (energy <= 0)
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
				isJumping = false;
			}
          
            if (targetVelocityX != 0 && (!Input.GetButtonDown("Jump"))) // checking if player moving, then executing MovementSFX
            {
                if (!isMoving)
                {
                 //   MovementSFX();
                    isMoving = true;
				}
            }

            if (targetVelocityX == 0 || Input.GetButtonDown("Jump") || sfxVelocity != 0)
            {
             //   MovementSFXStop();
                isMoving = false;
            }
        
			if (targetVelocityX > 0)
				isRight = true;
			else if (targetVelocityX < 0)
				isRight = false;
			UpdateAnimationParameters();
        }
	}

    #region Abilities

    public void Dash()
	{
        if (canDash)
        {
            StartCoroutine(dashCoroutine());
            FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_DASH, GetComponent<Transform>().position);
        }
     }

	IEnumerator dashCoroutine()
	{
		float normalGravity = gravity;

		canDash = false;
		moveSpeed += dashmoveSpeed;
		accelerationTimeAirborne -= dashAcceleration;
		accelerationTimeGrounded -= dashAcceleration;
		gravity = 0;
		energy -= 5;
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
		float xPos = (isRight) ? 0.75f : -0.75f;
		Burning b = Instantiate(burningPrefab, new Vector3(transform.position.x + xPos, transform.position.y+0.2f, transform.position.z), Quaternion.identity, transform).GetComponent<Burning>();
		b.player = this;
		energy -= 10;
		isBurning = true;
        BurnSFX();
    }

	public void Climb()
	{
		StartCoroutine(climbCoroutine());
	}
	IEnumerator climbCoroutine()
	{
		float normalAngle = controller.maxClimbAngle;
		controller.maxClimbAngle = 90;
		energy -= 15;
		yield return new WaitForSeconds(climbDuration);
		controller.maxClimbAngle = normalAngle;
	}

	public void DoubleJump()
	{
		if (canDoubleJump)
		{
			velocity.y = jumpVelocity;
			energy -= 5;
            DoubleJumpSFX();
            canDoubleJump = false;
		}
	}

	public void changeAbility(int ind)
	{
		ability1 = ind;
	}

    # endregion

    #region SFX methods


    public void RunSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_MOVE, GetComponent<Transform>().position);
    }

    public void JumpSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_JUMP, GetComponent<Transform>().position);
    }

    public void BurnSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_BURN, GetComponent<Transform>().position);
    }

    public void DoubleJumpSFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_DJ, GetComponent<Transform>().position);
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

	#region animations

	void UpdateAnimationParameters()
	{
		animator.SetBool("isMoving", isMoving);
		animator.SetBool("isCrouching", isCrouching);
		animator.SetBool("isJumping", isJumping);
		animator.SetBool("isBurning", isBurning);
		animator.SetBool("isDashing", isDashing);
		if (isRight)
			GetComponent<SpriteRenderer>().flipX = false;
		else
			GetComponent<SpriteRenderer>().flipX = true;
	}

	#endregion

}
