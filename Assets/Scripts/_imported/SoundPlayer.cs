using NUnit.Framework.Constraints;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Pacman
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        private AudioSource m_AudioSource;
        private AudioSource m_BGMSource;
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_BGM;

        protected override void Awake()
        {
            base.Awake();
            m_AudioSource = GetComponent<AudioSource>();
            m_BGMSource = gameObject.AddComponent<AudioSource>();
            m_BGMSource.loop = true;
            m_BGMSource.volume = 0.5f;
            PlayBGM(m_BGM);
        }

        public void PlayBGM(AudioClip clip)
        {
            if (clip == null) return;
            m_BGMSource.clip = clip;
            m_BGMSource.Play();
        }

        public void StopBGM()
        {
            m_BGMSource.Stop();
        }

        public void Play(Sound sound)
        {
            var clip = m_Sounds[sound];
            if (clip == null) return;
            m_AudioSource.PlayOneShot(clip);
        }
    }
}
