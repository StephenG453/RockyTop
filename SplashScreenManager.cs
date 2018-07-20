using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour 
{
	public GameObject odysseyText;

	public Animator screenFaderAnimator;

    private static void Awake ()
    {
        Application.targetFrameRate = 60;
    }

    private void Start ()
	{
		screenFaderAnimator.GetComponent<Animator> ();

		odysseyText.gameObject.SetActive (false);

		StartCoroutine (EnableOdysseyText ());

		StartCoroutine (FadeOutScene ());
	}

    private IEnumerator EnableOdysseyText ()
	{
		yield return new WaitForSeconds (3);

		odysseyText.gameObject.SetActive (true);
	}

    private IEnumerator FadeOutScene ()
	{
		yield return new WaitForSeconds (4.5f);

		screenFaderAnimator.SetBool ("isFadingOut", true);

        if (PlayerPrefs.GetInt("PLAYERPREFS_BETAWELCOMEPLAYED") == 0)
        {
            StartCoroutine(LoadBETAWelcome());  //BETA OVERRIDE
        }
        else if (PlayerPrefs.GetInt("PLAYERPREFS_BETAWELCOMEPLAYED") == 1)
        {
            StartCoroutine (LoadMainMenu ()); //BETA OVERRIDE
        }
	}

    private IEnumerator LoadMainMenu ()   //BETA OVERRIDE
	{
		yield return new WaitForSeconds (1);

		SceneManager.LoadScene (2);
	}

    private IEnumerator LoadBETAWelcome ()  //BETA OVERRIDE
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene (1);
    }
}
