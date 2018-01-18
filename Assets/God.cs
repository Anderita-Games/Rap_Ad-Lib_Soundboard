using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using System;

public class God : MonoBehaviour {
	//Sound Button
	int Max_Buttons = 21;
	int Starting_Sound;
	AudioSource[] Sound_Sources;
	//Bottom Button
	public GameObject Button_Last;
	public GameObject Button_Next;

	void Start () {
		if (PlayerPrefs.GetInt("Page_Number") == 0) {
			PlayerPrefs.SetInt("Page_Number", 1);
		}
		Starting_Sound = PlayerPrefs.GetInt("Page_Number") * 21 - 20; //Adjust with playerprefs in future!!!!
		var Sounds = Resources.LoadAll("Sound Bytes", typeof(AudioClip)).ToArray();
		Sound_Sources = new AudioSource[Max_Buttons + 1];

		for (int i = 1; i <= Max_Buttons; i++) {
			if (Sounds.Length <= Starting_Sound + i - 1) {
				Destroy(GameObject.Find(i.ToString()));
			}else {
				GameObject.Find(i.ToString()).transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = Sounds[Starting_Sound + i - 1].name;
				AudioSource Temp_Source = gameObject.AddComponent<AudioSource>();
				Sound_Sources[i] = Temp_Source;
			}
		}

		//Bottom Button
		if (PlayerPrefs.GetInt("Page_Number") == 1) {
			Button_Last.SetActive(false);
		}else if (PlayerPrefs.GetInt("Page_Number") > Sounds.Length / Max_Buttons) {
			Button_Next.SetActive(false);
		}
	}

	public void Sound_Button () {
		var Sounds = Resources.LoadAll("Sound Bytes", typeof(AudioClip)).ToArray();
		int Sound_Number = int.Parse(EventSystem.current.currentSelectedGameObject.name);
		Sound_Sources[Sound_Number].PlayOneShot(Sounds[Starting_Sound + Sound_Number - 1] as AudioClip);
	}

	//Bottom Buttons
	public void Page_Last () {
		PlayerPrefs.SetInt("Page_Number", PlayerPrefs.GetInt("Page_Number") - 1);
		Application.LoadLevel("God");
	}

	public void Mute () {
		for (int i = 1; i <= Max_Buttons; i++) {
			Sound_Sources[i].Stop();
		}
	}

	public void Page_Next () {
		PlayerPrefs.SetInt("Page_Number", PlayerPrefs.GetInt("Page_Number") + 1);
		Application.LoadLevel("God");
	}
}