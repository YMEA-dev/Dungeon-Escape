using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationStateController : MonoBehaviour
{
    Animator animator;
	float velocityX = 0.0f, velocityZ = 0.0f;
	public float acceleration = 2.0f, deceleration = 2.0f;
	public float maximumWalkVelocity = 0.5f, maximumRunVelocity = 2.0f; 

	//Refactoring code
	int VelocityXHash, VelocityZHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //Refactor
        VelocityZHash = Animator.StringToHash("VelocityZ");
        VelocityXHash = Animator.StringToHash("VelocityX"); 
    }

    // Update is called once per frame
    void Update()
    {
        //Get player input
		bool forwardPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);		//Forward or Backward
		bool leftPressed = Input.GetKey(KeyCode.A);
		bool rightPressed = Input.GetKey(KeyCode.D);
		bool runPressed = Input.GetKey(KeyCode.LeftShift);

		//set current maximum velocity 
		float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

		ChangeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
		LockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

		animator.SetFloat(VelocityZHash, velocityZ);
		animator.SetFloat(VelocityXHash, velocityX);
    }

	void ChangeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
	{
		//Move the player forward, left or right
		if (forwardPressed && velocityZ < currentMaxVelocity)
		{
			velocityZ += Time.deltaTime * acceleration;
		}
		if (leftPressed && velocityX > -currentMaxVelocity)
			velocityX -= Time.deltaTime * acceleration;
    	if (rightPressed && velocityX < currentMaxVelocity)
			velocityX += Time.deltaTime * acceleration;

		//Decelerate the movement and stops moving 
		if (!forwardPressed && velocityZ > 0.0f)
			velocityZ -= Time.deltaTime * deceleration;

		if (!leftPressed && velocityX < 0.0f)
			velocityX += Time.deltaTime * deceleration;
		if (!rightPressed && velocityX > 0.0f)
			velocityX -= Time.deltaTime * deceleration;
	}

	void LockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity)
	{
		if (!forwardPressed && velocityZ < 0.0f)
			velocityZ = 0.0f;
		
		//Reset velocityX
		if (!leftPressed && !rightPressed && velocityX != 0.0f && velocityX > -0.05f && velocityX < 0.05f)
			velocityX = 0.0f;

		//Lock forward
		if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
			velocityZ = currentMaxVelocity;
		//Decelerates to maximum walk velocity
		else if (forwardPressed && velocityZ > currentMaxVelocity)
		{
			velocityZ -= Time.deltaTime * deceleration;	
			//Round to currentMaxVelocity
			if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
				velocityZ = currentMaxVelocity;
		}
		//Round to currentMaxVelocity
		else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
			velocityZ = currentMaxVelocity;

		//Lock left
		if (leftPressed && runPressed && velocityZ > currentMaxVelocity)
			velocityX = -currentMaxVelocity;
		//Decelerates to maximum walk velocity
		else if (leftPressed && velocityX < -currentMaxVelocity)
		{
			velocityZ += Time.deltaTime * deceleration;	
			//Round to currentMaxVelocity
			if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
				velocityX = -currentMaxVelocity;
		}
		//Round to currentMaxVelocity
		else if (leftPressed && velocityX < -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
			velocityX = -currentMaxVelocity;

		//Lock right
		if (rightPressed && runPressed && velocityX > currentMaxVelocity)
			velocityX = currentMaxVelocity;
		//Decelerates to maximum walk velocity
		else if (rightPressed && velocityX > currentMaxVelocity)
		{
			velocityX -= Time.deltaTime * deceleration;	
			//Round to currentMaxVelocity
			if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
				velocityX = currentMaxVelocity;
		}
		//Round to currentMaxVelocity
		else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
			velocityX = currentMaxVelocity;
	}
}
