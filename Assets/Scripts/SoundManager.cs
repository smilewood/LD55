using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;

namespace LD55
{
    [System.Serializable]
    public class AudioEnumCoupler
    {
        public SoundOrMusic Sound;
        public AudioSourceType Source;
        public AudioClip Clip;
    }

    public enum AudioState
    { 
        Muted,
        MainMenu,
        InGameDesert,
        InGameTower,
        InGameBoss,
        GameOver
    }

    public enum SoundOrMusic
    { 
	    // Movement
        FlightInsectEnd,
        FlightInsectLoop,
        FlightInsectStart,
        FlightWinged,
        FootstepSand,
        FootstepWood,
        FootstepStone,
        // Game
        GameOver,
        LevelCleared,
        SummonAlly,
        UICancel,
        UISubmit,
        UIInteraction,
        // Combat
        DamageEnemy,
        DamagePlayer,
        DefeatEnemy,
        DefeatPlayer,
        DefeatSummonedAlly,
        HealEnemy,
        HealPlayer,
        LaunchProjectileEnemy,
        LaunchProjectileSummonedAlly,
		// Music
		MainTheme,
		DesertTheme,
        // Ambience
        DesertAmbience,
        TowerAmbience,
        // Voice
        VoiceDesert1,
        VoiceDesert2,
        VoiceDesert3,
        VoicePyramid1,
        VoicePyramid2,
        VoicePyramid3,
        VoiceTower1,
        VoiceTower2,
        VoiceTower3,
        VoiceTower4,
        VoiceBoss1,
        VoiceBoss2,
        VoiceDefeat,
        VoiceVictory,
        VoiceLaugh,
        
        None,
        TowerTheme,
        BossTheme,
	}

    public enum AudioSourceType
    {
        Music,
        Effects,
        Ambience,
        Voice
    }

    public static class AudioExtensionMethods
    {
		public static T RandomElement<T>(this IEnumerable<T> enumerable)
		{
			int index = Random.Range(0, enumerable.Count());
			return enumerable.ElementAt(index);
		}
	}

    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public AudioState AudioState = AudioState.Muted;
        public bool CanMusicPlay;
        public bool CanEffectsPlay;
        public bool CanAmbiencePlay;
        public bool CanVoicePlay;

        [SerializeField]
        private AudioSource musicSource;
        [SerializeField]
        private AudioSource effectsSource;
        [SerializeField]
        private AudioSource ambienceSource;
        [SerializeField]
        private AudioSource voiceSource;
        [SerializeField]
        private List<AudioEnumCoupler> AudioCouplers;

        private AudioEnumCoupler musicCoupler;
        private AudioEnumCoupler ambienceCoupler;

        public static SoundManager GlobalSoundManager
        {
            get
            {
				GameObject soundManagerObject = GameObject.FindGameObjectWithTag("SoundManager");
				if (soundManagerObject == null)
				{
					soundManagerObject = Instantiate(PrefabUtility.LoadPrefabContents("Assets/Prefabs/SoundManager.prefab"));
				}

				return soundManagerObject.GetComponent<SoundManager>();
			}
        }

		public void PlaySound(SoundOrMusic sound)
        {
#nullable enable
            IEnumerable<AudioEnumCoupler>? audioCouplers = AudioCouplers.Where(audioCoupler => audioCoupler.Sound == sound);

            if (audioCouplers != null)
            {
				AudioEnumCoupler coupler = audioCouplers.Count() > 1 ? audioCouplers.RandomElement() : audioCouplers.First();
                switch (coupler.Source)
                {
                    case (AudioSourceType.Music):
                        {
                            if ((musicSource.clip?.name ?? string.Empty) != coupler.Clip.name)
                            {
                                musicSource.Stop();
                                musicSource.clip = coupler.Clip;
                            }

							if (!musicSource.isPlaying)
							{
                                musicSource.Play();
							}

							break;
                        }
					case (AudioSourceType.Effects):
						{
                            if (effectsSource.isPlaying)
                            {
								StartCoroutine(PlaySimulSound(coupler.Clip, effectsSource.outputAudioMixerGroup));
							}
                            else
                            {
								effectsSource.clip = coupler.Clip;
								effectsSource.Play();
							}

							break;
						}
					case (AudioSourceType.Ambience):
						{
							if ((ambienceSource.clip?.name ?? string.Empty) != coupler.Clip.name)
							{
								ambienceSource.Stop();
								ambienceSource.clip = coupler.Clip;
							}

							if (!ambienceSource.isPlaying)
							{
								ambienceSource.Play();
							}

							break;
						}
                    case (AudioSourceType.Voice):
                        {
							if ((voiceSource.clip?.name ?? string.Empty) != coupler.Clip.name)
							{
								voiceSource.clip = coupler.Clip;
							}

							if (!voiceSource.isPlaying)
							{
								voiceSource.Play();

                                GameObject playerMessagerObject = GameObject.FindGameObjectWithTag("PlayerMessages");
								if (playerMessagerObject != null)
                                {
                                    PlayerMessages playerMessager = playerMessagerObject.GetComponent<PlayerMessages>();

                                    playerMessager.DisplayPlayerMessage(coupler.Clip.length * 1.2f, coupler.Sound);
								}
							}
							break;
                        }
				}
			}
#nullable disable
        }

        private IEnumerator PlaySimulSound(AudioClip clip, AudioMixerGroup group)
        {
            AudioSource source = this.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.clip = clip;
            source.outputAudioMixerGroup = group;
            source.Play();
			yield return new WaitForSeconds(clip.length + 0.1f);
            Destroy(source);
        }

		void Start()
		{
            musicCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.MainTheme);
			ambienceCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.DesertAmbience);
		}

		void Update()
        {
            CheckAudioState();
            if (CanMusicPlay)
            {
                PlaySound(musicCoupler.Sound);
            }
            
            if (CanAmbiencePlay)
            {
                PlaySound(ambienceCoupler.Sound);
            }
		}

        void CheckAudioState()
        {
            switch (AudioState)
            {
                case AudioState.Muted:
                    {
                        CanMusicPlay = false;
                        CanEffectsPlay = false;
                        CanAmbiencePlay = false;
						break;
                    }
                case AudioState.MainMenu:
                    {
                        CanMusicPlay = true;
                        CanEffectsPlay = true;
                        CanAmbiencePlay = false;
						musicCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.MainTheme);
						break;
                    }
                case AudioState.GameOver:
                    {
						CanMusicPlay = false;
						CanEffectsPlay = true;
						CanAmbiencePlay = true;
						break;
					}
                case AudioState.InGameBoss:
                    {
						musicCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.BossTheme);
						ambienceCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.TowerAmbience);
						CanMusicPlay = true;
						CanEffectsPlay = true;
						CanAmbiencePlay = true;
						break;
					}
				case AudioState.InGameTower:
					{
						musicCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.TowerTheme);
						ambienceCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.TowerAmbience);
						CanMusicPlay = true;
						CanEffectsPlay = true;
						CanAmbiencePlay = true;
						break;
					}
				case AudioState.InGameDesert:
                    {
						musicCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.DesertTheme);
						ambienceCoupler = AudioCouplers.First(coupler => coupler.Sound == SoundOrMusic.DesertAmbience);
						CanMusicPlay = true;
						CanEffectsPlay = true;
						CanAmbiencePlay = true;
						break;
					}
                default:
                    {
						CanMusicPlay = true;
						CanEffectsPlay = true;
						CanAmbiencePlay = true;
                        break;
					}
            }
        }
    }
}
