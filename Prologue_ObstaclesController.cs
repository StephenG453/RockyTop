using UnityEngine;
using System.Collections;

public class Prologue_ObstaclesController : MonoBehaviour 
{
	public GameObject[] obstacles = new GameObject[4];	//Array of obstacle Gameobjects that contains 4 indexies

	private bool areObstaclesPropelling;	//Bool to see if the Obstacles should be propelling away from the mountain

    private void Start () 
	{
		for (int i = 0; i < obstacles.Length; i++)	//For every obstacle in the "obstacles" array...
		{
			obstacles [i].SetActive (false);	//Deactive the gameobject on the first frame
		}

		StartCoroutine (ActivateObstacles ());	//Start this Coroutine

		StartCoroutine (DisableObstacles ());	//Start this Coroutine
	}

    private void Update () 
	{
        if (areObstaclesPropelling)	//If it is time for the obstacles to propell...
		{	
			PickAxeAttributes ();	//Call this method

			DynamiteAttributes ();	//Call this method

			MineCartAttributes ();	//Call this method

			MineTrackAttributes ();	//Call this method
		} 
        else if (areObstaclesPropelling == false)	//If the obstacles should NOT be propelling...
		{
			return;	//Do nothing
		}
	}

    private void DynamiteAttributes ()
	{
		obstacles[0].gameObject.transform.position += ((Vector3.right * 3.25f) + (Vector3.up * -1.25f) + (Vector3.forward * -3.25f));	//Translate the Dynamite along all all three axies

		obstacles[0].gameObject.transform.Rotate(5, 2, 3);	//Rotate the Dynamite along all three axies
	}

    private void PickAxeAttributes ()
	{
		obstacles [1].gameObject.transform.position += ((Vector3.right * 3.18f) + (Vector3.up * -1.2f) + (Vector3.forward * -3.25f));	//Translate the Pick Axe along all all three axies

		obstacles[1].gameObject.transform.Rotate(0, 0, 10);	//Rotate the Pick Axe along all three axies
	}
		
    private void MineCartAttributes ()
	{
		obstacles [2].gameObject.transform.position += ((Vector3.right * 2.5f) + (Vector3.up * -1) + (Vector3.forward * -3.5f));	//Translate the Mine Cart along all all three axies

		obstacles[2].gameObject.transform.Rotate(2, 2, 2);	//Rotate the Mine Cart along all three axies
	}

    private void MineTrackAttributes ()
	{
		obstacles [3].gameObject.transform.position += ((Vector3.right * 3.25f) + (Vector3.up * -1.2f) + (Vector3.forward * -3.25f));	//Translate the Mine Track along all all three axies

		obstacles[3].gameObject.transform.Rotate(0, 6, 0);	//Rotate the Mine Track along all three axies
	}

    private IEnumerator ActivateObstacles ()
	{
		yield return new WaitForSeconds (17.2f);	//After 17.2 seconds of this Coroutine being called in Start ()...

		for (int i = 0; i < obstacles.Length; i++)	//For every obstacle in the "obstacles" array...
		{
			obstacles [i].SetActive (true);	//Activate the gameobject
		}

        areObstaclesPropelling = true;	//Set bool to true - time for the obstacles to propell

		yield return null;	//End this Coroutine
	}

    private IEnumerator DisableObstacles ()
	{
		yield return new WaitForSeconds (21);	//After 21 seconds of this Coroutine being called in Start ()...

		for (int i = 0; i < obstacles.Length; i++)	//For every obstacle in the "obstacles" array...
		{
			obstacles [i].SetActive (false);	//Deactivate the gameobject
		}

        areObstaclesPropelling = false;	//Bool is false - The obstacles are not longer propelling

		StartCoroutine (PlaceObstaclesByMonster ());

		yield return null;	//End this Coroutine
	}

    private IEnumerator PlaceObstaclesByMonster ()
	{
		yield return new WaitForSeconds (12);

		for (int i = 0; i < obstacles.Length; i++) 
		{
			obstacles [i].SetActive (true);
		}

		obstacles[0].gameObject.transform.position = new Vector3 (210, 89, 180);

		obstacles [0].gameObject.transform.rotation = Quaternion.Euler (240, 0, 0);

		obstacles [0].gameObject.transform.localScale = new Vector3 (7, 7, 7);

		obstacles [1].gameObject.transform.position = new Vector3 (222, 89, 185);

		obstacles [1].gameObject.transform.rotation = Quaternion.Euler (55, 0, 0);

		obstacles [1].gameObject.transform.localScale = new Vector3 (700, 700, 700);

		obstacles [2].gameObject.transform.position = new Vector3 (230, 89, 182);

		obstacles [2].gameObject.transform.rotation = Quaternion.Euler (340, 325, 102);

		obstacles [3].gameObject.transform.position = new Vector3 (217.4f, 81.7f, 162);

		obstacles [3].gameObject.transform.rotation = Quaternion.Euler (346, 314.4f, 0);

		yield return null;
	}
}
