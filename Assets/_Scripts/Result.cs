using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Result : MonoBehaviour {
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
        text.text = "Kills: " + GameManager.enemiesKilled + "\nTime: " + GameManager.GetTime();
    }
	
	// Update is called once per frame
	void Update () {
	    
	}
}
