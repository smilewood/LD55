using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace LD55
{
    public class MessageInformation
    {
        public float Duration;
        public string Message;
    }

    public class PlayerMessages : MonoBehaviour
    {
		public int MaxCharacterCount = 220;
        public TextMeshProUGUI messageTextBox;
        public GameObject messageDisplayPanel;

        private bool isMessageDisplayed;
        private bool currentMessageRequiresInteraction;
        private List<MessageInformation> messageQueue = new List<MessageInformation>();
        private MessageInformation currentMessage;
        private Coroutine currentMessageCoroutine;

        // Start is called before the first frame update
        void Awake()
        {
			messageDisplayPanel.SetActive(false);
            isMessageDisplayed = false;
            currentMessageRequiresInteraction = false;
		}

        // Update is called once per frame
        void Update()
        {
			if (messageDisplayPanel.activeSelf != isMessageDisplayed)
            {
                messageDisplayPanel.SetActive(isMessageDisplayed);
			}

			if (messageQueue.Count > 0)
            {
                if (!isMessageDisplayed)
                {
					// new message in the queue
					currentMessage = messageQueue.First();
                    currentMessageRequiresInteraction = currentMessage.Duration < 0;
					isMessageDisplayed = true;
					messageTextBox.text = currentMessage.Message;
                    currentMessageCoroutine = null;

					if (!currentMessageRequiresInteraction) 
                    {
						currentMessageCoroutine = StartCoroutine(PlayerMessageTimer());
                    }
				}
			}
        }

        public void SkipCurrentMessage()
        {
            isMessageDisplayed = false;

			if (currentMessageCoroutine != null)
            {
				StopCoroutine(currentMessageCoroutine);
			}

			messageQueue.Remove(currentMessage);
            currentMessage = null;
		}

        /// <summary>
        /// Shows a message to the player with the main dialogue text box.
        /// </summary>
        /// <param name="duration">how long to show the message - seconds. negative to require a user click interact</param>
        /// <param name="message">the message to display - 220 char max</param>
        public void DisplayPlayerMessage(float duration, string message)
        {
            if (message.Count() > MaxCharacterCount) 
            {
                Debug.LogError("message exceeds max character limit");
            }

            messageQueue.Add(new MessageInformation() { Duration = duration, Message = message });
        }

		public void DisplayPlayerMessage(float duration, SoundOrMusic voiceLine)
		{
            DisplayPlayerMessage(duration, GetVoiceLineTranscript(voiceLine));
		}

		private IEnumerator PlayerMessageTimer()
        {
            yield return new WaitForSeconds(currentMessage.Duration);
            SkipCurrentMessage();
		}

        private string GetVoiceLineTranscript(SoundOrMusic voiceLine)
        {
            switch (voiceLine)
            {
                case SoundOrMusic.VoiceDesert1:
                    return "What?! An umbral vestige dares enter my realm? Your pitiful afterlife will end in short order.";
                case SoundOrMusic.VoiceDesert2:
                    return "The dunes bring forth plenty of challengers that are crushed by my scorpion legions.";
			    case SoundOrMusic.VoiceDesert3:
                    return "I wonder what strange entity has found quarrel in my realm. You shall be vanquished!";
			    case SoundOrMusic.VoicePyramid1:
                    return "Impossible. A Summoned with the kiss of the desert goddess? No matter, you will soon be fodder for my minions!";
			    case SoundOrMusic.VoicePyramid2:
                    return "A moth child dares to wield the very craft I have perfected?!";
			    case SoundOrMusic.VoicePyramid3:
                    return "I admire your persistence, but you won’t get much farther against my ancient horrors.";
			    case SoundOrMusic.VoiceTower1:
                    return "Very well, intruder. I acknowledge your ability. You shall encounter my most hideous creatures, shaped from the dunes of time.";
			    case SoundOrMusic.VoiceTower2:
                    return "You still rush towards your doom? Very well. Come closer, and you shall have the fight you seek.";
			    case SoundOrMusic.VoiceTower3:
                    return "If even the creatures of my greatest imagination are of no issue, then perhaps I have underestimated you, strange visage.";
			    case SoundOrMusic.VoiceTower4:
                    return "Your soul… it’s strange. I recall your presence from a life long passed. Who might you be? For what reason do you lay ruin to my spawn?";
			    case SoundOrMusic.VoiceBoss1:
                    return "I recognize you now. The pitiful offspring of the desert goddess, and here you are squabbling in the eternal afterlife? I shall enjoy ending you once again!";
			    case SoundOrMusic.VoiceBoss2:
                    return "No... no! This can’t be happening. Has the eternal desert goddess herself summoned you to quell my power?";
			    case SoundOrMusic.VoiceDefeat:
                    return "As I expected. Yet another pest returns to the eternal desert.";
				case SoundOrMusic.VoiceVictory:
					return "My dynasty crushed and my power usurped... All by a mere child! Impudent! Curse you Copil, curse you!";
				case SoundOrMusic.VoiceLaugh:
                    return "Hahaha!";
                default:
                    return "";
			}
        }
    }
}
