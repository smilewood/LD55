using System.Collections.Generic;
using UnityEngine;

namespace LD55
{
	[System.Serializable]
	public class Tutorial
	{
		public GameObject TutorialPanel;
		public SoundOrMusic VoiceLine;
		public bool EnableMovement;
		public bool EnableSummoning;
	}

	public class TutorialsMessages : MonoBehaviour
    {
		public List<Tutorial> tutorials;

		public int currentTutorial = -1;

		private PlayerMouseActions pma;
		private PlayerMovement pm;

		private bool tutorialComplete = false;

      void Awake()
		{
			GameObject player = GameObject.Find("PlayerRoot");
			if (player != null) 
			{
            pma = player.GetComponent<PlayerMouseActions>();
            pma.CanISummon = false;
            pm = player.GetComponent<PlayerMovement>();
            pm.CanIMove = false;
         }

         AdvanceTutorial();
      }

		public void HideCurrentTutorial()
		{
         if (!tutorialComplete)
         {
				foreach (var tut in tutorials)
				{
					tut.TutorialPanel.SetActive(false);
				}
			}
      }

		public void ResumeCurrentTutorial()
		{
			if (!tutorialComplete) 
			{
            var tut = tutorials[currentTutorial];
            tut.TutorialPanel.SetActive(true);
         }
		}

		public void AdvanceTutorial()
		{
			foreach (var tut in tutorials)
			{
				tut.TutorialPanel.SetActive(false);
			}

			if (currentTutorial != tutorials.Count - 1)
         {
				currentTutorial++;
				var tut = tutorials[currentTutorial];
				tut.TutorialPanel.SetActive(true);
				SoundManager.GlobalSoundManager.PlaySound(tut.VoiceLine, false);
            
            pma.CanISummon = tut.EnableSummoning;
            pm.CanIMove = tut.EnableMovement;
         }
			else
			{
				tutorialComplete = true;
         }
		}

		public void SkipCurrentTutorial()
		{
			AdvanceTutorial();
		}

      void Update()
      {
         if (Input.GetKeyDown(KeyCode.Space))
			{
				SkipCurrentTutorial();
			}
      }
   }
}
