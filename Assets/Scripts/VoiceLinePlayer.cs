using UnityEngine;

namespace LD55
{
    public class VoiceLinePlayer : MonoBehaviour
    {
        public SoundOrMusic VoiceLine;
        void Awake()
        {
            SoundManager.GlobalSoundManager.PlaySound(VoiceLine);
        }
    }
}
