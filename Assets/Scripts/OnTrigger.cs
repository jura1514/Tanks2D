﻿using UnityEngine;
using System.Collections;

public class OnTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D( Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("You Entered a Battle Field!!! ");
            GameControl.EnterBattleField();
        }
    }
}
