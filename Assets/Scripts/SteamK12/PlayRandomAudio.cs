using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class PlayRandomAudio : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioClip[] audioClipArray;

        public int instanceLimit = 3;
        private int instances;

        private void Start() 
        {
            instances = 0;
        }

        AudioClip RandomClip()
        {
            return audioClipArray[Random.Range(0, audioClipArray.Length)];
        }

        public void PlayRandomClip()
        {
            if (instances >= instanceLimit) return;
            instances++;
            audioSource.PlayOneShot(RandomClip());
        }

        private void Update() 
        {
            if (!audioSource.isPlaying && instances >= 1)
            {
                instances--;
            }
        }
    }
}
