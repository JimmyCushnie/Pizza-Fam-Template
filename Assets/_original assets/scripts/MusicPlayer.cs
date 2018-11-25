using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommandTerminalPlus;

namespace PizzaFam
{
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer Instance;

        public enum type { MainMenu, Gameplay }

        public type MusicType;
        float LoopStartTime;
        float LoopEndTime;

        private void Awake()
        {
            if (Instance == null)
            {
                Stop();
                Instance = this;
                Play();
            }
            else if (Instance.MusicType != this.MusicType)
            {
                Stop();
                Instance = this;
                Play();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public AudioSource Source;

        private void Play()
        {
            Source.time = 0;
            Source.Play();
        }

        private void Update()
        {
            if (Source.timeSamples >= LoopEndTime * Source.clip.frequency)
                Source.timeSamples -= Mathf.RoundToInt((LoopEndTime - LoopStartTime) * Source.clip.frequency);
        }

        public static void Stop()
        {
            Instance?.Source.Stop();
        }

        public static void FadeOut(float duration)
        {
            Instance.StartCoroutine(Instance.FadeOutRoutine(duration));
        }

        private IEnumerator FadeOutRoutine(float duration)
        {
            float previousVolume = Source.volume;
            while(Source.volume > 0)
            {
                Source.volume -= (previousVolume / duration) * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            Source.Stop();
            Source.volume = previousVolume;
        }

        private static void Restart()
        {
            Stop();
            Instance.Play();
        }

        private static void Seek(float time)
        {
            Instance.Source.time = time;
        }


        [RegisterCommand(Name = "Music.Stop", MaxArgCount = 0)]
        private static void CommandStop(CommandArg[] args) => Stop();

        [RegisterCommand(Name = "Music.Restart", MaxArgCount = 0)]
        private static void CommandRestart(CommandArg[] args) => Restart();

        [RegisterCommand(Name = "Music.Seek", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandSeek(CommandArg[] args) => Seek(args[0].Float);

        [RegisterCommand(Name = "Music.Fade", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandFade(CommandArg[] args) => FadeOut(args[0].Float);
    }
}