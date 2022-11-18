using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IzumiTools
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public class OneShotSound : MonoBehaviour
    {
        public AudioSource AudioSource { get; private set; }
        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            AudioSource.Play();
        }
        private void Update()
        {
            if (AudioSource.time >= AudioSource.clip.length)
            {
                Destroy(gameObject);
            }
        }
        public static OneShotSound PlayAtAnchor(AudioClip clip, Transform anchor)
        {
            GameObject oneshotObj = new GameObject("(SE: " + clip.name + ")");
            oneshotObj.transform.SetParent(anchor, false);
            OneShotSound oneshotSound = oneshotObj.AddComponent<OneShotSound>();
            oneshotSound.AudioSource.clip = clip;
            return oneshotSound;
        }
    }

}