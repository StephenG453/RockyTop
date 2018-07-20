using UnityEngine;
using System.Collections;

public class MainMenu_ChangeMaterial : MonoBehaviour 
{
	private Shader shader;	//Reference to the Shader of everything BUT the bodies of the playable characters

	private Shader bodiesShader;	//Reference to the Shader of the bodies of the playable characters - Uses different Toon shaders if ToonMode is activated
	
	public Material[] materials = new Material[39];	//Material array consisting of 39 indexes - Everything but the bodies of the five playable characters

	public Material[] fiveBodyMaterials = new Material[5];	//Material array consisting of 5 indexes - The bodies of the five playable characters

    void Start () 
	{
		if (PlayerPrefs.GetInt ("PLAYERPREFS_GameMode") == 0)	//If "Regular" GameMode is selected...
		{
			//shader = Shader.Find ("Standard");	//Find the "Standard" shader for the "shader" variable

			//bodiesShader = Shader.Find("Standard");	//Find the "Standard" shader for the "bodiesShader" variable

			shader = Shader.Find ("Mobile/DiffuseX");

			bodiesShader = Shader.Find ("Mobile/DiffuseX");
		} 
		else if (PlayerPrefs.GetInt ("PLAYERPREFS_GameMode") == 1)	//If "Toon" GameMode is selected...
		{
			shader = Shader.Find("Toon/Lit Outline");	//Find the "Toon < Lit Outline" shader for the "shader" variable

			bodiesShader = Shader.Find("Toon/Lit");	//Find the "Toon < Lit" shader for the "bodiesShader" variable - The bodies do not look right with an outline
		}
			
		for (int i = 0; i < materials.Length; i++)	//For every index inside the length of the "materials" array...
		{
			materials[i].shader = shader;	//Every index inside the "materials" array will have the shader of what is pulled from the "shader" variable
		}
		for (int i = 0; i < fiveBodyMaterials.Length; i++)	//For every index inside the length of the "fiveBodyMaterials" array...
		{
			fiveBodyMaterials[i].shader = bodiesShader;	//Every index inside the "fiveBodiesMaterials" array will have the shader of what is pulled from the "bodiesShader" variable
		}
	}
}