using UnityEngine;
using System.Collections;

public class Change_Scenes : MonoBehaviour {
	private bool end=false;
	private string next_scene="";
	private string currents="";
	void OnGUI(){
		GUIStyle style = GUI.skin.GetStyle ("box");
		style.fontSize = 30;
		if (GUI.Button(new Rect(20, 50 - 15, 300, 40), currents, style)){
		}
		if(end==false){
			if (GUI.Button(new Rect(Screen.width-200, Screen.height/2 - 15, 120, 40), "Next >", style)){
				Application.LoadLevel(next_scene);
			}
		}
	}

	// Use this for initialization
	void Start () {
		next_scene = Get_Next_Scene();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	string Get_Next_Scene(){
		string aux="";
		string current= Application.loadedLevelName;
		currents = current;
		if(current=="Space_Guy1"){
			aux= "Space_Guy2";
		}
		if(current=="Space_Guy2"){
			aux= "Space_Guy3";
		}
		if(current=="Space_Guy3"){
			aux= "Bad_Space_Warriors";
		}
		if(current=="Bad_Space_Warriors"){
			aux= "Master_Warrior";
		}
		if(current=="Master_Warrior"){
			aux= "Pilot";
		}
		if(current=="Pilot"){
			aux= "Space_Warrior";
		}
		if(current=="Space_Warrior"){
			end=true;
			aux= current;
		}
		return aux;
	}
}
