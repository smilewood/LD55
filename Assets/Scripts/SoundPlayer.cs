using UnityEngine;

namespace LD55
{
    public class SoundPlayer : MonoBehaviour
    {
        public SoundOrMusic Sound;
        public AudioState AudioState;
        public bool PlayOnAwake = true;
        void Awake()
        {
            SoundManager.GlobalSoundManager.AudioState = AudioState;
            if (PlayOnAwake)
            {
				SoundManager.GlobalSoundManager.PlaySound(Sound);
			}
        }

        public void Play()
        {
			SoundManager.GlobalSoundManager.PlaySound(Sound);
		}
    }
}
