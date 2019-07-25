using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //SceneManager.LoadSceneAsync(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.exitEnable)
            Debug.Log("Find the exit");
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            if (GameManager.exitEnable)
                SceneManager.LoadScene(1);
        }
    }
}
