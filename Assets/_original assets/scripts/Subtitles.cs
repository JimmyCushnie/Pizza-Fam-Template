using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CommandTerminalPlus;

namespace PizzaFam
{
    public class Subtitles : MonoBehaviour
    {
        public TextMeshProUGUI SubtitleOutput;

        public static void Say(string text, float duration)
            => Instance?.SaySomething(text, duration);

        public static void Clear()
            => Instance?.SaySomething("", 0);

        public static bool Enabled
        {
            get => GameData.Get("Subtitles", true);
            set
            {
                Instance.enabled = value;
                Instance.SubtitleOutput.enabled = value;
                GameData.Set("Subtitles", value);
            }
        }

        

        private void SaySomething(string text, float duration)
        {
            SubtitleOutput.text = text;
            TimeUntilFade = duration;

            if (enabled)
                SubtitleOutput.enabled = true;
        }

        float TimeUntilFade = 0;

        private void Update()
        {
            TimeUntilFade -= Time.deltaTime;
            if(TimeUntilFade <= 0)
                SubtitleOutput.enabled = false;
        }




        private static Subtitles Instance;
        private void Awake()
        {
            Instance = this;
            SubtitleOutput.text = "";

            Enabled = Enabled;
        }
    }
}