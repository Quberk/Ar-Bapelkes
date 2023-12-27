using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bapelkes.Markerless
{
    public class PercakapanEventTrigger : MonoBehaviour
    {
        [SerializeField] private string chatName;
        private PercakapanController percakapanController;
        private ButtonAnimationController buttonAnimationController;

        private void Awake()
        {
            percakapanController = FindObjectOfType<PercakapanController>();
            buttonAnimationController = FindObjectOfType<ButtonAnimationController>();
        }

        public void TriggerChat(int chatNum)
        {
           percakapanController.TriggerChat(chatName, chatNum);
        }

        public void AnimationFinish()
        {
            buttonAnimationController.ActivateAllChatButtons();
            percakapanController.StopChat();
        }
    }
}