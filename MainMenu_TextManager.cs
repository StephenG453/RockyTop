/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, May 2017	 
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
using UnityEngine.UI;
using System.Globalization;

public class MainMenu_TextManager : MonoBehaviour 
{
	public Text[] mainMenu_Text = new Text[7];	//Array of Text element that consists of 7 indices

	private void Start () 
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0)	//If the Language is set to English...
		{
			for (int a = 0; a < mainMenu_Text.Length; a++)	//For all the Text inside this array...
			{
				if (a == 0)	//If the index is 0 - "Rocky Top" title text
				{
					mainMenu_Text [a].font = (Font)Resources.Load ("Fonts/Landslide");	//Make title text font Landslide

                    mainMenu_Text [a].fontSize = 180;

                    mainMenu_Text [a].text = "Rocky" + "\n" + "Top";
				} 
				else if (a != 0)	//If the index is NOT 0 - Everything else
				{
					mainMenu_Text [a].font = (Font)Resources.Load ("Fonts/JFRocSol");	//Make title text font JFRocSolid

                    if (a == 1) //If the index is 1...
                    {
                        mainMenu_Text [a].text = " Tap " + "\n" + "To " + "\n" + "Play!";   //Set text

                        mainMenu_Text[a].fontSize = 105;    //Set font size
                    }
                    else if (a >= 2 && a <= 5)  //If the index is greater than or equal to 2 AND less than or equal to 5...
                    {
                        mainMenu_Text[a].fontSize = 75; //Set font size

                        if (a == 2)    //If 2...
                        {
                            mainMenu_Text[a].text = "Options";  //Set text
                        }
                        else if (a == 3)    //If 3...
                        {
                            mainMenu_Text [a].text = "Store";   //Set text
                        }
                        else if (a == 4)    //If 4...
                        {
                            mainMenu_Text [a].text = "About";   //Set text
                        }
                        else if (a == 5)    //If 5...
                        {
                            mainMenu_Text [a].text = " Character " + "\n" + "Select";   //Set text
                        }
                    }
                    else if (a == 6)    //If 6...
                    {
                        mainMenu_Text [a].text = "Gem Market";  //Set text

                        mainMenu_Text[a].fontSize = 80; //Set font size
                    }
				}
			}

			SetTextPosition_English_Spanish (); //Call this method
		}	
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1)	//If the Language is set to Spanish...
		{
			for (int a = 0; a < mainMenu_Text.Length; a++) //For all the Text inside this array...
			{
                if (a == 0) //If the index is 0 - "Rocky Top" title text
                {
                    mainMenu_Text [a].font = (Font)Resources.Load ("Fonts/Landslide");  //Make title text font Landslide

                    mainMenu_Text [a].fontSize = 180;

                    mainMenu_Text [a].text = "Rocky" + "\n" + "Top";
                } 
                else if (a != 0)    //If the index is NOT 0 - Everything else
                {
                    mainMenu_Text [a].font = (Font)Resources.Load ("Fonts/JFRocSol");   //Make title text font JFRocSolid

                    if (a == 1) //If the index is 1...
                    {
                        mainMenu_Text [a].text = "¡Presione" + "\n" + "para" + "\n" + "Jugar!";   //Set text

                        mainMenu_Text[a].fontSize = 95;    //Set font size
                    }
                    else if (a >= 2 && a <= 5)  //If the index is greater than or equal to 2 AND less than or equal to 5...
                    {
                        mainMenu_Text[a].fontSize = 65; //Set font size

                        if (a == 2)    //If 2...
                        {
                            mainMenu_Text [a].text = "Opciones";  //Set text
                        }
                        else if (a == 3)    //If 3...
                        {
                            mainMenu_Text [a].text = "Tienda";   //Set text
                        }
                        else if (a == 4)    //If 4...
                        {
                            mainMenu_Text [a].text = "Informacion";   //Set text
                        }
                        else if (a == 5)    //If 5...
                        {
                            mainMenu_Text [a].text = "Seleccionar" + "\n" + "Personaje";   //Set text
                        }
                    }
                    else if (a == 6)    //If 6...
                    {
                        mainMenu_Text [a].text = "Mercado de Gemas";  //Set text

                        mainMenu_Text[a].fontSize = 75; //Set font size
                    }
                }
			}
                
			SetTextPosition_English_Spanish (); //Call this method
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 2)	//If the Language is set to Hindi...
		{
			for (int a = 0; a < mainMenu_Text.Length; a++)	//For every text element inside this Array...
			{
				mainMenu_Text [a].font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;	//Make all Text elements display as Arial
			}
			mainMenu_Text [0].text = "रॉकी टॉप";	//What the first element will say

			mainMenu_Text [0].fontSize = 180;	//Font size of the first element

			mainMenu_Text [1].text = " खेलने के लिए" + "\n" + " दबाओ!";	//What the second element will say

			mainMenu_Text [1].rectTransform.anchoredPosition = new Vector2 (-30, 85);	//Position of the second text element

			mainMenu_Text [1].rectTransform.sizeDelta = new Vector2 (545, 205);	//Size of the second text element

			mainMenu_Text [1].fontSize = 100;	//Font size of the second element

			mainMenu_Text [2].text = "विकल्प";	//What the third element will say

			mainMenu_Text [2].fontSize = 75;	//Font size of the third element

			mainMenu_Text [2].rectTransform.sizeDelta = new Vector2 (235, 105);	//Position of the fourth text element

			mainMenu_Text [3].text = "दुकान";	//What the fourth element will say

			mainMenu_Text [3].fontSize = 75;	//Font size of the fourth element

			mainMenu_Text [3].rectTransform.sizeDelta = new Vector2 (175, 90);	//Size of the fourth text element

			mainMenu_Text [4].text = "के बारे में";	//What the fifth element will say

			mainMenu_Text [4].fontSize = 75;	//Font size of the fifth element

			mainMenu_Text [5].text = " विशेष गुण";	//What the sixth element will say

			mainMenu_Text [5].fontSize = 75;	//Font size of the sixth element

			mainMenu_Text [5].rectTransform.sizeDelta = new Vector2 (285, 130);	//Size of the sixth text element

			mainMenu_Text [6].text = "रत्न बाजार";	//What the seventh element will say

			mainMenu_Text [6].fontSize = 80;	//Font size of the seventh element

			mainMenu_Text [6].rectTransform.sizeDelta = new Vector2 (400, 70);	//Size of the seventh text element
		}
	}

	private void SetTextPosition_English_Spanish ()
	{
		mainMenu_Text [1].rectTransform.anchoredPosition = new Vector2 (8.5f, 70);	//Position of the second text element

		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0) 
		{
			mainMenu_Text [2].rectTransform.anchoredPosition = new Vector2 (-220, -330);	//Position of the third text element
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1) 
		{
			mainMenu_Text [2].rectTransform.anchoredPosition = new Vector2 (-220, -335);	//Position of the third text element
		}
		mainMenu_Text [3].rectTransform.anchoredPosition = new Vector2 (-195, -545);	//Position of the fourth text element

		mainMenu_Text [4].rectTransform.anchoredPosition = new Vector2 (175, -543);	//Position of the fifth text element

		mainMenu_Text [5].rectTransform.anchoredPosition = new Vector2 (170, -367.5f);	//Position of the sixth text element

		mainMenu_Text [6].rectTransform.anchoredPosition = new Vector2 (0, -195);	//Position of the seventh text element

	}
}
/* Copyright (C) Odyssey Group - All Rights Reserved - OdysseyGroup.co
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Stephen P. George <txsteven@gmail.com>, May 2017	 
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