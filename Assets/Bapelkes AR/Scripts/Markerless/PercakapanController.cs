using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Bapelkes.Markerless
{
    public class PercakapanController : MonoBehaviour
    {
        [SerializeField] private Percakapan[] percakapan;

        [Header("Chat Atribute")]
        [SerializeField] private Image chatIcon;
        [SerializeField] private TMP_Text chatMsg;

        [SerializeField] private Animator chatAnimator;

        public void TriggerChat(string chatName, int chatNum)
        {
            Percakapan myPercakapan = null;

            foreach (Percakapan thisPercakapan in percakapan)
            {
                if (chatName == thisPercakapan.chatName)
                {
                    myPercakapan = thisPercakapan;
                    break;
                }
            }

            chatIcon.sprite = myPercakapan.chat[chatNum].chatIcon;
            chatMsg.text = myPercakapan.chat[chatNum].chatMsg;

            chatAnimator.SetTrigger("TriggerChat");

        }

        public void StopChat()
        {
            chatAnimator.SetTrigger("StopChat");
        }
    }

    [System.Serializable]
    public class Percakapan
    {
        public string chatName;
        public Chat[] chat;

    }

    [System.Serializable]
    public class Chat
    {
        public string chatMsg;
        public Sprite chatIcon;
    }
}

