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

        private void Play(Sound sound)
        {
            SoundsSource.pitch = Random.Range(1 - sound.MaxPitchVariation, 1 + sound.MaxPitchVariation);
            SoundsSource.PlayOneShot(sound.RandomClips.RandomItem(), sound.VolumeScale);
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
        }

        private static Dictionary<string, Sound> NameToSound = new Dictionary<string, Sound>();


        public AudioSource SoundsSource;

        [SerializeField]
        private Sound[] SoundList;

        [Serializable]
        private class Sound
        {
            public string name = "HEY YOU ADD A SOUND NAME HERE";
            public AudioClip[] RandomClips = new AudioClip[1];

            public float MaxPitchVariation = 0;
            public float VolumeScale = 1;
        }



        [RegisterCommand(Name = "Sounds.Play", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandSoundPlay(CommandArg[] args) => Play(args[0].String);

        [RegisterCommand(Name = "Sounds.List", MaxArgCount = 0)]
        private static void CommandSoundList(CommandArg[] args)
        {
            Terminal.Log("Initialized sounds:");
            foreach (var sound in NameToSound)
                Terminal.Log($"- {sound.Key} ({sound.Value.RandomClips.Length} clips)");
            Terminal.Log("");
        }
    }
}