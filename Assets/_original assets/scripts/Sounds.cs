using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using CommandTerminalPlus;
using PizzaFam.Extensions;
using Random = UnityEngine.Random;

namespace PizzaFam
{
    public class Sounds : MonoBehaviour
    {
        public static void Play(string soundName)
        {
            if (!NameToSound.ContainsKey(soundName))
                throw new Exception($"tried to play uninitialized sound {soundName}");

            Instance.Play(NameToSound[soundName]);
        }

        public static void PlayVoice(string voiceName)
        {
            if (!NameToVoice.ContainsKey(voiceName))
                throw new Exception($"tried to play uninitialized voice line {voiceName}");

            Instance.Play(NameToVoice[voiceName]);
        }

        private void Play(SoundEffect sound)
        {
            SoundsSource.pitch = Random.Range(1 - sound.MaxPitchVariation, 1 + sound.MaxPitchVariation);
            SoundsSource.PlayOneShot(sound.clip, sound.VolumeScale);
        }

        private void Play(VoiceLine line)
        {
            if (Random.Range(0f, 1f) > line.ChanceOfPlaying) return;

            SoundsSource.pitch = 1;
            var clip = line.RandomClips.RandomItem();
            SoundsSource.PlayOneShot(clip.audio, line.VolumeScale);

            if (clip.subtitle.IsNullOrEmpty())
                Debug.LogWarning($"the voice line {line.name} doesn't have a subtitle!");
            else
                Subtitles.Say(clip.subtitle, clip.audio.length);
        }


        private static Sounds Instance;
        private void Awake()
        {
            Instance = this;

            foreach (var sound in SoundList)
            {
                if (NameToSound.ContainsKey(sound.name))
                    throw new Exception("error initializing sounds: duplicate sound name in list");
                if (sound.name.IsNullOrEmpty())
                    throw new Exception("error initializing sounds: all sounds must have a name");

                NameToSound.Add(sound.name, sound);
            }

            foreach(var voice in VoiceList)
            {
                if (NameToVoice.ContainsKey(voice.name))
                    throw new Exception("error initializing voice lines: duplicate name in list");
                if (voice.name.IsNullOrEmpty())
                    throw new Exception("error initializing voice lines: all lines must have a name");
            }
        }

        private static Dictionary<string, SoundEffect> NameToSound = new Dictionary<string, SoundEffect>();
        private static Dictionary<string, VoiceLine> NameToVoice = new Dictionary<string, VoiceLine>();


        public AudioSource SoundsSource;

        [SerializeField] private List<SoundEffect> SoundList;
        [SerializeField] private List<VoiceLine> VoiceList;

        [Serializable]
        private class SoundEffect
        {
            public string name;
            public AudioClip clip;
            public float MaxPitchVariation;
            public float VolumeScale;

            public void SetDefaults()
            {
                name = "HEY YOU ADD A NAME HERE";
                MaxPitchVariation = 0;
                VolumeScale = 1;
            }
        }

        [Serializable]
        private class VoiceLine
        {
            public string name = "HEY YOU ADD A NAME HERE";
            public ClipWithSubtitle[] RandomClips = new ClipWithSubtitle[1];
            public float VolumeScale = 1;
            public float ChanceOfPlaying = 1;

            public void SetDefaults()
            {
                name = "HEY YOU ADD A NAME HERE";
                VolumeScale = 1;
                ChanceOfPlaying = 1;
            }

            [Serializable]
            public class ClipWithSubtitle
            {
                public AudioClip audio;
                public string subtitle;
            }
        }


        // jesus fucking christ this is WAY more complicated than it needs to be. Get your shit together, unity
        [HideInInspector] public List<bool> SoundIndexesDefaulted = new List<bool>();
        [HideInInspector] public List<bool> VoiceIndexesDefaulted = new List<bool>();
        void OnValidate()
        {
            while (SoundIndexesDefaulted.Count > SoundList.Count)
                SoundIndexesDefaulted.RemoveAt(SoundIndexesDefaulted.Count - 1);

            while (SoundIndexesDefaulted.Count < SoundList.Count)
                SoundIndexesDefaulted.Add(false);

            for (int i = 0; i < SoundList.Count; i++)
            {
                if (SoundIndexesDefaulted[i]) continue;
                SoundList[i].SetDefaults();
                SoundIndexesDefaulted[i] = true;
            }



            while (VoiceIndexesDefaulted.Count > VoiceList.Count)
                VoiceIndexesDefaulted.RemoveAt(VoiceIndexesDefaulted.Count - 1);

            while (VoiceIndexesDefaulted.Count < VoiceList.Count)
                VoiceIndexesDefaulted.Add(false);

            for (int i = 0; i < VoiceList.Count; i++)
            {
                if (VoiceIndexesDefaulted[i]) continue;
                VoiceList[i].SetDefaults();
                VoiceIndexesDefaulted[i] = true;
            }
        }



        [RegisterCommand(Name = "Sounds.Play", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandSoundPlay(CommandArg[] args) => Play(args[0].String);

        [RegisterCommand(Name = "Sounds.PlayVoice", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandVoicePlay(CommandArg[] args) => PlayVoice(args[0].String);

        [RegisterCommand(Name = "Sounds.List", MaxArgCount = 0)]
        private static void CommandSoundList(CommandArg[] args)
        {
            Terminal.Log("Initialized sounds effects:");
            foreach (var sound in NameToSound)
                Terminal.Log($"- {sound.Key}");
            Terminal.Log("");

            Terminal.Log("Initialized voice lines:");
            foreach (var line in NameToVoice)
                Terminal.Log($"- {line.Key} ({line.Value.RandomClips.Length} clips, {line.Value.ChanceOfPlaying} chance of playing");
            Terminal.Log("");
        }
    }
}