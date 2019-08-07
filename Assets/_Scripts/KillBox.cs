using UnityEngine;
using System.Collections;

public class KillBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        //Debug.LogError("WTFFF");

        if (col.tag == "Player")
            col.GetComponent<PlayerHealth>().DealDamage(9999);
        else if (col.tag == "Enemy")
            col.GetComponent<EnemyHealth>().DealDamage(9999);
        else
            Destroy(col.gameObject);
    }
}
