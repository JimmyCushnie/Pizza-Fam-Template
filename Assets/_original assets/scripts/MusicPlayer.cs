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
        public float LoopStartTime;
        public float LoopEndTime;

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
            if (Instance == null || Instance.Source == null) return;
            Instance.Source.Stop();
        }

        public static void FadeOut(float duration)
            =>
            Instance.StartCoroutine(Instance.FadeOutRoutine(duration));

        public static void FadeIn(float duration)
            => Instance.StartCoroutine(Instance.FadeInRoutine(duration));

        private IEnumerator FadeOutRoutine(float duration)
        {
            while(Source.volume > 0)
            {
                Source.volume -= (1 / duration) * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator FadeInRoutine(float duration)
        {
            while (Source.volume < 1)
            {
                Source.volume += (1 / duration) * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
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

        [RegisterCommand(Name = "Music.FadeOut", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandFadeOut(CommandArg[] args) => FadeOut(args[0].Float);

        [RegisterCommand(Name = "Music.FadeIn", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandFadeIn(CommandArg[] args) => FadeIn(args[0].Float);
    }
}