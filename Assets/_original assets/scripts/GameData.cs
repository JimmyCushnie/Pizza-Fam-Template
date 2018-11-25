using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUCC;
using CommandTerminalPlus;

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

        foreach(var line in file.GetRawLines())
            Terminal.Log(line);

        Terminal.Log("");
    }
}
