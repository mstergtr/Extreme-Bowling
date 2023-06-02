using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteamK12.ExtremeBowling
{
    public class PlayRandomAudio : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioClip[] audioClipArray;
        [SerializeField] float pitchMin = 1f;
        [SerializeField] float pitchMax = 1f;
        [SerializeField] float volumeMin = 1f;
        [SerializeField] float volumeMax = 1f;

        [Tooltip("Instances are released based on the length of clip index[0]")]
        [SerializeField] int instanceLimit = 30;
        private int instances;
        private float clipLength;

        [SerializeField] float minTimeBetweenClips = 0.01f;
        private float timeSinceLastClip;
        private bool canPlay;

        private void Start()
        {
            clipLength = audioClipArray[0].length;
        }

        private void Update()
        {
            if (timeSinceLastClip >= minTimeBetweenClips)
            {
                canPlay = true;
            }
            else
            {
                canPlay = false;
            }

            timeSinceLastClip += Time.deltaTime;
        }

        public void PlayRandomClip()
        {
            if (instances >= instanceLimit || !canPlay) return;
            instances++;
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.volume = Random.Range(volumeMin, volumeMax);
            audioSource.PlayOneShot(RandomClip());
            timeSinceLastClip = 0;
            StartCoroutine(InstanceDecrease());
        }

        private AudioClip RandomClip()
        {
            return audioClipArray[Random.Range(0, audioClipArray.Length)];
        }

        IEnumerator InstanceDecrease()
        {
            yield return new WaitForSeconds(clipLength);
            instances--;
        }
    }
}
