using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    public void PlayOneshot() { 
        SoundManager.instance.PlayOneShotClip(audioClip);
    }
}
