/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD
{
    public class AudioManager : MonoBehaviour
    {
        /// <summary>
        /// Audio source
        /// </summary>
       
        [SerializeField]
        private AudioSource audioSource;
        private bool _isAudioPlaying;
        private int _previousChooseClips;
        private bool inMenu;
        private bool inGame;
        

        [Header("Game Clips")] 
        [SerializeField]
        private List<AudioClip> clips;
        [Header("Loose Clips")]
        [SerializeField] private List<AudioClip> looseClips;
        [Header("Win Clips")]
        [SerializeField] private List<AudioClip> winClips;
        [Header("Menu Clips")]
        [SerializeField] private List<AudioClip> menuClips;
        
        public static AudioManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(gameObject);
            }

            Messenger.AddListener(GameEventsCH.START_BATTLE, PlayBattleMusic);
            Messenger.AddListener(GameEventsCH.LEVEL_FAIL, PlayLooseMusic);
            Messenger.AddListener(SystemEventsCH.MAIN_MENU, PlayMenuMusic);
            Messenger.AddListener(GameEventsCH.LEVEL_COMPLETE, PlayWinMusic);
            //DontDestroyOnLoad(gameObject);
        }
        private void OnDestroy()
        {
            Messenger.RemoveListener(GameEventsCH.START_BATTLE, PlayBattleMusic);
            Messenger.RemoveListener(SystemEventsCH.MAIN_MENU, PlayMenuMusic);
            Messenger.RemoveListener(GameEventsCH.LEVEL_FAIL, PlayLooseMusic);
            Messenger.RemoveListener(GameEventsCH.LEVEL_COMPLETE, PlayWinMusic);
        }
        private void Start()
        {
           StartCoroutine(CheckMusicState(audioSource));
        }

        private void SelectMusic(List<AudioClip> c, int previousTrack)
        {
            int randomNumber = Random.Range(0, c.Count);

            if (previousTrack != randomNumber)
            {
                previousTrack = randomNumber;
                _previousChooseClips = previousTrack;
                
                audioSource.clip = clips[randomNumber];
                audioSource.Play();
            }
        }
        public void PlayMenuMusic()
        {
            SelectSpecialMusic(menuClips);
            inMenu = true;
            inGame = false;
        }
        
        public void PlayBattleMusic()
        {
            SelectSpecialMusic(clips);
            inMenu = false;
            inGame = true;
        }

        public void PlayWinMusic()
        {
            SelectSpecialMusic(winClips);
        }
        public void PlayLooseMusic()
        {
            SelectSpecialMusic(looseClips);
        }
        private void SelectSpecialMusic(List<AudioClip> clips)
        {
            if(audioSource == null)
            {
                return;
            }
            
            audioSource.clip = RandomizeClips(clips);
            audioSource.Play();
        }

        //Create more complex randomization process
        private AudioClip RandomizeClips(List<AudioClip> clipsList)
        {
            int r = Random.Range(0, clipsList.Count);
            return clipsList[r];
        }

        private IEnumerator CheckMusicState(AudioSource source)
        {
            while (true)
            {
                if (!source.isPlaying)
                {
                    if (inMenu)
                    {
                        SelectSpecialMusic(menuClips);   
                    }
                    else if (inGame)
                    {
                        SelectSpecialMusic(clips);
                    }
                }
                yield return new WaitForSeconds(1.0f);
            }          
        }
    }
}*/