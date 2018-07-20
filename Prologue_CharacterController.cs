using UnityEngine;
using System.Collections;

[System.Serializable]
public class Prologue_Characters
{
	public GameObject mrAdv;	//Mr. Adventure gameobject

	public GameObject zoey;	//Zoey McBride gameobject

	public GameObject barry;	//Barry Green gameobject
}
[System.Serializable]
public class Prologue_CharacterAnimators
{
	public Animator mrAdvAnimator;	//Animator for Mr. Adventure

	public Animator zoeyAnimator;	//Animator for Zoey

	public Animator barryAnimator;	//Animator for Barry
}

public class Prologue_CharacterController : MonoBehaviour 
{
	public Prologue_Characters _characters;	//Reference to the Prologue_Characters class

	public Prologue_CharacterAnimators _animators;	//Reference to the Prologue_CharacterAnimators class

	private bool timeToRotateAfterPoint_MrAdv;	//Bool to represent it's time for Mr. Adv to rotate after he points at the Shadow Monster 

	private bool timeToWalk_MrAdv;	//Bool to represent it's time for Mr. Adv. to walk

	private bool timeToCrawl_ALL;	//Bool to represent it's time for all the characters to crawl up the mountain

	private bool timeToWalk_ZoeyAndBarry;	//Bool to represent it's time for Zoey and Barry to walk

	private bool timeToTurn_Barry;	//Bool to represent it's time for Barry to turn

    private const float walkAndCrawlSpeed_MrAdvAndZoey = 0.005f;	//The speed at which Mr. Adv. and Zoey will walk and crawl up the mountain

    private const float walkAndCrawlSpeed_Barry = 0.006f;	//The speed at which Barry will walk and crawl up the mountain

    private const int turnLeftSpeed_MrAdv = 20;	//The speed at which Mr. Adv. will turn his body to the left

    private const int turnSpeed_Barry = 100;	//The speed at which Barry will turn his body

    private void Start () 
	{
		_animators.mrAdvAnimator.enabled = false;	//Disable Mr. Adventure's animator on the first frame

		StartCoroutine (EnableMrAdventureAnimator ());	//Start the Coroutine

		StartCoroutine (MakeMrAdventureLookBehind ());	//Start the Coroutine

		StartCoroutine (MakeMrAdventurePointAndZoeyReactRight ());	//Start the Coroutine

		StartCoroutine (MakeMrAdventureWalkAndBarryTurn ());	//Start the Coroutine

		StartCoroutine (MakeAllCharactersCrawl ());	//Start the Coroutine

		StartCoroutine (MakeZoeyAndBarryNervous ());	//Start this Coroutine

		StartCoroutine (MakeZoeyAndBarryWalk ());	//Start this Coroutine
	}
	
    private void Update ()
	{
		if (timeToRotateAfterPoint_MrAdv)	//If it is time for Mr. Adv. to rotate left after he points at the mountain...
		{
			Vector3 vector3 = new Vector3 (-1, 0, 1);	//New vector3 variable

			Quaternion qTo = Quaternion.LookRotation (vector3);	//New quaternion variable - Mr. Adv. "looks" at the spot we set with the "vector3" variable

			StartCoroutine (TurnMrAdvAfterPoint (_characters.mrAdv.gameObject.transform, qTo, turnSpeed_Barry));	//Start the Coroutine
		}
		if (timeToWalk_MrAdv)	//If it time for Mr. Adv. to walk...
		{
			MrAdventureWalkingAttributes ();	//Call this method
		}
		if (timeToWalk_ZoeyAndBarry) 	//If it is time for Zoey to walk...
		{
			ZoeyWalkingAttributes ();	//Call this method

			BarryWalkingAttributes ();	//Call this method
		}
		if (timeToTurn_Barry)	//If it is time for Barry to turn...
		{
			Vector3 vector3 = new Vector3 (-10, 0, 20);	//New vector3 variable

			Quaternion qTo = Quaternion.LookRotation (vector3);	//New quaternion variable - Barry "looks" at the spot we set with the "vector3" variable

			StartCoroutine (RotateBarry (_characters.barry.gameObject.transform, qTo, turnSpeed_Barry));	//Start this Coroutine
		}
		if (timeToCrawl_ALL) 
		{
			_characters.mrAdv.gameObject.transform.position += new Vector3 (-1, 3.5f, 1) * walkAndCrawlSpeed_MrAdvAndZoey;	//Controls the translation of Mr. Adventure climbing up the mountain

			_characters.zoey.gameObject.transform.position += new Vector3 (-1, 3, 1) * walkAndCrawlSpeed_MrAdvAndZoey;	//Controls the translation of Zoey climbing up the mountain

			_characters.zoey.gameObject.transform.rotation = Quaternion.Euler (340, 300, 0);	//Rotation of Zoey while she is climbing up the mountain

			_characters.barry.gameObject.transform.position += new Vector3 (-1, 1.75f, 1) * walkAndCrawlSpeed_Barry;	//Controls the translation of Barry crawling the mountain

			MrAdventureWalkingAttributes ();	//Call this method

			ZoeyWalkingAttributes ();	//Call this method

			BarryWalkingAttributes ();	//Call this method
		}
	}

    private IEnumerator EnableMrAdventureAnimator ()
	{
		yield return new WaitForSeconds (8);	//After 8 seconds of this Coroutine being called in Start ()...	

		_animators.mrAdvAnimator.enabled = true;	//Enable the animator attached to Mr. Adv.

		yield return null;	//End the Coroutine
	}

    private IEnumerator MakeZoeyAndBarryNervous ()
	{
		yield return new WaitForSeconds (17);	//After 17 seconds of this Coroutine being called in Start ()...

		_animators.zoeyAnimator.SetBool ("isReacting_Left", true);	//Set transition bool to true - Zoey is now reacting by looking Left

		_animators.barryAnimator.SetBool ("isNervous", true);	//Set transition bool to true - Barry is now "Nervous"

		yield return null;	//End this Coroutine
	}

    private IEnumerator MakeMrAdventureLookBehind ()
	{
		yield return new WaitForSeconds (18.5f);	//After 18.5 seconds of this Coroutine being called in Start ()...

		_animators.mrAdvAnimator.SetBool ("isLooking", true);	//Set transition bool to true - Mr. Adventure is now "looking"

		yield return null;	//End the Coroutine
	}

    private IEnumerator MakeMrAdventurePointAndZoeyReactRight ()
	{
		yield return new WaitForSeconds (20.5f);	//After 20.5 seconds of this Coroutine being called in Start ()...

		timeToRotateAfterPoint_MrAdv = true;	//Bool is now true - time for Mr. Adv. to slightly rotate left after pointing in the Update () function...

		_animators.mrAdvAnimator.SetBool ("isPointing", true);	//Set transition bool to true - Barry is now "pointing" at the mountain

		_animators.zoeyAnimator.SetBool ("isReacting_Right", true);	//Set transition bool to true - Zoey is now reacting by looking Right 

		yield return null;	//End the Coroutine
	}

    private IEnumerator TurnMrAdvAfterPoint(Transform mrAdventure, Quaternion mrAdventureRotation, float mrAdventureTurnSpeed) 
	{
		float elapsed = 0;	//Variable that plays as a constant to remain at 0

		while(elapsed < mrAdventureTurnSpeed)	//This will always be true since 0 is less than the mrAdventureTurnSpeed variable
		{
			elapsed += Time.deltaTime;	//"elapsed" variable increases every second

			_characters.mrAdv.gameObject.transform.rotation = Quaternion.Slerp(mrAdventure.rotation, mrAdventureRotation, elapsed / mrAdventureTurnSpeed);	//Rotate the transform of Mr. Adv. via Quaternion.Slerp

			yield return null;	//End the Coroutine
		}
	}

    private IEnumerator RotateBarry(Transform barryGreen, Quaternion barryRotation, float barryTurnSpeed) 
	{
		float elapsed = 0;	//Variable that plays as a constant to remain at 0

		while(elapsed < barryTurnSpeed)	//This will always be true since 0 is less than the barryTurnSpeed variable
		{
			elapsed += Time.deltaTime;	//Increases every second

			_characters.barry.gameObject.transform.rotation = Quaternion.Slerp(barryGreen.rotation, barryRotation, elapsed / barryTurnSpeed);	//Rotate the transform of Barry via Quaternion.Slerp

			yield return null;	//End the Coroutine
		}
	}

	private IEnumerator MakeMrAdventureWalkAndBarryTurn ()
	{
		yield return new WaitForSeconds (22);	//After 22 seconds of this Coroutine being called in Start ()...

		timeToRotateAfterPoint_MrAdv = false;	//Bool is now false - no longer time for Mr. Adv. to turn after pointing the mountain

		timeToWalk_MrAdv = true;	//Bool is true now - time for Mr. Adv. to walk

		_animators.mrAdvAnimator.SetBool ("isWalking", true);	//Set transition bool to true - Mr. Adv. is now "walking"

		_animators.barryAnimator.SetBool ("isTurning", true);	//Set transition bool to true - Barry is now "turning"

		timeToTurn_Barry = true;	//Bool is true now - time for Barry to turn in the Update() function

		yield return null;	//End the Coroutine
	}

    private IEnumerator MakeZoeyAndBarryWalk ()
	{
		yield return new WaitForSeconds (23);	//After 23 seconds of this Coroutine being called in Start ()...

		timeToWalk_ZoeyAndBarry = true;	//Bool is now true - time for Zoey and Barry to walk in the Update () function

		_animators.zoeyAnimator.SetBool ("isWalking", true);	//Set transition bool to true - Zoey is now "walking"

		_animators.barryAnimator.SetBool ("isWalking", true);	//Set transition bool to true - Barry is now "walking"

		timeToTurn_Barry = false;	//Bool is now false - no longer time for Barry to turn

		yield return null;	//End this Coroutine
	}

    private IEnumerator MakeAllCharactersCrawl()
	{
		yield return new WaitForSeconds (27);	//After 27 seconds of this Coroutine being called in Start ()...

		timeToWalk_MrAdv = false;	//Bool is now false - no longer time for Mr. Adv. to walk

		timeToWalk_ZoeyAndBarry = false;

		timeToCrawl_ALL = true;	//Bool is true now - time for Mr. Adv. to crawl up the mountain

		_animators.mrAdvAnimator.SetBool ("isCrawling", true);	//Set transition to true - Mr. Adv. is now "crawling"

		_animators.zoeyAnimator.SetBool ("isCrawling", true);	//Set transition bool to true - Zoey is now "crawling"

		_animators.barryAnimator.SetBool ("isCrawling", true);	//Set transition bool to true - Barry is now "crawling"

		yield return null;	//End the Coroutine
	}

    private void MrAdventureWalkingAttributes ()
	{
		_characters.mrAdv.gameObject.transform.position += new Vector3 (-10, 0, 10) * walkAndCrawlSpeed_MrAdvAndZoey; 	//Controls the translation of Mr. Adv. walking	
	}

    private void ZoeyWalkingAttributes ()
	{
		_characters.zoey.gameObject.transform.position += new Vector3 (-10, 0, 7.5f) * walkAndCrawlSpeed_MrAdvAndZoey;	//Controls the translation of Zoey walking
	}

    private void BarryWalkingAttributes ()
	{
		_characters.barry.gameObject.transform.position += new Vector3 (-10, 0, 20) * (walkAndCrawlSpeed_Barry / 2);	//Controls the translation of Barry walking
	} 
}
