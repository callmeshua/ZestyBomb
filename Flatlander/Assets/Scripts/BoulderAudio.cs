using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderAudio : MonoBehaviour
{
    //SOUNDS
    public AudioClip collisionClip;

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.PlaySFX(collisionClip, true, .5f);
    }
}