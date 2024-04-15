using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
	[System.Serializable]
	public class Tutorial
	{
		public GameObject TutorialPanel;
		public SoundOrMusic VoiceLine;
	}

	public class TutorialsMessages : MonoBehaviour
    {
		public List<Tutorial> tutorials;

		public int currentTutorial = -1;

		private float currentTutorialDuration = 0f;
		private float timeElapsed = 0f;

		void Awake()
		{
			AdvanceTutorial();
		}

		public void CompleteTutorial()
		{
			// end it and go to next prefab 
		}

		public void AdvanceTutorial()
		{
			foreach (var tut in tutorials)
			{
				tut.TutorialPanel.SetActive(false);
			}

			if (currentTutorial == tutorials.Count - 1)
			{
				CompleteTutorial();
			}
			else
			{
				currentTutorial++;
				var tut = tutorials[currentTutorial];
				tut.TutorialPanel.SetActive(true);
				currentTutorialDuration = SoundManager.GlobalSoundManager.GetAudioLength(tut.VoiceLine);
				SoundManager.GlobalSoundManager.PlaySound(tut.VoiceLine);
				timeElapsed = 0f;
			}
		}

		public void SkipCurrentTutorial()
		{
			AdvanceTutorial();
		}


		void Update()
		{
			timeElapsed += Time.deltaTime;

			if (timeElapsed > currentTutorialDuration)
			{
				AdvanceTutorial();
			}
		}
	}
}