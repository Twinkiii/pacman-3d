using Pacman.Core.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pacman.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        private AudioSource m_AudioSource;
        private AudioSource m_BGMSource;
        [SerializeField] private Sounds m_Sounds;
        [SerializeField] private AudioClip m_GameBGM;
        [SerializeField] private AudioClip m_MenuBGM;

        [SerializeField] private string m_GameSceneName = "GameScene";
        [SerializeField] private string m_MenuSceneName = "MainMenu";

        protected override void Awake()
        {
            base.Awake();
            m_AudioSource = GetComponent<AudioSource>();
            m_BGMSource = gameObject.AddComponent<AudioSource>();
            m_BGMSource.loop = true;
            m_BGMSource.volume = 0.5f;

            SceneManager.sceneLoaded += OnSceneLoaded;


            PlayBGMForScene(SceneManager.GetActiveScene().name);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            PlayBGMForScene(scene.name);
        }

        private void PlayBGMForScene(object sceneName)
        {
            if (sceneName.ToString() == m_GameSceneName)
            {
                PlayBGM(m_GameBGM);
            }
            else if (sceneName.ToString() == m_MenuSceneName)
            {
                PlayBGM(m_MenuBGM);
            }
        }

        public void PlayBGM(AudioClip clip)
        {
            if (clip == null) return;


            if (m_BGMSource.clip == clip && m_BGMSource.isPlaying) return;

            m_BGMSource.clip = clip;
            m_BGMSource.Play();
        }

        public void StopBGM()
        {
            m_BGMSource.Stop();
        }

        public void Play(Sound sound)
        {
            if (m_Sounds == null) return;
            

                var clip = m_Sounds[sound];
            if (clip == null) return;
            m_AudioSource.PlayOneShot(clip);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
