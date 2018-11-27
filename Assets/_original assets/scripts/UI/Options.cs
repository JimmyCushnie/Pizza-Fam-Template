using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace PizzaFam.UI
{
    public class Options : MonoBehaviour
    {
        public AudioMixer Mixer;
        public Slider MusicSlider;
        public Slider SFXSlider;
        public Toggle SubtitleToggle;

        static float PercentToDecibels(float percent)
        {
            float decibels = -144.0f;
            if (percent != 0)
                decibels = 20.0f * Mathf.Log10(percent);

            return decibels;
        }

        private void Awake()
        {
            MusicSlider.value = GameData.Get("MusicVolume", 0.7f);
            SFXSlider.value = GameData.Get("SFXVolume", 0.7f);
            SubtitleToggle.isOn = Subtitles.Enabled;
        }

        public void ChangeMusicVolume(float newVolume)
        {
            Mixer.SetFloat("MusicVolume", PercentToDecibels(newVolume));
            GameData.Set("MusicVolume", newVolume);
        }
        public void ChangeSFXVolume(float newVolume)
        {
            Mixer.SetFloat("SFXVolume", PercentToDecibels(newVolume));
            GameData.Set("SFXVolume", newVolume);
        }

        public void ToggleSubtitles()
        {
            Subtitles.Enabled = SubtitleToggle.isOn;
        }
    }
}