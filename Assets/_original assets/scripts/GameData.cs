using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUCC;
using CommandTerminalPlus;

namespace PizzaFam
{
    public static class GameData
    {
        private static DataFile file = new DataFile("GameData", autoSave: true);

        public static void Set<T>(string key, T value) => file.Set(key, value);

        public static T Get<T>(string key, T defaultValue) => file.Get(key, defaultValue);


        [RegisterCommand(Name = "GameData.Print", Help = "prints the contents of GameData.SUCC", MaxArgCount = 0)]
        private static void PrintGameData(CommandArg[] args)
        {
            Terminal.Log("Contents of GameData.SUCC:");
            Terminal.Log("");

            foreach (var line in file.GetRawLines())
                Terminal.Log(line);

            Terminal.Log("");
        }

        [RegisterCommand(Name = "GameData.location", MaxArgCount = 0)]
        private static void PrintGameDataLocation(CommandArg[] args)
            => Terminal.Log($"GameData.SUCC is located at {file.FilePath}");

        [RegisterCommand(Name = "GameData.reload", MaxArgCount = 0)]
        private static void ReloadGameData(CommandArg[] args)
            => file.ReloadAllData();
    }
}