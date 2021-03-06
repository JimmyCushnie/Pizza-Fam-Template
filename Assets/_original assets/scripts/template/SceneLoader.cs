﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CommandTerminalPlus;

namespace PizzaFam
{
    public class SceneLoader : MonoBehaviour
    {
        public static int CurrentLevel { get; private set; }
        public static int LevelCount { get
            {
                int i = 1;
                while (Application.CanStreamedLevelBeLoaded("level" + i))
                    i++;
                return i - 1;
            } }

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);

            if (SceneManager.sceneCount == 1) // so you can have this scene open while working on a level or whatnot
                LoadMainMenu();
            else 
                for(int i = 1; i < LevelCount; i++)
                    if (SceneManager.GetSceneByName("level" + i).isLoaded)
                        CurrentLevel = i;
        }

        public static int HighestReachedLevel
        {
            get => GameData.Get("HighestReachedLevel", 0);
            private set => GameData.Set("HighestReachedLevel", value);
        }

        public static void LoadMainMenu()
        {
            SceneManager.LoadScene("main menu");
            Subtitles.Clear();
        }

        public static void LoadCutscene(bool start, float fadeDuration = 1)
        {
            MusicPlayer.FadeOut(fadeDuration);
            Fader.FadeOut(fadeDuration, Color.black, onComplete: () =>
            {
                Subtitles.Clear();
                MusicPlayer.Stop();
                CutscenePlayer.StartingCutscene = start;
                SceneManager.LoadScene("cutscene");
            });
        }

        public static void LoadLevel(int level)
        {
            if (level > HighestReachedLevel) HighestReachedLevel = level;

            Subtitles.Clear();
            SceneManager.LoadScene("InEveryLevel");
            SceneManager.LoadScene("level" + level, LoadSceneMode.Additive);
            SceneManager.LoadScene("GameplayUI", LoadSceneMode.Additive);
            CurrentLevel = level;
        }

        public static void LoadEndGame()
        {
            Subtitles.Clear();
            SceneManager.LoadScene("endgame");
        }



        [RegisterCommand(Name = "Load.MainMenu", MaxArgCount = 0)]
        private static void CommandLoadMM(CommandArg[] args) => LoadMainMenu();

        [RegisterCommand(Name = "Load.Level", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandLoadLevel(CommandArg[] args) => LoadLevel(args[0].Int);

        [RegisterCommand(Name = "Load.Cutscene", MinArgCount = 1, MaxArgCount = 1)]
        private static void CommandLoadCutscene(CommandArg[] args) => LoadCutscene(args[0].Bool);

        [RegisterCommand(Name = "Load.endgame", MaxArgCount = 0)]
        private static void CommandLoadEnd(CommandArg[] args) => LoadEndGame();
    }
}