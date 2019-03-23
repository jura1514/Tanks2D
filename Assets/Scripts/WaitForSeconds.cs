using UnityEngine;
using System.Collections;

public class WaitForSeconds : MonoBehaviour {

    public float tomeToWait;
    private float counter;

    // Use this for initialization
    void Start () {

        counter = tomeToWait;

	}
	
	// Update is called once per frame
	void Update () {

        counter = counter - Time.deltaTime;
        if(counter <= 0)
        {
            Destroy(gameObject);
        }
	}
}
