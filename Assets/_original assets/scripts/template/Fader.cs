using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using CommandTerminalPlus;

namespace PizzaFam
{
    public class Fader : MonoBehaviour
    {
        public static void FadeOut(float duration, Color color, Action onComplete)
            => Instance.StartCoroutine(Instance.FadeOutRoutine(duration, color, onComplete));

        public static void FadeIn(float duration, Color color, Action onComplete = null)
            => Instance.StartCoroutine(Instance.FadeInRoutine(duration, color, onComplete));


        private static Fader Instance;
        private void Awake()
        {
            Instance = this;
        }

        public Image FadeImage;

        private IEnumerator FadeOutRoutine(float duration, Color color, Action onComplete)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                FadeImage.color = Fade(color, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            FadeImage.color = new Color(0, 0, 0, 0);
            onComplete?.Invoke();
        }

        private IEnumerator FadeInRoutine(float duration, Color color, Action onComplete)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                FadeImage.color = Fade(color, 1 - elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            FadeImage.color = new Color(0, 0, 0, 0);
            onComplete?.Invoke();
        }

        private Color Fade(Color original, float alpha) => new Color(original.r, original.g, original.b, alpha);



        [RegisterCommand(Name = "Fade.Out", MinArgCount = 1, MaxArgCount = 1)]
        private static void FadeOutCommand(CommandArg[] args) => FadeOut(args[0].Float, Color.white, null);

        [RegisterCommand(Name = "Fade.In", MinArgCount = 1, MaxArgCount = 1)]
        private static void FadeInCommand(CommandArg[] args) => FadeIn(args[0].Float, Color.white, null);

        [RegisterCommand(Name = "Fade.OutAndIn", MinArgCount = 1, MaxArgCount = 1)]
        private static void FadeOutInCommand(CommandArg[] args)
        {
            var duration = args[0].Float;
            FadeOut(duration, Color.white, () => { FadeIn(duration, Color.white); });
        }
    }
}