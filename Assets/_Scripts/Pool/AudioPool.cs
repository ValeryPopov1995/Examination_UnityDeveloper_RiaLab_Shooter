using System.Collections;
using UnityEngine;

namespace RiaShooter.Scripts.Pool
{
    public class AudioPool : ObjectPool<AudioSource>
    {
        public static AudioPool Instance { get; private set; }

        // for UnityEvents
        public void Play(AudioClip clip) => Play(clip, false);
        public void PlayLoop(AudioClip clip) => Play(clip, true);



        private void Awake()
        {
            if (Instance != null)
                Destroy(Instance.gameObject);
            Instance = this;
        }

        // other public voids
        public void Play(AudioClip[] clips, bool loop = false)
        {
            if (clips == null || clips.Length == 0) return;
            Play(clips[Random.Range(0, clips.Length)], loop);
        }

        public void Play(AudioClip clip, bool loop = false)
        {
            if (!clip) return;
            StartCoroutine(EnableSoundCoroutine(clip, loop));
        }
        private IEnumerator EnableSoundCoroutine(AudioClip clip, bool loop = false)
        {
            var source = GetObjectFromPoolInternal();
            source.clip = clip;
            source.loop = loop;

            source.gameObject.SetActive(true);
            source.Play();

            while (source.isPlaying)
                yield return new WaitForSeconds(1);

            source.gameObject.SetActive(false);
        }
    }
}
