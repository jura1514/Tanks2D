using UnityEngine;
using System.Collections;

public class SoundHandler : MonoBehaviour {

    public AudioSource explosion;
    public AudioSource shot;

    public static AudioSource explosionSound;
    public static AudioSource shotSound;

    // Use this for initialization
    void Start()
    {
        explosionSound = explosion;
        shotSound = shot;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
}
