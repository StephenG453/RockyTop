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

public class Prologue_TextManager : MonoBehaviour 
{
	public Text[] prologue_Text = new Text[3];	//Array of Text elements that consists of 3 indexies

	public Button skipPrologueButton;	//Skip Prologue button

	private void Start () 
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 0)	//If the Language is set to English...
		{
			for (int a = 0; a < prologue_Text.Length; a++)	//For every index inside this array...
			{
				prologue_Text [a].font = (Font)Resources.Load ("Fonts/JFRocSol");	//Make text font JFRocSol

				if (a == 0 || a == 1)   //If the index is either 0 or 1...
				{
                    prologue_Text[a].lineSpacing = 1.15f;   //Set line spacing

					prologue_Text [a].fontSize = 90;    //Set font size

                    prologue_Text[a].rectTransform.anchoredPosition = new Vector2(0, 85);   //Set position

                    if (a == 0) //If index is 0...
                    {
                        prologue_Text [a].text = "May 20, 2016" + "\n" + "in the early" + "\n" + "morning" + "\n" + "Mr. Adventure," + "\n" + "Barry 'Nash',"
                        + "\n" + "and Zoey" + "\n" + "hear a loud" + "\n" + "explosion on" + "\n" + "their favorite" + "\n" + "mountain...";    //Set text
                    }
                    else if (a == 1)    //If index is 1...
                    {
                        prologue_Text [a].text = "While they are" + "\n" + "Climbing up" + "\n" + "to investigate" + "\n" + "what (or who)" + "\n"
                        + "caused the" + "\n" + "mining" + "\n" + "explosion...";   //Set text   
                    }
				}
				else if (a == 2)    //If index is 2...
				{
                    prologue_Text [a].text = "Skip Prologue";   //Set text

					prologue_Text [a].lineSpacing = 1;  //Set line spacing

					prologue_Text [a].fontSize = 45;    //Set font size

                    prologue_Text [a].rectTransform.anchoredPosition = new Vector2 (200, 40);   //Set position
				}
			}
			skipPrologueButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 100);	//Position of the Skip Prologue button

			skipPrologueButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, 65);	//Size of the Skip Prologue button
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 1)	//If the Language is set to Spanish...
		{
			for (int a = 0; a < prologue_Text.Length; a++)	//For every index inside this array...
			{
				prologue_Text [a].font = (Font)Resources.Load ("Fonts/JFRocSol");	//Make text font JFRocSol

				if (a == 0 || a == 1)   //If the index is either 0 or 1...
				{
					prologue_Text [a].fontSize = 85;    //Set font size

					prologue_Text [a].lineSpacing = 1.15f;  //Set line spacing

                    prologue_Text[a].rectTransform.anchoredPosition = new Vector2(0, 85);   //Set position

                    if (a == 0) //If the index is 0...
                    {
                        prologue_Text[a].text = "Al amanecer" + "\n" + "del dia 20" + "\n" + "de Mayo del 2016" + "\n" + "Sr. Adventure," + "\n" + "Barry 'Nash',"
                        + "\n" + "Y Zoey" + "\n" + "oyen una gran" + "\n" + "Explosion desde" + "\n" + "Su monataÑa" + "\n" + "preferida...";   //Set text
                    }
                    else if (a == 1)    //If the index is 1...
                    {
                        prologue_Text [a].text = "Mientras escalan" + "\n" + "la montaÑa para" + "\n" + "investigar" + "\n" + "Que (o quien)" + "\n"
                        + "Causo la" + "\n" + "explosion" + "\n" + "en la mina..."; //Set text
                    }
				}
				else if (a == 2)    //If the index is 2...
				{
                    prologue_Text [a].text = "terminar prologo";    //Set text

					prologue_Text [a].fontSize = 45;    //Set font size

					prologue_Text [a].lineSpacing = 1;  //Set line spacing

                    prologue_Text [a].rectTransform.anchoredPosition = new Vector2 (225, 40);   //Set position
				}
			}
			skipPrologueButton.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 100);	//Position of the Skip Prologue button

			skipPrologueButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (450, 65);	//Size of the Skip Prologue button
		}
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_Language") == 2)	//If the Language is set to Hindi...
		{
			for (int a = 0; a < prologue_Text.Length; a++)	//For every text element in this Array...
			{
				prologue_Text [a].font = Resources.GetBuiltinResource (typeof(Font), "Arial.ttf") as Font;	//Make all Text elements display as Arial
			}
			prologue_Text[0].text = "मई २०, २०१६" + "\n" + "सुबह में" + "\n" + "श्री साहसिक," + "\n" + "बैरी 'नैश'," + "\n" + "और ज़ोई"
				+ "\n" + "एक जोर से सुन" + "\n" + "उनके विस्फोट पर" + "\n" + "पसंदीदा पर्वत:-";	//What the first text element will say

			prologue_Text [0].fontSize = 80;	//Font size of the first text element

			prologue_Text[1].text = "जबकि वे हैं" + "\n" + "ऊपर चढ़ना" + "\n" + "जांच के लिए" + "\n" + "क्या (या कौन)" + "\n" + "कारण होता है"
				+ "\n" + "खनन विस्फोट:-";	//What the second text element will say
			
			prologue_Text [1].fontSize = 80;	//Font size of the second text element

			prologue_Text[2].text = "प्रस्तावना छोड़ें";	//What the third text element will say

			prologue_Text [2].fontSize = 45;	//Font size of the third text element

			prologue_Text [2].rectTransform.anchoredPosition = new Vector2 (205, 45);	//Position of the third text element
		}
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