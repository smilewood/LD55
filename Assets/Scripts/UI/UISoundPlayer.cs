using UnityEngine;

namespace LD55
{
    public class UISoundPlayer : MonoBehaviour
    {
		public void Play_UI_Interact_Sound()
		{
			SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.UIInteraction);
		}

		public void Play_UI_Submit_Sound()
		{
			SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.UISubmit);
		}

		public void Play_UI_Cancel_Sound()
		{
			SoundManager.GlobalSoundManager.PlaySound(SoundOrMusic.UICancel);
		}
	}
}
