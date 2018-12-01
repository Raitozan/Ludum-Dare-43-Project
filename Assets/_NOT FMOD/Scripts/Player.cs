using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
public class Player : MonoBehaviour
{
	public float jumpHeight;
	public float timeToJumpApex;
	public float accelerationTimeAirborne;
	public float accelerationTimeGrounded;
	public float moveSpeed;

    private bool isMoving = false;

    float gravity;
	float jumpVelocity;

	Vector3 velocity;
	float velocityXSmoothing;
    float sfxVelocity = 0f;

    Controller2D controller;

    private FMOD.Studio.EventInstance robotMovementSFX;

    void Start ()
	{
		controller = GetComponent<Controller2D>();

		gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (controller.collisions.above || controller.collisions.below)
			velocity.y = 0;

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump") && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            FMODUnity.RuntimeManager.PlayOneShot(FMODPaths.ROBOT_JUMP, GetComponent<Transform>().position);  // jump sound      
        }

        sfxVelocity = velocity.y;

        float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);

        if (targetVelocityX != 0 && (!Input.GetButtonDown("Jump"))) // checking if player moving, then executing MovementSFX
        {
            if (!isMoving)
            {
                MovementSFX();
                isMoving = true;
            }
        }

        if (targetVelocityX == 0 || Input.GetButtonDown("Jump") || sfxVelocity != 0)
        {
            MovementSFXStop();
            isMoving = false;
        }

    }


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

    #endregion



}
