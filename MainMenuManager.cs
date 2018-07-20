/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, March 2016	 
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

public class MainMenuManager : MonoBehaviour 
{
	public GameObject beginningScreenFader;	//Beginning screenFader that fades out whenever the scene is loaded

	public GameObject endingScreenFader; 	//Ending screenFader that fades in whenever the scene is leaving

	public AudioSource[] mainMenu_Audio = new AudioSource[2];	//Array of Audiosources that consists of 2 indices - [0] = wind [1] = tap sound

	public Animator[] mainMenu_Animators = new Animator [7];	//Array of Animators that consists of 7 indices

	private int invisibilityCountFromStore;	//int to represent the amount of Invisibilities bought from the Store

	private int finalInvisibilityCount;	//int to represent the final Invisibility amount //Sent to Endless_UIPowerups 

	private int sluggishCountFromStore;	//int to represent the amount of Sluggish bought from the Store

	private int finalSluggishCount;	//int to represent the final Sluggish amount //Sent to Endless_UIPowerups

	private bool isLeavingScene;	//Bool to represent if the scene is being departed - Wind SFX will fade out when true

	private bool tapToPlayClicked = false;	//Has "Tap to Play" been clicked? - Debuts at false

	private bool optionsClicked = false;	//Has "Options" been clicked? - Debuts at false

	private bool storeClicked = false;	//Has "Store" been clicked? - Debuts at false

	private bool aboutClicked = false;	//Has "About" been clicked? - Debuts at false

	private bool characterSelectClicked = false;	//Has "Character Select" been clicked? - Debuts at false

	private bool gemMarketClicked = false;	//Has "Gem Market" been clicked? - Debuts at false

	private void Awake ()
	{
		Time.timeScale = 1;	//Time scale is set to 1

		QualitySettings.shadowDistance = 30;	//Rendering distance from the Main Camera that shadows will appear

		//---- TEMP. OVERRIDE FOR ALFONSO BELOW

		PlayerPrefs.SetInt ("PLAYERPREFS_ToonModeUnlock", 1);

		PlayerPrefs.SetInt ("PLAYERPREFS_NighttimeUnlock", 1);

		PlayerPrefs.SetInt ("PLAYERPREFS_AngelicaUnlock", 1);

		PlayerPrefs.SetInt ("PLAYERPREFS_AkioUnlock", 1);

		// ---------- Changed to 1 (June 8, 2018) in order to unlock everything while communicating with my recruiters
	}

	private void Start () 
	{
		isLeavingScene = false;	//Bool is false - Scene is not being departed on the first frame

		StartCoroutine (StartPulsatingTapToPlay ());	//Call this Coroutine

		StartCoroutine (DisableBeginningScreenFader ());	//Call this Coroutine

		if (PlayerPrefs.GetInt ("PLAYERPREFS_SoundFX") == 0)	//If SoundFX is selected to be On via Options menu 
		{
			mainMenu_Audio = GetComponents<AudioSource> ();	//Attach the Audiosource component to mainMenu_Audio

			mainMenu_Audio [0].volume = 0;	//Set volume of the Wind

			mainMenu_Audio [0].Play ();	//Play the Wind SX

			mainMenu_Audio [0].loop = true;	//The Wind will loop

			mainMenu_Audio [0].pitch = 1;	//Set pitch of the Wind

			mainMenu_Audio [1].volume = 1;	//Set volume of the Tap sound

			mainMenu_Audio [1].pitch = 1.15f;	//Set pitch of the Tap sound
		} 
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_SoundFX") == 1)	//If SoundFX is selected to be OFF via Options menu
		{
			for (int a = 0; a < mainMenu_Audio.Length; a++) //For every index inside this array...
			{
				mainMenu_Audio [a].volume = 0;	//Mute the volume so it cannot be heard
			}
		}

		if (PlayerPrefs.GetInt ("PLAYERPREFS_GoingToMainMenuFromStore") == 1 && PlayerPrefs.GetInt ("PLAYERPREFS_InvisibilityBool") == 1 && PlayerPrefs.GetInt("PLAYERPREFS_SluggishBool") == 0)	//If user is coming FROM the store AND you bought at least one Invisibility but no Sluggish...
		{	
			InvisiblityBought ();	//Call this method
		} 
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_GoingToMainMenuFromStore") == 1 && PlayerPrefs.GetInt ("PLAYERPREFS_SluggishBool") == 1 && PlayerPrefs.GetInt("PLAYERPREFS_InvisibilityBool") == 0)	//If user is coming FROM the store AND bought at least one Sluggish but no Invisibility...
		{
			SluggishBought ();	//Call this method
		} 
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_GoingToMainMenuFromStore") == 1 && PlayerPrefs.GetInt ("PLAYERPREFS_InvisibilityBool") == 1 && PlayerPrefs.GetInt ("PLAYERPREFS_SluggishBool") == 1)	//If user is coming FROM the store AND bought at least one Invisiblity AND Sluggish...
		{
			InvisiblityBought ();	//Call this method

			SluggishBought ();	//Call this method
		}
		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0)	//If the Language is set to English...
		{
			mainMenu_Animators [4].SetBool ("isEnglish", true);	//Set animation state to true - English is selected

			mainMenu_Animators [5].SetBool ("isEnglish", true);	//Set animation state to true - English is selected

			mainMenu_Animators [4].SetBool ("isSpanish", false);	//Set animation state to false - Spanish is NOT selected

			mainMenu_Animators [5].SetBool ("isSpanish", false);	//Set animation state to false - Spanish is NOT selected
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1) //If the Language is set to Spanish...
		{
			mainMenu_Animators [4].SetBool ("isEnglish", false);	//Set animation state to false - English is NOT selected

			mainMenu_Animators [5].SetBool ("isEnglish", false);	//Set animation state to false - English is NOT selected

			mainMenu_Animators [4].SetBool ("isSpanish", true);	//Set animation state to true - Spanish is selected

			mainMenu_Animators [5].SetBool ("isSpanish", true);	//Set animation state to true - Spanish is selected
		}
	}
	
	private void Update () 
	{
		if (mainMenu_Audio[0].volume < 0.25f && isLeavingScene == false)	//If the Wind SFX volume is less than 0.25f AND the scene is NOT being departed...
		{
			mainMenu_Audio [0].volume += 0.005f;	//Fade in the Wind SFX
		}

		if (isLeavingScene) //If the scene IS being departed...
		{
			mainMenu_Audio [0].volume -= 0.005f;	//Fade out the Wind SFX
		}

		if (tapToPlayClicked)	//If Tap To Play is clicked...
		{
			RockyTopDisappear ();	//Call this method

			GemMarketDisappear ();	//Call this method

			CharacterSelectDisappear ();	//Call this method

			AboutDisappear ();	//Call this method

			StoreDisappear ();	//Call this method

			OptionsDisappear ();	//Call this method
		} 

		else if (gemMarketClicked)	//Set up Gem Market in the TransitionScript
		{
			RockyTopDisappear ();	//Call this method

			TapToPlayDisappear ();	//Call this method

			CharacterSelectDisappear ();	//Call this method

			AboutDisappear ();	//Call this method

			StoreDisappear ();	//Call this method

			OptionsDisappear ();	//Call this method
		}	
		
		else if (characterSelectClicked)	//If Character Select is clicked...
		{
			RockyTopDisappear ();	//Call this method

			TapToPlayDisappear ();	//Call this method

			GemMarketDisappear ();	//Call this method

			AboutDisappear ();	//Call this method

			StoreDisappear ();	//Call this method

			OptionsDisappear ();	//Call this method
		} 
		else if (aboutClicked)	//If About is clicked...
		{
			RockyTopDisappear ();	//Call this method

			TapToPlayDisappear ();	//Call this method

			GemMarketDisappear ();	//Call this method

			CharacterSelectDisappear ();	//Call this method

			StoreDisappear ();	//Call this method

			OptionsDisappear ();	//Call this method
		} 
		else if (storeClicked)	//If Store is clicked...
		{
			RockyTopDisappear ();	//Call this method

			TapToPlayDisappear ();	//Call this method

			GemMarketDisappear ();	//Call this method

			CharacterSelectDisappear ();	//Call this method

			AboutDisappear ();	//Call this method

			OptionsDisappear ();	//Call this method
		} 
		else if (optionsClicked)
		{
			RockyTopDisappear ();	//Call this method

			TapToPlayDisappear ();	//Call this method

			GemMarketDisappear ();	//Call this method

			CharacterSelectDisappear ();	//Call this method

			AboutDisappear ();	//Call this method

			StoreDisappear ();	//Call this method
		}
	}

	private void InvisiblityBought ()
	{
		invisibilityCountFromStore = PlayerPrefs.GetInt ("PLAYERPREFS_InvisibilityCountInStore");	//This int variable is equal to the amount of Invisibilites bought while at the store

		finalInvisibilityCount = PlayerPrefs.GetInt ("PLAYERPREFS_FinalInvisibilityCount", 0);	//"Get" the Playerpref BEFORE we "set" it again so it is able to be incremented

		finalInvisibilityCount += invisibilityCountFromStore;	//finalInvisibilityCount will increment by the amount of Invisibilites bought at the store during one scene visit

		PlayerPrefs.SetInt ("PLAYERPREFS_FinalInvisibilityCount", finalInvisibilityCount);	//Set this Playerpref to int value of finalInvisibilityCount

 		//PlayerPrefs.SetInt ("PLAYERPREFS_GemScore_GameOver", (StoreManager.Instance.storeGemCount) - (StoreManager.Instance.invisibilityJustBought) + (StoreManager.Instance.invisibilityJustBought));	//This Playerpref makes sure the storeGemCount in StoreManager 
																																																			//is accurate to how many Invisibility powerups were bought	
		//The above line of code doesn't make any sense - June 7, 2017
	}
	private void SluggishBought ()
	{
		sluggishCountFromStore = PlayerPrefs.GetInt ("PLAYERPREFS_SluggishCountInStore");	//This int variable is equal to the amount of Sluggish bought while at the store

		finalSluggishCount = PlayerPrefs.GetInt ("PLAYERPREFS_FinalSluggishCount", 0);	//"Get" the Playerpref BEFORE we "set" it again so it able to be incremented

		finalSluggishCount += sluggishCountFromStore;	//finalSluggishCount will increment by the amount of Sluggish bought at the store during one scene visit

		PlayerPrefs.SetInt ("PLAYERPREFS_FinalSluggishCount", finalSluggishCount);	//Set this Playerpref to int value of finalSluggishCount

		//PlayerPrefs.SetInt ("PLAYERPREFS_GemScore_GameOver", (StoreManager.Instance.storeGemCount) - (StoreManager.Instance.sluggishJustBought) + (StoreManager.Instance.sluggishJustBought));		//This Playerpref makes sure the storeGemCount in StoreManager
																																																	//is accurate to how many Sluggish powerups were bought
		//The above line of code doesn't make any sense - June 7, 2017
	}

	public void TapToPlayButtonClicked()
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		tapToPlayClicked = true;	//Boolean is set to true - "Tap to Play" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the CoRoutine 
	}

	public void OptionsButtonClicked ()	
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		optionsClicked = true;	//Boolean is set to true - "Options" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the CoRoutine
	}

	public void StoreButtonClicked ()
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		storeClicked = true;	//Boolean is set to true - "Store" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the CoRoutine
	}

	public void AboutButtonClicked()
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		aboutClicked = true;	//Boolean is set to true - "About" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the CoRoutine
	}

	public void CharacterSelectButtonClicked ()	
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		characterSelectClicked = true;	//Boolean is set to true - "Character Select" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the CoRoutine
	}

	public void GemMarketButtonClicked ()	
	{
		endingScreenFader.SetActive (true);	//Activate EndingScreenFader

		gemMarketClicked = true;	//Boolean is set to true - "Gem Market" was clicked

		PlayTapSound ();	//Call this function

		IsLeavingScene_True ();	//Call this function

		StartCoroutine (LoadTheScene ());	//Start the Coroutine
	}

	private void IsLeavingScene_True ()
	{
		isLeavingScene = true;	//Set bool to true - Scene is being departed - Used in Update ()
	}

	private void RockyTopDisappear ()
	{
		mainMenu_Animators [0].SetBool ("isLeavingScene_English", true);	//Set transition bool in Animator to true - Rocky Top title text translates up off the screen
	}

	private void TapToPlayDisappear() 
	{
		mainMenu_Animators [1].SetBool ("isLeavingScene_English", true);	//Set transition bool in Animator to true - Tap To Play text fades off the screen
	}

	private void GemMarketDisappear ()
	{
		mainMenu_Animators [2].SetBool ("isLeavingScene_English", true);	//Set transition bool to true - Gem Market text fades off the screen
	}

	private void CharacterSelectDisappear ()
	{
		mainMenu_Animators [3].SetBool ("isLeavingScene_English", true);	//Set transition bool to true - Character Select text translates right off the screen
	}

	private void AboutDisappear ()
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0) 
		{
			mainMenu_Animators [4].SetBool ("isLeavingScene_English", true);	//Set transition bool to true - About text translates right off the screen
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1) 
		{
			mainMenu_Animators [4].SetBool ("isLeavingScene_Spanish", true);	//Set transition bool to true - About text translates right off the screen
		}
	}

	private void StoreDisappear ()
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0) 
		{
			mainMenu_Animators [5].SetBool ("isLeavingScene_English", true);	//Set transition bool to true - Store text translates left off the screen
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1) 
		{
			mainMenu_Animators [5].SetBool ("isLeavingScene_Spanish", true);	//Set transition bool to true - Store text translates left off the screen
		}
	}

	private void OptionsDisappear ()
	{
		mainMenu_Animators [6].SetBool ("isLeavingScene_English", true);	//Set transition bool to true - Options text translates left off the screen
	}

	private void PlayTapSound ()
	{
		mainMenu_Audio [1].Play ();	//Play the Tap sound
	}

	private IEnumerator StartPulsatingTapToPlay ()
	{
		yield return new WaitForSeconds (0.5f);	//After 0.5 seconds of this Coroutine being called...

		mainMenu_Animators [1].SetBool ("shouldBePulsating", true);	//Set transition bool in Animator to true - Tap To Play starts pulsating
	}

	private IEnumerator DisableBeginningScreenFader ()
	{
		yield return new WaitForSeconds (1.0f);	//After 1 second of this Coroutine being called...

		beginningScreenFader.SetActive (false);	//Beginning screenFader is deactivated and is no longer obstructing touch input
	}

	private IEnumerator LoadTheScene ()
	{
		yield return new WaitForSeconds (1);	//After 1 second of this Coroutine being called...

		if (tapToPlayClicked && PlayerPrefs.GetInt ("PLAYERPREFS_ProloguePlayed") == 0)	//If "Tap to Play" was clicked AND the Prologue has NEVER been played before...
		{
			//SceneManager.LoadScene (2);	//Load the Prologue

            SceneManager.LoadScene(3);  //BETA OVERRIDE 
		} 
		else if (tapToPlayClicked && PlayerPrefs.GetInt ("PLAYERPREFS_ProloguePlayed") == 1)	//If "Tap to Play" was clicked AND the Prologue HAS been played before...
		{
			//SceneManager.LoadScene (4);	//Load the Endless scene

            SceneManager.LoadScene(5);  //BETA OVERRIDE
		} 
		else if (optionsClicked)	//If "Options" was clicked...
		{
			//SceneManager.LoadScene (5);	//Load the Options menu

            SceneManager.LoadScene(6);  //BETA OVERRIDE
		} 
		else if (storeClicked)	//If "Store" was clicked...
		{
			//SceneManager.LoadScene (6);	//Load the Store menu

            SceneManager.LoadScene(7);  //BETA OVERRIDE
		} 
		else if (aboutClicked)	//If "About" was clicked...
		{
			//SceneManager.LoadScene (7);	//Load the About menu

            SceneManager.LoadScene(8);  //BETA OVERRIDE
		} 
		else if (characterSelectClicked)	//If "Character Select" was clicked...
		{
			//SceneManager.LoadScene (8);	//Load the Character Select menu

            SceneManager.LoadScene(9);  //BETA OVERRIDE
		} 
		else if (gemMarketClicked)	////If "Gem Market" was clicked...
		{
			//SceneManager.LoadScene (9);	//Load the Gem Market

            SceneManager.LoadScene(10); //BETA OVERRIDE
		}
	}
}
/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, March 2016	 
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