﻿using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    public float speed;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "UnbreakableBlock")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Block")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Player")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(other.gameObject);
            SoundHandler.PlaySound(SoundHandler.explosionSound);
        }
    }
}
