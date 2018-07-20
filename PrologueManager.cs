/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, November 2016	 
 *     ---------------
 *         ^     ^    
 *        ^       ^
 *       ^         ^
 *      ^  Odyssey  ^
 * 		^   Group   ^
 *      ^           ^
 *       ^         ^
 *        ^       ^
 *     --- ^ ^ ^ ^ ---
 * */
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Prologue_GameObjects
{
	public GameObject clouds;	//CloudsToy Manager gameobject that is set in the Inspector

	public GameObject shadowMonster_Climbing;	//Climbing Shadow Monster gameobject that is attached in the Inspector

	public GameObject shadowMonster_Dynamite;	//Throwing Dynamite Shadow Monster gameobjec that is attached in the Inspector

	public GameObject skipPrologueButton;	//"Skip Prologue" button found below the beginning text

	public GameObject skipPrologue_EndingScreenFader;	//Ending ScreenFader that appears if the "Skip Prologue" button is clicked
}

[System.Serializable]
public class Prologue_Animators
{
	public Animator screenFader_Controller;	//Animator for the screenFader

	public Animator storyTextController_Beginning;	//Animator for the story text found at the beginning of the Prologue

	public Animator storyTextController_Ending;	//Animator for the story text found at the ending of the Prologue

	public Animator skipPrologueButtonController;	//Animator for the "Skip Prologue" button found at the beginning of the Prologue

	public Animator monsterAnimator_Dynamite;	//Animator for the Shadow Monster gameobject that throws the Dynamite - Different than the Climbing Shadow Monster!
}

public class PrologueManager : MonoBehaviour 
{
	public Prologue_GameObjects _gameobjects;	//Reference to the Prologue_GameObjects class

	public Prologue_Animators _animators;	//Reference to the Prologue_Animators class

	public AudioSource[] prologue_Audio = new AudioSource[4];	//Array of Audiosources that consists of 4 indices - [0] = crickets, [1] = wolf howl, [2] = explosion, [3] = tap SFX 

	public Camera mainCamera;	//Main Camera that is attached in the Inspector

	private bool isLeavingScene;	//Bool to represent if the scene is being left (Either by Skip Prologue or Conclusion)

	private bool isMonsterThrowing;	//Boolean - is the monster throwing the dynamite?

    private float monsterTranslateTimer = 1.75f;	//How LONG of time span (in seconds) the monster will translate while he is throwing the dynamite

	private void Awake ()
	{
		QualitySettings.shadowDistance = 100;	//Set this shadow rendering distance for this scene
	}

	private void Start () 
	{
		PlayerPrefs.SetInt ("PLAYERPREFS_ProloguePlayed", 1);	//Set this Playerpref to equal 1 - This is because the Prologue is playing

		_gameobjects.clouds.gameObject.SetActive (false);	//Disable the clouds on the first frame

		_gameobjects.shadowMonster_Climbing.gameObject.transform.position = new Vector3 (194, 85.7f, 245);	//Set the position of the Monster gameobject that climbs

		_gameobjects.shadowMonster_Climbing.gameObject.transform.rotation = Quaternion.Euler (0, 128.75f, 0);	//Set the rotation of the Monster gameobject that climbs

		_gameobjects.shadowMonster_Climbing.gameObject.SetActive (false);	//Deactivate the Shadow Monster on the first frame

		_gameobjects.shadowMonster_Dynamite.gameObject.SetActive (false);	//Disable the Monster gameobject that throws the dynamite on the first frame

		_gameobjects.skipPrologue_EndingScreenFader.gameObject.SetActive (false);	//Disable the screenFader that is enabled if the "Skip Prologue" button is clicked

		isLeavingScene = false;	//Bool is false

		isMonsterThrowing = false;	//Bool is false - the monster does not throw the dynamite on the first frame

		prologue_Audio = GetComponents<AudioSource> ();	//Attach the Audiosource component

		for (int a = 0; a < prologue_Audio.Length; a++)	//For every index inside this array...
		{
			if (PlayerPrefs.GetInt ("PLAYERPREFS_SoundFX") == 0) 
			{
				if (a == 0) //If the index is 0... - Crickets
				{
					prologue_Audio [a].Play ();	//Play it

					prologue_Audio [a].volume = 0;	//Set volume

					prologue_Audio [a].pitch = 1;	//Set pitch
				}
				else if (a == 1) //If the index is 1... - Wolf Howl
				{
					prologue_Audio [a].volume = 0.1f;	//Set volume

					prologue_Audio [a].pitch = 1;	//Set pitch
				}
				else if (a == 2) //If the index is 2... - Explosion
				{
					prologue_Audio [a].volume = 0.6f;	//Set volume

					prologue_Audio [a].pitch = 1;	//Set pitch
				}
				else if (a == 3) //If the index is 3... - Tap SFX
				{
					prologue_Audio [a].volume = 1;	//Set volume

					prologue_Audio [a].pitch = 1.15f;	//Set pitch	
				}	
			}
			else if (PlayerPrefs.GetInt ("PLAYERPREFS_SoundFX") == 1) 
			{
				if (a == 0) //If the index is 0... - Crickets
				{
					prologue_Audio [a].volume = 0;	//Set volume
				}
				else if (a == 1) //If the index is 1... - Wolf Howl
				{
					prologue_Audio [a].volume = 0;	//Set volume
				}
				else if (a == 2) //If the index is 2... - Explosion
				{
					prologue_Audio [a].volume = 0;	//Set volume
				}
				else if (a == 3) //If the index is 3... - Tap SFX
				{
					prologue_Audio [a].volume = 0;	//Set volume
				}	
			}
		}

		StartCoroutine (BeginningUI_FadeOut ());	//Start this Coroutine

		StartCoroutine (DeactiveSkipPrologueButton ());	//Start this Coroutine

		StartCoroutine (EnableClouds ());	//Start this Coroutine

		StartCoroutine (PlayHowl ());	//Start this Coroutine

		StartCoroutine (MakeMonsterClimb ());	//Start this Coroutine

		StartCoroutine (PlayExplosion ());	//Start this Coroutine

		StartCoroutine (ScreenFader_FadeIn ());	//Start this Coroutine

		StartCoroutine (MoveCameraCoordinates ());	//Start this Coroutine

		StartCoroutine (FadeInEndingText ());	//Start this Coroutine

		StartCoroutine (FadeOutEndingTextAndScreenFader ());	//Start this Coroutine
	}

	private void Update ()
	{
		float speed = 0.2f;	//How fast the monster will translate while he is throwing the dynamite

		if (isMonsterThrowing)	//If the monster is throwing the dynamite...
		{
			monsterTranslateTimer -= Time.deltaTime;	//Value inside monsterTranslateTimer will countdown by 1 second, every second

			if (monsterTranslateTimer < 1.75f && monsterTranslateTimer > 0)	//If the timer is LESS than 1.75 seconds AND GREATER than 0...
			{
				_gameobjects.shadowMonster_Dynamite.gameObject.transform.position += new Vector3 (0, -0.6f, 0) * speed;	//Move the position of the monster gameobject by this vector3 value mulitplied by speed
			}
		}

		if (prologue_Audio [0].volume < 0.2f && isLeavingScene == false)	//If the volume is less than 0.2f and the Skip Prologue Button has NOT been clicked... 
		{
			prologue_Audio [0].volume += 0.005f;	//Fade in the crickets
		}

		if (isLeavingScene)	//If the Skip Prologue Button is clicked... 
		{
			FadeOutCrickets ();	//Call this method
		}
	}

	public void SkipPrologueButtonClicked ()	//Attached to the "Skip Prologue" button via a Button
	{
		TransitionToTutorialOrAbout ();	//Call this method
			
		_gameobjects.skipPrologue_EndingScreenFader.gameObject.SetActive (true);	//Enable the Ending screenFader that fades in if the "Skip Prologue" button is clicked

		prologue_Audio [3].Play ();	//Play the Tap sound effect	

		isLeavingScene = true;	//Bool is true - Skip Prologue Button was clicked
	}

	private void TransitionToTutorialOrAbout ()
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_PrologueOrTutorialPlayedFromAbout") == 0)	//If the Prologue is loaded because it is the user's first time playing...
		{
			StartCoroutine (ProceedToTutorial ());	//Start this Coroutine - Go to the Tutorial scene before the Endless scene
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_PrologueOrTutorialPlayedFromAbout") == 1) 	//If the Prologue is loaded from the About scene...
		{
			StartCoroutine (ProceedToAbout ());	//Start this Coroutine - Go back to the About scene 
		}
	}

	private void FadeOutCrickets ()
	{
		prologue_Audio [0].volume -= 0.005f;	//Fade out the Crickets SFX
	}

	private IEnumerator BeginningUI_FadeOut()
	{
		yield return new WaitForSeconds(7);	//After 7 seconds of this Coroutine being called in Start ()...

		_animators.screenFader_Controller.SetBool ("imageIsFadingOut", true);	//Set this transition state to true - the screenFader image is fading out of view

		_animators.storyTextController_Beginning.SetBool ("beginningTextIsFading", true);	//Set this transition state to true - the beginning text is fading out of view

		_animators.skipPrologueButtonController.SetBool ("prologueButtonIsFading", true);	//Set this transition state to true - the "Skip Prologue" button is fading out of view

		yield return null;	//End this Coroutine
	}

	private IEnumerator DeactiveSkipPrologueButton ()
	{
		yield return new WaitForSeconds (10);	//After 10 seconds of this Coroutine being called in Start ()...

		_gameobjects.skipPrologueButton.gameObject.SetActive (false);	//Disable the "Skip Prologue" button - It can no longer be pressed
	}

	private IEnumerator EnableClouds ()
	{
		yield return new WaitForSeconds (7);	//After 7 seconds of this Coroutine being called in Start ()...

		_gameobjects.clouds.SetActive (true);	//Activate the clouds - They are now visible

		yield return null;	//End the Coroutine
	}

	private IEnumerator PlayHowl ()
	{
		yield return new WaitForSeconds (11);	//After 11 seconds of this Coroutine being called in Start ()...

		prologue_Audio[1].Play ();	//Play the second index in the "sounds" array - Wolf Howl Audiosource

		yield return null;	//End the Coroutine
	}

	private IEnumerator MakeMonsterClimb ()
	{
		yield return new WaitForSeconds (13);	//After 13 seconds of this Coroutine being called on Start ()...

		_gameobjects.shadowMonster_Climbing.SetActive (true);	//Activate the Shadow Monster - Animator will automatically play and the monster will climb

		yield return null;	//End the Coroutine
	}

	private IEnumerator PlayExplosion ()
	{
		yield return new WaitForSeconds (17);	//After 17 seconds of this Coroutine being called in Start ()...

		prologue_Audio[2].Play ();	//Play the third index in the "sounds" array - Explosion Audiosource

		yield return null;	//End the Coroutine
	}

	private IEnumerator ScreenFader_FadeIn ()
	{
		yield return new WaitForSeconds (25);	//After 25 seconds of this Coroutine being called in Start ()...

		_animators.screenFader_Controller.SetBool ("imageIsFadingIn", true);	//Set this transition state to true - the ScreenFader image is fading into view to prepare for the 2nd sequence

		yield return null;	//End this Coroutine
	}

	private IEnumerator MoveCameraCoordinates ()
	{
		yield return new WaitForSeconds (28);	//Preparing for the 2nd sequence - After 28 seconds of this Coroutine being called in Start ()... 

		mainCamera.transform.position = new Vector3 (207, 157, 188);	//Move the main camera to its new position

		mainCamera.transform.rotation = Quaternion.Euler (58, 124, 0);	//Align the main camera to its new rotation

		mainCamera.fieldOfView = 61.7f;	//Change the field of view for the main camera

		yield return null;	//End this Coroutine
	}

	private IEnumerator FadeInEndingText ()
	{
		yield return new WaitForSeconds (28);	//After 28 seconds of this Coroutine being called in Start ()...

		_animators.storyTextController_Ending.gameObject.SetActive (true);	//Activate the gameobject of the Ending Story text that kicks off the 2nd sequence

		yield return null;	//End this Coroutine
	}

	private IEnumerator FadeOutEndingTextAndScreenFader ()
	{
		yield return new WaitForSeconds (33);	//After 33 seconds of this Coroutine being called in Start ()...

		_animators.storyTextController_Ending.SetBool ("endingTextIsFadingOut", true);	//Set this transiton state to true - The ending text that kicked off the 2nd sequence is fading out of view

		_animators.screenFader_Controller.SetBool ("imageIsFadingIn", false);	//Set this transition state to false - The screenFader is not fading into view

		_animators.screenFader_Controller.SetBool ("imageIsFadingOut_Again", true);	//Set this transition state to true - The screenFader is currently fading out of view

		_gameobjects.shadowMonster_Dynamite.transform.position = new Vector3 (212.5f, 93.5f, 187);	//Set this position of the monster that throws the dynamite

		_gameobjects.shadowMonster_Dynamite.transform.rotation = Quaternion.Euler (0, 138, 0);	//Set this rotation of the monster that throws the dynamite

		QualitySettings.shadowDistance = 800;	//Change the shadow distance to be pretty far - This is for the shadow of the dynamite that is thrown down the mountain

		StartCoroutine (ActivateMonster_Dynamite ());	//Start this Coroutine

		yield return null;	//End this Coroutine
	}

	private IEnumerator ActivateMonster_Dynamite ()
	{
		_gameobjects.shadowMonster_Dynamite.gameObject.SetActive (true);	//Activate the Monster that throws the dynamite

		StartCoroutine (MakeMonsterPickUpDynamite ());	//Start this Coroutine

		yield return null;	//End this Coroutine
	}

	private IEnumerator MakeMonsterPickUpDynamite ()
	{
		yield return new WaitForSeconds (2);	//After 2 seconds of this Coroutine being called in the ActivateMonster_Dynamite Coroutine

		_animators.screenFader_Controller.SetBool ("imageIsFadingOut_Again", false);	//Set this transition state to false - The image is no longer fading out again

		_animators.monsterAnimator_Dynamite.SetBool ("isPickingUpDynamite", true);	//Set this transition state to true - The monster is now picking up the dynamite

		StartCoroutine (MakeMonsterTranslateDown ());	//Start this Coroutine

		yield return null;	//End this Coroutine
	}

	private IEnumerator MakeMonsterTranslateDown ()
	{
		yield return new WaitForSeconds (1.5f);	//After 1.5 seconds of this Coroutine being called in the MakeMonsterPickUpDynamite Coroutine

		isMonsterThrowing = true;	//Bool is true - The monster is now throwing the Dynamite

		StartCoroutine (ScreenFader_FadeIn_Conclusion ());	//Start this Coroutine

		yield return null;	//End this Coroutine
	}

	private IEnumerator ScreenFader_FadeIn_Conclusion ()
	{
		yield return new WaitForSeconds (7);	//After 7 seconds of this Coroutine being called in the MakeMonsterTranslateDown Coroutine

		_gameobjects.skipPrologue_EndingScreenFader.gameObject.SetActive (true);	//Screenfader gameobject that is the closest UI element to the player is activated - Screen fades out

		isLeavingScene = true;	//Bool is true - Scene is being left - Crickets will fade out

		TransitionToTutorialOrAbout ();	//Call this method
	}

	private IEnumerator ProceedToTutorial ()
	{
		yield return new WaitForSeconds (2);	//After 2 seconds of this Coroutine being called in the TransitionToTutorialOrAbout method

		//SceneManager.LoadScene (3);	//Load the Tutorial scene

        SceneManager.LoadScene(4);  //BETA OVERRRIDE

		yield return null;	//End the Coroutine
	}

	private IEnumerator ProceedToAbout()
	{
		yield return new WaitForSeconds (2);	//After 2 seconds of this Coroutine being called in the TransitionToTutorialOrAbout method

		//SceneManager.LoadScene (7);	//Load the About scene

        SceneManager.LoadScene(8);  //BETA OVERRIDE

		yield return null;	//End the Coroutine
	}
}
/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, November 2016	 
 *     ---------------
 *         ^     ^    
 *        ^       ^
 *       ^         ^
 *      ^  Odyssey  ^
 * 		^   Group   ^
 *      ^           ^
 *       ^         ^
 *        ^       ^
 *     --- ^ ^ ^ ^ ---
 * */